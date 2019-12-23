﻿using System.Collections.Generic;

namespace Quiz
{
    internal class Question
    {
        public int Id { get; }
        public string Interrogation { get; }

        public List<string> Answers { get; }

        public string GoodAnswers { get; }

        public Question(List<string> question)
        {
            Interrogation = question[0];

            for (int i = 1; i < question.Count; i++)
            {
                Id = i;
                Answers = new List<string>();
                //Selectionne le premier caractère de la ligne
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