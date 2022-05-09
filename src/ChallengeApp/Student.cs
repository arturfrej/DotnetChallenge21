using System;
using System.Collections.Generic;

namespace ChallengeApp
{
    public delegate void GradeAddedDelegate(object sender, GradeAddedEventArgs args);

    public class Student : Human
    {
        private List<double> grades = new List<double>();
        
        public event GradeAddedDelegate GradeAdded;

        public Student(string name) : base(name)
        {
        }

        public void AddGrade(double grade)
        {
            if(grade < 1 || grade > 6)
            {
                throw new ArgumentException($"Invalid argument {nameof(grade)}");
            }

            this.grades.Add(grade);
            
            if(this.GradeAdded != null)
            {
                GradeAddedEventArgs args = new GradeAddedEventArgs(grade);
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

        public void AddGrade(string grade)
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

        public void ClearGrades()
        {
            this.grades.Clear();
        }

        public Statistics GetStatistics()
        {
            var stats = new Statistics(this.grades);
            return stats;
        }
    }
}