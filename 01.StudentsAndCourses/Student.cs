namespace _01.StudentsAndCourses
{
    using System;

    public class Student : IComparable<Student>
    {
        public Student(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public int CompareTo(Student other)
        {
            var comparisonResult = this.LastName.CompareTo(other.LastName);
            if (comparisonResult == 0)
            {
                return this.FirstName.CompareTo(other.FirstName);
            }

            return comparisonResult;
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}