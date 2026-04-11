using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace z1
{
    public partial class GradeDetailsWindow : Window
    {
        public GradeDetailsWindow(string subject, int grade, DateTime date)
        {
            InitializeComponent();

            SubjectText.Text = subject;
            GradeText.Text = grade.ToString();
            DateText.Text = date.ToString("dd.MM.yyyy");

            if (grade >= 8)
            {
                GradeText.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            }
            else if (grade >= 5)
            {
                GradeText.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Orange);
            }
            else
            {
                GradeText.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            MainBorder.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            var fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            fadeOut.Completed += (s, a) => Close();
            MainBorder.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }
    }
}