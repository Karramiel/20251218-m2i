using Exo_GestionPaiements;
using System.Text;

namespace GestionPaiements
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IHM ihm = new IHM();
            ihm.Demarrer();
        }
    }
}