using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Classe qui regroupe les fonctions de calcul
    /// </summary>
    public static class Utilitaire
    {
        /// <summary>
        /// Méthode qui retourne le nombre de points que rapporte une lettre
        /// </summary>
        /// <param name="l">Lettre de type char</param>
        /// <returns>Nombre de points rapportés par la lettre</returns>
        private static int PointsLettre(char l)
        {
            int p;
            if (l == 'D' || l == 'G' || l == 'M')
                p = 2;
            else
            {
                if (l == 'B' || l == 'C' || l == 'P')
                    p = 3;
                else
                {
                    if (l == 'F' || l == 'H' || l == 'V')
                        p = 4;
                    else
                    {
                        if (l == 'J' || l == 'Q')
                            p = 8;
                        else
                        {
                            if (l == 'K' || l == 'W' || l == 'X' || l == 'Y' || l == 'Z')
                                p = 10;
                            else
                                p = 1;
                        }
                    }
                }
            }
            return p;
        }
        /// <summary>
        /// Méthode qui retourne le nombre de points que rapporte un mot
        /// </summary>
        /// <param name="mot">Mot de type string</param>
        /// <returns>Nombre de points du mot</returns>
        public static int PointsMot(string mot)
        {
            mot = mot.ToUpper();
            int pts = 0;
            for (int i = 0; i <= mot.Length - 1; i++)
            {
                char lettre = mot[i];
                pts = pts + PointsLettre(lettre);
            }
            return pts;
        }

        #region Méthodes pour le scrabble
        public static string[] lettresScrabblesRandom()
        {
            // listes littres du scrabble
            string lettres = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLMMNNNNNNOOOOOOOOPPQRRRRRRSSTTTTTTUUUUVVWWXYYZ";
            Random rand = new Random();
            string[] hand = new string[7];

            for (int i = 0; i < hand.Length; i++)
            {
                int index = rand.Next(lettres.Length);
                hand[i] = lettres[index].ToString(); // covertie de char en string
            }

            return hand;
        }


        public static bool utilisationDesLettres(string mot, string[] hand)
        {
            // On crée une copie de la main pour ne pas modifier l'original
            var lettresDisponibles = new List<string>(hand);

            foreach (char c in mot.ToUpper())
            {
                string lettre = c.ToString();
                if (!lettresDisponibles.Remove(lettre))
                {
                    // La lettre n'est pas dans la main ou déjà utilisée
                    return false;
                }
            }

            return true; // Toutes les lettres du mot étaient disponibles
        }


        public static Joueur ChooseStarter(Joueur j1, Joueur j2)
        {
            Random rand = new Random();
            // Retourne un joueur au hasard
            return rand.Next(2) == 0 ? j1 : j2;
        }


        #endregion
    }
}
