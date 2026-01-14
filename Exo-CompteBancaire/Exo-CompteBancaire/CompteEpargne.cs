namespace Exo_CompteBancaire
{
        public class CompteEpargne : CompteBancaire
    {
        public double TauxInteret { get; set; } = 0.03;

        public CompteEpargne(Client client, double soldeInitial = 0) : base(client, soldeInitial) { }

        public override void Depot(double montant)
        {
            Solde += montant;
            Operations.Add(new Operation(montant, Statut.Depot));
        }

        public override bool Retrait(double montant)
        {
            if (montant <= Solde)
            {
                Solde -= montant;
                Operations.Add(new Operation(montant, Statut.Retrait));
                return true;
            }
            Console.WriteLine("Solde insuffisant !");
            return false;
        }

        public void InteretsAnnuels()
        {
            double interets = Solde * TauxInteret;
            Solde += interets;

            Operations.Add(new Operation(interets, Statut.Interet));

            Console.WriteLine($"Intérêts annuels appliqués : {interets} €");
        }
        public override void AfficherSolde()
        {
            base.AfficherSolde();

            double totalInterets = Operations
                .Where(o => o.Statut == Statut.Interet)
                .Sum(o => o.Montant);

            if (totalInterets > 0)
            {
                Console.WriteLine($"Intérêts cumulés : {totalInterets} €");
            }
        }

    }
}