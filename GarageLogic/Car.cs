namespace GarageLogic;

public class Car : Vehicle
{
    private const int k_NumOfWheels = 5;
    private const int k_MaxAirPressure = 31;



    private eColor m_Color;
    private eDoorAmount m_DoorAmount;
    public enum eDoorAmount
    {
        Two,
        Three,
        Four,
        Five
    } 
    public enum eColor
    {
        Yellow,
        White,
        Red,
        Black
    } 


}
