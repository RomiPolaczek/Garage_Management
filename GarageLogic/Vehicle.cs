namespace GarageLogic;

public class Vehicle
{
    private readonly string r_ModelName;
    private readonly string r_LicenseNumber;
    private List<Wheel> m_Wheels;
    private PowerUnit m_PowerUnit;

    public Vehicle(int numOfWheels, string licenseNumber) //eVehicleType vehicleType)
    {
       m_PowerUnit = new PowerUnit();
       r_ModelName = "mazda";
       r_LicenseNumber = licenseNumber;
       m_Wheels =  new List<Wheel>(numOfWheels);
        for (int i = 0; i < numOfWheels; i++)
        {
        //    m_Wheels.Add(new Wheel("DefaultManufacturer", maxAirPressure));
        }
    }
}
