using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_GestionPaiements.Paiements
{
    public interface IPaiement
    {
        string EffectuerPaiement(double montant);
    }
}
