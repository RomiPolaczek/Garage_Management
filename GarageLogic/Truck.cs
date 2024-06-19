namespace GarageLogic;

public class Truck : Vehicle
{
    private const int k_NumOfWheels = 12;
    private const int k_MaxAirPressure = 28;
    private const float k_MaxFuelLiters = 120;
    private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Soler;
    private float m_CargoVolume;
    private bool m_IsLeadingDangerousMaterial;

    public Truck(string i_ModelName, string i_LicenseNumber, Owner i_Owner) :
        base(i_ModelName, i_LicenseNumber, k_NumOfWheels, k_MaxAirPressure, i_Owner)
    {
        initialPowerUnit();
        initialSpecificDataString();
    }

    protected float CargoVolume
    {
        get { return m_CargoVolume; }
        set { m_CargoVolume = value; }
    }   

    protected bool IsLeadingDangerousMaterial
    {
        get { return m_IsLeadingDangerousMaterial; }
        set { m_IsLeadingDangerousMaterial = value; }
    }    

    private void initialPowerUnit()
    {
        PowerUnit = new FuelEngine(k_MaxFuelLiters, k_FuelType);
    }

    public void initialSpecificDataString()
    {
        string output = "Please enter the cargo volume:";
        m_SpecificData.Add(output);


        output = @"Does your truck leading dangerous material?
(1) Yes
(2) No";
        m_SpecificData.Add(output);
    }

    public override void CheckValidationForSpecificData(string i_Input, int i_StringIndex, out bool o_IsValidInput)
    {   
        if(i_StringIndex == 0)
        {
            float cargoVolumeInput = float.Parse(i_Input);
            o_IsValidInput = cargoVolumeInput >= 0;
            if(!o_IsValidInput)
            {
                throw new ArgumentException("Invalid input. The cargo volume has to be non-negative.");
            }
            m_CargoVolume = cargoVolumeInput;
        }
        else
        {
            o_IsValidInput = false;
            int dangerousMaterialInput = int.Parse(i_Input);
            CheckIfUserInputIsValid(dangerousMaterialInput, 1, 2, out o_IsValidInput);
            checkIfLeadingDangerousMaterialFromUserInput(dangerousMaterialInput);
        }
    }

    private void checkIfLeadingDangerousMaterialFromUserInput(int i_Input)
    {
        switch (i_Input)
        {
            case 1:
                m_IsLeadingDangerousMaterial = true;
                break;
            case 2:
                m_IsLeadingDangerousMaterial = false;
                break;
        }
    }

    public override string ToString()
    {
        string truckData = string.Format("{0}Cargo volume: {1}", base.ToString(), m_CargoVolume);
        if(m_IsLeadingDangerousMaterial)
        {
            truckData += "\nLeading dangerous materials";
        }
        else
        {
            truckData += "\nIs not leading dangerous materials";
        }
        return truckData;
    }
}
