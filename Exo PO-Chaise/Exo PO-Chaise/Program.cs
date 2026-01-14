using System; // Nécessaire pour Console
using System.Collections.Generic; // Nécessaire pour List<Chaise>

class Program // Point d'entrée du programme
{
    static void Main() // Méthode principale
    {
        List<Chaise> chaises = new List<Chaise> //  Création d'une liste de chaises
        {
            new Chaise(),
            new Chaise(3, "Métal", "Noir"),
            new Chaise(4, "Plastique", "Blanc")
        };

        foreach (Chaise chaise in chaises) // Parcours de chaque chaise dans la liste
        {
            Console.WriteLine(chaise); // Appelle automatiquement ToString() // Affiche les informations de la chaise
        }
    }
}