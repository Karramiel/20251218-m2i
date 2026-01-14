Console.WriteLine("=== Jeu de Deviner le Nombre ===\n");

// Choix de la difficulté
Console.WriteLine("Choisissez une difficulté :");
Console.WriteLine("1 - Facile (10 vies)");
Console.WriteLine("2 - Moyen (7 vies)");
Console.WriteLine("3 - Difficile (5 vies)");
Console.Write("Votre choix : ");
string choixDifficulte = Console.ReadLine();

int nombreVies;
switch (choixDifficulte)
{
    case "1":
        nombreVies = 10;
        break;
    case "2":
        nombreVies = 7;
        break;
    case "3":
        nombreVies = 5;
        break;
    default:
        Console.WriteLine("Choix invalide, difficulté par défaut (Moyen) sélectionnée.");
        nombreVies = 7;
        break;
}

// Générer un nombre aléatoire entre 0 et 100
Random random = new Random();
int nombreADeviner = random.Next(0, 101); // 0 à 100 inclus
//Console.WriteLine($"=============== Cheat Mode Acivé : {nombreADeviner} ===============");

// Boucle principale du jeu
bool trouve = false;
int proposition = 0;

while (nombreVies > 0 && !trouve)


{
    Console.WriteLine($"Il vous reste {nombreVies} vie(s). Entrez votre nombre : ");
    string saisie = Console.ReadLine();

    // Conversion sécurisée de la saisie en entier
    if (!int.TryParse(saisie, out proposition))
    {
        Console.WriteLine("Erreur : veuillez entrer un nombre valide !");
        continue; // recommence la boucle si invalide
    }

    // Vérifier si le nombre est correct
    if (proposition == nombreADeviner)
    {
        trouve = true;
        Console.WriteLine($"\nBravo ! Vous avez trouvé le nombre {nombreADeviner} !");
        Console.WriteLine($"\nIl vous restait {nombreVies} vie(s).");
    }
    else
    {
        nombreVies--;

        if (proposition < nombreADeviner)
        {
            Console.WriteLine("\nTrop petit ! Vise plus grand !\n\n");
        }
        else
        {
            Console.WriteLine("\nTrop grand ! Revois tes ambitions !\n\n");
        }

        if (nombreVies == 0)
        {
            Console.WriteLine($"\nDésolé, vous n'avez plus de vies. Le nombre était : {nombreADeviner}");
        }
    }
}

Console.WriteLine("Merci d'avoir joué !");
