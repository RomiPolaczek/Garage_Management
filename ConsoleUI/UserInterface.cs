using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using GarageLogic;

namespace ConsoleUI;

public class UserInterface
{ 
    private GarageLogic.Garage m_Garage;

    public UserInterface()
    {
        m_Garage = new Garage();
    }
    public void OpenGarage()
    {
        
        Menu();
    }

    public void Menu()
    {
        string menuOutput = @"Please select one of the following actions:
(1) To add a new vehicle to the garage.
(2) To view the license numbers of all the vehicles in the garage.
(3) To change a vehicle status.
(4) To inflate a vehicle's tires to the maximum.
(5) To refuel a fuel operated vehicle.
(6) To charge an electric vehicle.
(7) To view a specific vehicle full data.
(8) Exit.";
        
        HandleInputValidation(menuOutput, 1, 8, out int userChoiceFromMenu);

        if (userChoiceFromMenu != 2 && userChoiceFromMenu != 8)
        {
            GetLicenseNumber();
            // if(m_Garage.CheckIfVehicleInTheGarage())
            // {
            //     //changestatus
            // }
        }

        ImplementUserChoiceFromMenu(userChoiceFromMenu);
    }

    
    public void GetLicenseNumber()
    {
        Console.WriteLine("Please enter the license number:");
        
        m_Garage.CurrentLicenseNumber = Console.ReadLine();

       
    }

    public void ImplementUserChoiceFromMenu(int i_UserChoiceFromMenu)
    {
        switch(i_UserChoiceFromMenu)
        {
            case 1:
            AddNewVehicle();
            break;
        }
    }

    public void AddNewVehicle()
    {
        // if(m_Garage.CheckIfVehicleInTheGarage())
        // {
        //     //changestatus
        // }

        //name and phone

        getVehicleType();
        //model manifcture amountenergy

        Console.WriteLine("Please enter the model name: ");
        string modelName = Console.ReadLine();

        Console.WriteLine("Please enter the wheels manufacturer name: ");
        string wheelManufacturer = Console.ReadLine();

        //change energy to enum 
        //change the parse to float
        

        m_Garage.AddNewVehicleToTheGarage(modelName, m_Garage.CurrentLicenseNumber, wheelManufacturer);

        GetWheelsAirPressure();
        getRemainingEnergy();
    }

    private void getVehicleType()
    {
        StringBuilder output = CreateScreenOutputVehicleType("Please choose your vehicle type from the following options:");
        int numberofVehicleTypes = Enum.GetNames(typeof(Factory.eVehicleType)).Length;
        HandleInputValidation(output.ToString(), 1, numberofVehicleTypes, out int vehicleType);
        m_Garage.Factory.VehicleType = (Factory.eVehicleType)vehicleType;
    }

    internal StringBuilder CreateScreenOutputVehicleType(string i_Output)
    {
        StringBuilder screenOutput = new StringBuilder(i_Output);
        string[] currentVehicleType;
        int currentTypeNumber = 1;

        screenOutput.Append(Environment.NewLine);
        foreach (string type in Enum.GetNames(typeof(Factory.eVehicleType)))
        {
            currentVehicleType = type.Split('_');
            screenOutput.Append(currentTypeNumber + ". ");
            for (int currentString = 0; currentString < currentVehicleType.Length; ++currentString)
            {
                screenOutput.Append(currentVehicleType[currentString] + " ");
            }

            screenOutput.Append(Environment.NewLine);
            currentTypeNumber++;
        }

        return screenOutput;
    }

    public void getRemainingEnergy()
    {
        string enterRemainingEnergyOutputStr = "Please enter the remaining energy in your vehicle: ";
        Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
        float maxEnergyCapacity = currentVehicle.powerUnit.MaxEnergyCapacity;
        HandleInputValidation(enterRemainingEnergyOutputStr, 0, maxEnergyCapacity, out float currentEnergyAmount);
        currentVehicle.powerUnit.CurrentEnergyAmount = currentEnergyAmount;
        currentVehicle.powerUnit.CalculateEnergyLeftPercentage();
    }

    public void GetWheelsAirPressure()
    {
        string enterAllTheWheelsAtOnceOutputStr = @"Do you want to enter the air pressure of all the wheels at once?
(1) Yes
(2) No";

        HandleInputValidation(enterAllTheWheelsAtOnceOutputStr, 1, 2, out int enterAllTheWheelsAtOnceOutput);

        Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
        float maxAirPressure = currentVehicle.Wheels[0].MaxAirPressure;
        string enterCurrentAirPressureOutputStr;
    
        if(enterAllTheWheelsAtOnceOutput == 1)
        {
            enterCurrentAirPressureOutputStr = "Please enter the current air pressure of the wheels:";
            HandleInputValidation(enterCurrentAirPressureOutputStr, 0, maxAirPressure, out float currentAirPressure);
           
            foreach(Wheel wheel in currentVehicle.Wheels)
            {
               wheel.CurrentAirPressure = currentAirPressure;
            }
        }
        else
        {
            int i = 0;
            foreach(Wheel wheel in currentVehicle.Wheels)
            {
                enterCurrentAirPressureOutputStr = string.Format("Please enter current air pressure in wheel number {0}: ", i + 1);
                HandleInputValidation(enterCurrentAirPressureOutputStr, 0, maxAirPressure, out float currentAirPressure);
                wheel.CurrentAirPressure = currentAirPressure;
                i++;
            }
        }
    }

    public void HandleInputValidation(string i_Output, float i_MinValue, float i_MaxValue, out float io_Input)
    {
        bool isValidInput = false;
        io_Input = 0;
        
        do
        {
            Console.WriteLine(i_Output);
            try
            {
                io_Input = float.Parse(Console.ReadLine());
                isValidInput = true;
                Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
                currentVehicle.CheckIfUserInputIsValid(io_Input, i_MinValue,i_MaxValue, out isValidInput);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(valueOutOfRangeException.Message);
            }
        }
        while (!isValidInput);
    }

    public void HandleInputValidation(string i_Output, int i_MinValue, int i_MaxValue, out int io_Input)
    {
        bool isValidInput = false;
        io_Input = 0;
        
        do
        {
            Console.WriteLine(i_Output);
            try
            {
                io_Input = int.Parse(Console.ReadLine());
                isValidInput = true;
                CheckIfUserInputIsValid(io_Input, i_MinValue,i_MaxValue, out isValidInput);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(valueOutOfRangeException.Message);
            }
        }
        while (!isValidInput);
    }

    public void CheckIfUserInputIsValid(int i_Input, int i_MinValue, int i_MaxValue, out bool io_IsValid)
    {
        io_IsValid = (i_Input >= i_MinValue) && (i_Input <= i_MaxValue);
        if (!io_IsValid)
        {
            throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
        }
    }
}