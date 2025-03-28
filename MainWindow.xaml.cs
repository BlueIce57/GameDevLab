﻿using GameDevLab.Tools;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows;

namespace GameDevLab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            LoadRecentProjects();
        }

        private void LoadRecentProjects()
        {
            var projects = DatabaseHelper.GetProjects();
            RecentProjectsList.ItemsSource = projects;
        }

        private void CreateNewProject_Click(object sender, RoutedEventArgs e)
        {
            NewProjectWindow newProjectWindow = new NewProjectWindow();
            if (newProjectWindow.ShowDialog() == true)
            {
                DatabaseHelper.AddProject(newProjectWindow.ProjectName, newProjectWindow.ProjectDescription, newProjectWindow.ProjectPath);
                LoadRecentProjects();
                System.Windows.Forms.MessageBox.Show($"Nouveau projet créé: {newProjectWindow.ProjectName}");
            }
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            if (RecentProjectsList.SelectedItem != null)
            {
                var selectedProject = (dynamic)RecentProjectsList.SelectedItem;
                WorkWindow workWindow = new WorkWindow(selectedProject.Name);
                workWindow.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Veuillez sélectionner un projet à ouvrir.");
            }
        }

        private void CompressTest_Click(object sender, RoutedEventArgs e)
        {
            var projectData = new { Name = "MonProjet", Date = DateTime.Now, Data = "Données du projet" };
            DataController.SaveProjectToGdlf(projectData, "MonProjet.gdlf");

        }

        private void DecompressTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var projectData = DataController.LoadProjectFromGdlf<dynamic>("MonProjet.gdlf");
                Console.WriteLine(projectData.Name);
                System.Windows.Forms.MessageBox.Show($"Projet décompressé: {projectData.Name} Data : {projectData.Data}");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Erreur lors de la décompression: {ex.Message}");
            }
        }
    }
}