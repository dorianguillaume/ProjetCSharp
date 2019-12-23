using System;
using System.Collections.Generic;

namespace Quiz
{
    public class Player
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public DateTime Date { get; set; }

        public int Score { get; set; }

        public List<int> Errors { get; set; }

        public Player(string name, string firstname)
        {
            Name = name.ToUpper();
            FirstName = char.ToUpper(firstname[0])+firstname.Substring(1);
            Errors = new List<int>();
            Date = DateTime.Now;
        }

        public Player(string ligne)
        {
            string[] tab = ligne.Split('\t');
                       
            Date = DateTime.Parse(tab[0]);
            Name = tab[1];
            FirstName = tab[2];
            Score = Int32.Parse(tab[3].Substring(0,1));
            Errors = new List<int>();
            foreach (var item in tab[4].Split(", "))
            {
                if (item != string.Empty)
                {
                    Errors.Add(Int32.Parse(item));
                                    }
            }
        }

        public override string ToString()
        {
            int nbQuestions = Score + Errors.Count;
            string message = Date.ToString("yyyy-MM-dd") + "\t" + Name + "\t" + FirstName + "\t" + Score + "/"+nbQuestions+"\t";

            for (int i = 0; i < Errors.Count; i++)
            {
                if (i == Errors.Count - 1)
                {
                    message += Errors[i];
                }
                else message += Errors[i] + ", ";
            }
            
            return message;
        }
    }
}