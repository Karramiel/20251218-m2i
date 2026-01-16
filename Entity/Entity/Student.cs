using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoEfCore.Models
{
    internal class Student
    {
        //[Key]
        public int Id { get; set; }


        [Required]
        [StringLength(100)]
        public string LastName { get; set; }


        [StringLength(100)]
        public string FirstName { get; set; }

        public int ClasseNumber { get; set; }

        public DateTime DiplomeDate { get; set; }

        public Student(string lastName, string firstName, int classeNumber, DateTime diplomeDate)
        {
            LastName = lastName;
            FirstName = firstName;
            ClasseNumber = classeNumber;
            DiplomeDate = diplomeDate;
        }

        public Student()
        {
        }

        public override string ToString()
        {
            return $"Student N°{Id} : {LastName} {FirstName}, numero de classe : {ClasseNumber}, Date de diplome : {DiplomeDate}";
        }

    }
}
