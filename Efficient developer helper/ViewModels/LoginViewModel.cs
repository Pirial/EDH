using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Windows;
using TeamCitySharp;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class LoginViewModel : NotifyPropertyChangedViewModel
    {
        private readonly Credentials _credentials;
        private string _errorMessage;
        private const string _credentialsFileName = "Credentials.txt";
        private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + _credentialsFileName;
        private string _password;

        public event EventHandler<AutentificatedEventArgs> AutentificatedEvent;
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            CancelCommand = new RelayCommand(Cancel);

            if (File.Exists(_path))
            {
                var data = File.ReadAllText(_path);
                _credentials = new JsonFx.Json.JsonReader().Read<Credentials>(data);
            }
            else
                _credentials = new Credentials();
        }

        #region Properties
        public string UserName
        {
            get { return _credentials.LastUserName; }
            set
            {
                _credentials.LastUserName = value;
                if (value != string.Empty)
                    RefreshErrorMessage();
                NotifyPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string ServerUrl
        {
            get { return _credentials.LastServerUrl; }
            set
            {
                _credentials.LastServerUrl = value;
                if (value != string.Empty)
                    RefreshErrorMessage();
                NotifyPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands
        public RelayCommand LoginCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }
        #endregion

        #region Commands implementation
        private void Login()
        {
            TeamCityClient client;
            try
            {
                client = new TeamCityClient(ServerUrl);
            }
            catch
            {
                ErrorMessage = ResourceStrings.ServerShouldNotBeEmpty;
                return;
            }

            client.Connect(UserName, Password);

            try
            {
                if (!client.Authenticate()) throw new AuthenticationException();

                if (AutentificatedEvent != null)
                    AutentificatedEvent(this, new AutentificatedEventArgs(client, UserName));
            }
            catch (AuthenticationException)
            {
                ErrorMessage = ResourceStrings.IncorrectLoginOrPassword;
                return;
            }
            catch (WebException)
            {
                ErrorMessage = ResourceStrings.ServerIsNotFound;
                return;
            }

            if (!_credentials.UserNames.Contains(UserName))
                _credentials.UserNames.Add(UserName);
            if (!_credentials.ServerUrls.Contains(ServerUrl))
                _credentials.ServerUrls.Add(ServerUrl);
            StoreCredenials();
        }

        private void Cancel()
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Methods
        private void RefreshErrorMessage()
        {
            if (_errorMessage != string.Empty)
                ErrorMessage = string.Empty;
        }

        private void StoreCredenials()
        {
            var j = new JsonFx.Json.JsonWriter().Write(_credentials);
            File.WriteAllText(_path, j);
        }
        #endregion
    }

    [Serializable]
    public class Credentials : NotifyPropertyChangedViewModel
    {
        public IList<string> UserNames { get; set; }

        public string LastUserName { get; set; }

        public IList<string> ServerUrls { get; set; }

        public string LastServerUrl { get; set; }

        public Credentials()
        {
            UserNames = new List<string>();
            ServerUrls = new List<string>();
        }
    }

    public class AutentificatedEventArgs : EventArgs    
    {
        public AutentificatedEventArgs(TeamCityClient teamCityClient, string userName)
        {
            TeamCityClient = teamCityClient;
            UserName = userName;
        }

        public TeamCityClient TeamCityClient { get; private set; }

        public string UserName { get; private set; }
    }
}
