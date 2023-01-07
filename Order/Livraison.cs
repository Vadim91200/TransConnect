using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TransConnect
{
    public class Livraison : IConvert
    {
        private string departure;
        private string arrival;
        private int price;
        private bool ispaid;
        private TimeSpan duration;
        private DateTime deliverydate;
        private int distance;
        private DateTime Startingdate;
        private List<String> delivery_Route;
        private string deliveredproduct;
        private int deliveryID;
        public DateTime Deliverydate { get => this.deliverydate; set => this.deliverydate = value; }
        public Livraison(string deliveredproduct, string PDepart, string PArrive, DateTime datelivraison)
        {
            this.deliveredproduct = deliveredproduct;
            this.departure = PDepart;
            this.arrival = PArrive;
            this.ispaid = false;
            this.Startingdate = DateTime.Now;
            this.deliverydate = datelivraison;
            this.deliveryID = new Random().Next(0, 999999);
        }
        public Livraison(string deliveredproduct, string PDepart, string PArrive, DateTime datelivraison, int id)
        {
            this.deliveredproduct = deliveredproduct;
            this.departure = PDepart;
            this.arrival = PArrive;
            this.ispaid = false;
            this.Startingdate = DateTime.Now;
            this.deliverydate = datelivraison;
            this.deliveryID = id;
        }
        public string Departure { get => this.departure; }
        public string Arrival { get => this.arrival; }
        public int Distance { get => this.distance; set => this.distance = value;}
        public int Price { get => price; set => this.price = value; }
        public TimeSpan Duration { get => this.Startingdate - DateTime.Now; }
        public DateTime DeliveryDate { get => this.deliverydate; }
        public int DeliveryID { get => this.deliveryID; }
        public void Paid()
        {
            this.ispaid = true;
            this.duration = Startingdate - DateTime.Now;
        }
        public static Livraison CreateDeliveryFromInput()
        {
            string[] DeliveryDetails;
            Livraison EnteredDelivery = null;
            do
            {
                Console.WriteLine("Enter the detail of the delivery separte by a ; ( Delivered Product; City of departure; City of arrival; Delivery Date )");
                DeliveryDetails = Console.ReadLine().Split(';');
                StreamWriter sWriter = null;
                try
                {
                    EnteredDelivery = ParseFromArrayString(DeliveryDetails, false);

                    FileStream fileStream = new FileStream("../../../CompanyDetails/DeliveryList.csv", FileMode.Append, FileAccess.Write);

                    sWriter = new StreamWriter(fileStream);
                    sWriter.Write(string.Format("{0};{1};{2};{3};{4}\n", EnteredDelivery.DeliveryID, DeliveryDetails[0], DeliveryDetails[1], DeliveryDetails[2], DeliveryDetails[3]));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (sWriter != null) sWriter.Close();
                }
            } while (EnteredDelivery == null);
            return EnteredDelivery;
        }

        public static Livraison ParseFromArrayString(String[] ObjectDetails, bool nbr)
        {
            Livraison l = null;
            if (nbr)
            {
                string Product = IConvert.ConvertTo<string>(ObjectDetails[1]).Trim();
                string departure = IConvert.ConvertTo<string>(ObjectDetails[2]).Trim();
                string arrival = IConvert.ConvertTo<string>(ObjectDetails[3]).Trim();
                DateTime deliverydate = IConvert.ConvertTo<DateTime>(ObjectDetails[4]);
                int ID = IConvert.ConvertTo<int>(ObjectDetails[0]);
                l = new Livraison(Product, departure, arrival, deliverydate, ID);
            }
            else
            {
                string Product = IConvert.ConvertTo<string>(ObjectDetails[0]).Trim();
                string departure = IConvert.ConvertTo<string>(ObjectDetails[1]).Trim();
                string arrival = IConvert.ConvertTo<string>(ObjectDetails[2]).Trim();
                DateTime deliverydate = IConvert.ConvertTo<DateTime>(ObjectDetails[3]);
                l = new Livraison(Product, departure, arrival, deliverydate);
            }
            l.delivery_Route = FileInteraction.CalculateDistance(l);
            return l;

        }
    }
}
