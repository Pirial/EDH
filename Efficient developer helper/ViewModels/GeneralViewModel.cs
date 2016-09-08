using Efficient_developer_helper.Views;
using System.Windows.Controls;
using TeamCitySharp;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class GeneralViewModel: NotifyPropertyChangedViewModel
    {
        private bool _autoHide = true;
        private bool _showSettings;
        private readonly SettingsView _settingsContent;
        private readonly BuildTrackerView _trackerContentControl;
        private ContentControl _activeContent;

        public GeneralViewModel(TeamCityClient client, string userName)
        {
            _settingsContent = new SettingsView()
            {
                DataContext = new SettingsViewModel()
            };

            _trackerContentControl = new BuildTrackerView()
            {
               DataContext = new BuildTrackerViewModel(client, userName)
            };

            ActiveContent = _trackerContentControl;
        }

        #region Properties
        public bool AutoHide
        {
            get { return _autoHide; }
            set { _autoHide = value; }
        }

        public bool ShowSettings
        {
            get { return _showSettings; }
            set
            {
                _showSettings = value;
                if (value)
                    ActiveContent = _settingsContent;
                else
                    ActiveContent = _trackerContentControl;
            }
        }

        public ContentControl ActiveContent
        {
            get { return _activeContent; }
            private set
            {
                _activeContent = value; 
                NotifyPropertyChanged();
            }
        }
        #endregion
    }
}
