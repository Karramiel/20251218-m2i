using System;

class Program
{
    static void Main(string[] args)
    {
        // Création de salariés
        Salarie s1 = new Salarie(1, "Informatique", "Cadre", "Dupont", 2500);
        Salarie s2 = new Salarie(2, "RH", "Employé", "Martin", 1800);
        Salarie s3 = new Salarie(3, "Finance", "Cadre", "Durand", 3000);

        // Affichage individuel
        Console.WriteLine(s1.AfficherSalaire());
        Console.WriteLine(s2.AfficherSalaire());
        Console.WriteLine(s3.AfficherSalaire());

        Console.WriteLine("\n--- Statistiques entreprise ---");
        Console.WriteLine($"Nombre d'employés : {Salarie.NombreEmployes}");
        Console.WriteLine($"Salaire total : {Salarie.SalaireTotal} €");

        // Remise à zéro
        Console.WriteLine("\nRemise à zéro des statistiques...");
        Salarie.ResetStatistiques();

        Console.WriteLine($"Nombre d'employés : {Salarie.NombreEmployes}");
        Console.WriteLine($"Salaire total : {Salarie.SalaireTotal} €");
    }
}


