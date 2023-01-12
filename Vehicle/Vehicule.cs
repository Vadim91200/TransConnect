namespace TransConnect
{
    /// <summary>
    /// The Vehicule class is an abstract class that represents a vehicle.
    /// </summary>
    public abstract class Vehicule
    {
        private string model;
        public int Id { get; set; }
        private int nbrplace;
        /// <summary>
        /// Creates a new instance of the Vehicule class.
        /// </summary>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="nbrplace">The number of seats in the vehicle.</param>
        public Vehicule(string model, int nbrplace)
        {
            this.Id = new Random().Next(0, 999999);
            this.model = model;
            this.nbrplace = nbrplace;
        }
 
        /// <summary>
        /// Returns a string representation of the vehicle, including the model and number of seats.
        /// </summary>
        /// <returns>A string that includes the model and number of seats of the vehicle</returns>
        public override string ToString()
        {
            return this.model + " With " + this.nbrplace + " place ";
        }
        /// <summary>
        /// Creates a new vehicle from input given by the user and append it to the file called "../../../CompanyDetails/VehiclesList.csv"
        /// </summary>
        /// <returns> A new instance of the vehicle</returns>
        public static Vehicule CreateVehicleFromInput()
        {
            string[] VehicleDetails;
            Vehicule EnteredVehicle = null;
            do
            {
                Console.WriteLine("Enter the Vehicle detail separte by a ; (Model; Number of place; Type; (Type Details)");
                VehicleDetails = Console.ReadLine().Split(';');
                StreamWriter sWriter = null;
                try
                {
                    EnteredVehicle = ParseFromArrayString(VehicleDetails, false);
                    FileStream fileStream = new FileStream("../../../CompanyDetails/VehiclesList.csv", FileMode.Append, FileAccess.Write);

                    sWriter = new StreamWriter(fileStream);
                    sWriter.Write(string.Format("{0};{1};{2};{3}\n", EnteredVehicle.Id, VehicleDetails[0], VehicleDetails[1], VehicleDetails[2]));

                }
                catch (Exception e)
                {
                    Console.WriteLine("Un error occured while trying to add Vehicle in a file");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (sWriter != null) sWriter.Close();
                }
            } while (EnteredVehicle == null);
            return EnteredVehicle;
        }
        /// <summary>
        /// Create an instance of the vehicle from an array of strings
        /// </summary>
        /// <param name="ObjectDetails">Array of string that represent the vehicle details</param>
        /// <param name="nbr">A boolean indicating whether or not the vehicle has an ID. If true, the first element in the array is assumed to be the ID.</param>
        /// <returns>An instance of the appropriate vehicle subtype, or null if the type specified in the input is not recognized.</returns>
        public static Vehicule ParseFromArrayString(string[] ObjectDetails, bool nbr)
        {
            if (nbr)
            {
                int id = IConvert.ConvertTo<int>(ObjectDetails[0]);
                string Model = IConvert.ConvertTo<string>(ObjectDetails[1]).Trim();
                int Number_of_place = IConvert.ConvertTo<int>(ObjectDetails[2]);
                string Type = (IConvert.ConvertTo<string>(ObjectDetails[3])).Trim();

                if (Type.ToUpper() == "VOITURE")
                {
                    Vehicule v = new Voiture(Model, Number_of_place);
                    v.Id = id;
                    return v;
                }
                else if (Type.ToUpper() == "CAMIONNETTE")
                {
                    string Usage = (IConvert.ConvertTo<string>(ObjectDetails[4])).Trim();
                    Vehicule v =  new Camionnette(Model, Number_of_place, Usage);
                    v.Id = id;
                    return v;
                }
                else if (Type.ToUpper() == "CAMMIONCITERNE")
                {
                    Cuve Cuve = IConvert.ConvertTo<Cuve>(ObjectDetails[4]);
                    Vehicule v = new Camion_citerne(Model, Number_of_place, "Citerne", Cuve);
                    v.Id = id;
                    return v;
                }
                else if (Type.ToUpper() == "CAMMIONFRIGORIFIQUE")
                {
                    Vehicule v = new Camion_frigorifique(Model, Number_of_place, "Frigorifique");
                    v.Id = id;
                    return v;
                }
                else if (Type.ToUpper() == "CAMMIONBENNE")
                {
                    Vehicule v = new Camion_benne(Model, Number_of_place, "Benne");
                    v.Id = id;
                    return v;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                string Model = IConvert.ConvertTo<string>(ObjectDetails[0]).Trim();
                int Number_of_place = IConvert.ConvertTo<int>(ObjectDetails[1]);
                string Type = (IConvert.ConvertTo<string>(ObjectDetails[2])).Trim();

                if (Type.ToUpper() == "VOITURE")
                {
                    return new Voiture(Model, Number_of_place);
                }
                else if (Type.ToUpper() == "CAMIONNETTE")
                {
                    string Usage = (IConvert.ConvertTo<string>(ObjectDetails[4])).Trim();
                    return new Camionnette(Model, Number_of_place, Usage);
                }
                else if (Type.ToUpper() == "CAMMIONCITERNE")
                {
                    Cuve Cuve = IConvert.ConvertTo<Cuve>(ObjectDetails[4]);
                    return new Camion_citerne(Model, Number_of_place, "Citerne", Cuve);
                }
                else if (Type.ToUpper() == "CAMMIONFRIGORIFIQUE")
                {
                    return new Camion_frigorifique(Model, Number_of_place, "Frigorifique");
                }
                else if (Type.ToUpper() == "CAMMIONBENNE")
                {
                    return new Camion_benne(Model, Number_of_place, "Benne");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
