using System.Collections.Generic;
using System;

namespace ChallengeApp
{
    public class Statistics
    {
        public double Sum {get; private set;}
        public double Count {get; private set;} 
        private double minimum;
        private double maximum;
        public double Minimum
        {
            get
            {
                if(this.Count == 0)
                    return double.NaN;
                else
                {
                    return this.minimum;
                }
            }
        }
        public double Maximum
        {
            get
            {
                if(this.Count == 0)
                    return double.NaN;
                else
                {
                    return this.maximum;
                }
            }
        }
        public double Average
        {
            get
            {
                if(this.Count == 0)
                    return double.NaN;
                else
                {
                    return this.Sum / this.Count;
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

        public Statistics()
        {
            this.ClearGrades();
        }

        public Statistics(List<double> grades)
        {
            this.ClearGrades();
            
            foreach(var grade in grades)
            {
                this.AddGrade(grade);
            }
        }

        public void ClearGrades()
        {
            this.Count = 0;
            this.Sum = 0;
            this.minimum = double.PositiveInfinity;
            this.maximum = double.NegativeInfinity;
        }

        public void AddGrade(double grade)
        {
            this.Count += 1;
            this.Sum += grade;
            this.minimum = Math.Min(grade, this.minimum);
            this.maximum = Math.Max(grade, this.maximum);
        }
    }
}