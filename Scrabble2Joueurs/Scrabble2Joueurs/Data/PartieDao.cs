using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Scrabble2Joueurs.Data
{
    internal class PartieDao
    {
        /// <summary>
        /// Récupère tout l'historique des parties jouées de manière synchrone.
        /// </summary>
        public static List<PartieHistorique> GetHistory()
        {
            var history = new List<PartieHistorique>();
            string query = "SELECT date, joueur1, joueur2, score1, score2 FROM parties_historiques ORDER BY date DESC";

            using (var connection = Connexion.Connect())
            {
                if (connection == null) return history;
                try
                {
                    connection.Open();

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Récupération des données par index
                            DateTime date = reader.GetDateTime(0);
                            string j1 = reader.GetString(1);
                            string j2 = reader.GetString(2);
                            int s1 = reader.GetInt32(3);
                            int s2 = reader.GetInt32(4);

                            history.Add(new PartieHistorique(date, j1, j2, s1, s2));
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERREUR SQL : " + ex.Message);
                }
            }
            return history;
        }

        /// <summary>
        /// Sauvegarde les résultats d'une partie de manière synchrone.
        /// </summary>
        public static void SaveParty(PartieHistorique partie)
        {
            string query = "INSERT INTO parties_historiques (date, joueur1, joueur2, score1, score2) VALUES (@date, @j1, @j2, @s1, @s2)";

            using (var connection = Connexion.Connect())
            {
                if (connection == null) return;

                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", partie.Date);
                        command.Parameters.AddWithValue("@j1", partie.Joueur1);
                        command.Parameters.AddWithValue("@j2", partie.Joueur2);
                        command.Parameters.AddWithValue("@s1", partie.Score1);
                        command.Parameters.AddWithValue("@s2", partie.Score2);

                        command.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERREUR SQL lors de la sauvegarde : " + ex.Message);
                }
            }
        }
    }
}
