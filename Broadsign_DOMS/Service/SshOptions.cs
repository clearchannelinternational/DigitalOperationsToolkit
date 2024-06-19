using Broadsign_DOMS.Model;
using Microsoft.Win32;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;



namespace Broadsign_DOMS.Service
{
    public class SshOptions : ObservableObject
    {
        string _jumpHost = "wireguard.ccuk.io";
        string _jumpUsername = "ubuntu";
        string _username = "ccplayer";
        string _password = "test1234";
        bool _isConnected;
 

        PrivateKeyFile privateKeyFile;
        ConnectionInfo _connectionInfo;
        ForwardedPortLocal _forwardedPortLocal;
        ForwardedPortLocal _forwardedPortLocalScp;
        ForwardedPortLocal _forwardedPortLocalVnc;
        ScpClient scpClient;
        SshClient sshClient;
        SshClient sshJump;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }
        public int VncPort { get; set; }
        public string HostName { get; set; }

        int[] port = new int[] { 5999, 6000, 6001, 6002, 6003, 6004, 6005, 6006, 6007, 6008, 6009, 6010,6011,6012, 60200, 60201, 60202, 60203,60204,60205,60206,60207,60208,60209,60210, 60211, 60212, 60213, 60214, 60215, 60216, 60217, };
        int index = 0;

        
        //TODO find a way to detect when a ssh connection has been disconnected and return value to the value

