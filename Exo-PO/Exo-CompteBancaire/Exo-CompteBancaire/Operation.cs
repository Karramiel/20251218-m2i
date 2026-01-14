public enum Statut
{
    Depot,
    Retrait,
    Interet
}

public class Operation
{
    private static int _Compteur = 1;

    public int Numero { get; private set; }
    public double Montant { get; set; }
    public Statut Statut { get; set; }

    public Operation(double montant, Statut statut)
    {
        Numero = _Compteur++;
        Montant = montant;
        Statut = statut;
    }

    public override string ToString()
    {
        return $"Opération n°{Numero} : {Statut} de {Montant} €";
    }
}
