namespace ChallengeApp
{
    public delegate void GradeAddedDelegate(object sender, GradeAddedEventArgs args);
    public delegate void GradesClearedDelegate(object sender, GradesClearedEventArgs args);
    
    public abstract class StudentBase : Human, IStudent
    {
        public StudentBase(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract event GradesClearedDelegate GradesCleared;
        public abstract void AddGrade(double grade);
        public abstract void AddGrade(string grade);
        public abstract void ClearGrades();
        public abstract Statistics GetStatistics();
    }
}