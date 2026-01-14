
#region Fonctions principale
// Fonction principale :
void Main()
{

    // --- Question pour activer le Cheat Mode ---
    Console.Write("Cheat mode activé ? (O/N) : ");
    string reponse = Console.ReadLine();
    bool modeCheat = reponse == "O"; // true si "O", false sinon


    int nbProduits;
    string[] noms;
    double[] prixUnitaires;
    int[] stocks;
    int[] ventes;

    if (modeCheat)
    {
        // --- Mode Cheat : tableau déjà rempli 
        noms = new string[] { "Produit 1", "Produit 2", "Produit 3", "Produit 4", "Produit 5" };
        prixUnitaires = new double[] { 1, 2, 0.5, 3.5, 0.5 };
        stocks = new int[] { 50, 20, 0, 8, 15 };
        ventes = new int[] { 30, 15, 5, 7, 10 };

        nbProduits = noms.Length; // Récupère le nombre de produits
    }
    else
    {
        // --- Mode normal : saisie utilisateur 
        nbProduits = LireEntier("Nombre de produits (2 à 20) : ", 2, 20);

        noms = new string[nbProduits];
        prixUnitaires = new double[nbProduits];
        stocks = new int[nbProduits];
        ventes = new int[nbProduits];

        for (int i = 0; i < nbProduits; i++)
        {
            Console.WriteLine($"\n--- Produit {i + 1} ---");
            Console.Write("Nom du produit : ");
            noms[i] = Console.ReadLine();
            prixUnitaires[i] = LireDouble("Prix unitaire (0.01 à 10000) : ", 0.01, 10000);
            stocks[i] = LireEntier("Quantité en stock (0 à 1000) : ", 0, 1000);
            ventes[i] = LireEntier("Unités vendues ce mois (0 à 1000) : ", 0, 1000);
        }
    }


    Console.WriteLine("\nSaisie terminée !");


    // Affichage de la fiche de chaque produit
    for (int i = 0; i < nbProduits; i++)
    {
        AfficherFicheProduit(noms[i], prixUnitaires[i], stocks[i], ventes[i]);
    }

    Console.WriteLine("\n==== Statistiques globales ====");

    // Calcul et affichage des statistiques globales
    double valeurStock = CalculerValeurStock(prixUnitaires, stocks);
    Console.WriteLine($"Valeur totale du stock : {valeurStock} Euros");

    double chiffreAffaires = CalculerChiffreAffaires(prixUnitaires, ventes);
    Console.WriteLine($"Chiffre d'affaires total du mois : {chiffreAffaires} Euros");

    double prixMoyen = CalculerMoyenne(prixUnitaires);
    Console.WriteLine($"Prix moyen des produits : {prixMoyen} Euros");

    Console.WriteLine($"Produit le plus cher : {TrouverProduitPlusCher(noms, prixUnitaires)}");
    Console.WriteLine($"Produit le plus vendu : {TrouverProduitLePlusVendu(noms, ventes)}");

    Console.WriteLine($"Nombre de produits en rupture : {CompterProduitsEnRupture(stocks)}");
    Console.WriteLine($"Nombre de produits en stock faible (<10) : {CompterProduitsStockFaible(stocks, 10)}");

    // Affichage des alertes de stock
    Console.WriteLine("\n--- Alertes de stock ---");
    AfficherAlerteStock(noms, stocks, 10);

    Console.WriteLine("\nProgramme terminé !");


}


// Appel de la fonction principale pour démarrer le programme :
Main();
#endregion


#region Fonctions Utilitaires

// Fonction pour lire un nombre entier avec validation :
 int LireEntier(string message, int min, int max) 
{
    // Déclaration de la variable pour stocker la valeur saisie :
    int valeur;
    do
    {
        Console.Write(message); // Affiche le message à l'utilisateur
    }
        
    while (!int.TryParse(Console.ReadLine(), out valeur) || valeur < min || valeur > max); //  Boucle jusqu'à ce que l'utilisateur saisisse une valeur valide

    return valeur; // Retourne la valeur saisie
}



// Fonction pour lire un double (deux décimales) avec validation :
 double LireDouble(string message, double min, double max)
{
    double valeur;
    do
    {
        Console.Write(message); // Affiche le message à l'utilisateur
    }
    while (!double.TryParse(Console.ReadLine(), out valeur) || valeur < min || valeur > max);  //  Boucle jusqu'à ce que l'utilisateur saisisse une valeur valide

    return valeur;
}




// Fonction 1 : pour calculer la valeur totale du stock :
 double CalculerValeurStock(double[] prix, int[] quantites) // Le tableau des prix est de type double, le tableau des quantités est de type int
{
    double total = 0; // Initialisation du total à 0

    for (int i = 0; i < prix.Length; i++) // Boucle sur tous les produits
    {
        total += prix[i] * quantites[i]; // Ajoute au total le prix × quantité du produit i
    }

    return total; // Retourne la valeur totale du stock
}

