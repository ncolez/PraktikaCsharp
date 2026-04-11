using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using z1.Services;

namespace z1
{
    public partial class MainWindow : Window
    {
        private readonly NotificationService _notificationService = new NotificationService();
        private Storyboard? _loadingStoryboard;
        private Storyboard? _fadeOutStoryboard;
        private Storyboard? _fadeInStoryboard;
        private StudentViewModel? _viewModel;

        public MainWindow(UserModel currentUser)
        {
            InitializeComponent();
            _viewModel = new StudentViewModel(currentUser);
            DataContext = _viewModel;

            _loadingStoryboard = Resources["LoadingStoryboard"] as Storyboard;
            _fadeOutStoryboard = Resources["FadeOutStoryboard"] as Storyboard;
            _fadeInStoryboard = Resources["FadeInStoryboard"] as Storyboard;

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
                _viewModel.GradeAdded += ViewModel_GradeAdded;
            }

            Loaded += async (s, e) =>
            {
                StartLoadingAnimation();
                if (_viewModel != null)
                {
                    await _viewModel.LoadDataAsync();
                }
                StopLoadingAnimation();
                await CheckNotifications();
            };
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StudentViewModel.SelectedStudent))
            {
                AnimateSubjectChange();
            }
        }

        private void ViewModel_GradeAdded(GradeModel? newGrade)
        {
            if (newGrade == null) return;

            Dispatcher.Invoke(() =>
            {
                var storyboard = new Storyboard();
                var animation = new ColorAnimation
                {
                    From = Colors.Green,
                    To = Colors.Red,
                    Duration = TimeSpan.FromSeconds(0.3),
                    AutoReverse = true,
                    RepeatBehavior = new RepeatBehavior(2)
                };

                storyboard.Children.Add(animation);

                var container = GradesListView.ItemContainerGenerator.ContainerFromItem(newGrade);
                if (container != null)
                {
                    var gradeText = FindVisualChild<TextBlock>(container, "GradeTextBlock");
                    if (gradeText != null)
                    {
                        Storyboard.SetTarget(animation, gradeText);
                        Storyboard.SetTargetProperty(animation, new PropertyPath(TextBlock.ForegroundProperty));
                        storyboard.Begin();
                    }
                }
            });
        }

        private void AnimateSubjectChange()
        {
            if (GradesListView == null) return;

            var fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            fadeOut.Completed += (s, a) =>
            {
                var fadeIn = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.2)
                };
                GradesListView.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            };

            GradesListView.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void StartLoadingAnimation()
        {
            if (LoadingIcon != null)
            {
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(1),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                LoadingIcon.RenderTransform = new RotateTransform();
                LoadingIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                LoadingIcon.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
        }

        private void StopLoadingAnimation()
        {
            if (LoadingIcon != null)
            {
                LoadingIcon.RenderTransform?.BeginAnimation(RotateTransform.AngleProperty, null);
            }
        }

        private async System.Threading.Tasks.Task CheckNotifications()
        {
            var notification = await _notificationService.ReadNotificationAsync();
            if (!string.IsNullOrEmpty(notification))
            {
                _notificationService.ShowNotification(notification);
            }
        }

        private void GradesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            var selectedGrade = listView?.SelectedItem as GradeModel;

            if (selectedGrade != null)
            {
                var detailsWindow = new GradeDetailsWindow(selectedGrade.Subject, selectedGrade.GradeValue, selectedGrade.Date);
                detailsWindow.ShowDialog();
            }
        }

        private T? FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T element && element.Name == name)
                {
                    return element;
                }
                var result = FindVisualChild<T>(child, name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}