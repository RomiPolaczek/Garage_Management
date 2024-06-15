namespace GarageLogic;

public class PowerUnit
{   
    private readonly float r_MaxEnergyCapacity;
    private float m_EnergyLeftPercentage;
    private float m_CurrentEnergyAmount;
    
    public PowerUnit (float i_MaxEnergyCapacity)
    {
        r_MaxEnergyCapacity = i_MaxEnergyCapacity;
    }

    public float MaxEnergyCapacity
    {
        get { return r_MaxEnergyCapacity; }
    }  

    public float CurrentEnergyAmount
    {
        get { return m_CurrentEnergyAmount; }
        set { m_CurrentEnergyAmount = value; }
    }      

    public void CalculateEnergyLeftPercentage()
    {
        m_EnergyLeftPercentage = (m_CurrentEnergyAmount / r_MaxEnergyCapacity) * 100;
    }

}