using System.Runtime.CompilerServices;

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

    public Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels, string i_ManufacturerName, float i_MaxAirPressure, Owner i_Owner)
    {
        r_ModelName = i_ModelName;
        r_LicenseNumber = i_LicenseNumber;
        InitialWheelsList(i_NumOfWheels, i_ManufacturerName, i_MaxAirPressure);
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

    public abstract int GetNumOfWheels();

    public void InitialWheelsList(int i_NumOfWheels, string i_ManufacturerName, float i_MaxAirPressure)
    {
        m_Wheels = new List<Wheel>(i_NumOfWheels);

        for (int i = 0; i < i_NumOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(i_ManufacturerName, i_MaxAirPressure));
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

    
}
