using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    class Quiz
    {
        public List<Question> Questions { get; set; }

        public Quiz()
        {
            Questions = DAL.GetQuestions();
        }

        public void Play()
        {
            Player player = InitGame();
            string answer = string.Empty;

            foreach (var question in Questions)
            {
                bool check = false;
                Console.WriteLine(question.Interrogation);
                foreach (var item in question.Answers)
                {
                    Console.WriteLine(item);
                }


                Console.WriteLine("Hey c'est quoi la bonne réponse ? Ou les bonnes répondes (Ex : AC)");
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

            //On vide les questions
            Console.Clear();

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

                Console.WriteLine("\r");

                Console.WriteLine("Les bonnes réponses étaient : "+Questions[error - 1].GoodAnswers);
            }


            //AFFICHAGE 
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

        public void CheckAnswerPlayer(string answer, int nbAnswers)
        {
            //Pour vérifier qu'il ne choissise pas une lettre qui ne correspond à une réponse
            int answerPossible = 65 + nbAnswers;

            List<char> charAnswer = new List<char>();

            if (answer.Length >= nbAnswers || answer == string.Empty)
            {
                throw new ArgumentOutOfRangeException("Veuillez saisir un nombre de réponse valide, à savoir " + (nbAnswers - 1) + " réponses maximum");
            }
            for (int i = 0; i < answer.Length; i++)
            {
                if (answer[i] < 65 || answer[i] > answerPossible)
                {
                    throw new FormatException("IL FAUT ECRIRE UNE LETTRE MAJUSCULE CORRESPONDANT A UNE REPONSE !!!!!!!");
                }

                if (charAnswer.Contains(answer[i]))
                {
                    throw new FormatException("2 fois la même réponse ??? Il y a un problème COCO !!");
                }

                charAnswer.Add(answer[i]);

            }
        }

        public void CheckGoodAnswer(string answer, string goodAnswers, Player player, int idQuestion)
        {
            if (answer == goodAnswers)
            {
                player.Score++;
            }
            else player.Errors.Add(idQuestion);
        }

        public void CheckDisplayStats(char answer)
        {
            if (answer != 'o' && answer != 'n')
            {
                throw new Exception("Veuillez saisir une réponse valide !");
            }
        }
    }
}
