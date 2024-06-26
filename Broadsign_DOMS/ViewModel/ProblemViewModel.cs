﻿using Broadsign_DOMS.Model;

using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    //TODO: how to establish a connection with HOSTNAME TYPED OR Selected HOSTNAME
    public class ProblemViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private ICommand _remoteOptionsCommand;
        private ICommand _connectSshCommand;
        private ICommand _disconnectSshCommand;
        private ICommand _downloadFilesCommand;
        private ICommand _backCommand;
        private IEnumerable<string> _domainList;
        private Visibility _logFileGridVisibility;

        private SshOptions _sshSession;
        private PlayerModel _selectedPlayer;
        private SshOptions _selectedSession;
        private ObservableCollection<PlayerModel> _playerList;
        private ObservableCollection<SshOptions> _activeSessions;
        private ObservableCollection<FileModel> _files;
        private ObservableCollection<FileModel> _selectedFiles = new ObservableCollection<FileModel>();


        private string  _hostName;
        private string  _status;
        private string  _result;
        private string  _selectedDomain;
        private string _selectedFile;
        private string _customCommand;
        private RelayCommand _clearFieldsCommand;
        private RelayCommand _removeFilesCommand;
        #endregion
        #region Properties
        public ICommand RemoteOptionsCommand
        {
            get
            {
                return _remoteOptionsCommand ?? (new RelayCommand(
                    _executeRemoteCommand,
                    param => SelectedSession != null
                    )) ;
            }
        }
        public ICommand ConnectSshCommand
        {
            get
            {
                return _connectSshCommand ?? (new RelayCommand(param => _sshConnection()));
            }
        }
        public ICommand DisconnectSshCommand
        {
            get
            {
                return _disconnectSshCommand ?? (new RelayCommand(_sshDisconnection));
            }
        }
        public ICommand DownloadFilesCommand
        {
            get
            {
                return _downloadFilesCommand ?? new RelayCommand(param => SelectedSession.DownloadFiles(SelectedFiles), param => SelectedFiles.Count > 0) ;
                
            }
         
        }
        public ICommand RemoveFilesCommand
        {
            get
            {
                return _removeFilesCommand ?? new RelayCommand(param => SelectedSession.DeleteFiles(SelectedFiles), param => SelectedFiles.Count > 0) ;
                
            }
         
        }
        public ICommand ClearFieldsCommand
        {
            get
            {
                return _clearFieldsCommand ?? (new RelayCommand(_clearAllFields));
            }
        }
        public IEnumerable<string> DomainList
        {
            get
            {
                if (_domainList == null)
                    _domainList = CommonResources.Players.Select(x => x.AssignedDomain.Name).Distinct();
                return _domainList;
            }
            set
            {
                _domainList = value;
                OnPropertyChanged("DomainList");
            }
        }
        public ICommand BackCommand 
        {
            get
            {
                if (_backCommand == null) 
                {
                    _backCommand = new RelayCommand(x => Mediator.Notify("HomeViewModel", ""));
                }

                return _backCommand;
            } 
            
        }

        public Visibility LogFileGridVisibility
        {
            get => _logFileGridVisibility;
            set
            {
                _logFileGridVisibility = value;
                OnPropertyChanged("LogFileGridVisibility");
            }
        }

        public PlayerModel SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                //TODO: set a maximum of 5 selected players and connecitons
                _selectedPlayer = value;
                OnPropertyChanged("SelectedPlayer");
            }
        }
        public SshOptions SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged("SelectedSession");
            }
        }
        public ObservableCollection<PlayerModel> PlayerList
        {
            get
            {

                return _playerList ?? new ObservableCollection<PlayerModel>();
            }
            set
            {
                _playerList = value;
                OnPropertyChanged("PlayerList");
            }
        }
        public ObservableCollection<SshOptions> ActiveSessions
        {
            get
            {
                if (_activeSessions == null)
                {
                    _activeSessions = new ObservableCollection<SshOptions>();
                    
                }
                return _activeSessions;
            }
            set
            {
                _activeSessions = value;
                OnPropertyChanged("ActiveSessions");
            }
        }
        public ObservableCollection<FileModel> Files 
        {
            get
            {
               return _files ?? new ObservableCollection<FileModel>();
            }
            set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }
        
        public string HostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                _hostName = value;
                OnPropertyChanged("HostName");
                if(PlayerList != null)
                    PlayerList = new ObservableCollection<PlayerModel>(CommonResources.Players.Where(x => x.Name.Contains(_hostName) && x.AssignedDomain.Name == SelectedDomain));
            }

        }
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }
        public string SelectedDomain
        {
            get => _selectedDomain;
            set
            {
                _selectedDomain = value;
                OnPropertyChanged("SelectedDomain");
                PlayerList = new ObservableCollection<PlayerModel>(CommonResources.Players.Where(x => x.AssignedDomain.Name == _selectedDomain));
            }
        }


        public string CustomCommand { get => _customCommand; set => _customCommand = value; }
        public ObservableCollection<FileModel> SelectedFiles 
        {
            get
            {
                return _selectedFiles ?? new ObservableCollection<FileModel>();
            }
            set
            {
                _selectedFiles = value;
                OnPropertyChanged("SelectedFiles");
            }
        }

        #endregion
        #region Contructors
        #endregion
        #region Methods
        private void _executeRemoteCommand(object param)
        {
            if((string)param == "files")
            {
                Files = new ObservableCollection<FileModel>();
                foreach(var file in _sshSession.GetLogList())
                {
                    Files.Add(new FileModel(OnFileSelected) { FileName = file.Substring(file.LastIndexOf('/') +  1), FilePath = file.Substring(0, file.LastIndexOf('/') + 1) });
                };
                return;
            }
            else if((string)param == "documents")
            {
                Files = new ObservableCollection<FileModel>();
                foreach (var file in _sshSession.GetAdCopies())
                {
                    Files.Add(new FileModel(OnFileSelected) { FileName = file });
                };
                return;
            }
            string cmd = "";
            if ((string)param == "reboot")
                cmd = "sudo reboot";
            else if ((string)param == "xrandr")
                cmd = "export DISPLAY=:0 && xrandr";
            else if ((string)param == "consul")
                cmd = "sudo /opt/configuration/converge.sh checknow";
            else if ((string)param == "poll")
                cmd = "/opt/scripts/pollnow";
            else if ((string)param == "custom")
                cmd = CustomCommand;
            Result = _sshSession.ExecuteCommand(cmd);
        }

        private void OnFileSelected(FileModel obj)
        {

            SelectedFiles.Clear();
            foreach (var file in Files.Where(x => x.IsChecked))
                SelectedFiles.Add(file);
        }
        private void _sshDisconnection(object obj)
        {
            if (SelectedSession != null)
            {
                SelectedSession.DisconnectSshSession();
                ActiveSessions.Remove(ActiveSessions.Where(x => x == SelectedSession).First());
            }
        }
        private void _sshConnection()
        {
            //assign hostname to local var name
            //a check is required in order to determine if the given or selected name meets the standard naming xx-xx-xxx-xxx
            try
            {
                string name = HostName;
                if (SelectedPlayer != null)
                    name = SelectedPlayer.Name.Substring(0, 14);
                //show ssh connections into the ldatagrid
                if (_checkHostNameIsValid(name))
                {

                    _sshSession = new SshOptions
                    {
                        HostName = name
                    };

                    _sshSession.StartSshSession();
                    if (_sshSession.IsConnected)
                        ActiveSessions.Add(_sshSession);
                    else
                        MessageBox.Show($"Could not connect to {HostName}, try again or try to ping");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.Message.ToString());
            }

        }
        private bool _checkHostNameIsValid(string selected)
        {
            try
            {
                if (HostName == null && selected == null)
                    throw new ArgumentNullException();
                Regex.IsMatch(selected.Trim(), "^[A-Za-z]{2}-[A-Za-z]{2}-[A-Za-z]{3}-[A-Za-z0-9]{4}$");
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show(e.Message + "Please enter a name or select a player");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n EXAMPLE: UK-RO-LON-P001");
                return false;
            }

 
            return true;
        }
        private void _clearAllFields(object obj)
        {
            SelectedSession = null;
            SelectedPlayer = null;
            HostName = string.Empty;
            SelectedDomain = null;
            Result = "";
            Files.Clear();
            
        }
        #endregion
    }
}
