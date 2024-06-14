namespace GarageLogic;

public class Wheel
{
    private readonly string r_ManufacturerName;
    private float m_CurrentAirPressure;
    private readonly float r_MaxAirPressure;

    public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
    {
        r_ManufacturerName = i_ManufacturerName;
        m_CurrentAirPressure = i_CurrentAirPressure;
        r_MaxAirPressure = i_MaxAirPressure;
    }

    //public float CurrentAirPressure
    //{
    //    get { return m_CurrentAirPressure; }
    //    set { m_CurrentAirPressure = value; }
    //}
}
