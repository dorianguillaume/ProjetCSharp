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
            Name = name;
            FirstName = name;
            Date = DateTime.Now;
        }
    }
}