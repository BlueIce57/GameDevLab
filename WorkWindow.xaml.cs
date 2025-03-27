using System.Windows;

namespace GameDevLab
{
    public partial class WorkWindow : Window
    {
        public WorkWindow(string projectName)
        {
            InitializeComponent();
            ProjectTitleTextBlock.Text += projectName;
        }

        // Additional functionality can be added here for project-specific operations
    }
}
