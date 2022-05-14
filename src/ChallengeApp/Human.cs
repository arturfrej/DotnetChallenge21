using System;

namespace ChallengeApp
{
    public class Human
    {
        private string name;

        public Human(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            
            private set
            {
                if(String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid argument {nameof(Name)}");
                }
                foreach(char ch in value)
                {
                    if(char.IsDigit(ch))
                    {
                        throw new ArgumentException($"Invalid argument {nameof(Name)}");
                    }
                }
                this.name = value;
            }
        }

        public void ChangeName(string newName)
        {
            this.Name = newName;
        }
    }
}