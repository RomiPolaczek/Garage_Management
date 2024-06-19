namespace GarageLogic;

public class FuelEngine : PowerUnit
{
    private eFuelType m_FuelType;
    public enum eFuelType
    {
        Soler = 1,
        Octan95,
        Octan96, 
        Octan98
    } 

    public eFuelType FuelType
    {
        get { return m_FuelType; }
    }

    public FuelEngine (float i_MaxEnergyCapacity, eFuelType i_FuelType) :
        base(i_MaxEnergyCapacity)
    {
        m_FuelType = i_FuelType;
    }
}
