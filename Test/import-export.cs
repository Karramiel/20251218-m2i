
using System;
using System.IO;
using System.Text;
namespace Exo_GestionPaiements
{
    public class DuplicationEtConcat
    {
        public void Export()
        {
            string dossierProjet = Directory.GetParent(
                Directory.GetCurrentDirectory()
            ).Parent.Parent.FullName;

            string dossierDestination = Directory.GetParent(dossierProjet).FullName;
            string fichierConcatene = Path.Combine(dossierDestination, "UnPourTous.cs");

            Console.WriteLine("Dossier source : " + dossierProjet);
            Console.WriteLine("Dossier destination : " + dossierDestination);
            Console.WriteLine("Fichier concaténé : " + fichierConcatene);

            using (StreamWriter sw = new StreamWriter(fichierConcatene, false, Encoding.UTF8))
            {
                foreach (var fichier in Directory.GetFiles(dossierProjet, "*.cs", SearchOption.AllDirectories))
                {
                    if (fichier.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") ||
                        fichier.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
                        continue;

                    sw.WriteLine("// ===============================");
                    sw.WriteLine("// Fichier : " + Path.GetFileName(fichier));
                    sw.WriteLine("// ===============================");
                    sw.WriteLine();

                    sw.WriteLine(File.ReadAllText(fichier));
                    sw.WriteLine();
                }
            }

            Console.WriteLine("✅ Concaténation terminée !");
        }

        public void Import()
        {
            string fichierImport = @"C:\Users\Administrateur\Exercices\Exo C#\Exo-GestionPaiements\UnPourTous.cs";
            string dossierDestination = @"C:\Users\Administrateur\Exercices\Exo C#\Test";

            Directory.CreateDirectory(dossierDestination);

            string[] lignes = File.ReadAllLines(fichierImport, Encoding.UTF8);

            StringBuilder contenuCourant = null;
            string nomFichierCourant = null;

            foreach (string ligne in lignes)
            {
                if (ligne.StartsWith("// Fichier :"))
                {
                    if (contenuCourant != null && nomFichierCourant != null)
                    {
                        File.WriteAllText(
                            Path.Combine(dossierDestination, nomFichierCourant),
                            contenuCourant.ToString(),
                            Encoding.UTF8
                        );
                    }

                    nomFichierCourant = ligne.Replace("// Fichier :", "").Trim();
                    contenuCourant = new StringBuilder();
                    continue;
                }

                if (ligne.StartsWith("// ==============================="))
                    continue;

                if (contenuCourant != null)
                    contenuCourant.AppendLine(ligne);
            }

            if (contenuCourant != null && nomFichierCourant != null)
            {
                File.WriteAllText(
                    Path.Combine(dossierDestination, nomFichierCourant),
                    contenuCourant.ToString(),
                    Encoding.UTF8
                );
            }

            Console.WriteLine("✅ Import et division terminés !");
        }
    }
}

