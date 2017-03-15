using Efficient_developer_helper.ViewModels.Login;
using Efficient_developer_helper.Views.Login;
using TeamCitySharp;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels.Settings
{
    public class TeamCitySettingsViewModel : NotifyPropertyChangedViewModel
    {
        private string _folderToStoreBuildPath;
        private bool _autentificated;

        public TeamCitySettingsViewModel()
        {
            SelectFolderToStoreBuildsCommand = new RelayCommand(SelectFolderToStoreBuilds);
            LoginCommand = new RelayCommand(Login);
        }

        #region Properties
        public string FolderToStoreBuildPath
        {
            get { return _folderToStoreBuildPath; }
            private set
            {
                _folderToStoreBuildPath = value;
                NotifyPropertyChanged();
            }
        }

        public bool Autentificated
        {
            get { return _autentificated; }
            set
            {
                _autentificated = value;
                NotifyPropertyChanged();
            }
        }

        public TeamCityClient Client { get; private set; }
        #endregion

        #region Commands
        public RelayCommand LoginCommand { get; set; }

        public RelayCommand SelectFolderToStoreBuildsCommand { get; set; }
        #endregion

        #region Commands implementation
        private void Login()
        {
            var window = new LoginWindow { Topmost = true };
            var dataContext = new TeamCityLoginViewModel(x =>
            {
                Autentificated = x != null;
                Client = x;
                window.Hide();
            });
            window.DataContext = dataContext;
            window.Show();
        }

        private void SelectFolderToStoreBuilds()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            FolderToStoreBuildPath = dialog.SelectedPath;
        }
        #endregion

        #region Methods
        #endregion
    }
}
