using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Exo_efcore_liste_contact
{
    public static class ContactRepository
    {
        // On passe le DbContext en paramètre pour que le Repo puisse l'utiliser
        public static void ListerLesContacts(AppDbContext db, int idMisEnEvidence = -1)
        {
            List<Exo_efcore_liste_contact.Contact> contacts = db.Set<Exo_efcore_liste_contact.Contact>().ToList(); string gras = "\x1b[1m";
            string normal = "\x1b[0m";

            Console.WriteLine("ID | Nom        | Prénom    | Date Naissance | Age | Email");
            Console.WriteLine("----------------------------------------------------------");
            foreach (var c in contacts)
            {
                bool estCible = (c.Id == idMisEnEvidence);
                string prefixe = estCible ? gras : "";
                string suffixe = estCible ? normal : "";
                Console.WriteLine($"{prefixe}{c.Id,2} | {c.Nom,-9} | {c.Prenom,-9} | {c.DateNaissance:dd/MM/yyyy}   | {c.Age,3} | {c.Email}{suffixe}");
            }
            Console.WriteLine("\n----------------------------------------------------------");
            Console.ReadKey();
        }

        public static void AlerteRouge()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.BackgroundColor = (i % 2 == 0) ? ConsoleColor.Red : ConsoleColor.Black;
                Console.Clear();
                Console.WriteLine("\n\n\n\t\t\t⚠️ ALERTE SYSTÈME ⚠️");
                Thread.Sleep(150);
            }
            Console.ResetColor();
            Console.Clear();
        }

        public static void AfficherNomStyleHack(AppDbContext db, string texte)
        {
            Random rand = new Random();
            char[] lettres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*".ToCharArray();
            foreach (char c in texte)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(lettres[rand.Next(lettres.Length)]);
                    Thread.Sleep(15);
                    Console.Write("\b");
                }
                Console.Write(c);
            }
            Console.WriteLine();
        }

        public static void MenuContact(AppDbContext db)
        {
            Console.Clear();
            Console.WriteLine("--- MODE AJOUT ---");
            Console.WriteLine("1. Saisie Manuelle");
            Console.WriteLine("2. Génération Aléatoire (Bot)");
            Console.Write("\nVotre choix : ");
            string choix = Console.ReadLine()!;

            if (choix == "1")
            {
                AjouterUnContact(db);
            }
            else if (choix == "2")
            {
                GenererContactAleatoire(db);
            }
        }
        public static void AjouterUnContact(AppDbContext db)
        {

            Console.Write("Nom : ");
            string n = Console.ReadLine()!;

            Console.Write("Prénom : ");
            string p = Console.ReadLine()!;

            Console.Write("Date Naissance (JJMMAAAA) : ");
            string saisie = Console.ReadLine()!;
            DateTime d; // On déclare la variable à l'extérieur de la boucle

            // On boucle TANT QUE ( !) la saisie ne correspond pas au format
            while (!DateTime.TryParseExact(Console.ReadLine(), "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out d))
            {
                Console.Write("\x1b[31mFormat invalide !\x1b[0m Merci de taper JJMMAAAA (ex: 01051990) : ");
            }
            var nouveauContact = new Contact
            {
                Nom = n,
                Prenom = p,
                DateNaissance = d,
                Email = $"{n[0]}.{p}@mail.com".ToLower(),
                Genre = "M",
                Telephone = "06..."
            };

            db.Set<Contact>().Add(nouveauContact);

            try
            {
                db.SaveChanges();
                //ListerLesContacts(nouveauContact.Id);
                Console.WriteLine("Contact Ajouté.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }


        }

        public static void GenererContactAleatoire(AppDbContext db)
        {
            Console.Write("Combien de contacts voulez-vous générer ? ");
            if (!int.TryParse(Console.ReadLine(), out int nombre) || nombre <= 0)
            {
                Console.WriteLine("Nombre invalide, opération annulée.");
                return;
            }

            Random rand = new Random();
            string[] noms = { "Vador", "Skywalker", "Kenobi", "Solo", "Fett", "Yoda", "Palpatine", "Windu" };
            string[] prenoms = { "Dark", "Luke", "Obi-Wan", "Han", "Boba", "Master", "Sheev", "Mace" };

            int dernierIdCree = -1;

            for (int i = 0; i < nombre; i++)
            {
                string n = noms[rand.Next(noms.Length)];
                string p = prenoms[rand.Next(prenoms.Length)];

                DateTime dateDebut = new DateTime(1970, 1, 1);
                int range = (DateTime.Today - dateDebut).Days;
                DateTime d = dateDebut.AddDays(rand.Next(range));

                var botContact = new Contact
                {
                    Nom = n,
                    Prenom = p,
                    DateNaissance = d,
                    Email = $"{n[0]}.{p}{rand.Next(10, 99)}@empire.com".ToLower(), // Ajout d'un chiffre pour éviter les doublons d'emails
                    Genre = rand.Next(2) == 0 ? "M" : "F", // Aléatoire entre M et F
                    Telephone = "06" + rand.Next(10000000, 99999999)
                };

                db.Set<Contact>().Add(botContact);
                dernierIdCree = botContact.Id; // On mémorise l'ID du dernier créé pour la mise en gras

                // Petit feedback visuel pour chaque création
                Console.Write($"\rGénération du clone {i + 1}/{nombre}...");
                Thread.Sleep(50);
            }

            db.SaveChanges(); // On sauvegarde tout d'un coup pour être plus rapide !

            Console.WriteLine($"\n\x1b[32m[ARMÉE] {nombre} contacts ont été déployés dans la base MySQL !\x1b[0m");
            Thread.Sleep(1000);

            // On affiche la liste en mettant en évidence le dernier ajouté
            ListerLesContacts(db, dernierIdCree);
        }


        public static void ModifierUnContact(AppDbContext db)
        {
            ListerLesContacts(db);
            Console.Write("\n\x1b[1mID à modifier :\x1b[0m ");
            string saisieId = Console.ReadLine()!;

            if (!int.TryParse(saisieId, out int idModif))
            {
                Console.WriteLine("\x1b[31mErreur : ID invalide.\x1b[0m");
                Console.ReadKey();
                return;
            }

            var contact = db.Set<Contact>().Find(idModif);
            if (contact == null)
            {
                Console.WriteLine("\x1b[31mContact introuvable.\x1b[0m");
                Console.ReadKey();
                return;
            }

            // --- SOUS-MENU DE MODIFICATION ---
            Console.Clear();
            Console.WriteLine($"--- MODIFICATION DE : {contact.Prenom} {contact.Nom} ---");
            Console.WriteLine("1. Modifier le Nom");
            Console.WriteLine("2. Modifier le Prénom");
            Console.WriteLine("3. Modifier la Date de Naissance");
            Console.WriteLine("4. Annuler");
            Console.Write("\nVotre choix : ");

            string choix = Console.ReadLine()!;

            switch (choix)
            {
                case "1":
                    Console.Write($"Nouveau nom [{contact.Nom}] : ");
                    contact.Nom = Console.ReadLine()!;
                    break;
                case "2":
                    Console.Write($"Nouveau prénom [{contact.Prenom}] : ");
                    contact.Prenom = Console.ReadLine()!;
                    break;
                case "3":
                    Console.Write("Nouvelle Date (JJMMAAAA) : ");
                    DateTime d;
                    while (!DateTime.TryParseExact(Console.ReadLine(), "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out d))
                    {
                        Console.Write("\x1b[31mFormat invalide. Réessayez : \x1b[0m");
                    }
                    contact.DateNaissance = d;
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Choix invalide.");
                    Thread.Sleep(1000);
                    return;
            }

            // Mise à jour automatique de l'email si le nom/prénom a changé
            contact.Email = $"{contact.Nom[0]}.{contact.Prenom}@mail.com".ToLower();

            db.SaveChanges();

            Console.WriteLine("\x1b[32mModification enregistrée !\x1b[0m");
            Thread.Sleep(1000);

            // On réaffiche la liste avec la mise en gras du contact modifié
            ListerLesContacts(db, contact.Id);
        }




        public static void SupprimerUnContact(AppDbContext db)
        {
            ListerLesContacts(db);
            Console.Write("\n\x1b[1;31mCible à éliminer (ID) : \x1b[0m");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var cible = db.Set<Contact>().Find(id);
            if (cible == null)
            {
                Console.WriteLine("La cible a déjà pris la fuite (ID introuvable).");
                return;
            }

            Console.Clear();
            AfficherNomStyleHack($"--- DOSSIER : {cible.Nom.ToUpper()} {cible.Prenom.ToUpper()} ---");

            Thread.Sleep(800);
            Console.WriteLine(" Connexion au réseau crypté...");
            Thread.Sleep(1000);
            Console.WriteLine(" Cible verrouillée.");
            Thread.Sleep(800);

            Console.Write(" Préparation de la charge SQL");
            for (int i = 0; i < 3; i++) { Thread.Sleep(500); Console.Write("."); }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[ACTION REQUISE] Confirmer l'effacement définitif ? (O/N)");
            string confirm = Console.ReadLine()!.ToUpper();
            Console.ResetColor();

            if (confirm == "O")
            {
                Console.WriteLine("\n Ordre d'élimination envoyé à l'assassin...");
                Thread.Sleep(1000);

                // --- NOUVELLE ANIMATION : LE TIR ---
                Console.Write("\n Positionnement du tireur...");
                Thread.Sleep(1000);
                Console.WriteLine("\n");

                string cibleVisuelle = "(°_°)";
                int distance = 30;

                for (int i = 0; i <= distance; i++)
                {
                    // On construit la ligne : espaces + balle + espaces restants + cible
                    string espaceAvant = new string(' ', i);
                    string espaceApres = new string(' ', distance - i);

                    // \r pour revenir au début de la ligne sans effacer tout l'écran
                    Console.Write($"\r   {espaceAvant}— {espaceApres}{cibleVisuelle}");

                    Thread.Sleep(50); // Vitesse de la balle
                }

                // Impact !
                Console.Write($"\r   {new string(' ', distance + 3)} (X_X) ");
                Console.WriteLine("\n\n   * PAF *");
                Thread.Sleep(1000);

                // Action réelle en BDD
                db.Set<Contact>().Remove(cible);
                db.SaveChanges();

                Console.WriteLine("\r\x1b[1;32m Mission accomplie. Le témoin n'existe plus.\x1b[0m");
                Thread.Sleep(1000);
                Console.WriteLine("Nettoyage des preuves dans la base de données...");
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine("\n Mission avortée. La cible a eu de la chance.");
            }

            Console.WriteLine("\nAppuyez sur une touche pour retourner dans l'ombre...");
            Console.ReadKey();
        }

        public static void AfficherNomStyleHack(string texte)
        {
            Random rand = new Random();
            char[] lettres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*".ToCharArray();
            char[] resultat = new char[texte.Length];

            for (int i = 0; i < texte.Length; i++)
            {
                // On fait clignoter des caractères aléatoires 5 fois avant de fixer la bonne lettre
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(lettres[rand.Next(lettres.Length)]);
                    Thread.Sleep(20);
                    Console.Write("\b"); // Retour arrière pour effacer
                }
                Console.Write(texte[i]); // On affiche la vraie lettre
            }
            Console.WriteLine();
        }


        public static void DossierSpecial(AppDbContext db)
        {
            Console.CursorVisible = false;
            int largeur = 40;
            int hauteur = 10;
            int joueurX = 2, joueurY = 5;
            int coffreX = 35, coffreY = 5;

            int laser1X = 10, dir1 = 1, laser1Y = 1;
            int laser2X = 20, dir2 = -1, laser2Y = 8;
            int laser3X = 30, dir3 = 1, laser3Y = 4;

            Console.Clear();
            Console.WriteLine("--- MISSION : INFILTRATION ---");
            Console.WriteLine("Utilisez les FLÉCHES pour atteindre le coffre [#] sans toucher les lasers |");
            Thread.Sleep(2000);

            bool enJeu = true;

            while (enJeu) // Début de la boucle de jeu
            {
                // 1. DESSIN DU TERRAIN
                Console.SetCursorPosition(0, 3);
                for (int y = 0; y < hauteur; y++)
                {
                    for (int x = 0; x < largeur; x++)
                    {
                        if (x == joueurX && y == joueurY) Console.Write("\x1b[32m*\x1b[0m");
                        else if (x == coffreX && y == coffreY) Console.Write("\x1b[33m[#]\x1b[0m");
                        else if ((x == laser1X && y == laser1Y) || (x == laser2X && y == laser2Y) || (x == laser3X && y == laser3Y))
                            Console.Write("\x1b[31m|\x1b[0m");
                        else if (x == 0 || x == largeur - 1 || y == 0 || y == hauteur - 1) Console.Write("█");
                        else Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                // 2. MOUVEMENT DES LASERS 
                laser1Y += dir1; if (laser1Y <= 1 || laser1Y >= hauteur - 2) dir1 *= -1;
                laser2Y += dir2; if (laser2Y <= 1 || laser2Y >= hauteur - 2) dir2 *= -1;
                laser3Y += dir3; if (laser3Y <= 1 || laser3Y >= hauteur - 2) dir3 *= -1;

                // 3. ENTRÉE CLAVIER
                if (Console.KeyAvailable)
                {
                    var touche = Console.ReadKey(true).Key;
                    if (touche == ConsoleKey.UpArrow && joueurY > 1) joueurY--;
                    if (touche == ConsoleKey.DownArrow && joueurY < hauteur - 2) joueurY++;
                    if (touche == ConsoleKey.LeftArrow && joueurX > 1) joueurX--;
                    if (touche == ConsoleKey.RightArrow && joueurX < largeur - 2) joueurX++;
                }

                // 4. COLLISIONS
                if ((joueurX == laser1X && joueurY == laser1Y) ||
                    (joueurX == laser2X && joueurY == laser2Y) ||
                    (joueurX == laser3X && joueurY == laser3Y))
                {
                    Console.Clear();
                    AlerteRouge();
                    AlerteRouge();
                    Console.WriteLine("\x1b[31m  ALARME DÉCLENCHÉE ! VOUS AVEZ ÉTÉ REPÉRÉ.\x1b[0m");
                    enJeu = false; // Arrête la boucle
                    Console.ReadKey(); // Pause pour voir le message
                }

                // 5. VICTOIRE
                if (joueurX >= coffreX - 1 && joueurY == coffreY)
                {
                    Console.Clear();
                    Console.WriteLine("\x1b[32m ACCÈS ACCORDÉ. DOSSIER DÉVERROUILLÉ.\x1b[0m");
                    enJeu = false;
                    Thread.Sleep(1000);
                    AccesAccorde(db);
                }

                Thread.Sleep(50);
            } // FIN DE LA BOUCLE while (enJeu)

            Console.CursorVisible = true;
        }

        public static void AccesAccorde(AppDbContext db)
        {
            Console.Clear();
            Console.WriteLine("\x1b[32m[SYSTÈME] Accès aux données confidentielles réussi...\x1b[0m\n");

            // On affiche par exemple les 5 derniers contacts ajoutés
            var secrets = db.Set<Contact>().OrderByDescending(c => c.Id).Take(5).ToList();

            Console.WriteLine("=== ARCHIVES SECRETES ===");
            foreach (var c in secrets)
            {
                Console.WriteLine($"[CONFIDENTIEL] {c.Nom} {c.Prenom} - Email: {c.Email}");
                Thread.Sleep(200); // Petit effet de chargement
            }

            Console.WriteLine("\nAppuyez sur une touche pour quitter les archives.");
            Console.ReadKey();
        }
        
    }
}