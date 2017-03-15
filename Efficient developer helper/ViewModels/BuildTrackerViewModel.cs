using System.Collections.Generic;
using Efficient_developer_helper.Model;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class BuildTrackerViewModel : NotifyPropertyChangedViewModel
    {
        public IList<BuildInfo> Builds { get; set; }
        private readonly TeamCityClient _client;

        public BuildTrackerViewModel(TeamCityClient client, string userName)
        {
            _client = client;
            Builds = GetPersonalBuilds(userName);
        }

        private IList<BuildInfo> GetPersonalBuilds(string userName)
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
