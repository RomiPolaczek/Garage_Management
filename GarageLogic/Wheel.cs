namespace GarageLogic;

public class Wheel
{
    private string m_ManufacturerName;
    private float m_CurrentAirPressure;
    private readonly float r_MaxAirPressure;

    public Wheel(float i_MaxAirPressure)
    {
        r_MaxAirPressure = i_MaxAirPressure;
    }

    public string ManufacturerName
    {
       get { return m_ManufacturerName; }
       set { m_ManufacturerName = value; }
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

    public override string ToString()
    {
        return string.Format("{0}Manufacturer's name: {1}{2}Air pressure: {3}{4}",
           Environment.NewLine, m_ManufacturerName, Environment.NewLine, m_CurrentAirPressure, Environment.NewLine);
    }
}
