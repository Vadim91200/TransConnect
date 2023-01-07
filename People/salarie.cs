using System.Formats.Asn1;
using System.Xml.Linq;

namespace TransConnect
{
    public class Salarie: Personne
    {
        private DateTime dateEntree;
        private string title;
        private List<Salarie> directreports;
        private Salarie manager;
        private int salaire;
        public Salarie(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone, DateTime dateEntree, string poste, int salaire, Salarie manager) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.dateEntree = dateEntree;
            this.title = poste;
            this.salaire = salaire;
            this.directreports = new List<Salarie>();
            this.manager = manager;
            if (this.manager != null) { this.manager.directreports.Add(this); }
        }
        public string Title { get => title;}
        public List<Salarie> DirectReports { get => directreports; }
        public Salarie Manager { get => manager; set => manager = value; }
        public override string ToString()
        {
            return base.ToString() + "he works as " + this.title + " is manager is " + this.manager.Surname + "he was hired the " + this.dateEntree + " and his salary is " + this.salaire + "€";
        }
        public static Salarie CreateEmployeeFromInput(Dictionary<string, Salarie> employees)
        {
            string[] EmployeeDetails;
            Salarie EnteredEmployee = null;
            do
            {
                Console.WriteLine("Enter the employee detail separte by a ; (Social security number; Surname; Name; Date Of Birth; Postal Adress format (number street name city zipcode); Email Adress; Phone; date of hiring; position; salary; Manager Surname;'Leave empty'");
                EmployeeDetails = Console.ReadLine().Split(';');
                StreamWriter sWriter = null;
                try
                {
                    EnteredEmployee = ParseFromArrayString(EmployeeDetails, employees);
                    FileStream fileStream = new FileStream("../../../CompanyDetails/EmployeeList.csv", FileMode.Append, FileAccess.Write);

                    sWriter = new StreamWriter(fileStream);
                    sWriter.Write(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}\n", EmployeeDetails[0], EmployeeDetails[1], EmployeeDetails[2], EmployeeDetails[3], EmployeeDetails[4], EmployeeDetails[5], EmployeeDetails[6], EmployeeDetails[7], EmployeeDetails[8], EmployeeDetails[9], EmployeeDetails[10], EmployeeDetails[11]));
                
                }
                catch (Exception e)
                {
                    Console.WriteLine("Un error occured while trying to add Employee in a file");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (sWriter != null) sWriter.Close();
                }
            } while (EnteredEmployee == null);
            return EnteredEmployee;
        }
        public static Salarie ParseFromArrayString(string[] ObjectDetails, Dictionary<string, Salarie> employees)
        {
            long SSN = IConvert.ConvertTo<long>(ObjectDetails[0]);
            string Surname = (IConvert.ConvertTo<string>(ObjectDetails[1])).Trim();
            string Name = (IConvert.ConvertTo<string>(ObjectDetails[2])).Trim();
            DateTime DateBirth = IConvert.ConvertTo<DateTime>(ObjectDetails[3]);
            string PostalAdress = (IConvert.ConvertTo<string>(ObjectDetails[4])).Trim();
            string EmailAdress = (IConvert.ConvertTo<string>(ObjectDetails[5])).Trim();
            int Phone = IConvert.ConvertTo<int>(ObjectDetails[6]);
            DateTime DateHiring = IConvert.ConvertTo<DateTime>(ObjectDetails[7]);
            string Position = (IConvert.ConvertTo<string>(ObjectDetails[8])).Trim();
            int Salary = IConvert.ConvertTo<int>(ObjectDetails[9]);
            string ManagerName = (IConvert.ConvertTo<string>(ObjectDetails[10])).Trim();
            string ReportNames = IConvert.ConvertTo<string>(ObjectDetails[11]);
            string[] directReportNames = ReportNames.Split(' ');
            Salarie Manager = null;
            if (ManagerName != "")
            {
                Manager = employees[ManagerName];
            }
            Salarie employee = null;
            if (Position.ToUpper() == "CHAUFFEUR")
            {
                employee = new Chauffeur(SSN, Surname, Name, DateBirth, PostalAdress, EmailAdress, Phone, DateHiring, Position, Salary, Manager);
            }
            else
            {
                employee = new Salarie(SSN, Surname, Name, DateBirth, PostalAdress, EmailAdress, Phone, DateHiring, Position, Salary, Manager);
            }
            if (directReportNames.Length > 0 && directReportNames[0] != "")
            {
                foreach (var directReportName in directReportNames)
                {
                    Salarie directReport = employees[directReportName];
                    directReport.Manager = employee;
                    employee.DirectReports.Add(directReport);
                }
            }
            employees[Surname] = employee;
            return employee;
        }
    }
}