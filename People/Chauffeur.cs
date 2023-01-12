namespace TransConnect
{
    /// <summary>
    /// The class represents a Chauffeur with additional functionality such as a daily rate and a list of deliveries
    /// </summary>
    public class Chauffeur : Salarie
    {
        public int Daily_rate { get; set; }
        private List<Livraison> deliverieslist;
        /// <summary>
        /// Initializes a new instance of the <see cref="chauffeur"/> class
        /// </summary>
        /// <param name="NSS">The Social Security Number of the chauffeur</param>
        /// <param name="nom">The last name of the chauffeur</param>
        /// <param name="prenom">The first name of the chauffeur</param>
        /// <param name="dateNaissance">The date of birth of the chauffeur</param>
        /// <param name="adressePostale">The postal address of the chauffeur</param>
        /// <param name="adresseMail">The email address of the chauffeur</param>
        /// <param name="telephone">The telephone number of the chauffeur</param>
        /// <param name="dateEntree">The date of entry of the chauffeur</param>
        /// <param name="poste">The position of the chauffeur</param>
        /// <param name="salaire">The salary of the chauffeur</param>
        /// <param name="manager">The manager of the chauffeur</param>
        public Chauffeur(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone, DateTime dateEntree, string poste, int salaire, Salarie manager) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone, dateEntree, poste, salaire, manager)
        {
            this.deliverieslist = new List<Livraison>();
        }
        /// <summary>
        /// Gets the number of deliveries of the chauffeur
        /// </summary>
        public int NumberOfDeliveries
        {
            get { return this.deliverieslist.Count; }
        }
        /// <summary>
        /// Returns a string that represents the current chauffeur
        /// </summary>
        /// <returns>A string containing the chauffeur's number of deliveries and daily rate</returns>
        public override string ToString()
        {
            return base.ToString() + "he has " + this.deliverieslist.Count + " deliveries" + " and a daily rate of " + this.Daily_rate;
        }
        /// <summary>
        /// Determines if the chauffeur is free on a given date
        /// </summary>
        /// <param name="d">The date to check</param>
        /// <returns>True if the chauffeur is free on the given date, false otherwise</returns>
        public bool EstLibre(DateTime d)
        {
            if (this.deliverieslist.Find(Livraison => Livraison.Deliverydate == d) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}