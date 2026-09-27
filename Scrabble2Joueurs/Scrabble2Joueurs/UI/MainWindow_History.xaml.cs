using MySql.Data.MySqlClient;
using Scrabble2Joueurs;
using Scrabble2Joueurs.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scrabble2Joueurs
{
    public partial class MainWindow_History : Window
    {
        public ObservableCollection<PartieHistorique> Historique { get; set; }
        

        public MainWindow_History()
        {
            this.Loaded += MainWindow_History_Loaded;
            InitializeComponent();
            Historique = new ObservableCollection<PartieHistorique>();
            this.DataContext = this;
        }

        private void MainWindow_History_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHistory();
        }


        private void LoadHistory()
        {
            MessageBox.Show("Chargement de l'historique des parties...");
            Historique.Clear();

            List<PartieHistorique> historyData = PartieDao.GetHistory();

            if (historyData != null && historyData.Count > 0)
            {
                foreach (var partie in historyData)
                {
                    Historique.Add(partie);
                }

                MessageBox.Show($"Succès ! {historyData.Count} parties chargées.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Aucune donnée n'a pu être chargée de l'historique. Vérifiez les données en base.", "Alerte", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnNouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new MainWindow();
            gameWindow.Show();
            this.Close();
        }
    }
}