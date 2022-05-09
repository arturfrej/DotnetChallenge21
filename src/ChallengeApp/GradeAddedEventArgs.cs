using System;

namespace ChallengeApp
{
    public class GradeAddedEventArgs : EventArgs
    {
        private readonly double grade;

        public GradeAddedEventArgs(double grade)
        {
            this.grade = grade;
        }

        public double Grade
        {
            get {return this.grade; }
        }
    }
}