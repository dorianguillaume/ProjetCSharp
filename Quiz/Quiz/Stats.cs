using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz
{
    class Stats
    {

       public static double GetAverageScore(List<Player> player)
        {

            return player.Average(p => p.Score);
        }        

       public static List<double> GetPercentQuestion(List<Player> players)
        {
            int nbQuestions = players[0].Errors.Count + players[0].Score;
            List<double> percents = new List<double>();

            for (int i = 1; i <= nbQuestions; i++)
            {
                int nbError = 0 ;
                foreach (var item in players)
                {
                    if (!item.Errors.Contains(i))
                    {
                        nbError++;
                    }
                }

                percents.Add((double)nbError / (double)players.Count * 100);
            }

            return percents;
        }
    }
}
