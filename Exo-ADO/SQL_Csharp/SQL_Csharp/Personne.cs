using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_Csharp
{
    internal class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int  Age { get; set; }
        public string Email { get; set; }  

        public Personne() 
        {
            
        }
        
        public Personne(int id, string nom, string prenom, int age, string email)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Age = age;
            Email = email;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nom: {Nom}, Prénom: {Prenom}, Âge: {Age}, Email: {Email}";
        } 


    }
}
