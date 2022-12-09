using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Prolème
{
    //  Un salarié est donc une personne qui possède un N°SS, un nom, un prénom, une date de naissance, une adresse postale et une adresse mail ainsi qu’un téléphone. Le nom, l’adresse, le mail et le téléphone sont modifiables.

    public class personne
    {
        private int NSS;
        private string nom; get set
        private string prenom; get set
        private DateTime dateNaissance;
        private string adressePostale; get set
        private string adresseMail; get set
        private string telephone; get set

TransConnect possède également une base de données de ses clients avec les mêmes caractéristiques que les personnes ci-dessus décrites. (On simulera ces données par une collection de clients en mémoire). Les clients commandent des services de livraisons de marchandises de toutes sortes et de tout volume aux quatre coins de la France.
    }
}