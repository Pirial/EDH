
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class SettingsViewModel : NotifyPropertyChangedViewModel
    {
        private string _folderToStoreBuildPath;
        private bool _enableNotifications = true;

        public SettingsViewModel()
        {
            SelectFolderToStoreBuildsCommand = new RelayCommand(SelectFolderToStoreBuilds);
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

        public bool EnableNotifications
        {
            get { return _enableNotifications; }
            set { _enableNotifications = value; }
        }

        #endregion

        #region Commands
        public RelayCommand SelectFolderToStoreBuildsCommand { get; set; }
        #endregion

        #region Commands implementation
        private void SelectFolderToStoreBuilds()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            FolderToStoreBuildPath = dialog.SelectedPath;
        }
        #endregion
    }
}
