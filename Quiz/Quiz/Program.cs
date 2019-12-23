using System;

namespace Quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            Quiz quizzz = new Quiz();
            quizzz.Play();
            Console.WriteLine("Veuillez saisir la touche \"q\" pour quitter le programme ");
            while (Console.ReadKey().Key!=ConsoleKey.Q)
            {
            }
        }
    }
}
