using System;
using Efficient_developer_helper.ViewModels.Settings;
using Efficient_developer_helper.Views;
using Efficient_developer_helper.Views.Contents;

namespace Efficient_developer_helper.ViewModels.Creators
{
    public class HomePageCreator
    {
        public static Content GetNewInstanse(Action<Content> setContentAction)
        {
            var content = new MainPageView
            {
                DataContext = new HomePageViewModel(setContentAction)
            };
            var settings = new GeneralSettingsView
            {
                DataContext = new GeneralSettingsViewModel()
            };
            return new Content("Home", content, settings);
        }
    }
}
