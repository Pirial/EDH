using System;
using System.Collections.ObjectModel;
using Efficient_developer_helper.ViewModels.Creators;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class HomePageViewModel : NotifyPropertyChangedViewModel
    {
        private Content _selectedItem;
        private readonly Action<Content> _setContentAction;

        public HomePageViewModel(Action<Content> setContentAction)
        {
            _setContentAction = setContentAction;
            Items = new ObservableCollection<Content>
            {
                DDTSandBoxCreator.GetNewInstanse(),
                TeamCityCreator.GetNewInstanse()
            };
            SetContentCommand = new RelayCommand<Content>(SetContent);
        }

        public ObservableCollection<Content> Items { get; private set; }

        public RelayCommand<Content> SetContentCommand { get; private set; }

        private void SetContent(Content content)
        {
            _setContentAction.Invoke(content);
        }
    }
}
