namespace GarageLogic;

public class Wheel
{
    private readonly string r_ManufacturerName;
    private float m_CurrentAirPressure;
    private readonly float r_MaxAirPressure;

    public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
    {
        r_ManufacturerName = i_ManufacturerName;
        r_MaxAirPressure = i_MaxAirPressure;
    }

    public float CurrentAirPressure
    {
       get { return m_CurrentAirPressure; }
       set { m_CurrentAirPressure = value; }
    }

    public float MaxAirPressure
    {
       get { return r_MaxAirPressure; }
       set { MaxAirPressure = value; }
    }
}
