using Broadsign_DOMS.Service;
using Ionic.Zip;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Broadsign_DOMS.Model
{
    public class Domain : ObservableObject
    {
        private string _name;
        private string userName;
        private string token;

        ObservableCollection<Domain> domainList;
        private string _id;

        public ObservableCollection<Domain> DomainList
        {
            get
            {
                _generateAllTokens();
                return domainList;
            }
            set
            {
                domainList = value;
                OnPropertyChanged(nameof(DomainList));
            }
        }

        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(Name);
            }
        }
        public string UserName 
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged(UserName);
            }
        }
        public string Token 
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
                OnPropertyChanged(Token);
            }
        }

        public string Id { get => _id; set => _id = value; }

        private void _generateAllTokens()
        {
            domainList = new ObservableCollection<Domain>();

            using (ZipFile zip = ZipFile.Read(@"api.zip"))
            {
                try
                {
                    zip.Password = "CCI2023";
                    ZipEntry e = zip["api.csv"];
                    using (StreamReader streamReader = new StreamReader(e.OpenReader()))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string[] l = streamReader.ReadLine().Split(',', ';');
                            domainList.Add(new Domain { Name = l[0], Token = l[1], Id = l[2] });
                        }
                    }
                }catch(Exception e)
                {
                    MessageBox.Show("password for zip file incorrect");
                    return;
                }
                
            }
        }
    }
}
