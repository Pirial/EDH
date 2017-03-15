using System.Windows.Forms;
using ThreeShape.WPF.Utilities;

namespace Efficient_developer_helper.ViewModels
{
    public class DDTSandBoxViewModel
    {
        public DDTSandBoxViewModel()
        {
            SelectBuildCommand = new RelayCommand(SelectBuild);
        }

        public RelayCommand SelectBuildCommand { get; set; }

        private void SelectBuild()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            var aaa = dialog.FileName;
        }
    }
}
