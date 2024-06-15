using System.Runtime.CompilerServices;

namespace GarageLogic;

public abstract class Vehicle
{
    private readonly string r_ModelName;
    private readonly string r_LicenseNumber;
    private PowerUnit m_PowerUnit;
    private List<Wheel> m_Wheels;


    public Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels, string i_ManufacturerName, float i_MaxAirPressure)
    {
        r_ModelName = i_ModelName;
        r_LicenseNumber = i_LicenseNumber;
        InitialWheelsList(i_NumOfWheels, i_ManufacturerName, i_MaxAirPressure);
    }

    public PowerUnit powerUnit
    {
        get { return m_PowerUnit ; }
        set { m_PowerUnit = value ; }
    }

    public List<Wheel> Wheels
    {
        get { return m_Wheels ; }
        set { m_Wheels = value ; }
    }

    public abstract int GetNumOfWheels();

    public void InitialWheelsList(int i_NumOfWheels, string i_ManufacturerName, float i_MaxAirPressure)
    {
        m_Wheels = new List<Wheel>(i_NumOfWheels);

        for (int i = 0; i < i_NumOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(i_ManufacturerName, i_MaxAirPressure));
        }
    }

    public void CheckIfUserInputIsValid(float i_Input, float i_MinValue, float i_MaxValue, out bool io_IsValid)
    {
        io_IsValid = (i_Input >= i_MinValue) && (i_Input <= i_MaxValue);
        if (!io_IsValid)
        {
            throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
        }
    }
}
