using System.Collections;
using System.Reflection;
using System.Text;
using System.Threading;
using GarageLogic;

namespace ConsoleUI;

public class UserInterface
{ 
    private GarageLogic.Garage m_Garage;
    private bool m_StillInGarage;
    private int m_CurrentUserChoiceFromMenu;

    public UserInterface()
    {
        m_Garage = new Garage();
        m_StillInGarage = true;
    }

    public void OpenGarage()
    {
        while(m_StillInGarage)
        {
            Menu();
            GarageAction();
        }
    }

    public void Menu()
    {
        Console.Clear();
        string menuOutput = @"Please select one of the following actions:
(1) To add a new vehicle to the garage.
(2) To view the license numbers of all the vehicles in the garage.
(3) To change a vehicle status.
(4) To inflate a vehicle's tires to the maximum.
(5) To refuel a fuel operated vehicle.
(6) To charge an electric vehicle.
(7) To view a specific vehicle full data.
(8) Exit.";
        
        HandleInputValidation(menuOutput, 1, 8, out m_CurrentUserChoiceFromMenu);
    }

    public void GarageAction()
    {
        if (m_CurrentUserChoiceFromMenu != 2 && m_CurrentUserChoiceFromMenu != 8)
        {
            GetLicenseNumber();
        }
        ImplementUserChoiceFromMenu();
    }

    public void GetLicenseNumber()
    {
        string licenseNumber;
        getName("license number", out licenseNumber);
        m_Garage.CurrentLicenseNumber = licenseNumber;
    } 

    public void ImplementUserChoiceFromMenu()
    {
        switch(m_CurrentUserChoiceFromMenu)
        {
            case 1:
            AddNewVehicle();
            break;
        }
    }

    public void AddNewVehicle()
    {
        if(m_Garage.CheckIfVehicleInTheGarage())
        {
            m_Garage.changeStatus(Garage.eStatus.In_Repair);
            string vehicleInGarageMessage = string.Format("The vehicle with number license {0} is already in the garage.\nChanging vehicle status to in repair.", m_Garage.CurrentLicenseNumber);
            Console.WriteLine(vehicleInGarageMessage);
            Thread.Sleep(2000);
        }
        else
        {
            Owner newOwner = getOwnerDetails();
            getVehicleType();

            string modelName;
            getName("model name", out modelName);

            //change energy to enum     

            m_Garage.AddNewVehicleToTheGarage(modelName, m_Garage.CurrentLicenseNumber, newOwner);

            GetWheelsData();
            getRemainingEnergy();
            GetSpecificData();
        }
    }

    private Owner getOwnerDetails()
    {
        Owner newOwner = new Owner();
        bool isValidName = false;
        bool isValidPhoneNumber = false;

        do
        {
            try
            {
                Console.WriteLine("Please enter your name: ");
                newOwner.Name = Console.ReadLine();
                isValidName = true;
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
                isValidName = false;
            }
        }
        while (!isValidName);

        do
        {
            try
            {
                Console.WriteLine("Please enter your phone number: ");
                newOwner.phoneNumber = Console.ReadLine();
                isValidPhoneNumber = true;
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
                isValidPhoneNumber = false;
            }
        }
        while (!isValidPhoneNumber);

        return newOwner;  
    }

    private void getName(string stringName, out string o_StringInput)
    {
        o_StringInput = string.Empty;
        bool isValidName = false;

        do
        {
            try
            {
                Console.WriteLine("Please enter the {0}: ", stringName);
                o_StringInput = Console.ReadLine();
                isValidName = true;
                if(string.IsNullOrWhiteSpace(o_StringInput))
                {
                    isValidName = false;
                    string exceptionMessage = string.Format("The {0} cannot be empty or whitespace.", stringName);
                    throw new ArgumentException(exceptionMessage);
                }
            }
            catch(ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
        }
        while(!isValidName);
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
        float maxEnergyCapacity = currentVehicle.PowerUnit.MaxEnergyCapacity;
        HandleInputValidation(enterRemainingEnergyOutputStr, 0, maxEnergyCapacity, out float currentEnergyAmount);
        currentVehicle.PowerUnit.CurrentEnergyAmount = currentEnergyAmount;
        currentVehicle.PowerUnit.CalculateEnergyLeftPercentage();
    }

    public void GetWheelsData()
    {
        string enterAllTheWheelsAtOnceOutputStr = @"Do you want to enter the air pressure and manufacturer's name of all the wheels at once?
(1) Yes
(2) No";

        HandleInputValidation(enterAllTheWheelsAtOnceOutputStr, 1, 2, out int enterAllTheWheelsAtOnceOutput);

        Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
        float maxAirPressure = currentVehicle.Wheels[0].MaxAirPressure;
        string enterCurrentAirPressureOutputStr;
        string enterManufacturerNameOutputStr;
        string manufacturerName;
    
        if(enterAllTheWheelsAtOnceOutput == 1)
        {
            enterCurrentAirPressureOutputStr = "Please enter the current air pressure of the wheels:";
            enterManufacturerNameOutputStr = "wheels' manufacturer name";

            HandleInputValidation(enterCurrentAirPressureOutputStr, 0, maxAirPressure, out float currentAirPressure);
            getName(enterManufacturerNameOutputStr, out manufacturerName);
           
            foreach(Wheel wheel in currentVehicle.Wheels)
            {
                wheel.CurrentAirPressure = currentAirPressure;
                wheel.ManufacturerName = manufacturerName;
            }
        }
        else
        {
            int i = 0;
            foreach(Wheel wheel in currentVehicle.Wheels)
            {
                enterCurrentAirPressureOutputStr = string.Format("Please enter current air pressure in wheel number {0}: ", i + 1);
                enterManufacturerNameOutputStr = string.Format("wheel's manufacturer name in wheel number {0}", i + 1);
                HandleInputValidation(enterCurrentAirPressureOutputStr, 0, maxAirPressure, out float currentAirPressure);
                getName(enterManufacturerNameOutputStr, out manufacturerName);

                wheel.CurrentAirPressure = currentAirPressure;
                wheel.ManufacturerName = manufacturerName;
                i++;
            }
        }
    }

    public void GetSpecificData()
    {
        int index = 0;
        bool isValidInput = false;
        Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
        foreach(string specificDataString in currentVehicle.SpecificData)
        {
            do{
                Console.WriteLine(specificDataString);
                string inputSpecificData = Console.ReadLine();
                try
                {
                    currentVehicle.CheckValidationForSpecificData(inputSpecificData, index , out isValidInput);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.WriteLine(valueOutOfRangeException.Message);
                }
            }
            while(!isValidInput);
            index++;
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