namespace Exo_CompteBancaire
{
    public abstract class CompteBancaire
    {
        public double Solde { get; set; }
        public Client Client { get; set; }
        public List<Operation> Operations { get; set; }
        public string NomCompte { get; set; }

        public CompteBancaire(Client client, double soldeInitial = 0, string nomCompte = null)
        {
            Client = client;
            Solde = soldeInitial;
            Operations = new List<Operation>();
            NomCompte = nomCompte ?? this.GetType().Name; // nom par défaut si non fourni

        }

        public abstract void Depot(double montant);
        public abstract bool Retrait(double montant);

        public void AfficherOperations()
        {
            foreach (var op in Operations)
            {
                Console.WriteLine(op);
            }
        }

        public virtual void AfficherSolde()
        {
            Console.WriteLine($"Solde actuel : {Solde} €");
        }

        public override string ToString()
        {
            return base.GetType().Name + " de " + Client.Nom + " " + Client.Prenom + " - Solde : " + Solde + " €";
        }
    }
}
