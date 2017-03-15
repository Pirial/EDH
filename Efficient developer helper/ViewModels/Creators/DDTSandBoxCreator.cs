using Efficient_developer_helper.ViewModels.Settings;
using Efficient_developer_helper.Views.Contents;
using Efficient_developer_helper.Views.Settings;
using System.Reflection;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels.Creators
{
    public class DDTSandBoxCreator
    {
        public static Content GetNewInstanse()
        {
            var content = new DDTSandBoxView
            {
                DataContext = new DDTSandBoxViewModel()
            };
            var settings = new DDTSandBoxSettingsView
            {
                DataContext = new DDTSandBoxSettingsViewModel()
            };

            var icon = ImageUtility.LoadImage(@"DDTSandBoxIcon.png", Assembly.GetAssembly(typeof(TeamCityCreator)));

            return new Content("3DD SandBox", content, settings, icon);
        }
    }
}
