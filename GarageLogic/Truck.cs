namespace GarageLogic;

public class Truck : Vehicle
{
    private const int k_NumOfWheels = 12;
    private const int k_MaxAirPressure = 28;
    private const float k_MaxFuelLiters = 120;
    private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Soler;
    private bool m_IsLeadingDangerousMaterial;
    private float m_CargoVolume;

    public Truck(bool i_IsLeadingDangerousMaterial, float i_CargoVolume, string i_ModelName, string i_LicenseNumber, string i_ManufacturerName, float i_CurrentAirPressure, float i_CurrentEnergyLeft) :
        base(i_ModelName, i_LicenseNumber, k_NumOfWheels, i_ManufacturerName, i_CurrentAirPressure, k_MaxAirPressure)
    {
        m_IsLeadingDangerousMaterial = i_IsLeadingDangerousMaterial;
        m_CargoVolume = i_CargoVolume;
        initialPowerUnit(i_CurrentEnergyLeft);
    }

    private void initialPowerUnit(float i_CurrentEnergyLeft)
    {
        powerUnit = new FuelEngine(k_MaxFuelLiters, i_CurrentEnergyLeft, k_FuelType);
    }
}
