
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels.Login
{
    internal interface ILoginViewModel
    {
        string UserName { get; }

        string Password { get; }

        string ServerUrl { get; }

        string ErrorMessage { get; }

        string SourceName { get; }

        RelayCommand LoginCommand { get; }

        RelayCommand CancelCommand { get; }
    }
}
