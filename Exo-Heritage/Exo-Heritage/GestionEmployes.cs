namespace Exo_Heritage
{

public class GestionEmployes
    {
        private List<Salarie> employes = new List<Salarie>();

        public void AjouterEmploye()
        {
            if (employes.Count >= 20)
            {
                Console.WriteLine("Limite de 20 employés atteinte.");
                return;
            }

            Console.Write("Salarie (S) ou Commercial (C) ? ");
            string type = Console.ReadLine().ToUpper();

            Console.Write("Nom : ");
            string nom = Console.ReadLine();

            Console.Write("Salaire fixe : ");
            double salaire = double.Parse(Console.ReadLine());

            if (type == "C")
            {
                Console.Write("Chiffre d'affaire : ");
                double ca = double.Parse(Console.ReadLine());

                Console.Write("Commission (%) : ");
                double commission = double.Parse(Console.ReadLine());

                employes.Add(new Commercial(nom, salaire, ca, commission));
            }
            else
            {
                employes.Add(new Salarie(nom, salaire));
            }

            Console.WriteLine("Employé ajouté.");
        }

        public void AfficherSalaires()
        {
            foreach (Salarie e in employes)
            {
                Console.WriteLine(e);
            }
        }

        public void RechercherEmploye()
        {
            Console.Write("Début du nom : ");
            string recherche = Console.ReadLine().ToLower();

            var resultats = employes
                .Where(e => e.Nom.ToLower().StartsWith(recherche));

            foreach (Salarie e in resultats)
            {
                Console.WriteLine(e);
            }
        }
    }

}

