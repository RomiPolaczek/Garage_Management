namespace GarageLogic;

public class Truck : Vehicle
{
    private const int k_NumOfWheels = 12;
    private const int k_MaxAirPressure = 28;
    private const float k_MaxFuelLiters = 120;
    private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Soler;
    private bool m_IsLeadingDangerousMaterial;
    private float m_CargoVolume;


    public Truck(string i_ModelName, string i_LicenseNumber, string i_ManufacturerName) :
        base(i_ModelName, i_LicenseNumber, k_NumOfWheels, i_ManufacturerName, k_MaxAirPressure)
    {
        // m_IsLeadingDangerousMaterial = i_IsLeadingDangerousMaterial;
        // m_CargoVolume = i_CargoVolume;
        initialPowerUnit();
    }

    public override int GetNumOfWheels()
    {
        return  k_NumOfWheels; 
    }

    void initialPowerUnit()
    {
        powerUnit = new FuelEngine(k_MaxFuelLiters, k_FuelType);
    }

    //bool i_IsLeadingDangerousMaterial, float i_CargoVolume, 
}
