namespace GarageLogic;

public class Motorcycle : Vehicle
{   
    private const int k_NumOfWheels = 2;
    private const int k_MaxAirPressure = 33;
    private eLicenseType m_LicenseType;
    public enum eLicenseType
    {
        A,
        A1,
        AA,
        B1
    } 

    private int m_EngineCapacity;
}
