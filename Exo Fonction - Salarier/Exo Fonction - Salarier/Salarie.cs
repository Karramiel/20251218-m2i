public class Salarie
{
    // Attributs
    public int Matricule { get; set; }
    public string Service { get; set; }
    public string Categorie { get; set; }
    public string Nom { get; set; }
    public decimal Salaire { get; set; }

    // Champs statiques (communs à tous les salariés)
    public static int NombreEmployes { get; private set; }
    public static decimal SalaireTotal { get; private set; }

    // Constructeur
    public Salarie(int matricule, string service, string categorie, string nom, decimal salaire)
    {
        Matricule = matricule;
        Service = service;
        Categorie = categorie;
        Nom = nom;
        Salaire = salaire;

        NombreEmployes++;
        SalaireTotal += salaire;
    }

    // Méthode demandée
    public string AfficherSalaire()
    {
        return $"{Nom} gagne {Salaire} €";
    }

    // Méthode statique de remise à zéro
    public static void ResetStatistiques()
    {
        NombreEmployes = 0;
        SalaireTotal = 0;
    }
}