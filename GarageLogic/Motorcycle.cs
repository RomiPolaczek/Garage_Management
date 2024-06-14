using static GarageLogic.Car;

namespace GarageLogic;

public class Motorcycle : Vehicle
{
    private const int k_NumOfWheels = 2;
    private const int k_MaxAirPressure = 33;
    private const float k_MaxFuelLiters = 5.5f;
    private const float k_MaxBatteryHours = 2.5f;
    private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Octan98;
    private float m_EngineVolume;
    private eLicenseType m_LicenseType;
    public enum eLicenseType
    {
        A,
        A1,
        AA,
        B1
    }

    public Motorcycle(float i_EngineVolume, eLicenseType i_LicenseType, string i_ModelName, string i_LicenseNumber, string i_ManufacturerName, float i_CurrentAirPressure, float i_CurrentEnergyLeft, eVehicleType i_VehicleType) :
           base(i_ModelName, i_LicenseNumber, k_NumOfWheels, i_ManufacturerName, i_CurrentAirPressure, k_MaxAirPressure)
    {
        m_EngineVolume = i_EngineVolume;
        m_LicenseType = i_LicenseType;
        initialPowerUnit(i_CurrentEnergyLeft, i_VehicleType);
    }

    private void initialPowerUnit(float i_CurrentEnergyLeft, eVehicleType i_VehicleType)
    {
        if (i_VehicleType == eVehicleType.FuelCar)
        {
            powerUnit = new FuelEngine(k_MaxFuelLiters, i_CurrentEnergyLeft, k_FuelType);
        }
        else
        {
            powerUnit = new ElectricBattery(k_MaxBatteryHours, i_CurrentEnergyLeft);
        }
    }
}
