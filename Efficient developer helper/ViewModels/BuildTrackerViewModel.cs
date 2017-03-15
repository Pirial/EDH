using Efficient_developer_helper.Model;
using System.Collections.Generic;
using System.Linq;
using Efficient_developer_helper.ViewModels.Settings;
using TeamCitySharp;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class BuildTrackerViewModel : NotifyPropertyChangedViewModel
    {
        public IList<BuildInfo> Builds { get; set; }
        private readonly TeamCityClient _client;

        public BuildTrackerViewModel(TeamCitySettingsViewModel settings)
        {
            _client = settings.Client;
            Builds = GetPersonalBuilds(""); //settings.Client.Users.All().First().Username);
        }

        private static IList<BuildInfo> GetPersonalBuilds(string userName)
        {
            return new List<BuildInfo> 
            { 
                new BuildInfo(45, "adasd", "asdas", false, "asdasdas"), 
                new BuildInfo(45, "adasd", "asdas", false, "asdasdas"), 
                new BuildInfo(45, "adasd", "asdas", false, "asdasdas"), 
                new BuildInfo(45, "adasd", "asdas", false, "asdasdas") 
            };
        }
    }
}
