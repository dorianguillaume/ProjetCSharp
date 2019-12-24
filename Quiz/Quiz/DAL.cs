using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quiz
{
    /// <summary>
    /// Data Access Layer
    /// Accede aux différents fichiers
    /// </summary>
    static class DAL
    {
        private const string QCM = @"..\..\..\..\..\QCM.txt";
        private const string STAT = @"..\..\..\..\..\STAT.txt";

        /// <summary>
        /// Divise chaques lignes ligne du document en bloc de Question
        /// </summary>
        /// <returns>Renvoie une Liste d'objet Question</returns>
        public static List<Question> GetQuestions()
        {
            var questions = new List<Question>();
            var question = new List<string>();

            //Encoding --> Pour gérer les caractères spéciaux
            var file = File.ReadAllLines(QCM, Encoding.UTF8);

            //On parcours chaque ligne du document
            for (int i = 0; i < file.Length; i++)
            {
                //Si la ligne est vide on créer un objet Question à partir des lignes récupérées.
                if (file[i] == string.Empty)
                {
                    questions.Add(new Question(question));

                    //On réinitialise la liste de lignes pour la prochaine question.
                    question.Clear();
                }
                else question.Add(file[i]);
            }

            //Pour gérer la dernière question (sans mettre un retour à la ligne dans le QCM.txt)
            questions.Add(new Question(question));
            
            return questions;
        }

        /// <summary>
        /// Renvoie les différents statistiques demandés en faisant appelle à la classe Stat pour les plus "complexes"
        /// </summary>
        /// <param name="nbGame">Nombre total de partie effectuée</param>
        /// <param name="average">Renvoie le résultat de Stats.GetAverageScore(List<Player> player)</param>
        /// <param name="percentQuestion">Renvoie le résultat de Stats.GetPercentQuestion(List<Player> player)</param>
        public static void GetStats(out int nbGame, out double average, out List<double> percentQuestion)
        {
            var file = File.ReadAllLines(STAT, Encoding.UTF8);
            var players = new List<Player>();

            //Pour chaque ligne du document on stock un Player dans la liste
            for (int i = 1; i < file.Length; i++)
            {
                players.Add(new Player(file[i]));
            }

            //Correspond aux nombres de Player instancié soit à la taille de la liste ci-dessus
            nbGame = players.Count;

            average = Stats.GetAverageScore(players);

            percentQuestion = Stats.GetPercentQuestion(players);

            
        }

        /// <summary>
        /// Ajoute les résultat de la partie d'un joueur au document STAT.txt
        /// Qu'il soit créé ou non
        /// </summary>
        /// <param name="player">Player de la partie en cours</param>
        public static void AddStat(Player player)
        {

            //On vérifie que le fichier existe déjà ou non
            if (!File.Exists(STAT))
            {

                //Grâce à using on s'assure de ne pas corrompre le fichier
                using (StreamWriter sw = new StreamWriter(STAT))
                {
                    sw.WriteLine("DATE\tNOM\tScore\tErreurs");
                    sw.WriteLine(player.ToString());
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(STAT, true))
                {
                    sw.WriteLine(player.ToString());
                }
            }
        }
    }
}
