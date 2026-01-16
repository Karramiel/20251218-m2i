using System;

namespace Exo_efcore_liste_contact
{
    public class Contact
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Telephone { get; set; } = null!;

        // Propriété calculée pour l'affichage
        public int Age => DateTime.Now.Year - DateNaissance.Year;
    }
}