using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskTurner.Views
{
    /// <summary>
    /// Логика взаимодействия для NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        public NewTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var clickedElement = Mouse.DirectlyOver as DependencyObject;

            if (!IsDescendantOf(clickedElement, ImportancePanelContainer) &&
                !IsDescendantOf(clickedElement, ImportanceToggleButton))
            {
                ImportancePanelContainer.Visibility = Visibility.Collapsed;
            }
        }

        private bool IsDescendantOf(DependencyObject child, DependencyObject parent)
        {
            while (child != null)
            {
                if (child == parent)
                    return true;
                child = VisualTreeHelper.GetParent(child);
            }
            return false;
        }

        private void ImportanceToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ImportancePanelContainer.Visibility =
                ImportancePanelContainer.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void ImportanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                ImportanceToggleButton.Content = btn.Content;

                ImportancePanelContainer.Visibility = Visibility.Collapsed;
            }
        }
    }
}
