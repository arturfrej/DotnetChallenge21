using System;
using System.Collections.Generic;

namespace ChallengeApp
{
    public class Student : StudentBase
    {
        private List<double> grades;
        private Statistics stats;
        public override event GradeAddedDelegate GradeAdded;
        public override event GradesClearedDelegate GradesCleared;

        public Student(string name) : base(name)
        {
            this.grades = new List<double>();
            this.stats = new Statistics();
        }

        public override void AddGrade(double grade)
        {
            if(grade < 1 || grade > 6)
            {
                throw new ArgumentException($"Invalid argument {nameof(grade)}");
            }

            this.grades.Add(grade);
            this.stats.AddGrade(grade);
            
            if(this.GradeAdded != null)
            {
                GradeAddedEventArgs args = new GradeAddedEventArgs(this.Name, grade);
                this.GradeAdded(this, args);
            }
        }

        public void AddGrade2(string grade)
        {
            int result;

            if(int.TryParse(grade, out result))
            {
                this.AddGrade(result);
            }
        }

        public override void AddGrade(string grade)
        {
            double val;

            switch(grade)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                    double.TryParse(grade, out val);
                    break;
                case "1+":
                case "2+":
                case "3+":
                case "4+":
                case "5+":
                    double.TryParse(grade.Substring(0, 1), out val);
                    val += 0.5;
                    break;
                default:
                    throw new ArgumentException($"Invalid argument {nameof(grade)}");
            }

            this.AddGrade(val);
        }

        public override void ClearGrades()
        {
            this.grades.Clear();
            this.stats.ClearGrades();

            if(this.GradesCleared != null)
            {
                GradesClearedEventArgs args = new GradesClearedEventArgs(this.Name);
                this.GradesCleared(this, args);
            }
        }

        public override Statistics GetStatistics()
        {
            return this.stats;
        }
    }
}