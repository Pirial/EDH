
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.Model
{
    public class BuildInfo:NotifyPropertyChangedViewModel
    {
        public BuildInfo(int progress, string branchName, string estimatedTime, bool isPostponed, string changeList)
        {
            Progress = progress;
            BranchName = branchName;
            EstimatedTime = estimatedTime;
            IsPostponed = isPostponed;
            ChangeList = changeList;
        }

        public int Progress { get; set; }

        public string BranchName { get; set; }

        public string EstimatedTime { get; set; }

        public bool IsPostponed { get; set; }

        public string ChangeList { get; set; }

        public bool StoreLocally { get; set; }
    }
}
