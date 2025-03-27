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

            // Vous pouvez ajouter la logique pour sauvegarder le projet ici

            System.Windows.Forms.MessageBox.Show($"Projet '{ProjectName}' créé avec succès !", "Succès");
            DialogResult = true;
            Close();
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
