namespace _01.StudentsAndCourses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using _00.ExtensionMethods;
    using Wintellect.PowerCollections;

    public static class StudentsAndCourses
    {
        private const string StudentsListPath = @"../../students.txt";

        private static readonly OrderedDictionary<string, SortedSet<Student>> StudentDb =
              new OrderedDictionary<string, SortedSet<Student>>();

        public static void Main()
        {
            using (var reader = new StreamReader(StudentsListPath, Encoding.UTF8))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var tokens = line
                        .Split(new[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                    var firstName = tokens[0];
                    var lastName = tokens[1];
                    var courseName = tokens[2];
                   
                    var student = new Student(firstName, lastName);
                    StudentDb.AddValueToKey(courseName, student);

                    line = reader.ReadLine();
                }
                
                foreach (var keyValue in StudentDb)
                {
                    Console.WriteLine($"{keyValue.Key}: {string.Join(", ", keyValue.Value)}");
                }
            }
        }
    }
}
