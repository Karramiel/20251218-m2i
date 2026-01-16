using Exo_efcore_liste_contact;
using Org.BouncyCastle.Bcpg;


using var db = new AppDbContext();
db.Database.EnsureCreated();

bool continuer = true;

while (continuer)
{
    Console.Clear();
    Console.WriteLine("=== REPERTOIRE CONTACTS (MySQL) ===");
    Console.WriteLine("1. Afficher | 2. Ajouter | 3. Modifier | 4. Supprimer | 5. Dossier Spécial | 0. Quitter");
    var choix = Console.ReadLine();

    switch (choix)
    {
        case "1": // LISTER
            ContactRepository.ListerLesContacts(db);
            break;

        case "2": // AJOUTER
            ContactRepository.MenuContact(db);
            break;

        case "3": // MODIFIER
            ContactRepository.ModifierUnContact(db);
            break;

        case "4": // SUPPRIMER
            ContactRepository.AlerteRouge();
            ContactRepository.SupprimerUnContact(db);
            break;

            case "5": // DOSSIER SPÉCIAL
            Console.Clear();
            ContactRepository.DossierSpecial(db);
            break;

        case "0": return;
    }
}



