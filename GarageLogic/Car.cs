namespace GarageLogic;

public class Car : Vehicle
{
    private const int k_NumOfWheels = 5;
    private const int k_MaxAirPressure = 31;
    private const float k_MaxFuelLiters = 45;
    private const float k_MaxBatteryHours = 3.5f;
    private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Octan95;
    private eColor m_Color;
    private eDoorAmount m_DoorAmount;
   
    public enum eDoorAmount
    {
        Two = 2,
        Three,
        Four,
        Five
    }

    public enum eColor
    {
        Yellow = 1,
        White,
        Red,
        Black
    }

    public Car(string i_ModelName, string i_LicenseNumber, Owner i_Owner, Factory.eVehicleType i_VehicleType) :
        base(i_ModelName, i_LicenseNumber, k_NumOfWheels, k_MaxAirPressure, i_Owner)
    {
        initialPowerUnit(i_VehicleType);
        initialSpecificDataString();
    }

    private void initialPowerUnit(Factory.eVehicleType i_VehicleType)
    {
        if(i_VehicleType == Factory.eVehicleType.Fuel_Car)
        {
            PowerUnit = new FuelEngine(k_MaxFuelLiters, k_FuelType);
        }
        else
        {
            PowerUnit = new ElectricBattery(k_MaxBatteryHours);
        }
    }

    private void initialSpecificDataString()
    {
        string output = @"Please choose the car color:
(1) Yellow
(2) White
(3) Red
(4) Black";
        m_SpecificData.Add(output);

        output = @"Please choose the number of doors:
(2) Two
(3) Three
(4) Four
(5) Five";
        m_SpecificData.Add(output);
    }

    public override void CheckValidationForSpecificData(string i_Input, int i_StringIndex, out bool o_IsValidInput)
    {   
        if(i_StringIndex == 0)
        {
            int colorInput = int.Parse(i_Input);
            CheckIfUserInputIsValid(colorInput, 1, 4, out o_IsValidInput);
            m_Color = (eColor)colorInput;
        }
        else
        {
            o_IsValidInput = false;
            int numOfDoorsInput = int.Parse(i_Input);
            CheckIfUserInputIsValid(numOfDoorsInput, 2, 5, out o_IsValidInput);
            m_DoorAmount = (eDoorAmount)numOfDoorsInput;
        }
    }

    public override string ToString()
    {
        return string.Format("{0}Color: {1}{2}Number of doors: {3}{4}", base.ToString(), m_Color, Environment.NewLine, m_DoorAmount, Environment.NewLine);
    }
}
