using System;

namespace Exo_Heritage
{
    class Program
    {
        static void Main()
        {
            GestionEmployes gestion = new GestionEmployes();
            int choix;

            do
            {
                Console.Clear();
                Console.WriteLine("Gestion des employes =====\n");
                Console.WriteLine("1 -- Ajouter un employe");
                Console.WriteLine("2 -- Afficher le salaire des employés");
                Console.WriteLine("3 -- Rechercher un employe");
                Console.WriteLine("0 -- Quitter\n");
                Console.Write("Entrez votre choix : ");

                choix = int.Parse(Console.ReadLine());

                switch (choix)
                {
                    case 1:
                        gestion.AjouterEmploye();
                        break;
                    case 2:
                        gestion.AfficherSalaires();
                        break;
                    case 3:
                        gestion.RechercherEmploye();
                        break;
                }

                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();

            } while (choix != 0);
        }
    }
}