        public SshOptions()
        {
            //TODO: Make it selectable so that the user can establish a valid connectoin

            string sshKey = "c:\\ssh\\id_rsa";

            _connectSshJump(sshKey);

        }
        private void _connectSshJump(string sshKey = "")
        {
            if(sshKey == "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                sshKey = openFileDialog.FileName;
            }
            try
            {
                privateKeyFile = new PrivateKeyFile(sshKey);
                _connectionInfo = new ConnectionInfo(_jumpHost, 22, _jumpUsername, new PrivateKeyAuthenticationMethod(_jumpUsername, privateKeyFile));
                sshJump = new SshClient(_connectionInfo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
    
            }
        }
        public void StartSshSession()
        {
  
            if(sshJump == null)
            {
                MessageBox.Show("ssh key was not found, select an valid ssh key to continue");
                _connectSshJump();
                StartSshSession();

            }
            try
            {

                if(!sshJump.IsConnected)
                    sshJump.Connect();

                _forwardedPortLocal     = new ForwardedPortLocal("localhost", HostName, 22);
                
                sshJump.AddForwardedPort(_forwardedPortLocal);
                
                _forwardedPortLocal.Start();

                _connectionInfo = new ConnectionInfo(_forwardedPortLocal.BoundHost,(int)_forwardedPortLocal.BoundPort,_username, new PasswordAuthenticationMethod(_username, _password));
                try
                {
                    sshClient = new SshClient(_connectionInfo);
                    sshClient.Connect();               
                    IsConnected = true;
                    
                    _startVncSession();
                }
                catch (Exception e)
                {
                   MessageBox.Show(e.Message);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }


        }
        public void DisconnectSshSession()
        {
            sshClient.Disconnect();
            _forwardedPortLocal.Stop();
            _forwardedPortLocalVnc.Stop();

            sshJump.Disconnect();
        }
        public string ExecuteCommand(string cmd)
        {
            var result = "";
            if (!sshClient.IsConnected)
                StartSshSession();
            try
            {
                var command = sshClient.CreateCommand($"{cmd}");
    
                result = command.Execute();
                var eStatus = command.ExitStatus;
                if (eStatus != 0)
                    result = $"error while executing request {eStatus} \n {command.Error}";
                ////execute commands asynchronilsy
                //var asyncResult = command.BeginExecute();
                //result = command.EndExecute(asyncResult);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _forwardedPortLocal.Dispose();
            }

            return result;
        }
        private void _startScpSession()
        {
            // Create an SSH client for the jump host
            var jumpHostConnectionInfo = new ConnectionInfo("wireguard.ccuk.io", 22, "ubuntu", new PrivateKeyAuthenticationMethod("ubuntu", privateKeyFile));
            using (var jumpHostClient = new SshClient(jumpHostConnectionInfo))
            {
                // Connect to the jump host
                jumpHostClient.Connect();

                //Create a forwarded port tunnel on the jump host
                _forwardedPortLocalScp = new ForwardedPortLocal("localhost", HostName, 22);
                sshJump.AddForwardedPort(_forwardedPortLocalScp);
                _forwardedPortLocalScp.Start();

                // Create an SSH client for the remote host
                var remoteHostConnectionInfo = new ConnectionInfo("localhost", (int)_forwardedPortLocalScp.BoundPort, _username, new PasswordAuthenticationMethod(_username, _password));

                // SCP client for transferring files
                scpClient = new ScpClient(remoteHostConnectionInfo);
                try
                {
                    // Connect SCP client
                    scpClient.Connect();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }



        
            
        }
        private void _startVncSession()
        {
            //initialize a connection info with the jump server's information (ubuntu@wireguard.ccuk.io)
            var _connectionInfo = new ConnectionInfo(_jumpHost, 22, _jumpUsername, new PrivateKeyAuthenticationMethod(_jumpUsername, privateKeyFile));

            try
            {
                var sshJump = new SshClient(_connectionInfo);
                sshJump.Connect();
                //return jump connection true


                _forwardedPortLocalVnc = new ForwardedPortLocal("localhost", (uint)port[index], HostName, 5900);
                sshJump.AddForwardedPort(_forwardedPortLocalVnc);
                _forwardedPortLocalVnc.Start();
                VncPort = (int)_forwardedPortLocalVnc.BoundPort;
  
            }
            catch (Exception e)
            {
                //problem with jump connection
                Debug.WriteLine(e.Message);
                index++;
                _startVncSession();
                
            }
        }
        public List<string> GetLogList()
        {

            List<string> collection = new List<string>
            {
                "/opt/broadsign/suite/bsp/share/bsp/bsp.log",
                "/opt/broadsign/suite/bsp/share/bsp/bsp.db"
            };
            string[] cmd = ExecuteCommand("ls /opt/broadsign/suite/bsp/share/bsp/logs/").Split('\n');
            foreach(string line in cmd)
            {
                if (line != "")
                    collection.Add($"/opt/broadsign/suite/bsp/share/bsp/logs/{line}");

            }
            return collection;
        }
        public ObservableCollection<string> GetAdCopies()
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();
            
            string[] cmd = ExecuteCommand("sudo ls /opt/broadsign/suite/bsp/share/documents/").Split('\n');

            foreach (string line in cmd)
            {
                if (line != "")
                    collection.Add($"/opt/broadsign/suite/bsp/share/documents/{line}");
            }
            return collection;
        }
        public void DownloadFiles(ObservableCollection<FileModel> files)
        {
            //TODO: Add a boolean to see if the files a downloadable or not only log files should be !
            _startScpSession();
            var localFilePath = "";
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    localFilePath = dialog.SelectedPath;
                }
            }
            foreach (FileModel file in files)
            {
   

                try
                {
                    // Download a remote file
                    string remoteFilePath = file.FilePath + file.FileName;


                    string fileName = $"{localFilePath}\\{file.FileName}";

                    var fileStream = new FileStream(fileName, FileMode.Create);
                    scpClient.Download(remoteFilePath, fileStream);


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

        }
        public void DeleteFiles(ObservableCollection<FileModel> files)
        {
            var msg = MessageBox.Show("you're about to remove files from a remote computer are you sure you want to continue ?", "WARNING", MessageBoxButton.YesNo);
            if (msg == MessageBoxResult.No)
            {
                MessageBox.Show("Cancel removal of files on remote computer !");
                return;
            }
            //getting all files to delete
            foreach (var file in files)
            {
                try
                {
                    
                    ExecuteCommand($"sudo rm -r {file.FilePath + file.FileName}");

                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error while deleting file {file.FileName}");
                }
            }

        }

    }
}