// Fonction 2 : pour calculer le chiffre d'affaires total :
 double CalculerChiffreAffaires(double[] prix, int[] ventes)
{
    double total = 0; // Initialise le chiffre d'affaires à 0

    for (int i = 0; i < prix.Length; i++) // Boucle sur tous les produits
    {
        total += prix[i] * ventes[i]; // Ajoute au total le prix × unités vendues du produit i
    }

    return total; // Retourne le chiffre d'affaires total
}

// Fonction 3 : pour trouver le produit le plus cher :
 string TrouverProduitPlusCher(string[] noms, double[] prix)
{
    int indexMax = 0; // On part du premier produit
    for (int i = 1; i < prix.Length; i++)
    {
        if (prix[i] > prix[indexMax])
            indexMax = i; // On mémorise l'indice du prix le plus élevé
    }
    return noms[indexMax]; // Retourne le nom du produit le plus cher
}

// Fonction 4 : pour trouver le produit le plus vendu :
 string TrouverProduitLePlusVendu(string[] noms, int[] ventes)
{
    int indexMax = 0;
    for (int i = 1; i < ventes.Length; i++) // Boucle sur tous les produits 
    {
        if (ventes[i] > ventes[indexMax]) //    Si le nombre d'unités vendues du produit i est supérieur au maximum actuel
            indexMax = i;
    }
    return noms[indexMax];
}

// Fonction 5 : pour compter le nombre de produits en rupture de stock :
 int CompterProduitsEnRupture(int[] quantites)
{
    int compteur = 0;
    for (int i = 0; i < quantites.Length; i++) // Boucle sur tous les produits
    {
        if (quantites[i] == 0) // Si la quantité est égale à 0
            compteur++;
    }
    return compteur;
}


// Fonction 6 : pour compter le nombre de produits avec un stock faible (en dessous d'un seuil donné) :
 int CompterProduitsStockFaible(int[] quantites, int seuil) // Le seuil est un entier
{
    int compteur = 0;
    for (int i = 0; i < quantites.Length; i++) // Boucle sur tous les produits
    {
        if (quantites[i] < seuil) // Si la quantité est inférieure au seuil
            compteur++;
    }
    return compteur;
}

// Fonction 7 : pour afficher la fiche d'un produit :
 void AfficherFicheProduit(string nom, double prix, int quantite, int ventes) 
{
    double chiffreAffaires = prix * ventes; // Calcul du chiffre d'affaires pour ce produit
    string statut; // Déclaration de la variable pour le statut du stock

    if (quantite == 0)
        statut = "En rupture";
    else if (quantite < 10)
        statut = "Stock faible (<10)";
    else
        statut = "Stock correct (>=10)";

    Console.WriteLine($"\n--- Produit : {nom} ---");
    Console.WriteLine($"Prix unitaire : {prix} Euros");
    Console.WriteLine($"Quantité en stock : {quantite}");
    Console.WriteLine($"Unités vendues : {ventes}");
    Console.WriteLine($"Chiffre d'affaires : {chiffreAffaires} Euros");
    Console.WriteLine($"Statut : {statut}");
}

// Fonction 8 : pour calculer la moyenne des valeurs d'un tableau de doubles :
 double CalculerMoyenne(double[] valeurs)
{
    double somme = 0;
    for (int i = 0; i < valeurs.Length; i++)
    {
        somme += valeurs[i]; // Additionne chaque valeur au total
    }
    return valeurs.Length > 0 ? somme / valeurs.Length : 0; // Évite division par 0 / retourne la moyenne
}



// Fonction 9 : pour afficher une alerte pour les produits avec un stock faible (en dessous d'un seuil donné) :
 void AfficherAlerteStock(string[] noms, int[] quantites, int seuil)
{
    bool alerte = false; // Indique si une alerte a été déclenchée


    for (int i = 0; i < quantites.Length; i++) //   Boucle sur tous les produits
    {
        
        if (quantites[i] == 0)
        {
            Console.WriteLine($"{noms[i]} - Stock : {quantites[i]} (URGENT - Je suis à vide !!!! Allleeeeeerte)");
            alerte = true;

        }

        else if (quantites[i] > 0 && quantites[i] < seuil)
        {
            Console.WriteLine($"{noms[i]} - Stock : {quantites[i]} (Sorts le portfeuille et vas faire du shopping)");
            alerte = true;

        }

    }

    if (!alerte)
        Console.WriteLine("Aucune alerte : tous les stocks sont suffisants, tu géres !");
}


#endregion
