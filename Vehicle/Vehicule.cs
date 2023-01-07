namespace TransConnect
{
    public abstract class Vehicule
    {
        private string model;
        private int id;
        private int nbrplace;
        public Vehicule(string model, int nbrplace)
        {
            this.id = new Random().Next(0, 999999);
            this.model = model;
            this.nbrplace = nbrplace;
        }
        public int Id { get => id; }
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
                    EnteredVehicle = ParseFromArrayString(VehicleDetails);
                    FileStream fileStream = new FileStream("../../../CompanyDetails/VehiclesList.csv", FileMode.Append, FileAccess.Write);

                    sWriter = new StreamWriter(fileStream);
                    sWriter.Write(string.Format("{0};{1};{2};{3}\n", VehicleDetails[0], VehicleDetails[1], VehicleDetails[2], VehicleDetails[3]));

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
        public static Vehicule ParseFromArrayString(string[] ObjectDetails)
        {
            int id = IConvert.ConvertTo<int>(ObjectDetails[0]);
            string Model = IConvert.ConvertTo<string>(ObjectDetails[1]).Trim();
            int Number_of_place = IConvert.ConvertTo<int>(ObjectDetails[2]);
            string Type = (IConvert.ConvertTo<string>(ObjectDetails[3])).Trim();
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
