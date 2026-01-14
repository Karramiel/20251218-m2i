namespace Exo_Heritage
{
    public class Commercial : Salarie
    {
        private double chiffreAffaire;
        private double commission;

        public Commercial() : base()
        {
        }

        public Commercial(string nom, double salaire, double chiffreAffaire, double commission)
            : base(nom, salaire)
        {
            this.chiffreAffaire = chiffreAffaire;
            this.commission = commission;
        }

        public override double AfficherSalaire()
        {
            return salaire + (chiffreAffaire * commission / 100);
        }

        public override string ToString()
        {
            return $"{nom} (Commercial) - Salaire : {AfficherSalaire()} €";
        }
    }
}
