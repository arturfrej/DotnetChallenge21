using System;
using System.Collections.Generic;

namespace ChallengeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var student = new StudentInFile("Anton");

            student.ChangeName("Janek");

            student.GradeAdded += OnGradeAdded;
            
            student.AddGrade(5);
            student.AddGrade(1);
            student.AddGrade(6);
            student.AddGrade(2);
            student.AddGrade("3+");

            DisplayStatistics(student);
        }
        
        static void OnGradeAdded(object sender, GradeAddedEventArgs args)
        {
            if(args.Grade < 3)
            {
                Console.WriteLine("Oh no! We should inform student's parents about this fact!");
            }
        }

        static public void DisplayStatistics(IStudent student)
        {
            var stats = student.GetStatistics();

            string letterGrade = stats.LetterGrade;
            
            if(letterGrade == null)
            {
                letterGrade = "-";
            }

            Console.WriteLine($"Student: {student.Name}; Min: {stats.Minimum}; Max: {stats.Maximum}; Mean: {Math.Round(stats.Average, 2)}; LetterGrade: {letterGrade}");

        }
    }
}