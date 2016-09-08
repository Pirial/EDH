using System.Windows;

namespace Efficient_developer_helper
{
    /// <summary>
    /// Interaction logic for ContainerWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        private Rect _desktopWorkingArea;

        public GeneralWindow()
        {
            InitializeComponent();
            _desktopWorkingArea = SystemParameters.WorkArea;
            MaxHeight = _desktopWorkingArea.Bottom;
            MinHeight = _desktopWorkingArea.Bottom / 3;
        }

        private void GeneralWindowOnSizeChangedHandler(object sender, SizeChangedEventArgs e)
        {

            Left = _desktopWorkingArea.Right - ActualWidth;
            Top = _desktopWorkingArea.Bottom - ActualHeight;

        }
    }
}
