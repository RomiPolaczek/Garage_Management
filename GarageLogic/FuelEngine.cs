namespace GarageLogic;

public class FuelEngine : PowerUnit
{
    public enum eFuelType
    {
        Soler,
        Octan95,
        Octan96, 
        Octan98
    } 

    public FuelEngine (float i_MaxEnergyCapacity, float i_CurrentEnergyAmount, eFuelType i_FuelType) :
        base(i_MaxEnergyCapacity, i_CurrentEnergyAmount)
    {

    }
}
