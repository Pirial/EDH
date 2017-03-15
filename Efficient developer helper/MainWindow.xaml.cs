using Efficient_developer_helper.ViewModels;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using ThreeShape.TitlelessWindow;
using Application = System.Windows.Application;

namespace Efficient_developer_helper
{
    /// <summary>Converter for getting height for empty time slot</summary>
    public class ContentVisibilityConvertor : IMultiValueConverter
    {
        /// <summary>Gets height for empty time slot based on specified parameters.</summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;
            if (values.Length < 2) return null;

            var content = (Content)values[0];
            var showSettings = (bool)values[1];

            return showSettings ? content.Settings : content.Main;
        }

        /// <summary>Two way conversion is not supported.</summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Rect _desktopWorkingArea;
        private NotifyIcon _notifier;
        private CancellationTokenSource _autoHideCancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            _desktopWorkingArea = SystemParameters.WorkArea;
            CreateNotifier();
            _notifier.Visible = true;
            _notifier.Icon = new Icon(@"Images\TrayIcon.ico");
            MaxHeight = _desktopWorkingArea.Bottom;
            MinHeight = _desktopWorkingArea.Bottom / 3;
            Application.Current.Exit += HandleApplicationExit;
            MouseLeave += MainWindowMouseLeaveHandler;
            MouseEnter += MainWindowMouseEnterHandler;

            Hide();
        }

        private void GeneralWindowOnSizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
            Left = _desktopWorkingArea.Right - ActualWidth;
            Top = _desktopWorkingArea.Bottom - ActualHeight;
        }

        private void CreateNotifier()
        {
            _notifier = new NotifyIcon();

            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem(ResourceStrings.Exit,
                (sender, args) => { Application.Current.Shutdown(); }));

            _notifier.ContextMenu = contextMenu;
            _notifier.MouseClick += NotifierMouseClickHandler;
        }

        private void NotifierMouseClickHandler(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == 0) return;
            switch (Visibility)
            {
                case Visibility.Hidden:
                    Show();
                    Activate();
                    WindowState = WindowState.Normal;
                    break;
                case Visibility.Visible:
                    Hide();
                    break;
                case Visibility.Collapsed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleApplicationExit(object sender, ExitEventArgs e)
        {
            _notifier.Dispose();
        }

        private void MainWindowMouseEnterHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_autoHideCancellationTokenSource != null)
                _autoHideCancellationTokenSource.Cancel();
        }

        private void MainWindowMouseLeaveHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var window = (Window)sender;
            if (((MainViewModel)DataContext).AutoHide) return;

            _autoHideCancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => AutoHide(window, _autoHideCancellationTokenSource));
        }

        private void AutoHide(Window window, CancellationTokenSource cancellationTokenSource)
        {
            Thread.Sleep(1000);
            if (cancellationTokenSource.IsCancellationRequested) return;

            Dispatcher.Invoke(window.Hide);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }
    }
}