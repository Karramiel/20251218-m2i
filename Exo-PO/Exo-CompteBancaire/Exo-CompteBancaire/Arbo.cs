using System.Reflection.Metadata;

Exo_CompteBancaire /        < --Dossier racine du projet
│
├─ Program.cs              <-- Point d'entrée du programme (Main)
│
├─ Client.cs               <-- Classe Client
│   └─ public class Client
│       ├─ string Nom
│       ├─ string Prenom
│       ├─ string Identifiant
│       ├─ string Telephone
│       ├─ List<CompteBancaire> Comptes
│       ├─ void AjoutCompteClient()
│
├─ CompteBancaire.cs       <-- Classe abstraite CompteBancaire
│   └─ public abstract class CompteBancaire
│       ├─ double Solde
│       ├─ Client Client
│       ├─ List<Operation> Operations
│       ├─ string NomCompte
│       ├─ abstract void Depot(double montant)
│       ├─ abstract bool Retrait(double montant)
│       ├─ void AfficherOperations()
│       ├─ void AfficherSolde()
│       └─ override string ToString()
│
├─ CompteCourant.cs < --Classe CompteCourant(hérite de CompteBancaire)
│   └─ public class CompteCourant : CompteBancaire
│       ├─ override void Depot(double montant)
│       └─ override bool Retrait(double montant)
│
├─ CompteEpargne.cs<-- Classe CompteEpargne (hérite de CompteBancaire)
│   └─ public class CompteEpargne : CompteBancaire
│       ├─ override void Depot(double montant)
│       ├─ override bool Retrait(double montant)
│       └─ void InteretsAnnuels()
│
├─ ComptePayant.cs<-- Classe ComptePayant (hérite de CompteBancaire)
│   └─ public class ComptePayant : CompteBancaire
│       ├─ override void Depot(double montant)
│       └─ override bool Retrait(double montant)
│
└─ Operation.cs<-- Classe Operation (pour enregistrer les opérations)
    └─ public class Operation
        ├─ string Type
        ├─ double Montant
        ├─ DateTime Date
        └─ override string ToString()
