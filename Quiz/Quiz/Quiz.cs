using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    /// <summary>
    /// Classe Quiz princiaple du programme. Permet la création du jeu.
    /// </summary>
    class Quiz
    {
        /// <summary>
        /// Propriété --> Contient la liste des Questions
        /// </summary>
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Constructeur --> Création du Quiz
        /// </summary>
        public Quiz()
        {
            Questions = DAL.GetQuestions();
        }

        /// <summary>
        /// Fonction principal.
        /// Déroulement d'une partie complète par étape et affichage des informations en console.
        /// </summary>
        public void Play()
        {
            //*****************************************
            //DEROULEMENT DU JEU
            //*****************************************

            Player player = InitGame();
            string answer = string.Empty;

            //Pour chaque question
            foreach (var question in Questions)
            {
                bool check = false;
                //Affiche la question
                Console.WriteLine(question.Interrogation);
                //Affiche les réponses
                foreach (var item in question.Answers)
                {
                    Console.WriteLine(item);
                }
                
                Console.WriteLine("Hey c'est quoi la bonne réponse ? Ou les bonnes réponses ? (Ex : AC)");

                //On attend une réponse valide
                while (!check)
                {
                    try
                    {
                        answer = Console.ReadLine();
                        CheckAnswerPlayer(answer, question.Answers.Count);
                        check = true;
                    }
                    catch (FormatException e)
                    {

                        Console.WriteLine(e.Message); ;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {

                        Console.WriteLine(e.Message); ;
                    }
                }

                CheckGoodAnswer(answer, question.GoodAnswers, player, question.Id);
            }

            //*****************************************
            //RESULTAT DU JOUEUR
            //*****************************************

            //On vide les questions pour afficher les stats
            Console.Clear();
            //On ajoute les stats du joueur au document
            DAL.AddStat(player);

            Console.WriteLine("Votre résultat est de : " + player.Score + "/" + Questions.Count);

            foreach (var error in player.Errors)
            {
                //-1 --> Car error correspond à l'ID de la question
                Console.WriteLine(Questions[error - 1].Interrogation);

                foreach (var item in Questions[error - 1].Answers)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("Les bonnes réponses étaient : "+Questions[error - 1].GoodAnswers);
                Console.WriteLine("\r");
            }


            //*****************************************
            //DEMANDE STATISTIQUE
            //*****************************************
            Console.ReadKey();
            Console.Clear();
            bool stats = false;

            while (!stats)
            {
                Console.WriteLine("Voulez vous voir les statistiques ?");
                try
                {
                    CheckDisplayStats(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                    DAL.GetStats(out int nbGame, out double average, out List<double> percentQuestion);

                    // F2 --> Pour afficher uniquement 2 chiffre après la virgule
                    Console.WriteLine("Nombre de Game : " + nbGame + " // Moyenne totale : " + average.ToString("F2"));

                    for (int i = 0; i < percentQuestion.Count; i++)
                    {
                        Console.WriteLine("La moyenne de bonne réponse pour la question " + (i + 1) + " est de " + percentQuestion[i].ToString("F2") + " %");
                    }


                    stats = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        /// <summary>
        /// Initialise le jeu et demande aux joueurs ses infos
        /// </summary>
        /// <returns>Renvoie un Player avec nom/prenom</returns>
        public Player InitGame()
        {
            bool check = false;
            string firstname = string.Empty;
            string name = string.Empty;

            while (!check)
            {
                try
                {
                    Console.WriteLine("Quel est votre nom de famille ?");
                    name = Console.ReadLine();
                    CheckReadPlayer(name, "nom");
                    check = true;

                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            check = false;
            while (!check)
            {
                try
                {
                    Console.WriteLine("Quel est votre prenom ?");
                    firstname = Console.ReadLine();
                    CheckReadPlayer(firstname, "prénom");
                    check = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return new Player(name, firstname);
        }

        /// <summary>
        /// Méthode --> Si le joueur indique mal son nom ou prénom on lève une exception (en fonction des caractères ascii)
        /// </summary>
        /// <param name="name">string --> nom et prenom du joueur</param>
        /// <param name="type"string --> Indique si il s'agit du nom ou du prénom pour modifier le message d'erreur></param>
        public void CheckReadPlayer(string name, string type)
        {
            if (name.Equals(string.Empty))
            {
                throw new FormatException("Veuillez saisir quelques chose !");
            }
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != 45
                    && (name[i] < 65 || name[i] > 90)
                    && (name[i] < 97 || name[i] > 122)
                    && (name[i] < 129 || name[i] > 154)
                    )
                {
                    throw new FormatException("Veuillez saisir votre " + type);
                }
            }
        }

        /// <summary>
        /// Méthode --> Si le joueur ne rentre pas une réponse valide on lève une exception
        /// </summary>
        /// <param name="answer">lettre(s) --> réponse donnée par le joueur</param>
        /// <param name="nbAnswers">int --> nombre de réponse dans la question</param>
        public void CheckAnswerPlayer(string answer, int nbAnswers)
        {
            //Pour vérifier qu'il ne choissise pas une lettre qui ne correspond à une réponse
            //65 code ascii de la lettre A
            int answerPossible = 65 + nbAnswers;

            //Liste pour vérifier si il n'y a pas 2 fois la même lettre
            List<char> charAnswer = new List<char>();

            //Si le joueur à donner trop de réponse (égale ou supérieur aux nombres de réponse) ou aucune
            if (answer.Length >= nbAnswers || answer == string.Empty)
            {
                throw new ArgumentOutOfRangeException("Veuillez saisir un nombre de réponse valide, à savoir " + (nbAnswers - 1) + " réponses maximum");
            }

            //Pour chaque lettre
            for (int i = 0; i < answer.Length; i++)
            {
                //SI la lettre est inférieur au code ascii A ou supérieur au code ascii de la dernière lettre
                if (answer[i] < 65 || answer[i] > answerPossible)
                {
                    throw new FormatException("Veuillez saisir une majuscule correspondant à une réponse possible");
                }

                //SI une lettre est inscrite 2 fois
                if (charAnswer.Contains(answer[i]))
                {
                    throw new FormatException("2 fois la même réponse ??? Il y a un problème COCO !!");
                }

                charAnswer.Add(answer[i]);

            }
        }

        /// <summary>
        /// Méthode --> Si le joueur à une bonne réponse on incrémente son score
        /// SINON on ajoute la question sa liste d'erreur
        /// </summary>
        /// <param name="answer">lettre(s) --> Réponse donnée par le joueur</param>
        /// <param name="goodAnswers">lettre(s) --> Bonne réponse à la questions</param>
        /// <param name="player">Player --> joueur en cours</param>
        /// <param name="idQuestion">int --> Id de la question en cours</param>
        public void CheckGoodAnswer(string answer, string goodAnswers, Player player, int idQuestion)
        {
            if (answer == goodAnswers)
            {
                player.Score++;
            }
            else player.Errors.Add(idQuestion);
        }

        /// <summary>
        /// Renvoie une exception pour réponse non valide
        /// </summary>
        /// <param name="answer">Réponse du joueur pour rejouer ou non</param>
        public void CheckDisplayStats(char answer)
        {
            if (answer != 'o' && answer != 'n')
            {
                throw new Exception("Veuillez saisir une réponse valide !");
            }
        }
    }
}
