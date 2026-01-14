namespace Exo_Heritage
{
  
        public class Salarie
        {
            protected string nom;
            protected double salaire;

            public string Nom => nom;

            public Salarie()
            {
            }

            public Salarie(string nom, double salaire)
            {
                this.nom = nom;
                this.salaire = salaire;
            }

            public virtual double AfficherSalaire()
            {
                return salaire;
            }

            public override string ToString()
            {
                return $"{nom} - Salaire : {AfficherSalaire()} €";
            }
        }

    
}
