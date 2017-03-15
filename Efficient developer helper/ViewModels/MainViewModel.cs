using Efficient_developer_helper.ViewModels.Creators;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedViewModel
    {
        private bool _autoHide = true;
        private Content _activeContent;
        private bool _showSettings;
        private readonly Content _homePageContent;
        private bool _isHomeActive;

        public MainViewModel()
        {
            _homePageContent = HomePageCreator.GetNewInstanse(x => ActiveContent = x);
            ShowSettings = false;
            ActiveContent = _homePageContent;
            HomeCommand = new RelayCommand(GoHome);
        }

        public RelayCommand HomeCommand { get; set; }

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
                NotifyPropertyChanged();
            }
        }

        public Content ActiveContent
        {
            get { return _activeContent; }
            private set
            {
                _activeContent = value;
                IsHomeActive = _activeContent == _homePageContent;
                NotifyPropertyChanged();
            }
        }

        public bool IsHomeActive
        {
            get { return _isHomeActive; }
            private set
            {
                _isHomeActive = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public void GoHome()
        {
            ActiveContent = _homePageContent;
        }
    }
}