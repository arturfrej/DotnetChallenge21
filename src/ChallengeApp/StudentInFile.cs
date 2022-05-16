using System;
using System.Collections.Generic;
using System.IO;

namespace ChallengeApp
{
    public class StudentInFile : StudentBase
    {
        public override event GradeAddedDelegate GradeAdded;
        public override event GradesClearedDelegate GradesCleared;
        private const string filenameAudit = "audit.txt";
        private string Filename
        {
            get { return $"{this.Name}.txt"; }
        }

        public StudentInFile(string name) : base(name)
        {
        }

        public override void AddGrade(double grade)
        {
            if(grade < 1 || grade > 6)
            {
                throw new ArgumentException($"Invalid argument {nameof(grade)}");
            }

            using (var writer = File.AppendText(this.Filename))
            using (var writerAudit = File.AppendText(filenameAudit))
            {
                writer.WriteLine(grade);
                writerAudit.WriteLine($"{DateTime.UtcNow}: {this.Name}: GradeAdded: {grade}");

                if(this.GradeAdded != null)
                {
                    GradeAddedEventArgs args = new GradeAddedEventArgs(this.Name, grade);
                    this.GradeAdded(this, args);
                }
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
            File.Create(this.Filename).Dispose();

            using (var writerAudit = File.AppendText(filenameAudit))
            {
                writerAudit.WriteLine($"{DateTime.UtcNow}: {this.Name}: GradesCleared");
            }

            if(this.GradesCleared != null)
            {
                GradesClearedEventArgs args = new GradesClearedEventArgs(this.Name);
                this.GradesCleared(this, args);
            }
        }

        public new void ChangeName(string newName)
        {
            var oldName = this.Name;
            var oldFilename = this.Filename;

            base.ChangeName(newName);

            if(File.Exists(oldFilename))
            {
                File.Move(oldFilename, this.Filename);
            }
            
            using (var writerAudit = File.AppendText(filenameAudit))
            {
                writerAudit.WriteLine($"{DateTime.UtcNow}: {oldName}: NameChanged: {this.Name}");
            }
        }

        public override Statistics GetStatistics()
        {
            Statistics stats = new Statistics();
            double grade;
            
            if(File.Exists(this.Filename))
            {
                using (var reader = File.OpenText(this.Filename))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        if(double.TryParse(line, out grade))
                        {
                            stats.AddGrade(grade);
                        }
                        else
                        {
                            throw new InvalidDataException($"Invalid content of {this.Filename}");
                        }
                        line = reader.ReadLine();
                    }
                }
            }

            return stats;
        }
    }
}