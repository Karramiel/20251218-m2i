using Exo_CompteBancaire;


class Program
{
    static void Main()
    {
        List<Client> clients = new List<Client>();

        // Client de test
        Client clientTest = new Client("Dupont", "Jean", "JD123", "0601020304");
        clients.Add(clientTest);

        // Création de comptes pour ce client
        clientTest.Comptes.Add(new CompteCourant(clientTest, 1000));
        clientTest.Comptes.Add(new CompteEpargne(clientTest, 500));
        clientTest.Comptes.Add(new ComptePayant(clientTest, 200));

        bool quit = false;
        while (!quit)
        {
            Console.WriteLine("\n=== Banque - Menu Clients ===");
            Console.WriteLine("1 - Rechercher un client");
            Console.WriteLine("2 - Ajouter un client");
            Console.WriteLine("0 - Quitter");
            Console.Write("Choix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Client client = RechercherClient(clients);
                    if (client != null)
                        MenuClient(client);
                    break;

                case "2":
                    Client nouveauClient = AjouterClient(clients);
                    if (nouveauClient != null)
                        MenuClient(nouveauClient);  // On passe directement au menu client
                    break;

                case "0":
                    quit = true;
                    break;
            }
        }
    }

    // ===== MENU CLIENT =====
    static void MenuClient(Client client)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine($"\n=== Client : {client.Nom} {client.Prenom} ===");
            Console.WriteLine("1 - Sélectionner un compte");
            Console.WriteLine("2 - Afficher tous les comptes");
            Console.WriteLine("3 - Créer un nouveau compte");
            Console.WriteLine("0 - Retour");
            Console.Write("Choix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    CompteBancaire compte = ChoisirCompte(client);
                    if (compte != null) MenuCompte(compte);
                    break;

                case "2":
                    AfficherComptes(client);
                    break;

                case "3":
                    client.AjoutCompteClient();
                    break;

                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide !");
                    return;
                    break;
            }
        }
    }

    // ===== RECHERCHE CLIENT =====
    static Client RechercherClient(List<Client> clients)
    {
        Console.Write("Nom du client : ");
        string nom = Console.ReadLine();

        var client = clients.FirstOrDefault(c =>
            c.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase));

        if (client == null)
        {
            Console.WriteLine("Client introuvable.");
            return null;
        }

        Console.WriteLine($"Client sélectionné : {client.Nom} {client.Prenom}");
        return client;
    }

    // ===== AJOUT CLIENT =====
    static Client AjouterClient(List<Client> clients)
    {
        Console.Write("Nom : ");
        string nom = Console.ReadLine();

        Console.Write("Prénom : ");
        string prenom = Console.ReadLine();

        Console.Write("Identifiant : ");
        string id = Console.ReadLine();

        Console.Write("Téléphone : ");
        string tel = Console.ReadLine();

        // Création du client
        Client nouveauClient = new Client(nom, prenom, id, tel);

        // Création des comptes par défaut
        nouveauClient.Comptes.Add(new CompteCourant(nouveauClient, 0));
        nouveauClient.Comptes.Add(new CompteEpargne(nouveauClient, 0));
        nouveauClient.Comptes.Add(new ComptePayant(nouveauClient, 0));

        clients.Add(nouveauClient);

        Console.WriteLine($"Client {nouveauClient.Nom} {nouveauClient.Prenom} ajouté avec succès !");
        // Retourne le client pour l'utiliser immédiatement
        return nouveauClient;


    }

    // ===== CHOIX COMPTE =====
    static CompteBancaire ChoisirCompte(Client client)
    {
        Console.WriteLine("\nSélectionnez un compte :");
        for (int i = 0; i < client.Comptes.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {client.Comptes[i].GetType().Name}");
        }
        Console.Write("Choix : ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= client.Comptes.Count)
        {
            return client.Comptes[index - 1];
        }
        Console.WriteLine("Choix invalide !");
        return null;
    }

    // ===== MENU COMPTE =====
    static void MenuCompte(CompteBancaire compte)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine($"\n=== {compte.GetType().Name} de {compte.Client.Nom} {compte.Client.Prenom} ===");
            Console.WriteLine("1 - Dépôt");
            Console.WriteLine("2 - Retrait");
            Console.WriteLine("3 - Afficher solde");
            Console.WriteLine("4 - Afficher opérations");

            if (compte is CompteEpargne)
                Console.WriteLine("5 - Appliquer intérêts annuels");

            Console.WriteLine("0 - Retour");
            Console.Write("Choix : ");

            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    Console.Write("Montant : ");
                    if (double.TryParse(Console.ReadLine(), out double dep))
                        compte.Depot(dep);
                    break;

                case "2":
                    Console.Write("Montant : ");
                    if (double.TryParse(Console.ReadLine(), out double ret))
                        compte.Retrait(ret);
                    break;

                case "3":
                    compte.AfficherSolde();
                    break;

                case "4":
                    compte.AfficherOperations();
                    break;

                case "5":
                    if (compte is CompteEpargne ce)
                        ce.InteretsAnnuels();
                    break;

                case "0":
                    back = true;
                    break;
            }
        }
    }

    // ===== AFFICHAGE COMPTES =====
    static void AfficherComptes(Client client)
    {
        Console.WriteLine("\nListe des comptes :");
        foreach (var compte in client.Comptes)
        {
            Console.WriteLine($"{compte.NomCompte} - Solde : {compte.Solde} €");
        }
    }
}
