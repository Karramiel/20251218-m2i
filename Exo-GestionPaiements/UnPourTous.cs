// ===============================
// Fichier : arbo.cs
// ===============================



// ===============================
// Fichier : IHM.cs
// ===============================

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


// ===============================
// Fichier : import-export.cs
// ===============================

using System;
using System.IO;
using System.Text;
namespace Exo_GestionPaiements
{
    public class DuplicationEtConcat
    {
        public void Export()
        {
            string dossierProjet = Directory.GetParent(
                Directory.GetCurrentDirectory()
            ).Parent.Parent.FullName;

            string dossierDestination = Directory.GetParent(dossierProjet).FullName;
            string fichierConcatene = Path.Combine(dossierDestination, "UnPourTous.cs");

            Console.WriteLine("Dossier source : " + dossierProjet);
            Console.WriteLine("Dossier destination : " + dossierDestination);
            Console.WriteLine("Fichier concaténé : " + fichierConcatene);

            using (StreamWriter sw = new StreamWriter(fichierConcatene, false, Encoding.UTF8))
            {
                foreach (var fichier in Directory.GetFiles(dossierProjet, "*.cs", SearchOption.AllDirectories))
                {
                    if (fichier.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") ||
                        fichier.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
                        continue;

                    sw.WriteLine("// ===============================");
                    sw.WriteLine("// Fichier : " + Path.GetFileName(fichier));
                    sw.WriteLine("// ===============================");
                    sw.WriteLine();

                    sw.WriteLine(File.ReadAllText(fichier));
                    sw.WriteLine();
                }
            }

            Console.WriteLine("✅ Concaténation terminée !");
        }

        public void Import()
        {
            string fichierImport = @"C:\Users\Administrateur\Exercices\Exo C#\Exo-GestionPaiements\UnPourTous.cs";
            string dossierDestination = @"C:\Users\Administrateur\Exercices\Exo C#\Test";

            Directory.CreateDirectory(dossierDestination);

            string[] lignes = File.ReadAllLines(fichierImport, Encoding.UTF8);

            StringBuilder contenuCourant = null;
            string nomFichierCourant = null;

            foreach (string ligne in lignes)
            {
                if (ligne.StartsWith("// Fichier :"))
                {
                    if (contenuCourant != null && nomFichierCourant != null)
                    {
                        File.WriteAllText(
                            Path.Combine(dossierDestination, nomFichierCourant),
                            contenuCourant.ToString(),
                            Encoding.UTF8
                        );
                    }

                    nomFichierCourant = ligne.Replace("// Fichier :", "").Trim();
                    contenuCourant = new StringBuilder();
                    continue;
                }

                if (ligne.StartsWith("// ==============================="))
                    continue;

                if (contenuCourant != null)
                    contenuCourant.AppendLine(ligne);
            }

            if (contenuCourant != null && nomFichierCourant != null)
            {
                File.WriteAllText(
                    Path.Combine(dossierDestination, nomFichierCourant),
                    contenuCourant.ToString(),
                    Encoding.UTF8
                );
            }

            Console.WriteLine("✅ Import et division terminés !");
        }
    }
}

// ===============================
// Fichier : Program.cs
// ===============================

using Exo_GestionPaiements;
using System.Text;

namespace GestionPaiements
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IHM ihm = new IHM();
            ihm.Demarrer();
        }
    }
}

// ===============================
// Fichier : CarteDeCredit.cs
// ===============================

using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_GestionPaiements.Paiements
{
    public class CarteDeCredit : IPaiement
    {
        public string NumeroCarte { get; set; }
        public string Titulaire { get; set; }
        public string DateExpiration { get; set; }
        public string Cvv { get; set; }

        public double SoldeDisponible { get; private set; }

        public CarteDeCredit(string numeroCarte, 
                                string titulaire, 
                                string dateExpiration, 
                                string cvv,
                                double soldeInitial = 1000.0)
        {
            NumeroCarte = numeroCarte;
            Titulaire = titulaire;
            DateExpiration = dateExpiration;
            Cvv = cvv;
            SoldeDisponible = soldeInitial;


        }

        public string EffectuerPaiement(double montant)
        {
            if (montant <= 0)
            {
                return "❌ Paiement par carte refusé : montant invalide.";
            }
            
            if (montant > SoldeDisponible)
            {
                return $"❌ Paiement refusé : solde insuffisant ({SoldeDisponible}€ disponibles).";
            }

            SoldeDisponible -= montant;

            return $"✅ Paiement de {montant}€ effectué avec succès par carte de crédit.\n" +
                $"💳 Il vous reste {SoldeDisponible}€ sur votre compte.";
        }
    }
}


// ===============================
// Fichier : IPaiments.cs
// ===============================

using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_GestionPaiements.Paiements
{
    public interface IPaiement
    {
        string EffectuerPaiement(double montant);
    }
}


// ===============================
// Fichier : PayPal.cs
// ===============================

using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_GestionPaiements.Paiements
{
    public class PayPal : IPaiement
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }

        public PayPal(string email, string motDePasse)
        {
            Email = email;
            MotDePasse = motDePasse;
        }

        public string EffectuerPaiement(double montant)
        {
            if (montant <= 0)
            {
                return "❌ Paiement PayPal refusé : montant invalide.";
            }

            return $"✅ Paiement de {montant}€ effectué avec succès via PayPal.";
        }
    }
}


