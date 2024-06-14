namespace GarageLogic;

public class Vehicle
{
    private readonly string r_ModelName;
    private readonly string r_LicenseNumber;
    private PowerUnit m_PowerUnit;
    private List<Wheel> m_Wheels;


    public Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumOfWheels, string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
    {
        r_ModelName = i_ModelName;
        r_LicenseNumber = i_LicenseNumber;
        initialWheelsList(i_NumOfWheels, i_CurrentAirPressure, i_ManufacturerName, i_MaxAirPressure);
    }

    public PowerUnit powerUnit
    {
        get { return m_PowerUnit ; }
        set { m_PowerUnit = value ; }
    }

    private void initialWheelsList(int i_NumOfWheels, float i_CurrentAirPressure, string i_ManufacturerName, float i_MaxAirPressure)
    {
        m_Wheels = new List<Wheel>(i_NumOfWheels);
        for (int i = 0; i < i_NumOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(i_ManufacturerName, i_CurrentAirPressure, i_MaxAirPressure));
        }
    }
}
