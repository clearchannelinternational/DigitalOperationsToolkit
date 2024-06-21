
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;
using Auth0.OidcClient;
using Auth0.AuthenticationApi.Models;
using System.Windows;
using System.Diagnostics;

namespace Broadsign_DOMS.ViewModel
{
    public class LoginViewModel : ObservableObject, IPageViewModel
    {
        private bool _loginClicked = false;
        private ICommand loginButtonCommand;

        //Add login form
        public ICommand LoginButtonCommand 
        {
            get
            {
                return loginButtonCommand ?? (new RelayCommand(x =>
                {
                    if (!_loginClicked)
                    {
                        _loginClicked = true;
                        _auth0Login();
                    }
                })); 
            }
        }

        private async void _auth0Login()
        {
           
            var options = new Auth0ClientOptions
            {
                Domain = "dev-xa8nz4kn1wlb81ce.us.auth0.com",
                ClientId = "ufrEcNnUZKvO3KtKViSoVWfZHWP8O8WG",
                LoadProfile= true
            };
            Auth0Client client = new Auth0Client(options);
            options.PostLogoutRedirectUri = options.RedirectUri;
            

            var loginResult = await client.LoginAsync();

            if (loginResult.IsError)
            {
                _loginClicked = false;
                MessageBox.Show("there was an error try again");
                return;
            }
            _loginClicked = false;
            Messenger.Default.Send(true, "StartViewModel");
            foreach(var claim in loginResult.User.Claims)
                Debug.WriteLine(claim.ToString());

            //client.LogoutAsync();
          
        }
    }
}
