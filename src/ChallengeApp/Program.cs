using System;
using System.Collections.Generic;
using System.IO;

namespace ChallengeApp
{
    class Program
    {
        private const string filenameAudit = "auditOverEvents.txt";

        static void Main(string[] args)
        {
            
            var student = new StudentInFile("Anton");

            student.ChangeName("Janek");

            student.GradeAdded += OnGradeAdded;
            student.GradesCleared += OnGradesCleared;
            
            student.AddGrade(5);
            student.AddGrade(1);
            student.AddGrade(6);
            student.AddGrade(2);
            student.AddGrade("3+");
            // student.ClearGrades();

            DisplayStatistics(student);
        }
        
        static void OnGradeAdded(object sender, GradeAddedEventArgs args)
        {
            if(args.Grade < 3)
            {
                Console.WriteLine("Oh no! We should inform student's parents about this fact!");
            }

            using (var writerAudit = File.AppendText(filenameAudit))
            {
                writerAudit.WriteLine($"{DateTime.UtcNow}: {args.Name}: GradeAdded: {args.Grade}");
            }
        }
        
        static void OnGradesCleared(object sender, GradesClearedEventArgs args)
        {
            using (var writerAudit = File.AppendText(filenameAudit))
            {
                writerAudit.WriteLine($"{DateTime.UtcNow}: {args.Name}: GradesCleared");
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