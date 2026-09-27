using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Scrabble2Joueurs.Data
{
    internal static class Connexion
    {
        public static MySqlConnection Connect()
        {
            MySqlConnection connex = null;
            string chaineConnex = "server=localhost; port=3306 ; user id=root; password=; database=bdd_scrabble;";
            try
            {
                connex = new MySqlConnection(chaineConnex);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la connexion à la base de données." + e.Message);
            }
            return connex;
        }
    }
}
