using Exo_GestionPaiements.Paiements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_GestionPaiements
{
    public class IHM
    {
        public void Demarrer()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // pour afficher €

            Console.WriteLine("=== Gestion des Paiements ===");

            // Carte créée UNE SEULE FOIS → solde conservé
            CarteDeCredit carte = new CarteDeCredit(
                "1234 5678 9012 3456",
                "Jean Dupont",
                "12/26",
                "123",
                1000.0
            );

            // Instance de la classe Export.cs
            DuplicationEtConcat exportateur = new DuplicationEtConcat();

            while (true) // boucle pour permettre plusieurs paiements
            {
                Console.WriteLine("\n1 - Paiement par Carte de Crédit");
                Console.WriteLine("2 - Paiement via PayPal");
                Console.WriteLine("3 - Exporter un fichier");
                Console.WriteLine("4 - Importer un fichier");
                Console.WriteLine("0 - Quitter");
                Console.Write("Choisissez un mode : ");

                if (!int.TryParse(Console.ReadLine(), out int choix))
                {

                    Console.WriteLine("❌ Entrée invalide !");
                    continue;
                }

                if (choix == 0) break;

                if (choix == 3)
                {
                    exportateur.Export();
                    continue; // retourne au menu
                }

                if (choix == 4)
                {

                    exportateur.Import();
                    continue; // retourne au menu
                }

                Console.Write("\n=== Entrez le montant à payer : ");
                if (!double.TryParse(Console.ReadLine(), out double montant))
                {
                    Console.WriteLine("❌ Montant invalide !");
                    continue;
                }

                IPaiement paiement;

                if (choix == 1)
                {
                    Console.WriteLine($"💳 Solde disponible : {carte.SoldeDisponible}€");
                    paiement = carte; // réutilisation de la carte existante
                }
                else if (choix == 2)
                {
                    Console.Write("Email PayPal : ");
                    string email = Console.ReadLine();

                    Console.Write("Mot de passe : ");
                    string motDePasse = Console.ReadLine();

                    paiement = new PayPal(email, motDePasse);
                }
                else
                {
                    Console.WriteLine("❌ Choix invalide.");
                    continue;
                }

                // 🔴 Effectuer le paiement
                string resultat = paiement.EffectuerPaiement(montant);
                Console.WriteLine(resultat);
            }
        }
    }
}
