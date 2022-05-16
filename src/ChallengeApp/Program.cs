using System;
using System.Collections.Generic;
using System.IO;

namespace ChallengeApp
{
    class Program
    {
        private const string filenameAudit = "auditOverEvents.txt";
        private const string ver = "1.0";
        static void Main(string[] args)
        {
            StudentInFile student = null;

            if(!Directory.Exists("studentdb"))
            {
                Directory.CreateDirectory("studentdb");
            }
            
            Console.WriteLine($"\nChallangeApp v{ver}. Type HELP to see available commands.");
            
            var continueLoop = true;
            while(continueLoop)
            {
                Console.Write("\n>> ");
                var command = Console.ReadLine();
                if(command != null)
                {
                    command = command.Trim();
                }

                switch(command)
                {
                    case string c when c.Equals("ADDGRADES", StringComparison.OrdinalIgnoreCase):
                        CmdAddGrades(student);
                        break;
                    case string c when c.StartsWith("ADDGRADE", StringComparison.OrdinalIgnoreCase):
                        CmdAddGrade(command, student);
                        break;
                    case string c when c.StartsWith("ADDSTUDENT", StringComparison.OrdinalIgnoreCase):
                        CmdAddStudent(command);
                        break;
                    case string c when c.Equals("DELETE", StringComparison.OrdinalIgnoreCase):
                        CmdDelete(student);
                        student = null;
                        break;
                    case string c when c.Equals("CLEARGRADES", StringComparison.OrdinalIgnoreCase):
                        CmdClearGrades(student);
                        break;
                    case string c when c.Equals("EXIT", StringComparison.OrdinalIgnoreCase):
                        continueLoop = false;
                        break;
                    case string c when c.Equals("HELP", StringComparison.OrdinalIgnoreCase):
                        CmdHelp();
                        break;
                    case string c when c.Equals("LISTGRADES", StringComparison.OrdinalIgnoreCase):
                        CmdListGrades(student);
                        break;
                    case string c when c.Equals("LISTSTUDENTS", StringComparison.OrdinalIgnoreCase):
                        CmdListStudents(student);
                        break;
                    case string c when c.StartsWith("SELECTSTUDENT", StringComparison.OrdinalIgnoreCase):
                        StudentInFile newStudent;
                        if(CmdSelectStudent(command, out newStudent))
                        {
                            student = newStudent;
                            student.GradeAdded += OnGradeAdded;
                            student.GradesCleared += OnGradesCleared;
                        }
                        break;
                    case string c when c.Equals("STATS", StringComparison.OrdinalIgnoreCase):
                        CmdStats(student);
                        break;
                    default:
                        Console.WriteLine("Invalid command. Type HELP to see list of available commands.");
                        break;
                }
            }
        }

        static void CmdAddGrade(string command, IStudent student)
        {
            if(student == null)
            {
                Console.WriteLine("First select a student please.");
                return;
            }

            var inData = command.Remove(0, "ADDGRADE".Length).Trim();

            if(String.IsNullOrWhiteSpace(inData))
            {   
                Console.WriteLine("Invalid argument. Example usage: ADDGRADE 5+");
                return;
            }

            try
            {
                student.AddGrade(inData);
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Invalid grade. Allowed values: [1-6] with optional '+' suffix. Try again.");
            }
        }

        static void CmdAddGrades(IStudent student)
        {
            if(student == null)
            {
                Console.WriteLine("First select a student please.");
                return;
            }

            Console.WriteLine($"Adding grades for student {student.Name}. Confirm each grade with [RETURN]. Type q and confirm when done.\n");
            
            while(true)
            {
                var inData = Console.ReadLine();

                if(inData != null && inData.Equals("q"))
                {
                    break;
                }

                try
                {
                    student.AddGrade(inData);
                }
                catch(ArgumentException)
                {
                    Console.WriteLine("\nInvalid grade. Allowed values: [1-6] with optional '+' suffix. Try again.\n");
                }
            }
        }

