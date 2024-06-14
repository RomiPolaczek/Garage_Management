namespace GarageLogic;

public class PowerUnit
{   
    private readonly float r_MaxEnergyCapacity;
    private float m_EnergyLeftPercentage;
    private float m_CurrentEnergyAmount;
    
    public PowerUnit (float i_MaxEnergyCapacity, float i_CurrentEnergyAmount)
    {
        r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        m_CurrentEnergyAmount = i_CurrentEnergyAmount;
        m_EnergyLeftPercentage = calculateEnergyLeftPercentage();
    }

    private float calculateEnergyLeftPercentage()
    {
        return ((m_CurrentEnergyAmount / r_MaxEnergyCapacity) * 100);
    }





}