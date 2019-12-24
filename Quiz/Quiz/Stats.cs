using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz
{
    /// <summary>
    /// Classe statique qui renvoie différent statistique sur le Quiz
    /// </summary>
    static class Stats
    {
        /// <summary>
        /// Score moyen de l'ensemble des joueurs sur l'ensemble des parties
        /// </summary>
        /// <param name="player">Liste de l'ensemble de Player</param>
        /// <returns>Un double</returns>
        public static double GetAverageScore(List<Player> player)
        {

            return player.Average(p => p.Score);
        }

        /// <summary>
        /// Pourcentage de réussite sur chaque question
        /// </summary>
        /// <param name="players"></param>
        /// <returns>Une liste de double qui fait la taille de l'ensemble des questions</returns>
        public static List<double> GetPercentQuestion(List<Player> players)
        {
            //Pour ne pas inscrire le nombre de question en brut on le déduit
            //--> Correspond aux nombres d'erreur d'un joueur + son score
            int nbQuestions = players[0].Errors.Count + players[0].Score;
            List<double> percents = new List<double>();

            //pour chaque question
            for (int i = 1; i <= nbQuestions; i++)
            {
                int nbError = 0;
                foreach (var item in players)
                {
                    //Pour chaque joueur si la question figure dans ses erreurs on incrémente le compteur
                    if (!item.Errors.Contains(i))
                    {
                        nbError++;
                    }
                }

                //On calcul le pourcentage de réussite en divisant le nombre d'erreur sur le nombre de joueur * 100
                percents.Add((double)nbError / (double)players.Count * 100);
            }

            return percents;
        }
    }
}
