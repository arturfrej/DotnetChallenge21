using System;
using Xunit;
using ChallengeApp;

namespace Challenge.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CheckStatistics()
        {
            var student = new Student("Krzychu");

            student.AddGrade(1);
            student.AddGrade(2);
            student.AddGrade(6);
            student.AddGrade(3.5);

            var stat = student.GetStatistics();

            Assert.Equal(1, stat.Minimum);
            Assert.Equal(6, stat.Maximum);
            Assert.Equal(3.12, stat.Average, 2);
        }

        [Fact]
        public void ReferenceChecks()
        {
            var student1 = new Student("Marek");
            var student2 = new Student("Jarek");
            var student3 = new Student("Jarek");
            var student3a = student3;


            Assert.Same(student3, student3a);
            Assert.NotSame(student1, student2);
            Assert.NotSame(student2, student3);
            Assert.False(Object.ReferenceEquals(student2, student3));
            Assert.True(Object.ReferenceEquals(student3, student3a));
            Assert.False(student2.Equals(student3));
            Assert.True(student3.Equals(student3a));
        }
    }
}
