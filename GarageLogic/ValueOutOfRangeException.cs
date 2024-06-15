namespace GarageLogic;

public class ValueOutOfRangeException : Exception
{
    private float m_MaxValue;
    private float m_MinValue;

    public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) :
    base(string.Format("Invalid input. Please enter input in the range {0}-{1}.", i_MinValue, i_MaxValue))
    {
        m_MinValue = i_MinValue;
        m_MaxValue = i_MaxValue;
    }

    public float MaxValue
    {
        get { return m_MaxValue ; }
        set { m_MaxValue = value ; }
    }

    public float MinValue
    {
        get { return m_MinValue ; }
        set { m_MinValue = value ; }
    }
}