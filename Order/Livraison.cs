using System.Collections;
using System.Collections.Generic;

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
        public DateTime Deliverydate { get => deliverydate; set => deliverydate = value; }
        public Livraison(string PDepart, string PArrive, DateTime datelivraison)
        {
            this.departure = PDepart;
            this.arrival = PArrive;
            this.ispaid = false;
            this.Startingdate = DateTime.Now;
            this.deliverydate = datelivraison;
        }
        public int Distance
        {
            get { return this.distance; }
        }
        public int Price { get => price; set => this.price = value; }
        public TimeSpan Duration { get => this.Startingdate - DateTime.Now; }
        public DateTime DeliveryDate { get => this.deliverydate; }
        public void Paid()
        {
            this.ispaid = true;
            this.duration = Startingdate - DateTime.Now;
        }
        public void CalculateDistance()
        {
            StreamReader sReader = null;
            try
            {
                sReader = new StreamReader("../../../Distances.csv");
                string line;
                List<List<string>> Path = new List<List<string>>(); 
                while ((line = sReader.ReadLine()) != null)
                {
                    Path.Add(line.Split(',').ToList());

                }
                //var result = FindTheSortestPath(Path);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sReader != null) { sReader.Close(); }
            }
        }
        public static Livraison CreateDeliveryFromInput()
        {
            string[] DeliveryDetails;
            Livraison EnteredDelivery = null;
            do
            {
                Console.WriteLine("Enter the detail of the delivery separte by a ; ( City of departure; City of arrival; Delivery Date )");
                DeliveryDetails = Console.ReadLine().Split(';');
                try
                {
                    EnteredDelivery = ParseFromArrayString(DeliveryDetails);
                    EnteredDelivery.CalculateDistance();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (EnteredDelivery == null);
            return EnteredDelivery;
        }
        
        public static Livraison ParseFromArrayString(String[] ObjectDetails)
        {
            string departure = IConvert.ConvertTo<string>(ObjectDetails[0]);
            string arrival = IConvert.ConvertTo<string>(ObjectDetails[1]);
            DateTime deliverydate = IConvert.ConvertTo<DateTime>(ObjectDetails[2]);
            return new Livraison(departure, arrival, deliverydate);
        }
    }
}
