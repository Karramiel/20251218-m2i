#region Choix et mise en place 
using System.ComponentModel.Design;
using System.Timers;

Console.WriteLine("Choississez une difficulte : Facile, Moyen, Difficile");
Console.WriteLine("1 - Facile");
Console.WriteLine("2 - Moyen");
Console.WriteLine("3 - Difficile");
string choixDifficulte = Console.ReadLine();


int     nombrePions = 4;
int     nombreVies = 10;

switch (choixDifficulte)
{
    case "1":
        nombrePions = 4;
        nombreVies = 10;
        break;

    case "2":
        nombrePions = 5;
        nombreVies = 15;
        break;

    case "3":
        nombrePions = 6;
        nombreVies = 20;
        break;

    default:
        Console.WriteLine("Choix invalide, difficulté par défaut (Moyen) sélectionnée.");
        nombrePions = 5;  // valeur par défaut Moyen
        nombreVies = 15;  // valeur par défaut Moyen
        break;




}

Console.WriteLine($"Difficulté sélectionnée : {choixDifficulte}");

char[] couleurs = { 'B', 'J', 'R', 'V' };
char[] combinaisonSecrete = new char[nombrePions];

Random random = new Random();

for (int i = 0; i < nombrePions; i++)
{

    combinaisonSecrete[i] = couleurs[random.Next(couleurs.Length)];
}

Console.WriteLine("Combinaison secrète générée. Commencez à deviner !");
Console.WriteLine($"Cheat mode : {new string(combinaisonSecrete)}");

#endregion

#region Jeu principal
bool gagne = false;

while (nombreVies > 0 && !gagne)
{
    Console.WriteLine("\nEntrez votre proposition (ex: BJRV) : ");
    string proposition = Console.ReadLine();

    // Vérifier la longueur de la proposition
    if (proposition.Length != nombrePions)
    {
        Console.WriteLine("Longueur incorrecte !");
        continue;
    }

    // Vérifier que toutes les lettres sont valides
    bool lettresInvalides = false;
    foreach (char check in proposition)
    {
        if (!couleurs.Contains(check))
        {
            lettresInvalides = true;
            break;
        }
    }

    if (lettresInvalides)
    {
        Console.WriteLine($"Erreur : seules les lettres {string.Join(", ", couleurs)} sont autorisées !");
        continue;
    }

    int bienPlaces = 0;
    int malPlaces = 0;
    int inexistant = 0;

    bool[] utilisesSecret = new bool[nombrePions];
    bool[] utilisesProposition = new bool[nombrePions];

    // Comptage des pions bien placés
    for (int i = 0; i < nombrePions; i++)
    {
        if (proposition[i] == combinaisonSecrete[i])
        {
            bienPlaces++;
            utilisesSecret[i] = true;
            utilisesProposition[i] = true;

        }
    }
    string plurielAvecUnS = bienPlaces > 1 ? "s" : "";
    string est_sont = bienPlaces > 1 ? "sont" : "est";

    if (bienPlaces > 0 )
    {
        Console.WriteLine($"{bienPlaces} pion{plurielAvecUnS} faisant partie de l'ensemble des pions {est_sont} placé{plurielAvecUnS} au bon emplacement");

     }


    // Comptage des pions mal placés
    for (int i = 0; i < nombrePions; i++)
    {
        if (utilisesProposition[i])
            continue;

        for (int j = 0; j < nombrePions; j++)
        {
            if (!utilisesSecret[j] && proposition[i] == combinaisonSecrete[j])
            {
                malPlaces++;
                utilisesSecret[j] = true;
                utilisesProposition[i] = true;
                break;
            }
        }
    }

    string plurielAvecUnS_MP = malPlaces > 1 ? "s" : "";
    string est_sont_MP = malPlaces > 1 ? "sont" : "est";


    if (malPlaces > 0)
    {
        Console.WriteLine($"{malPlaces} pion{plurielAvecUnS_MP} faisant partie de l'ensemble des pions {est_sont_MP} MAL placé{plurielAvecUnS_MP} ! Tu es mauvais !");

    }


    // Couleur inexistante dans la solution
    for (int i = 0; i < nombrePions; i++)
    {
        if (!utilisesProposition[i])
        {
            // Si la couleur proposée n'est pas dans la combinaison secrète
            if (!combinaisonSecrete.Contains(proposition[i]))
            {
                inexistant++;
            }
        }
    }

    string plurielAvecUnS_In = inexistant > 1 ? "s" : "";
    string fait_font_In = inexistant > 1 ? "font" : "fait";

    if (inexistant > 0)
    {
        Console.WriteLine($"{inexistant} pion{plurielAvecUnS_In} ne {fait_font_In} pas partie de l'ensemble des pions ! Tu es très mauvais !");
            }

    // Bonne réponse ?
    string plurielAvecUnS_Vies = nombreVies > 1 ? "s" : "";
    nombreVies--;
    Console.WriteLine($"{nombreVies} Vie{plurielAvecUnS_Vies} restante{plurielAvecUnS_Vies}");
    if (bienPlaces == nombrePions)
    {
        gagne = true;
        Console.WriteLine("Bravo ! Vous avez trouvé la combinaison !");
    }
}
//Game over
Console.WriteLine("Désolé, plus de vie");




#endregion
