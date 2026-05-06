using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private HashSet<string> words;
        public MainWindow()
        {
            InitializeComponent();


            // Chargement du dictionnaire (J'ai mis le dictionnaire dans un dossier assets)
            // Charge les mots de la liste
            // Accès au dossier du projet
            string projectFolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            // Chemin du dictionnaire
            string dictionaryPath = System.IO.Path.Combine(projectFolder, "assets", "French_ODS_dictionary.txt");

            // Petit check des familles pour voir si le dictionnaire existe
            if (!File.Exists(dictionaryPath))
            {
                MessageBox.Show("Dictionary file not found:\n" + dictionaryPath);
                words = new HashSet<string>();
                return;
            }

            // Charger le dictionnaire dans un HashSet
            words = new HashSet<string>(File.ReadAllLines(dictionaryPath), StringComparer.OrdinalIgnoreCase);
        }

        private bool EstCeQueLeMotExiste()
        {
            return words.Contains(txtMot.Text.Trim());
        }





        Joueur J1;
        Joueur J2;

        /// <summary>
        /// Méthode qui permet de lancer la partie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startGame(object sender, RoutedEventArgs e)
        {

            string nom1 = txtNomJ1.Text;
            string nom2 = txtNomJ2.Text;
            
            if (nom1 == "" || nom2 == "") { MessageBox.Show("Veuillez saisir un nom pour chaque joueur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); }

            else if (Regex.IsMatch(nom1, @"^[A-Za-z]+$") == true && Regex.IsMatch(nom2, @"^[A-Za-z]+$") == true && nom1 != nom2)
            {
                DebutPartie.Visibility = Visibility.Collapsed;
                Lettres.Visibility = Visibility.Visible;
                AfficheurNomJ2.Visibility = Visibility.Visible;
                

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
                FinPartie.Visibility = Visibility.Visible;
                Joueur starter = Utilitaire.ChooseStarter(J1, J2);
                if (starter == J1)
                {
                    Joueur1play.Visibility = Visibility.Visible;
                    AfficheurNomJ1.Visibility = Visibility.Visible;
                    nbrMotJ1.Visibility = Visibility.Visible;
                    txtTotalPointsJ1.Visibility = Visibility.Visible;

                    AfficheurNomJ2.Visibility = Visibility.Visible;
                    nbrMotJ2.Visibility = Visibility.Visible;
                    txtTotalPointsJ2.Visibility = Visibility.Visible;
                    MessageBox.Show(J1.GetNom() + " commence en premier", "Partie", MessageBoxButton.OK, MessageBoxImage.Information);
                    BordureJoueur1.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 180, 0)) ;
                    BordureJoueur1.BorderThickness = new Thickness(4);
                }
                else
                {
                    Joueur2play.Visibility = Visibility.Visible;
                    AfficheurNomJ1.Visibility = Visibility.Visible;
                    nbrMotJ1.Visibility = Visibility.Visible;
                    txtTotalPointsJ1.Visibility = Visibility.Visible;

                    AfficheurNomJ2.Visibility = Visibility.Visible;
                    nbrMotJ2.Visibility = Visibility.Visible;
                    txtTotalPointsJ2.Visibility = Visibility.Visible;
                    MessageBox.Show(J2.GetNom() + " commence en premier", "Partie", MessageBoxButton.OK, MessageBoxImage.Information);
                    BordureJoueur2.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 180, 0));
                    BordureJoueur2.BorderThickness = new Thickness(4);
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir un nom different pour chaque joueur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


        /// <summary>
        /// Méthode qui permet d'ajouter un mot à la liste des mots du joueur 1 et qui actualise le nombre total de points du joueur 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_J1(object sender, RoutedEventArgs e)
        {
            if (Utilitaire.utilisationDesLettres(txtMot.Text, new[] { Letter1.Text, Letter2.Text, Letter3.Text, Letter4.Text, Letter5.Text, Letter6.Text, Letter7.Text }) == true)
            {
                if (EstCeQueLeMotExiste() == true)
                {
                    J1.AjouterMot(txtMot.Text);
                    nbrMotJ1.Content = "Mots : " + J1.GetNbMots() + " / 10";
                    txtMot.Clear();
                    txtTotalPointsJ1.Text = J1.GetTotalPoints().ToString();
                    txtMot.Focus();

                    string[] hand = Utilitaire.lettresScrabblesRandom();
                    Letter1.Text = hand[0].ToString();
                    Letter2.Text = hand[1].ToString();
                    Letter3.Text = hand[2].ToString();
                    Letter4.Text = hand[3].ToString();
                    Letter5.Text = hand[4].ToString();
                    Letter6.Text = hand[5].ToString();
                    Letter7.Text = hand[6].ToString();

                    Joueur1play.Visibility = Visibility.Hidden;
                    Joueur2play.Visibility = Visibility.Visible;

                    BordureJoueur2.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 180, 0));
                    BordureJoueur2.BorderThickness = new Thickness(4);

                    BordureJoueur1.BorderBrush = new SolidColorBrush(Color.FromRgb(104, 146, 179));
                    BordureJoueur1.BorderThickness = new Thickness(2);

                    if (J2.GetNbMots() >= 10 && J1.GetNbMots() == 10)
                    {
                        if (J1.GetTotalPoints() == J2.GetTotalPoints())
                        {
                            MessageBox.Show(J1.GetNom() + " " + J2.GetNom() + " on finit ex æquo !! \n Leurs meilleurs mots était pour " + J1.GetNom() + " le mot : " + J1.MotMeilleur() + " et pour " + J2.GetNom() + " le mot : " + J2.MotMeilleur(), "Fin de partie", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        if (J1.GetTotalPoints() > J2.GetTotalPoints())
                        {
                            var mots = J1.GetLesMots();
                            string message = string.Join("\n", mots);
                            MessageBox.Show(J1.GetNom() + " a gagné la partie, son meilleur mot était " + J1.MotMeilleur() + "\n Listes des mots du vainceur" + message + "", "Fin de partie", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            var mots = J2.GetLesMots();
                            string message = string.Join("\n", mots);
                            MessageBox.Show(J2.GetNom() + " a gagné la partie, son meilleur mot était " + J2.MotMeilleur() + "\n Listes des mots du vainceur" + message + "", "Fin de partie", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        FinPartie.Visibility = Visibility.Hidden;
                        Joueur2play.Visibility = Visibility.Hidden;
                        Joueur1play.Visibility = Visibility.Hidden;
                    }
                }
                else { MessageBox.Show("Le mot n'existe pas", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else { MessageBox.Show("Vous utilisez des lettres qui ne sont pas dans votre main Ou vous avez deja utilise ces lettres", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        /// <summary>
        /// Méthode qui permet d'ajouter un mot à la liste des mots du joueur 2 et qui actualise le nombre total de points du joueur 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_J2(object sender, RoutedEventArgs e)
        {
            if (Utilitaire.utilisationDesLettres(txtMot.Text, new[] { Letter1.Text, Letter2.Text, Letter3.Text, Letter4.Text, Letter5.Text, Letter6.Text, Letter7.Text }) == true)
            {
                if (EstCeQueLeMotExiste() == true)
                {
                    J2.AjouterMot(txtMot.Text);
                    nbrMotJ2.Content = "Mots : " + J2.GetNbMots() + " / 10";
                    txtMot.Clear();
                    txtTotalPointsJ2.Text = J2.GetTotalPoints().ToString();
                    txtMot.Focus();

                    string[] hand = Utilitaire.lettresScrabblesRandom();
                    Letter1.Text = hand[0].ToString();
                    Letter2.Text = hand[1].ToString();
                    Letter3.Text = hand[2].ToString();
                    Letter4.Text = hand[3].ToString();
                    Letter5.Text = hand[4].ToString();
                    Letter6.Text = hand[5].ToString();
                    Letter7.Text = hand[6].ToString();

                    Joueur1play.Visibility = Visibility.Visible;
                    Joueur2play.Visibility = Visibility.Hidden;

                    BordureJoueur1.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 180, 0));
                    BordureJoueur1.BorderThickness = new Thickness(4);

                    BordureJoueur2.BorderBrush = new SolidColorBrush(Color.FromRgb(104, 146, 179));
                    BordureJoueur2.BorderThickness = new Thickness(2);

                    if (J2.GetNbMots() == 10 && J1.GetNbMots() == 10)
                    {
                        if (J1.GetTotalPoints() == J2.GetTotalPoints()) 
                        {
                            MessageBox.Show(J1.GetNom() + " " + J2.GetNom() + " on finit ex æquo !! \n Leurs meilleurs mots était pour " + J1.GetNom() + " le mot : " + J1.MotMeilleur() + " et pour " + J2.GetNom() + " le mot : " + J2.MotMeilleur(), "Fin de partie", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        if (J1.GetTotalPoints() > J2.GetTotalPoints())
                        {
                            var mots = J1.GetLesMots();
                            string message = string.Join("\n", mots);
                            MessageBox.Show(J1.GetNom() + " a gagné la partie, son meilleur mot était " + J1.MotMeilleur() + "\n Listes des mots du vainceur" + message + "", "Fin de partie", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            var mots = J2.GetLesMots();
                            string message = string.Join("\n", mots);
                            MessageBox.Show(J2.GetNom() + " a gagné la partie, son meilleur mot était " + J2.MotMeilleur() + "\n Listes des mots du vainceur" + message + "", "Fin de partie", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        FinPartie.Visibility = Visibility.Hidden;
                        Joueur2play.Visibility = Visibility.Hidden;
                        Joueur1play.Visibility = Visibility.Hidden;
                    }
                }
                else { MessageBox.Show("Le mot n'existe pas", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else { MessageBox.Show("Vous utilisez des lettres qui ne sont pas dans votre main Ou vous avez deja utilise ces lettres", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        /// <summary>
        /// Méthode qui permet de mettre le focus sur le TextBox txtMot lorsqu'on clique dessus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void focusOnTxtxMot(object sender, RoutedEventArgs e)
        {
                txtMot.Text = "";
                txtMot.Foreground = Brushes.Black;
        }


        private void lostFocusOnTxtMot(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Méthode qui permet de mettre le texte "Entrez votre mot" dans le TextBox txtMot 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMot_init(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMot.Text))
            {
                txtMot.Text = "Entrez votre mot";
                txtMot.Foreground = Brushes.Gray;
            }
        }

        private void start(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomJ1.Text))
            {
            txtNomJ1.Foreground = Brushes.Gray;
            txtNomJ1.Text = Utilitaire.ChoisirNomAleatoire();
            }
        }

        private void focusOnTxtNomJ1(object sender, RoutedEventArgs e)
        {
            txtNomJ1.Text = "";
            txtNomJ1.Foreground = Brushes.Black;
        }

        private void start2(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomJ2.Text))
            {
                txtNomJ2.Foreground = Brushes.Gray;
                txtNomJ2.Text = Utilitaire.ChoisirNomAleatoire();
            }
        }

        private void gotFocusOnTxtNomJ2(object sender, RoutedEventArgs e)
        {
            txtNomJ2.Text = "";
            txtNomJ2.Foreground = Brushes.Black;
        }
    }
}
