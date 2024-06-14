namespace GarageLogic;

public class Truck : Vehicle
{
    private const int k_NumOfWheels = 12;
    private const int k_MaxAirPressure = 28;
    private bool m_IsLeadingDangerousMaterial;
    private float m_CargoVolume;

    public Truck(float cargoVolume, bool isLeadingDangerous) 
    base: (int numOfWheels, string licenseNumber, eVehicleType vehicleType)
    {
        m_CargoVolume = cargoVolume;
        m_IsLeadingDangerousMaterial = isLeadingDangerous;
    }   
}
