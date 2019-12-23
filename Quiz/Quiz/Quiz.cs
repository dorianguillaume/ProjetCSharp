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
            bool check = false;
            string answer = string.Empty;

            foreach (var question in Questions)
            {
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

            DAL.AddStat(player);
            Console.WriteLine("Votre résultat est de : " + player.Score + "/" + Questions.Count);

            foreach (var error in player.Errors)
            {
                //-1 --> Car error correspond à l'ID de la question
                Console.WriteLine(Questions[error - 1].Interrogation);
                Console.WriteLine(Questions[error - 1].Answers);
                Console.WriteLine("\r");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Questions[error - 1].GoodAnswers);
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
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != 45
                    || (name[i] < 65 && name[i] > 90)
                    || (name[i] < 97 && name[i] > 122)
                    || (name[i] < 129 && name[i] > 154))
                {
                    throw new FormatException("Veuillez saisir votre"+type);
                }
            }
        }

        public void CheckAnswerPlayer(string answer, int nbAnswers)
        {
            //Pour vérifier qu'il ne choissise pas une lettre qui ne correspond à une réponse
            int answerPossible = 64 + nbAnswers;

            List<char> charAnswer = new List<char>();

            if(answer.Length >= nbAnswers)
            {
                throw new ArgumentOutOfRangeException("Veuillez saisir un nombre de réponse valide, à savoir " + (nbAnswers-1) + " réponses maximum");
            }
            for (int i = 0; i < answer.Length; i++)
            {
                if (answer[i] < 65 && answer[i] > answerPossible)
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
    }
}
