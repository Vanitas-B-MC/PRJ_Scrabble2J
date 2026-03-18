using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void startGame(object sender, RoutedEventArgs e)
        {
            string nom1 = txtNomJ1.Text;
            string nom2 = txtNomJ2.Text;
            nomJoueur1.Text = nom1;
            nomJoueur2.Text = nom2;
            if (nom1 == nom2)
                MessageBox.Show("Veuillez saisir un nom different pour chaque joueur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

            if (Regex.IsMatch(nom1, @"^[A-Za-z]+$") == true && Regex.IsMatch(nom2, @"^[A-Za-z]+$") == true)
            {
                btnCommencer.Visibility = Visibility.Collapsed;
                txtNomJ1.Visibility = Visibility.Collapsed;
                txtNomJ2.Visibility = Visibility.Collapsed;
                aDeleteJ1.Visibility = Visibility.Collapsed;
                aDeleteJ2.Visibility = Visibility.Collapsed;
                nomJoueur1.Visibility = Visibility.Visible;
                nomJoueur2.Visibility = Visibility.Visible;
            }
            else MessageBox.Show("Veuillez saisir un nom pour chaque joueur","Erreur",MessageBoxButton.OK, MessageBoxImage.Error);
        }


    }
}
