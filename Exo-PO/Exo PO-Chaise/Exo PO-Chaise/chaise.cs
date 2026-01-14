using System.Runtime.InteropServices;

public class Chaise
{
    // Variables d'instance
    public int NbPieds { get; set; }
    public string Materiau { get; set; }
    public string Couleur { get; set; }

    // Constructeur par défaut
    public Chaise()
    {
        NbPieds = 4;
        Materiau = "Bois";
        Couleur = "Marron";
    }

    // Constructeur avec paramètres
    public Chaise(int nbPieds, string materiau, string couleur)
    {
        NbPieds = nbPieds;
        Materiau = materiau;
        Couleur = couleur;
    }

    // Surcharge de la méthode Object.ToString()
    public override string ToString()
    {
        return $"Chaise : {NbPieds} pieds, matériau = {Materiau}, couleur = {Couleur}";
    }

    //public void Affichage()
    //{
    //   Console.WriteLine($"Chaise : {NbPieds} pieds, matériau = {Materiau}, couleur = {Couleur}");
    //}
}

