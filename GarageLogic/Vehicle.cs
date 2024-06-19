using System.Runtime.CompilerServices;
using System.Text;

namespace GarageLogic;

public abstract class Vehicle
{
    private readonly string r_ModelName;
    private readonly string r_LicenseNumber;
    private readonly Owner m_Owner;
    private PowerUnit m_PowerUnit;
    private List<Wheel> m_Wheels;
    protected List<string> m_SpecificData;
    private Garage.eStatus m_Status;

    public Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels, float i_MaxAirPressure, Owner i_Owner)
    {
        r_ModelName = i_ModelName;
        r_LicenseNumber = i_LicenseNumber;
        InitialWheelsList(i_NumOfWheels, i_MaxAirPressure);
        m_SpecificData = new List<string>();
        m_Owner = i_Owner;
        m_Status = Garage.eStatus.In_Repair;
    }

    public PowerUnit PowerUnit
    {
        get { return m_PowerUnit; }
        set { m_PowerUnit = value; }
    }

    public Garage.eStatus Status
    {
        get { return m_Status; }
        set { m_Status = value; }
    }

    public List<Wheel> Wheels
    {
        get { return m_Wheels; }
        set { m_Wheels = value; }
    }

    public List<string> SpecificData
    {
        get { return m_SpecificData; }
        set { m_SpecificData = value; }
    }

    public void InitialWheelsList(int i_NumOfWheels, float i_MaxAirPressure)
    {
        m_Wheels = new List<Wheel>(i_NumOfWheels);

        for (int i = 0; i < i_NumOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(i_MaxAirPressure));
        }
    }

    public abstract void CheckValidationForSpecificData(string i_Input, int i_StringIndex, out bool o_IsValidInput);

    public void CheckIfUserInputIsValid(float i_Input, float i_MinValue, float i_MaxValue, out bool io_IsValid)
    {
        io_IsValid = (i_Input >= i_MinValue) && (i_Input <= i_MaxValue);
        if (!io_IsValid)
        {
            throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
        }
    }

    public void CheckIfUserInputIsValid(int i_Input, int i_MinValue, int i_MaxValue, out bool io_IsValid)
    {
        io_IsValid = (i_Input >= i_MinValue) && (i_Input <= i_MaxValue);
        if (!io_IsValid)
        {
            throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
        }
    }

    public override string ToString()
    {
        StringBuilder vehicleDataStr = new StringBuilder(string.Format("License number: {0}{1}Model name: {2}{3}Owner: {4}{5}Status: {6}{7}",
            r_LicenseNumber, Environment.NewLine, r_ModelName, Environment.NewLine, m_Owner.Name, Environment.NewLine, m_Status, Environment.NewLine));

        int currentWheel = 1;
        foreach (Wheel wheel in m_Wheels)
        {
            vehicleDataStr.Append(string.Format("Wheel number {0}:", currentWheel));
            vehicleDataStr.Append(wheel.ToString());
            currentWheel++;
        }

        if (m_PowerUnit is FuelEngine)
        {
            FuelEngine currentPowerUnit = m_PowerUnit as FuelEngine;
            vehicleDataStr.Append(string.Format("Fuel type: {0}{1}Remaining fuel in liters: {2}{3}Percentage of fuel remaining: {4}{5}",
               currentPowerUnit.FuelType, Environment.NewLine, currentPowerUnit.CurrentEnergyAmount, Environment.NewLine, currentPowerUnit.EnergyLeftPercentage, Environment.NewLine));
        }
        else
        {
            ElectricBattery currentPowerUnit = m_PowerUnit as ElectricBattery;
            vehicleDataStr.Append(string.Format("Remaining hours in battery: {0}{1}Percentage of remaining charge: {2}{3}",
              currentPowerUnit.CurrentEnergyAmount, Environment.NewLine, currentPowerUnit.EnergyLeftPercentage, Environment.NewLine));
        }

        return vehicleDataStr.ToString();   
    }
}
