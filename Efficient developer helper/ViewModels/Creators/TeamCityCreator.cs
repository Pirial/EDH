using Efficient_developer_helper.ViewModels.Settings;
using Efficient_developer_helper.Views;
using System.Reflection;
using ThreeShape.WPF.Utilities;
using BuildTrackerView = Efficient_developer_helper.Views.Contents.BuildTrackerView;

namespace Efficient_developer_helper.ViewModels.Creators
{
    public class TeamCityCreator
    {
        public static Content GetNewInstanse()
        {
            var teamCitySettingsViewModel = new TeamCitySettingsViewModel();

            var content = new BuildTrackerView
            {
                DataContext = new BuildTrackerViewModel(teamCitySettingsViewModel)
            };
            var settings = new TeamCitySettingsView
            {
                DataContext = teamCitySettingsViewModel
            };

            var icon = ImageUtility.LoadImage(@"TeamCityIcon.png", Assembly.GetAssembly(typeof(TeamCityCreator)));

            return new Content("TeamCity test", content, settings, icon);
        }
    }
}
