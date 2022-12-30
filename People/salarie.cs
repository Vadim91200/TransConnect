namespace TransConnect
{
    public class Salarie: Personne
    {
        private DateTime dateEntree;
        private string poste { get; set; }
        private int salaire { get; set; }

        public Salarie(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone, DateTime dateEntree, string poste, int salaire) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
        }
        public static Salarie CreateEmployeeFromInput()
        {
            string[] EmployeeDetails;
            Salarie EnteredEmployee = null;
            do
            {
                Console.WriteLine("Enter the employee detail separte by a ; (Social security number; Surname; Name; Date Of Birth; Postal Adress format (number street name city zipcode); Email Adress; Phone; date of hiring; position; salary");
                EmployeeDetails = Console.ReadLine().Split(';');
                try
                {
                    int SSN = IConvert.ConvertTo<int>(EmployeeDetails[0]);
                    string Surname = IConvert.ConvertTo<string>(EmployeeDetails[1]);
                    string Name = IConvert.ConvertTo<string>(EmployeeDetails[2]);
                    DateTime DateBirth = IConvert.ConvertTo<DateTime>(EmployeeDetails[3]);
                    string PostalAdress = IConvert.ConvertTo<string>(EmployeeDetails[4]);
                    string EmailAdress = IConvert.ConvertTo<string>(EmployeeDetails[5]);
                    int Phone = IConvert.ConvertTo<int>(EmployeeDetails[6]);
                    DateTime DateHiring = IConvert.ConvertTo<DateTime>(EmployeeDetails[7]);
                    string Position = IConvert.ConvertTo<string>(EmployeeDetails[8]);
                    int Salary = IConvert.ConvertTo<int>(EmployeeDetails[9]);
                    EnteredEmployee = new Salarie(SSN, Surname, Name, DateBirth, PostalAdress, EmailAdress, Phone, DateHiring, Position, Salary);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (EnteredEmployee == null);
            return EnteredEmployee;
        }
    }
}