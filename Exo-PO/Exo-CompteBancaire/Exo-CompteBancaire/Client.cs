using Exo_CompteBancaire;

namespace Exo_CompteBancaire
{
        public class Client
        {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Identifiant { get; set; }
        public string Telephone { get; set; }
        public List<CompteBancaire> Comptes { get; set; }
        public string NomCompte { get; set; } 

        public Client(string nom, string prenom, string identifiant, string telephone)
        {
            Nom = nom;
            Prenom = prenom;
            Identifiant = identifiant;
            Telephone = telephone;
            Comptes = new List<CompteBancaire>();
        }

        public void AjoutCompteClient()
        {
            Console.WriteLine("Création d'un nouveau compte courant.");

            // Demander un libellé ou nom pour le compte
            Console.Write("Nom du compte : ");
            string nomCompte = Console.ReadLine();

            // Créer le compte courant avec ce nom (ici on suppose que CompteCourant peut avoir un nom)
            CompteCourant nouveauCompte = new CompteCourant(this, 0)
            {
                NomCompte = nomCompte // Si tu n’as pas encore de propriété NomCompte, on peut l’ajouter dans CompteBancaire
            };

            // Ajouter le compte au client
            this.Comptes.Add(nouveauCompte);

            Console.WriteLine($"Compte courant '{nomCompte}' créé pour {this.Nom} {this.Prenom}.");

        }

    }
}
