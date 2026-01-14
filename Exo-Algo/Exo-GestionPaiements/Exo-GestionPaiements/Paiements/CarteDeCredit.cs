using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_GestionPaiements.Paiements
{
    public class CarteDeCredit : IPaiement
    {
        public string NumeroCarte { get; set; }
        public string Titulaire { get; set; }
        public string DateExpiration { get; set; }
        public string Cvv { get; set; }

        public double SoldeDisponible { get; private set; }

        public CarteDeCredit(string numeroCarte, 
                                string titulaire, 
                                string dateExpiration, 
                                string cvv,
                                double soldeInitial = 1000.0)
        {
            NumeroCarte = numeroCarte;
            Titulaire = titulaire;
            DateExpiration = dateExpiration;
            Cvv = cvv;
            SoldeDisponible = soldeInitial;


        }

        public string EffectuerPaiement(double montant)
        {
            if (montant <= 0)
            {
                return "❌ Paiement par carte refusé : montant invalide.";
            }
            
            if (montant > SoldeDisponible)
            {
                return $"❌ Paiement refusé : solde insuffisant ({SoldeDisponible}€ disponibles).";
            }

            SoldeDisponible -= montant;

            return $"✅ Paiement de {montant}€ effectué avec succès par carte de crédit.\n" +
                $"💳 Il vous reste {SoldeDisponible}€ sur votre compte.";
        }
    }
}
