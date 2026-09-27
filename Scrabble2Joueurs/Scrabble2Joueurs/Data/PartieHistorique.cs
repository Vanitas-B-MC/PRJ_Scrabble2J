using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs.Data
{
    public class PartieHistorique
    {
        public DateTime Date { get; set; }
        public string Joueur1 { get; set; }
        public string Joueur2 { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }

        // Constructeur
        public PartieHistorique(DateTime date, string j1, string j2, int s1, int s2)
        {
            Date = date;
            Joueur1 = j1;
            Joueur2 = j2;
            Score1 = s1;
            Score2 = s2;
        }

        // Constructeur par défaut
        public PartieHistorique() : this(DateTime.MinValue, "", "", 0, 0) { }
    }
}