        static void CmdAddStudent(string command)
        {
            var inData = command.Remove(0, "ADDSTUDENT".Length).Trim();

            if(String.IsNullOrWhiteSpace(inData))
            {   
                Console.WriteLine("Invalid argument. Example usage: ADDSTUDENT Anton");
                return;
            }

            var existingStudents = StudentInFile.ListStudents();

            foreach (string es in existingStudents)
            {
                if(es.Equals(inData, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Student {es} already exists. Cannot add a student with the same name.");
                    return;
                }
            }
            
            try
            {
                var newStudent = new StudentInFile(inData);
                newStudent.CreateStudentEntry();
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Invalid student's name. Only letters allowed. Try again.");
            }
        }

        static void CmdClearGrades(IStudent student)
        {
            if(student == null)
            {
                Console.WriteLine("First select a student please.");
                return;
            }

            Console.Write("Confirm clearing all grades of student {student.Name}? [y/N]: ");
            var inData = Console.ReadLine();

            if(inData != null && inData.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                student.ClearGrades();
            }
        }

        static void CmdDelete(StudentInFile student)
        {
            if(student == null)
            {
                Console.WriteLine("First select a student please.");
                return;
            }

            Console.Write("Confirm deleting student {student.Name}? [y/N]: ");
            var inData = Console.ReadLine();

            if(inData != null && inData.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                student.DeleteStudentData();
            }
        }

        static void CmdHelp()
        {
            Console.WriteLine($"ChallangeApp v{ver}. List of available commands:\n");
            Console.WriteLine("ADDGRADE grade     - Add a grade for the selected student");
            Console.WriteLine("ADDGRADES          - Add grades in a loop for the selected student");
            Console.WriteLine("ADDSTUDENT name    - Add a student");
            Console.WriteLine("CLEARGRADES        - Clear all grades of the selected student");
            Console.WriteLine("DELETE             - Delete the selected student");
            Console.WriteLine("EXIT               - Exit this program");
            Console.WriteLine("HELP               - Display a list of available commands");
            Console.WriteLine("LISTGRADES         - Display all grades of the selected student");
            Console.WriteLine("LISTSTUDENTS       - Display all available students");
            Console.WriteLine("SELECTSTUDENT name - Select a student");
            Console.WriteLine("STATS              - Display grades' statistics of the selected student");
        }

        static void CmdListGrades(IStudent student)
        {
            if(student == null)
            {
                Console.WriteLine("First select a student please.");
                return;
            }

            var grades = student.GetGrades();

            if(grades.Count == 0)
            {
                Console.WriteLine($"Student {student.Name} has no grades yet.");
            }
            else
            {
                Console.WriteLine($"Grades of student {student.Name}:\n{string.Join("\n", grades)}");
            }
        }

        static void CmdListStudents(StudentInFile student)
        {
            var existingStudents = StudentInFile.ListStudents();
            
            if(existingStudents.Count == 0)
            {
                Console.WriteLine("There are no students yet.");
            }
            else
            {
                Console.WriteLine("List of available students:\n");
                foreach (string es in existingStudents)
                {
                    if(student != null && es.Equals(student.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"{es} (*)");
                    }
                    else
                    {
                        Console.WriteLine($"{es}");
                    }
                }
            }
        }

        static bool CmdSelectStudent(string command, out StudentInFile student)
        {
            var inData = command.Remove(0, "SELECTSTUDENT".Length).Trim();
            var existingStudents = StudentInFile.ListStudents();
            var isFound = false;

            student = null;

            if(String.IsNullOrWhiteSpace(inData))
            {   
                Console.WriteLine("Invalid argument. Example usage: SELECTSTUDENT Anton");
                return false;
            }

            foreach (var es in existingStudents)
            {
                if(es.Equals(inData, StringComparison.OrdinalIgnoreCase))
                {
                    isFound = true;
                    break;
                }
            }

            if(!isFound)
            {
                Console.WriteLine($"Student {inData} does not exist");
                return false;
            }

            try
            {
                student = new StudentInFile(inData);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid name. Try again.");
            }

            return isFound;
        }

        static void CmdStats(IStudent student)
        {
            if(student == null)
            {
                Console.WriteLine("First select a student please.");
                return;
            }

            var stats = student.GetStatistics();

            if(stats.Count == 0)
            {
                Console.WriteLine($"Student {student.Name} has no grades yet.");
            }
            else
            {
                string letterGrade = stats.LetterGrade;
                
                if(letterGrade == null)
                {
                    letterGrade = "-";
                }

                Console.WriteLine($"Statistics for student {student.Name}:\nCount: {stats.Count}\nMin: {stats.Minimum}\nMax: {stats.Maximum}\nMean: {Math.Round(stats.Average, 2)}\nLetter grade: {letterGrade}");
            }
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
    }
}