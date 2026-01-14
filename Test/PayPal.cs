
using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_GestionPaiements.Paiements
{
    public class PayPal : IPaiement
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }

        public PayPal(string email, string motDePasse)
        {
            Email = email;
            MotDePasse = motDePasse;
        }

        public string EffectuerPaiement(double montant)
        {
            if (montant <= 0)
            {
                return "❌ Paiement PayPal refusé : montant invalide.";
            }

            return $"✅ Paiement de {montant}€ effectué avec succès via PayPal.";
        }
    }
}


