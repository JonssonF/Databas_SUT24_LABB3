using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FREDRIK_JONSSON_SUT24_LABB3.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateOnly? DoB { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public static void GetStudent()
    {
        using (var context = new LabbSchoolContext())
        {
            var allStudents = context.Students;

            foreach (var student in allStudents)
            {
                Console.WriteLine($"{student.FirstName} + {student.LastName}");
            }
        }
    }
}
