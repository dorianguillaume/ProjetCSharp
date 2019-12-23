using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quiz
{
    //Data Access Layer
    static class DAL
    {
        public const string QCM = @"..\..\..\..\..\QCM.txt";
        public const string STAT = @"..\..\..\STAT.txt";

        public static List<Question> GetQuestions()
        {
            var questions = new List<Question>();
            var question = new List<string>();

            //Exception levée si le fichier n'est pas trouvé

            var file = File.ReadAllLines(QCM, Encoding.UTF8);

            for (int i = 0; i < file.Length; i++)
            {
                if (file[i] == string.Empty)
                {
                    questions.Add(new Question(question));
                    question.Clear();

                }
                else question.Add(file[i]);
            }


            return questions;
        }


        public static List<string> GetStats()
        {
            var stats = new List<string>();

            var file = File.ReadAllLines(STAT, Encoding.UTF8);

            for (int i = 0; i < file.Length; i++)
            {
                stats.Add(file[i]);
            }
            return stats;
        }

        public static void AddStat(Player player)
        {
            if (!File.Exists(STAT))
            {
                //Ferme le fichier automatiquement
                using (StreamWriter sw = File.CreateText(STAT))
                {
                    sw.WriteLine("DATE\tNOM\tScore\tErreurs");
                    sw.WriteLine(player.ToString());
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(STAT))
                {
                    sw.WriteLine(player.ToString());
                }
            }
        }
    }
}
