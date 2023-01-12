using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TransConnect
{
    /// <summary>
    /// The class representing a delivery, it contains information like departure, arrival, price, if it is paid, duration, delivery date, distance and delivery id.
    /// It also contains methods to pay for the delivery, create a delivery from user input and parse from a string array.
    /// </summary>
    public class Livraison : IConvert
    {
        /// <summary>
        /// Gets or sets the departure city for the delivery.
        /// </summary>
        public string Departure { get; set; }
        /// <summary>
        /// Gets or sets the arrival city for the delivery.
        /// </summary>
        public string Arrival { get; set; }
        /// <summary>
        /// Gets or sets the price of the delivery.
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the delivery is paid or not.
        /// </summary>
        public bool ispaid { get; set; }
        /// <summary>
        /// private variable that hold the duration of the delivery
        /// </summary>
        private TimeSpan duration;
        /// <summary>
        /// Gets or sets the delivery date for the delivery.
        /// </summary>
        public DateTime Deliverydate { get; set; }
        /// <summary>
        /// Gets or sets the distance of the delivery.
        /// </summary>
        public int Distance { get; set; }
        /// <summary>
        /// private variable that hold the starting date of the delivery.
        /// </summary>
        private DateTime Startingdate;
        /// <summary>
        /// private variable that hold the delivery route.
        /// </summary>
        private List<String> delivery_Route;
        /// <summary>
        /// Gets the delivered product for the delivery.
        /// </summary>
        public string Deliveredproduct{ get; }
        // <summary>
        /// Gets or sets the delivery id for the delivery.
        /// </summary>
        public int DeliveryID { get; set; }
        /// <summary>
        /// Creates a new Livraison object with the specified delivered product, departure location, 
        /// arrival location, and delivery date, and a randomly generated delivery ID.
        /// </summary>
        /// <param name="deliveredproduct">The name of the product being delivered.</param>
        /// <param name="PDepart">The city of departure for the delivery.</param>
        /// <param name="PArrive">The city of arrival for the delivery.</param>
        /// <param name="datelivraison">The date of the delivery.</param>
        public Livraison(string deliveredproduct, string PDepart, string PArrive, DateTime datelivraison)
        {
            this.Deliveredproduct = deliveredproduct;
            this.Departure = PDepart;
            this.Arrival = PArrive;
            this.ispaid = false;
            this.Startingdate = DateTime.Now;
            this.Deliverydate = datelivraison;
            this.DeliveryID = new Random().Next(0, 999999);
        }
        /// <summary>
        /// Creates a new Livraison object with the specified delivered product, departure location, 
        /// arrival location, delivery date, and delivery ID.
        /// </summary>
        /// <param name="deliveredproduct">The name of the product being delivered.</param>
        /// <param name="PDepart">The city of departure for the delivery.</param>
        /// <param name="PArrive">The city of arrival for the delivery.</param>
        /// <param name="datelivraison">The date of the delivery.</param>
        /// <param name="id">The ID of the delivery.</param>
        public Livraison(string deliveredproduct, string PDepart, string PArrive, DateTime datelivraison, int id)
        {
            this.Deliveredproduct = deliveredproduct;
            this.Departure = PDepart;
            this.Arrival = PArrive;
            this.ispaid = false;
            this.Startingdate = DateTime.Now;
            this.Deliverydate = datelivraison;
            this.DeliveryID = id;
        }
        /// <summary>
        /// Gets the duration of the delivery from the starting date to the current date
        /// </summary>
        public TimeSpan Duration { get => this.Startingdate - DateTime.Now; }
        /// <summary>
        /// Sets the paid status of the delivery to true and calculates the duration of the delivery
        /// </summary>
        public void Paid()
        {
            this.ispaid = true;
            this.duration = Startingdate - DateTime.Now;
        }
        // <summary>
        /// Creates a new delivery from user input
        /// </summary>
        /// <returns>The newly created delivery</returns>
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
        /// <summary>
        /// Parses the delivery details from an array of strings
        /// </summary>
        /// <param name="ObjectDetails">The array of strings containing the delivery details</param>
        /// <param name="nbr">A boolean value indicating whether the delivery ID is included in the array</param>
        /// <returns>The parsed delivery</returns>
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
