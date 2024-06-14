namespace GarageLogic;

public class Car : Vehicle
{
    private const int k_NumOfWheels = 5;
    private const int k_MaxAirPressure = 31;
    private const float k_MaxFuelLiters = 45;
    private const float k_MaxBatteryHours = 3.5f;
    private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Octan95;
    private eColor m_Color;
    private eDoorAmount m_DoorAmount;
   
    public enum eDoorAmount
    {
        Two,
        Three,
        Four,
        Five
    }

    public enum eColor
    {
        Yellow,
        White,
        Red,
        Black
    }

    public Car(eColor i_Color, eDoorAmount i_DoorAmount, string i_ModelName, string i_LicenseNumber, string i_ManufacturerName, float i_CurrentAirPressure, float i_CurrentEnergyLeft, eVehicleType i_VehicleType) :
            base(i_ModelName, i_LicenseNumber, k_NumOfWheels, i_ManufacturerName, i_CurrentAirPressure, k_MaxAirPressure)
    {
        m_Color = i_Color;
        m_DoorAmount = i_DoorAmount;
        initialPowerUnit(i_CurrentEnergyLeft, i_VehicleType);
    }

    private void initialPowerUnit(float i_CurrentEnergyLeft, eVehicleType i_VehicleType)
    {
        if(i_VehicleType == eVehicleType.FuelCar)
        {
            powerUnit  = new FuelEngine(k_MaxFuelLiters, i_CurrentEnergyLeft, k_FuelType);
        }
        else
        {
            powerUnit = new ElectricBattery(k_MaxBatteryHours, i_CurrentEnergyLeft);
        }
    }
}
