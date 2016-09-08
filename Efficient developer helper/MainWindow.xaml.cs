using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Efficient_developer_helper.ViewModels;
using Efficient_developer_helper.Views;
using Application = System.Windows.Application;

namespace Efficient_developer_helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon _notifier;
        private GeneralViewModel _generalViewModel;
        private GeneralWindow _generalWindow;
        private CancellationTokenSource _autoHideCancellationTokenSource;
        private Task _autoHideTask;

        public MainWindow()
        {
            InitializeComponent();
            var dataContext = new LoginViewModel();
            DataContext = dataContext;
            CreateNotifier();
            dataContext.AutentificatedEvent += HandleAutentificatedEvent;
            Application.Current.Exit += HandleApplicationExit;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void CreateNotifier()
        {
            _notifier = new NotifyIcon();

            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem(ResourceStrings.Exit, (sender, args) => { Application.Current.Shutdown(); }));

            _notifier.ContextMenu = contextMenu;
            _notifier.MouseClick += NotifierMouseClickHandler;
        }

        private void NotifierMouseClickHandler(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
                _generalWindow.Show();
        }

        private void HandleApplicationExit(object sender, ExitEventArgs e)
        {
            _notifier.Dispose();
        }

        private void HandleAutentificatedEvent(object sender, AutentificatedEventArgs e)
        {
            Hide();
            _notifier.Visible = true;
            _notifier.Icon = new Icon(@"Images\TrayIcon.ico");

            _generalWindow = new GeneralWindow { Topmost = true };
            _generalWindow.MouseLeave += GeneralWindowMouseLeaveHandler;
            _generalWindow.MouseEnter += GeneralWindowMouseEnterHandler;
            _generalViewModel = new GeneralViewModel(e.TeamCityClient, e.UserName);
            _generalWindow.DataContext = _generalViewModel;

        }

        private void GeneralWindowMouseEnterHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_autoHideCancellationTokenSource != null)
                _autoHideCancellationTokenSource.Cancel();
        }

        private void GeneralWindowMouseLeaveHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var window = (Window)sender;
            if (!_generalViewModel.AutoHide) return;

            _autoHideCancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => AutoHide(window, _autoHideCancellationTokenSource));
        }

        private void AutoHide(Window window, CancellationTokenSource cancellationTokenSource)
        {
            Thread.Sleep(1000);
            if (cancellationTokenSource.IsCancellationRequested) return;

            Dispatcher.Invoke(window.Hide);
        }
    }
}
