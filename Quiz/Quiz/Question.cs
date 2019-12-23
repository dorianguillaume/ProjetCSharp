using System.Collections.Generic;

namespace Quiz
{
    internal class Question
    {
        public string Interrogation { get; }

        public List<string> Answers { get; }

        public List<char> GoodAnswers { get; }

        public Question(List<string> question)
        {
            Interrogation = question[0];

            for (int i = 1; i < question.Count; i++)
            {
                //Selectionne le premier caractère de la ligne
                if (question[i][0] == '*')
                {
                    GoodAnswers.Add(question[i][1]);
                    Answers.Add(question[i].Substring(1));
                }
                else Answers.Add(question[i]);
            }
        }

    }
}