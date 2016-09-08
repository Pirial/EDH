// <copyright>3Shape A/S</copyright>
using System.Windows;
using ThreeShape.WPF.Utilities;

namespace ThreeShape.Dental.DentalDesktop.CoreModuleUtilities.UserControls
{
    /// <summary>
    /// Interaction logic for LineProgressBar.xaml
    /// </summary>
    public partial class LineProgressBar
    {
        /// <summary>The can cancel property</summary>
        public static readonly DependencyProperty CanCancelProperty = DependencyProperty.Register("CanCancel", typeof(bool),
            typeof(LineProgressBar), new PropertyMetadata(default(bool)));

        /// <summary>The title property</summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(LineProgressBar), new PropertyMetadata(default(string)));

        /// <summary>The cancel command property</summary>
        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand",
            typeof(RelayCommand), typeof(LineProgressBar), new PropertyMetadata(default(RelayCommand)));

        /// <summary>The progress property</summary>
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof (int),
            typeof(LineProgressBar), new PropertyMetadata(50));

        public static readonly DependencyProperty StoreLocallyProperty = DependencyProperty.Register("StoreLocally", typeof(bool),
            typeof(LineProgressBar), new PropertyMetadata(false));

        /// <summary>Initializes a new instance of the <see cref="LineProgressBar"/> class.</summary>
        public LineProgressBar()
        {
            this.InitializeComponent(ref _contentLoaded, "component/UserControls/LineProgressBar.xaml");
        }

        /// <summary>Gets or sets the progress.</summary>
        /// <value>The progress.</value>
        public int Progress
        {
            get { return (int) GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public bool StoreLocally
        {
            get { return (bool)GetValue(StoreLocallyProperty); }
            set { SetValue(StoreLocallyProperty, value); }
        }

        /// <summary>Gets or sets a value indicating whether can cancel operation.</summary>
        public bool CanCancel
        {
            get { return (bool)GetValue(CanCancelProperty); }
            set { SetValue(CanCancelProperty, value); }
        }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>Gets or sets the cancel command.</summary>
        /// <value>The cancel command.</value>
        public RelayCommand CancelCommand
        {
            get { return (RelayCommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }
    }
}