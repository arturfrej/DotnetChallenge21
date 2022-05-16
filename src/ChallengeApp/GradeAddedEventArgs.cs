using System;

namespace ChallengeApp
{
    public class GradeAddedEventArgs : EventArgs
    {
        private readonly string name;
        private readonly double grade;

        public GradeAddedEventArgs(string name, double grade)
        {
            this.name = name;
            this.grade = grade;
        }

        public double Grade
        {
            get {return this.grade; }
        }

        public string Name
        {
            get {return this.name; }
        }
    }
}