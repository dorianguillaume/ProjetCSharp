using System.Collections.Generic;

namespace Quiz
{
    /// <summary>
    /// Classe Question permettant la création de question dans le Quiz 
    /// </summary>
    class Question
    {
        //On initialise un id à 1 
        static private int _id = 1;

        /// <summary>
        /// Propriété --> Id de la question.
        /// </summary>
        public int Id { get;  }

        /// <summary>
        /// Propriété --> Question de la question 
        /// </summary>
        public string Interrogation { get; }

        /// <summary>
        /// Propriété --> Liste de string comprenanant à caque fois une réponse possible
        /// </summary>
        public List<string> Answers { get; }

        /// <summary>
        /// Propriété --> Contient la lettre correspondant à la bonne réponse
        /// </summary>
        public string GoodAnswers { get; }

        /// <summary>
        /// Constructeur --> Création d'une question
        /// </summary>
        /// <param name="question">List de string récupéré par la classe DAL</param>
        public Question(List<string> question)
        {
            Interrogation = question[0];
            Answers = new List<string>();
            Id = _id++;

            //Pour chaque réponse
            for (int i = 1; i < question.Count; i++)
            {
                //Selectionne le premier caractère de la ligne
                //SI étoile on ajoute la bonne réponse
                //ET on ajoute la réponse dans la liste sans l'astérisque
                //SINON on ajoute juste la réponse dans la liste
                if (question[i][0] == '*')
                {
                    GoodAnswers += question[i][1];
                    Answers.Add(question[i].Substring(1));
                }
                else Answers.Add(question[i]);
            }
        }

    }
}