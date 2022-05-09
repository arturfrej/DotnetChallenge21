using System;

namespace ChallengeApp
{
    public class Human
    {
        private string name;

        public Human(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get {return this.name;}
        }

        public void ChangeName(string newName)
        {
            foreach(char ch in newName)
            {
                if(char.IsDigit(ch))
                {
                    throw new ArgumentException($"Invalid argument {nameof(newName)}");
                }
            }

            this.name = newName;
        }
    }
}