using System.Drawing;
using static GarageLogic.Car;

namespace GarageLogic;

public class Motorcycle : Vehicle
{
    private const int k_NumOfWheels = 2;
    private const int k_MaxAirPressure = 33;
    private const float k_MaxFuelLiters = 5.5f;
    private const float k_MaxBatteryHours = 2.5f;
    private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Octan98;
    private float m_EngineCapacity;
    private eLicenseType m_LicenseType;
    public enum eLicenseType
    {
        A = 1,
        A1,
        AA,
        B1
    }


    public Motorcycle(string i_ModelName, string i_LicenseNumber, Owner i_Owner, Factory.eVehicleType i_VehicleType) :
        base(i_ModelName, i_LicenseNumber, k_NumOfWheels, k_MaxAirPressure, i_Owner)
    {
        initialPowerUnit(i_VehicleType);
        InitialSpecificDataString();
    }

    private void initialPowerUnit(Factory.eVehicleType i_VehicleType)
    {
        if(i_VehicleType == Factory.eVehicleType.Fuel_Motorcycle)
        {
            PowerUnit = new FuelEngine(k_MaxFuelLiters, k_FuelType);
        }
        else
        {
            PowerUnit = new ElectricBattery(k_MaxBatteryHours);
        }
    }

    public void InitialSpecificDataString()
    {
        string output = @"Please enter the engine capacity:";
        m_SpecificData.Add(output);

        output = @"Please choose license type:
(1) A
(2) A1
(3) AA
(4) B1";
        m_SpecificData.Add(output);
    }

    public override void CheckValidationForSpecificData(string i_Input, int i_StringIndex, out bool o_IsValidInput)
    {   
        if(i_StringIndex == 0)
        {
            float engineCapacityInput = float.Parse(i_Input);
            o_IsValidInput = engineCapacityInput >= 0;
            if(!o_IsValidInput)
            {
                throw new ArgumentException("Invalid input. The engine capacity has to be non-negative.");
            }
            m_EngineCapacity = engineCapacityInput;
        }
        else
        {
            o_IsValidInput = false;
            int licenseTypeInput = int.Parse(i_Input);
            CheckIfUserInputIsValid(licenseTypeInput, 1, 4, out o_IsValidInput);
            m_LicenseType = (eLicenseType)licenseTypeInput;
        }
    }

    public override string ToString()
    {
        return string.Format("{0}License type: {1}{2}Engine capacity: {3}{4}", base.ToString(), m_LicenseType, Environment.NewLine, m_EngineCapacity, Environment.NewLine);
    }

}
