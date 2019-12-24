using System;
using System.Collections.Generic;

namespace Quiz
{
    /// <summary>
    /// Classe Player permettant la création de joueur dans le Quiz
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Propriété --> nom du joueur
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Propriété --> Prénom du joueur
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Propriété --> Date à laquelle le joueur a effectué son quiz
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Propriété --> Résultat du joueur après avoir effectué le quiz
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Propriété --> Liste d'entier correspondant aux numéros de question dans lequel le joueur s'est trompé
        /// </summary>
        public List<int> Errors { get; set; }

        /// <summary>
        /// Constructeur --> Création d'un joueur avec nom et prénom
        /// </summary>
        /// <param name="name">string --> nom du joeur en majuscule</param>
        /// <param name="firstname">string --> prénom du joueur avec la première lettre en majuscule</param>
        public Player(string name, string firstname)
        {
            Name = name.ToUpper();
            FirstName = char.ToUpper(firstname[0])+firstname.Substring(1);
            Errors = new List<int>();
            Date = DateTime.Now;
        }

        /// <summary>
        /// Constructeur --> Utile pour la classe Stats (car les joueurs ne sont pas stockés en BDD)
        /// </summary>
        /// <param name="ligne">Création d'un joueur par ligne</param>
        public Player(string ligne)
        {
            //On divise chaque ligne par tabulation
            string[] tab = ligne.Split('\t');
                       
            Date = DateTime.Parse(tab[0]);
            Name = tab[1];
            FirstName = tab[2];
            Score = Int32.Parse(tab[3].Substring(0,1));
            Errors = new List<int>();

            //Pour chaque erreur
            foreach (var item in tab[4].Split(", "))
            {
                //On s'assure qu'il existe des erreurs
                if (item != string.Empty)
                {
                    Errors.Add(Int32.Parse(item));
                }
            }
        }

        /// <summary>
        /// Affichage de l'objet Player
        /// </summary>
        /// <returns>Une chaine de caractère avec toutes les infos du joueur</returns>
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