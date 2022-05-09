using System.Collections.Generic;
using System;

namespace ChallengeApp
{
    public class Statistics
    {
        private readonly List<double> grades = new List<double>();
        public double Minimum
        {
            get
            {
                if(this.grades.Count == 0)
                    return double.NaN;
                else
                {
                    var min = double.PositiveInfinity;
                    foreach(var grade in this.grades)
                    {
                        min = Math.Min(grade, min);
                    }
                    return min;
                }
            }
        }
        public double Maximum
        {
            get
            {
                if(this.grades.Count == 0)
                    return double.NaN;
                else
                {
                    var max = double.NegativeInfinity;
                    foreach(var grade in this.grades)
                    {
                        max = Math.Max(grade, max);
                    }
                    return max;
                }
            }
        }
        public double Sum
        {
            get
            {
                double sum = 0;
                foreach(var grade in this.grades)
                {
                    sum += grade;
                }
                return sum;
            }
        }
        public double Average
        {
            get
            {
                if(this.grades.Count == 0)
                    return double.NaN;
                else
                {
                    return this.Sum / this.grades.Count;
                }
            }
        }

        public string LetterGrade
        {
            get
            {
                string letterGrade;

                switch(this.Average)
                {
                    case >= 5.5:
                        letterGrade = "A";
                        break;
                    case >= 4.75:
                        letterGrade = "B";
                        break;
                    case >= 3.75:
                        letterGrade = "C";
                        break;
                    case >= 2.75:
                        letterGrade = "D";
                        break;
                    case >= 2:
                        letterGrade = "E";
                        break;
                    case >= 1:
                        letterGrade = "F";
                        break;
                    default:
                        letterGrade = null;
                        break;
                }
                return letterGrade;
            }
        }

        public Statistics(List<double> grades)
        {
            this.grades = grades;
        }
    }
}