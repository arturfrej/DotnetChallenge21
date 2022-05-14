namespace ChallengeApp
{
    public delegate void GradeAddedDelegate(object sender, GradeAddedEventArgs args);
    
    public abstract class StudentBase : Human, IStudent
    {
        public StudentBase(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);
        public abstract void ClearGrades();
        public abstract Statistics GetStatistics();
    }
}