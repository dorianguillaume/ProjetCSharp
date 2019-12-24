using System;

namespace Quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Création d'un Quiz
            Quiz quizzz = new Quiz();
            //On lance le jeu
            quizzz.Play();

            //Tant que la touche Q n'est pas appuyé la console ne se ferme pas
            Console.WriteLine();
            Console.WriteLine("Veuillez saisir la touche \"q\" pour quitter le programme ");
            while (Console.ReadKey().Key!=ConsoleKey.Q)
            {
            }
        }
    }
}
