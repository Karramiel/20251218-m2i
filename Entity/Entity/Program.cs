// Package Nugget : 
// - Microsoft.EntityFrameworkCore
// - Microsoft.EntityFrameworkCore.Tools
// - Pomelo.EntityFrameworkCore.MySql


using DemoEfCore.Data;
using DemoEfCore.Models;
using Entity;
using Entity.Data;
using System.Globalization;

void AddStudent()
{
    Console.WriteLine("\n--- Add Student ---");
    Console.Write("LastName : ");
    string lastname = Console.ReadLine();
    Console.Write("FirstName : ");
    string firstname = Console.ReadLine();
    Console.Write("Classe Number : ");
    int classeNumber = int.Parse(Console.ReadLine());
    Console.Write("Diplome Date (dd-MM-yyyyy) : ");
    string diplomeDateStr = Console.ReadLine();

    DateTime date = DateTime.ParseExact(diplomeDateStr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
    //creation de la connexion a la base de donnée
    using (var db = new StudentDbContext())
    {
        Student student = new Student(lastname, firstname, classeNumber, date);

        //ajout de l'etudiant dans la le set liée a la table etudiant
        db.students.Add(student);

        //Sauvegarde des modification effectué dans la base de donnée
        db.SaveChanges();
    }
}

void ShowAllStudent()
{
    using (var db = new StudentDbContext())
    {
        //Recuperation de tout les etudiant present dans la table etudiant au format d'une liste
        List<Student> students = db.students.ToList();

        foreach (Student student in students)
        {
            Console.WriteLine(student);
        }
    }
}

void UpdateStudent()
{
    Console.WriteLine("\n---Update Student ---");
    Console.Write("ID Student : ");
    int id = int.Parse(Console.ReadLine());

    using (var db = new StudentDbContext())
    {
        Student? student = db.students.Find(id);

        if (student == null)
        {
            Console.WriteLine("Not Found");
            return;
        }
        Console.Write("LastName : ");
        string lastname = Console.ReadLine();
        Console.Write("FirstName : ");
        string firstname = Console.ReadLine();
        Console.Write("Classe Number : ");
        int classeNumber = int.Parse(Console.ReadLine());
        Console.Write("Diplome Date (dd-MM-yyyyy) : ");
        string diplomeDateStr = Console.ReadLine();

        DateTime date = DateTime.ParseExact(diplomeDateStr, "dd-MM-yyyyy", CultureInfo.InvariantCulture);

        student.LastName = lastname;
        student.FirstName = firstname;
        student.ClasseNumber = classeNumber;
        student.DiplomeDate = date;

        db.SaveChanges();

    }
}

void DeleteStudent()
{
    Console.WriteLine("\n---Delete Student ---");
    Console.Write("ID Student : ");
    int id = int.Parse(Console.ReadLine());

    using (var db = new StudentDbContext())
    {

        Student? student = db.students.Find(id);

        if (student == null)
        {
            Console.WriteLine("Not Found");
            return;
        }

        db.students.Remove(student);
        db.SaveChanges();
        Console.WriteLine("Student deleted");
    }
}

void GetByID()
{
    Console.WriteLine("\n---Get Student by Id ---");
    Console.Write("ID Student : ");
    int id = int.Parse(Console.ReadLine());

    using (var db = new StudentDbContext())
    {
        Student? student = db.students.Find(id);
        if (student == null)
        {
            Console.WriteLine("Not Found");
        }
        else
        {
            Console.WriteLine(student);
        }
    }
}

void GetStudentByLastName()
{
    Console.WriteLine("--- Get Student By Lastname ---");
    Console.WriteLine("Lastname : ");
    var lastname = Console.ReadLine();

    using (var db = new StudentDbContext())
    {
        List<Student> students = db.students.Where(student => student.LastName.Contains(lastname)).ToList();

        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
}

AddStudent();
ShowAllStudent();
GetByID();
GetStudentByLastName();