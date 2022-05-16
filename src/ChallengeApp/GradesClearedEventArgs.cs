using System;

namespace ChallengeApp
{
    public class GradesClearedEventArgs : EventArgs
    {
        private readonly string name;

        public GradesClearedEventArgs(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get {return this.name; }
        }
    }
}