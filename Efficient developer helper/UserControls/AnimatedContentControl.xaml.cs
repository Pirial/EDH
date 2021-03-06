﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Efficient_developer_helper.UserControls
{
    /// <summary>
    /// Interaction logic for AnimatedContentControl.xaml
    /// </summary>
    public partial class AnimatedContentControl
    {
        private Shape _mPaintArea;
        private ContentPresenter _mMainContent;

        public AnimatedContentControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This gets called when the template has been applied and we have our visual tree
        /// </summary>
        public override void OnApplyTemplate()
        {
            _mPaintArea = Template.FindName("PART_PaintArea", this) as Shape;
            _mMainContent = Template.FindName("PART_MainContent", this) as ContentPresenter;

            base.OnApplyTemplate();
        }

        /// <summary>
        /// This gets called when the content we're displaying has changed
        /// </summary>
        /// <param name="oldContent">The content that was previously displayed</param>
        /// <param name="newContent">The new content that is displayed</param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (_mPaintArea != null && _mMainContent != null)
            {
                _mPaintArea.Fill = CreateBrushFromVisual(_mMainContent);
                _mPaintArea.Stretch = Stretch.None;
                BeginAnimateContentReplacement();
            }
            base.OnContentChanged(oldContent, newContent);
        }

        /// <summary>
        /// Creates a brush based on the current appearance of a visual element. 
        /// The brush is an ImageBrush and once created, won't update its look
        /// </summary>
        /// <param name="v">The visual element to take a snapshot of</param>
        private Brush CreateBrushFromVisual(Visual v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            var target = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight,
                                                96, 96, PixelFormats.Pbgra32);
            target.Render(v);
            var brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }

        /// <summary>
        /// Starts the animation for the new content
        /// </summary>
        private void BeginAnimateContentReplacement()
        {
            var newContentTransform = new TranslateTransform();
            var oldContentTransform = new TranslateTransform();
            _mPaintArea.RenderTransform = oldContentTransform;
            _mMainContent.RenderTransform = newContentTransform;
            _mPaintArea.Visibility = Visibility.Visible;

            newContentTransform.BeginAnimation(TranslateTransform.XProperty,
                                          CreateAnimation(ActualWidth, 0));
            oldContentTransform.BeginAnimation(TranslateTransform.XProperty,
                                          CreateAnimation(0, -ActualWidth,
                                            (s, e) => _mPaintArea.Visibility = Visibility.Hidden));
        }

        /// <summary>
        /// Creates the animation that moves content in or out of view.
        /// </summary>
        /// <param name="from">The starting value of the animation.</param>
        /// <param name="to">The end value of the animation.</param>
        /// <param name="whenDone">(optional)
        ///   A callback that will be called when the animation has completed.</param>
        private static AnimationTimeline CreateAnimation(double from, double to,
                                  EventHandler whenDone = null)
        {
            IEasingFunction ease = new BackEase { Amplitude = 0.5, EasingMode = EasingMode.EaseOut };
            var duration = new Duration(TimeSpan.FromSeconds(0.5));
            var anim = new DoubleAnimation(from, to, duration) { EasingFunction = ease };
            if (whenDone != null)
                anim.Completed += whenDone;
            anim.Freeze();
            return anim;
        }
    }
}
