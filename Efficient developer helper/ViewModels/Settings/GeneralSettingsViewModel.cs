using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels.Settings
{
    public class GeneralSettingsViewModel : NotifyPropertyChangedViewModel
    {
        public GeneralSettingsViewModel()
        {
            EnableNotifications = true;
        }

        public bool EnableNotifications { get; set; }
    }
}