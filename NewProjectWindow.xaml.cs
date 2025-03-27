using GameDevLab.Tools;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms; // Correct namespace for FolderBrowserDialog

namespace GameDevLab
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        public string ProjectName { get; private set; }
        public string ProjectDescription { get; private set; }
        public string ProjectPath { get; private set; }

        public NewProjectWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectName = ProjectNameTextBox.Text;
            ProjectDescription = ProjectDescriptionTextBox.Text;
            ProjectPath = ProjectPathTextBox.Text;

            if (string.IsNullOrWhiteSpace(ProjectName) || string.IsNullOrWhiteSpace(ProjectPath))
            {
                System.Windows.Forms.MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur");
                return;
            }

            var projectData = new { Name = ProjectName, Description = ProjectDescription };

            // Construct the path for the .gdlf file
            string gdlfFilePath = System.IO.Path.Combine(ProjectPath, ProjectName + ".gdlf");
            DataController.SaveProjectToGdlf(projectData, gdlfFilePath);

            // Close the NewProjectWindow
            this.Close();

            // Open the WorkWindow
            WorkWindow workWindow = new WorkWindow(ProjectName);
            workWindow.Show();

            // Close the MainWindow
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window is MainWindow mainWindow)
                {
                    mainWindow.Close();
                    break;
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Sélectionnez le dossier du projet";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ProjectPathTextBox.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
