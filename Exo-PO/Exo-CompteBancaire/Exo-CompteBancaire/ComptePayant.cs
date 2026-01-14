namespace Exo_CompteBancaire
{
    public class ComptePayant : CompteBancaire
{
    private const double Frais = 1.0;

     public ComptePayant(Client client, double soldeInitial = 0) : base(client, soldeInitial) { }

    public override void Depot(double montant)
    {
        Solde += montant - Frais;
        Operations.Add(new Operation(montant, Statut.Depot));
        Console.WriteLine($"Dépôt effectué avec frais de {Frais} €.");
    }

    public override bool Retrait(double montant)
    {
        double total = montant + Frais;
        if (total <= Solde)
        {
            Solde -= total;
            Operations.Add(new Operation(montant, Statut.Retrait));
            Console.WriteLine($"Retrait effectué avec frais de {Frais} €.");
            return true;
        }
        Console.WriteLine("Solde insuffisant !");
        return false;
    }
}
}