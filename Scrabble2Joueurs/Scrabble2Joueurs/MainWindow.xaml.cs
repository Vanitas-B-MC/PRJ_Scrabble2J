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
                Lettres.Visibility = Visibility.Visible;
                J1 = new Joueur(nom1);
                J2 = new Joueur(nom2);
                AfficheurNomJ2.Content = J2.GetNom();
                AfficheurNomJ1.Content = J1.GetNom();
                string[] hand = Utilitaire.lettresScrabblesRandom();
                Letter1.Text = hand[0].ToString();
                Letter2.Text = hand[1].ToString();
                Letter3.Text = hand[2].ToString();
                Letter4.Text = hand[3].ToString();
                Letter5.Text = hand[4].ToString();
                Letter6.Text = hand[5].ToString();
                Letter7.Text = hand[6].ToString();
                txtMot.Visibility = Visibility.Visible;
                Joueur starter = Utilitaire.ChooseStarter(J1, J2);
                if (starter == J1)
                { 
                Visibility = Visibility.Visible;
                }
                else
                {
                }
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
            txtMot.Clear();
            txtTotalPointsJ2.Text = J2.GetTotalPoints().ToString();
            txtMot.Focus();
        }


    }
}
