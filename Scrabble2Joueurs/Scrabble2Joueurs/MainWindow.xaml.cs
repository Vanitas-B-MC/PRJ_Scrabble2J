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

        Joueur J1;
        Joueur J2;

        private void startGame(object sender, RoutedEventArgs e)
        {

            string nom1 = txtNomJ1.Text;
            string nom2 = txtNomJ2.Text;
            
            if (nom1 == "" || nom2 == "") { MessageBox.Show("Veuillez saisir un nom pour chaque joueur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); }

            else if (Regex.IsMatch(nom1, @"^[A-Za-z]+$") == true && Regex.IsMatch(nom2, @"^[A-Za-z]+$") == true && nom1 != nom2)
            {
                DebutPartie.Visibility = Visibility.Collapsed;
                nomJoueur1.Visibility = Visibility.Visible;
                nomJoueur2.Visibility = Visibility.Visible;
                J1 = new Joueur(nom1);
                J2 = new Joueur(nom2);
                nomJoueur1.Text = J1.GetNom();
                nomJoueur2.Text = J2.GetNom();
                AfficheurNomJ2.Content = J2.GetNom();
                AfficheurNomJ1.Content = J1.GetNom();
            }
            else
            {
                MessageBox.Show("Veuillez saisir un nom different pour chaque joueur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            }



        private void Button_Click_J1(object sender, RoutedEventArgs e)
        {
            nbrMotJ1.Content = J1.GetNbMots() + " / 10";
        }

        private void Button_Click_J2(object sender, RoutedEventArgs e)
        {
            nbrMotJ2.Content = J2.GetNbMots() + " / 10";
        }
    }
}
