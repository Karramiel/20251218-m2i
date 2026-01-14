namespace GestionConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quitter = false;

            while (!quitter)
            {
                // 🔹 Affichage du menu
                Console.WriteLine("=== MENU PRINCIPAL ===");
                Console.WriteLine("1 - Exercice 01 : Conversion d'une chaîne en entier");
                Console.WriteLine("2 - Exercice 02 : Calcul de la racine carrée d'un entier positif");
                Console.WriteLine("3 - Exercice 03 : Accès à un élément inexistant dans un tableau");
                Console.WriteLine("4 - Exercice 04 : Création d'une exception personnalisée");
                Console.WriteLine("0 - Quitter");
                Console.Write("Choisissez une option : ");

                string choix = Console.ReadLine()?.Trim();

                Console.WriteLine(); // ligne vide pour lisibilité

                switch (choix)
                {
                    case "1":
                        ExerciceConversion();
                        break;
                    case "2":
                        ExerciceRacineCarree();
                        break;
                    case "3":
                        ExerciceTableau();
                        break;
                    case "4":
                        ExerciceEtudiants();
                        break;
                    case "0":
                        quitter = true;
                        Console.WriteLine("Au revoir !");
                        break;
                    default:
                        Console.WriteLine("❌ Option invalide, réessayez !");
                        break;
                }

                Console.WriteLine(); // séparation entre les exécutions
            }
        }
        // ============= Exercice 1 =============
        static void ExerciceConversion()
        {
            Console.WriteLine("--- Conversion d'une chaîne en entier ---");

            while (true)
            {

                try
                {
                    Console.Write("Veuillez saisir un entier : ");
                    int input = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Vous avez saisi l'entier : {input}");
                    Console.WriteLine("--- Fin de l'exercice 1 ---");
                    return; // Sortie de la méthode si tout est OK

                }
                catch (FormatException) // Erreur de format
                {
                    Console.WriteLine("Erreur : ce n'est pas un entier. Réessayez !");
                }
                catch (OverflowException) // Entier trop grand ou trop petit
                {
                    Console.WriteLine("Erreur : l'entier est trop grand ou trop petit. Réessayez !");
                }
            }

        }

       
        // ============= Exercice 2 =============
        static void ExerciceRacineCarree()
        {
            Console.WriteLine("--- Calcul de la racine carrée ---");

            {
                double resultat = LireRacineCarree();
                Console.WriteLine($"✅ La racine carrée est : {resultat}");
              
            }
            Console.WriteLine("--- Fin de l'exercice 2 ---");

        }

        static double LireRacineCarree()
        {
            while (true)
            {
                Console.Write("Veuillez saisir un entier positif : ");
                string input = Console.ReadLine();

                try
                {
                    int nombre = int.Parse(input);

                    if (nombre < 0)
                        throw new ArgumentOutOfRangeException(nameof(nombre), "Le nombre doit être positif !");

                    return Math.Sqrt(nombre);
                }
                catch (FormatException)
                {
                    Console.WriteLine("❌ Erreur : ce n'est pas un nombre entier valide. Réessayez !");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("❌ Erreur : le nombre est trop grand ou trop petit. Réessayez !");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("❌ Erreur : " + ex.Message);
                }
            } 

        }

        // ============= Exercice 3 =============
        static void ExerciceTableau()
        {
            Console.WriteLine("--- Accès à un élément inexistant dans un tableau ---");

            int[] tableau = { 10, 20, 30, 40, 50 }; // tableau de taille 5

            Console.WriteLine("Entrée l'élément à accéder dans le tableau (index 0 à 4) :");
            int index = int.Parse(Console.ReadLine());

            try
            {
                if(index < 0 || index >= tableau.Length) throw new IndexOutOfRangeException("Index hors des limites du tableau.");
                Console.WriteLine($"Valeur : {index} / {tableau[index]}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Erreur : tentative d'accès à un élément inexistant !");
                Console.WriteLine("Détails : " + ex.Message);
            }

            Console.WriteLine("--- Fin de l'exercice 3 ---");
        }

        // ============= Exercice 4 =============

        public class InvalidAgeException : Exception
        {
            public InvalidAgeException(string message) : base(message) { }
        }

        // ----------------- Classe Student -----------------
        internal class Student
        {
            public string Name { get; set; }
            private int _age;
            public int Age
            {
                get => _age; 
                set
                {
                    if (value <= 0)
                        throw new InvalidAgeException("L'âge ne peut pas être négatif ou nul !");
                    _age = value;
                }
            }

            public Student(string name, int age)
            {
                Name = name;
                Age = age; // Utilise le setter qui lance l'exception si besoin
            }



            public override string ToString()
            {
                return $"Nom : { 
                    Name}, Âge : {Age}";
            }
        }

        // ----------------- Programme principal -----------------
        static void ExerciceEtudiants()
            {
                List<Student> listeEtudiants = new List<Student>();
                    bool quitter = false;

                while (!quitter)
                {
                    Console.WriteLine("=== Gestion des étudiants ===");
                    Console.WriteLine("1 - Ajouter un étudiant");
                    Console.WriteLine("2 - Afficher la liste des étudiants");
                    Console.WriteLine("0 - Quitter");
                    Console.Write("Choisissez une option : ");

                    string choix = Console.ReadLine()?.Trim();
                    Console.WriteLine();

                    switch (choix)
                    {
                        case "1":
                            AjouterEtudiant(listeEtudiants);
                            break;
                        case "2":
                            AfficherEtudiants(listeEtudiants);
                            break;
                        case "0":
                            quitter = true;
                            Console.WriteLine("--- Fin de l'exercice 4 ---");
                            break;
                        default:
                            Console.WriteLine("Option invalide, réessayez !");
                            break;
                    }

                    Console.WriteLine();
                }
            }

            // ----------------- Ajouter un étudiant -----------------
            static void AjouterEtudiant(List<Student> liste)
            {
                Console.Write("Nom de l'étudiant : ");
                string nom = Console.ReadLine()?.Trim();

                int age;
                while (true)
                {
                    Console.Write("Âge de l'étudiant : ");
                    string input = Console.ReadLine()?.Trim();

                    try
                    {
                        age = int.Parse(input);

                        // Tentative de création de l'étudiant
                        Student s = new Student(nom, age);
                        liste.Add(s);
                        Console.WriteLine("Étudiant ajouté avec succès !");
                        break; // sortie de la boucle si tout est OK
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Erreur : vous devez saisir un nombre entier pour l'âge.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Erreur : l'âge est trop grand ou trop petit.");
                    }
                    catch (InvalidAgeException ex)
                    {
                        Console.WriteLine("Erreur : " + ex.Message);
                    }
                }
            }

            // ----------------- Afficher les étudiants -----------------
            static void AfficherEtudiants(List<Student> liste)
            {
                if (liste.Count == 0)
                {
                    Console.WriteLine("La liste des étudiants est vide.");
                    return;
                }

                Console.WriteLine("--- Liste des étudiants ---");
                foreach (var etudiant in liste)
                {
                    Console.WriteLine(etudiant);
                }
                Console.WriteLine("--- Fin de la liste ---");
            }
    }
}