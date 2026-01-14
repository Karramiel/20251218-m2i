namespace Exo_CompteBancaire
{
        public class CompteCourant : CompteBancaire
    {
        public double DecouvertAutorise { get; set; } = 500;

        public CompteCourant(Client client, double soldeInitial = 0) : base(client, soldeInitial) { }

        public override void Depot(double montant)
        {
            Solde += montant;
            Operations.Add(new Operation(montant, Statut.Depot));
        }

        public override bool Retrait(double montant)
        {
            if (Solde - montant >= -DecouvertAutorise)
            {
                Solde -= montant;
                Operations.Add(new Operation(montant, Statut.Retrait));
                return true;
            }
            Console.WriteLine("Découvert autorisé dépassé !");
            return false;
        }
    }
}