using System.Text;
using System.Threading;
using GarageLogic;

namespace ConsoleUI;

public class UserInterface
{ 
    private GarageLogic.Garage m_Garage;
    private bool m_StillInGarage;
    private float m_CurrentUserChoiceFromMenu;

    public UserInterface()
    {
        m_Garage = new Garage();
        m_StillInGarage = true;
    }

    public void OpenGarage()
    {
        while(m_StillInGarage)
        {
            menu();
            garageAction();
        }
    }

    private void menu()
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
        
        handleInputValidation("int", menuOutput, 1, 8, out m_CurrentUserChoiceFromMenu);
    }

    private void garageAction()
    {
        bool goBackToMenu = false;

        Console.Clear();

        if (m_CurrentUserChoiceFromMenu != 2 && m_CurrentUserChoiceFromMenu != 8)
        {
            getLicenseNumber(out goBackToMenu);
        }

        if(!goBackToMenu)
        {
            implementUserChoiceFromMenu();
        }
    }

    private void getLicenseNumber(out bool o_GoBackToMenu)
    {
        string licenseNumber;
        o_GoBackToMenu = true;
        
        getName("license number (or Q to return to the menu)", out licenseNumber);

        if(licenseNumber != "Q")
        {
            o_GoBackToMenu = false;
            m_Garage.CurrentLicenseNumber = licenseNumber;
        }
    } 

    private void implementUserChoiceFromMenu()
    {
        switch(m_CurrentUserChoiceFromMenu)
        {
            case 1:
            addNewVehicle();
            break;
            case 2:
            displayLicenseNumberWithFilterOption();
            break;
            case 3:
            changeVehicleStatusAccordingNewStatus();
            break;
            case 4:
            inflateWheelsToMaxAirPressure();
            break;
            case 5:
            refuelVehicleAccordingToUserInput();
            break;
            case 6:
            chargeElectricVehicle();
            break;
            case 7:
            displayAllVehicleData();
            break;
            case 8:
            m_StillInGarage = false;
            break;
        }
    }

    private void addNewVehicle()
    {
        try
        {
            m_Garage.CheckIfVehicleInTheGarage();
            m_Garage.changeStatus(Garage.eStatus.In_Repair);
            string vehicleInGarageMessage = string.Format("The vehicle with number license {0} is already in the garage.\nChanging vehicle status to in repair.", m_Garage.CurrentLicenseNumber);
            Console.WriteLine(vehicleInGarageMessage);
            Thread.Sleep(3000);
        }
        catch(ArgumentException argumentException)
        {
            Owner newOwner = getOwnerDetails();

            getVehicleType();

            string modelName;
            getName("model name", out modelName);

            m_Garage.AddNewVehicleToTheGarage(modelName, m_Garage.CurrentLicenseNumber, newOwner);

            getWheelsData();
            getRemainingEnergy();
            getSpecificData();
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
        StringBuilder output = createScreenOutputVehicleType("Please choose your vehicle type from the following options:");
        int numberofVehicleTypes = Enum.GetNames(typeof(Factory.eVehicleType)).Length;

        handleInputValidation("int", output.ToString(), 1, numberofVehicleTypes, out float vehicleType);

        m_Garage.Factory.VehicleType = (Factory.eVehicleType)vehicleType;
    }

    private StringBuilder createScreenOutputVehicleType(string i_Output)
    {
        StringBuilder screenOutput = new StringBuilder(i_Output);
        string[] currentVehicleType;
        int currentTypeNumber = 1;

        foreach (string type in Enum.GetNames(typeof(Factory.eVehicleType)))
        {
            screenOutput.Append(Environment.NewLine);
            currentVehicleType = type.Split('_');
            screenOutput.Append(currentTypeNumber + ". ");

            for (int currentString = 0; currentString < currentVehicleType.Length; ++currentString)
            {
                screenOutput.Append(currentVehicleType[currentString] + " ");
            }

            currentTypeNumber++;
        }

        return screenOutput;
    }

    private void getRemainingEnergy()
    {
        string enterRemainingEnergyOutputStr = "Please enter the remaining energy in your vehicle: ";
        Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
        float maxEnergyCapacity = currentVehicle.PowerUnit.MaxEnergyCapacity;

        handleInputValidation("float", enterRemainingEnergyOutputStr, 0, maxEnergyCapacity, out float currentEnergyAmount);

        currentVehicle.PowerUnit.CurrentEnergyAmount = currentEnergyAmount;

        currentVehicle.PowerUnit.CalculateEnergyLeftPercentage();
    }

    private void getWheelsData()
    {
        string enterAllTheWheelsAtOnceOutputStr = @"Do you want to enter the air pressure and manufacturer's name of all the wheels at once?
(1) Yes
(2) No";

        handleInputValidation("int", enterAllTheWheelsAtOnceOutputStr, 1, 2, out float enterAllTheWheelsAtOnceOutput);

        Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
        float maxAirPressure = currentVehicle.Wheels[0].MaxAirPressure;
        string enterCurrentAirPressureOutputStr;
        string enterManufacturerNameOutputStr;
        string manufacturerName;
    
        if(enterAllTheWheelsAtOnceOutput == 1)
        {
            enterCurrentAirPressureOutputStr = "Please enter the current air pressure of the wheels:";
            enterManufacturerNameOutputStr = "wheels' manufacturer name";

            handleInputValidation("float", enterCurrentAirPressureOutputStr, 0, maxAirPressure, out float currentAirPressure);
            getName(enterManufacturerNameOutputStr, out manufacturerName);
           
            foreach(Wheel wheel in currentVehicle.Wheels)
            {
                wheel.CurrentAirPressure = currentAirPressure;
                wheel.ManufacturerName = manufacturerName;
            }
        }
        else
        {
            int currentWheelIndex = 0;

            foreach(Wheel wheel in currentVehicle.Wheels)
            {
                enterCurrentAirPressureOutputStr = string.Format("Please enter current air pressure in wheel number {0}: ", currentWheelIndex + 1);
                enterManufacturerNameOutputStr = string.Format("wheel's manufacturer name in wheel number {0}", currentWheelIndex + 1);
                handleInputValidation("float", enterCurrentAirPressureOutputStr, 0, maxAirPressure, out float currentAirPressure);
                getName(enterManufacturerNameOutputStr, out manufacturerName);

                wheel.CurrentAirPressure = currentAirPressure;
                wheel.ManufacturerName = manufacturerName;
                currentWheelIndex++;
            }
        }
    }

    private void getSpecificData()
    {
        int index = 0;
        bool isValidInput = false;
        Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];

        foreach(string specificDataString in currentVehicle.SpecificData)
        {
            do
            {
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

    private void handleInputValidation(string i_InputType, string i_Output, float i_MinValue, float i_MaxValue, out float io_Input)
    {
        bool isValidInput = false;
        io_Input = 0;
        
        do
        {
            Console.WriteLine(i_Output);

            try
            {
                if (i_InputType == "float")
                {
                    io_Input = float.Parse(Console.ReadLine());
                }
                else
                {
                    io_Input = int.Parse(Console.ReadLine());
                }

                Vehicle.CheckIfUserInputIsValid(io_Input, i_MinValue, i_MaxValue, out isValidInput);
                isValidInput = true;
                
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

    private void displayLicenseNumberWithFilterOption()
    {
        string displayLicenseOutputStr = string.Format("Please choose from the following options the desired filter for the license numbers list:\n(1) {0}\n(2) {1}\n(3) {2}\n(4) Display all vehicles (without filter).", 
        Garage.eStatus.In_Repair, Garage.eStatus.Fixed, Garage.eStatus.Paid);
        float chosenFilter;

        handleInputValidation("int", displayLicenseOutputStr, 1, 4, out chosenFilter);

        List<string> filteredLicenseNumbersList = m_Garage.GetLicenseNumbersListByFilter(chosenFilter);

        if(filteredLicenseNumbersList.Count != 0)
        {
            Console.WriteLine("The filtered license numbers:");

            foreach(string licenseNumber in filteredLicenseNumbersList)
            {
                Console.WriteLine(licenseNumber);
            }
        }
        else if(chosenFilter == 4)
        {
            Console.WriteLine("There are no vehicles in the garage.");
        }
        else
        {
            Console.WriteLine("There are no vehicles with the status {0} in the garage.",(Garage.eStatus)chosenFilter);
        }
        Thread.Sleep(2000);
    }

    private void changeVehicleStatusAccordingNewStatus()
    {
        float chosenStatus;
        string chooseNewStatusStr = string.Format("Please choose from the following options the desired new status for the vehicle:\n(1) {0}\n(2) {1}\n(3) {2}", 
        Garage.eStatus.In_Repair, Garage.eStatus.Fixed, Garage.eStatus.Paid);
        
        try
        {
            m_Garage.CheckIfVehicleInTheGarage();
            handleInputValidation("int", chooseNewStatusStr, 1, 3, out chosenStatus);
            m_Garage.changeStatus((Garage.eStatus)chosenStatus);
            Console.WriteLine("Vehicle number {0} status was changed to {1}.", m_Garage.CurrentLicenseNumber, (Garage.eStatus)chosenStatus);
        }
        catch(ArgumentException argumentException)
        {
            Console.WriteLine(argumentException.Message);
        }

        Thread.Sleep(2000);
    }

    private void inflateWheelsToMaxAirPressure()
    {
        try
        {
            m_Garage.CheckIfVehicleInTheGarage();
            m_Garage.InflateWheelsToMaxAirPressure();

            Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];

            Console.WriteLine("All wheels of vehicle number {0} were inflated to {1}.", m_Garage.CurrentLicenseNumber, currentVehicle.Wheels[0].MaxAirPressure);
        }
        catch(ArgumentException argumentException)
        {
            Console.WriteLine(argumentException.Message);
        }

        Thread.Sleep(3000);
    }

    private void refuelVehicleAccordingToUserInput()
    {
        try
        {
            m_Garage.CheckIfVehicleInTheGarage();
            m_Garage.CheckIfFuelOrElectricCompatibility("Fuel");
        
            getFuelType();

            float chosenFuelAmount;
            float updateFuelAmount;
            string enterAmountOfFuelOutput = string.Format("Please enter the amount of fuel you want to fill:");

            Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];

            handleInputValidation("float", enterAmountOfFuelOutput, 0, currentVehicle.PowerUnit.MaxEnergyCapacity, out chosenFuelAmount);
            m_Garage.RefuelVehicleAccordingToUserInput(chosenFuelAmount, out updateFuelAmount);

            Console.WriteLine("The update fuel amount is {0}.", updateFuelAmount);
        }
        catch(ArgumentException argumentException)
        {
            Console.WriteLine(argumentException.Message);
        }

        Thread.Sleep(3000);
    }

    private void getFuelType()
    {
        float chosenFuelType;
        bool isFuelTypeCompatible = false;

            do
            {
                try
                {
                    string enterFuelTypeOutput = string.Format("Please choose the appropriate fuel type:\n(1) {0}\n(2) {1}\n(3) {2}\n(4) {3}",
                    FuelEngine.eFuelType.Soler, FuelEngine.eFuelType.Octan95, FuelEngine.eFuelType.Octan96, FuelEngine.eFuelType.Octan98);
   
                    handleInputValidation("int", enterFuelTypeOutput, 1, 4, out chosenFuelType);
                    m_Garage.CheckFuelTypeCompatibility((FuelEngine.eFuelType)chosenFuelType, out isFuelTypeCompatible);
                }
                catch(ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
            }
            while(!isFuelTypeCompatible);
    }

    private void chargeElectricVehicle()
    {
        try
        {
            m_Garage.CheckIfVehicleInTheGarage();
            m_Garage.CheckIfFuelOrElectricCompatibility("Electric");

            float chosenAmountOfMinutesToCharge;
            float updateChargeAmountInHours;
            string enterAmountOfMinutesToChargeOutput = string.Format("Please enter the amount of minutes to charge:");
            Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];

            handleInputValidation("float", enterAmountOfMinutesToChargeOutput, 0, currentVehicle.PowerUnit.MaxEnergyCapacity * 60, out chosenAmountOfMinutesToCharge);
            m_Garage.RechargeVehicleAccordingToUserInput(chosenAmountOfMinutesToCharge, out updateChargeAmountInHours);

            Console.WriteLine("The update battery charge amount is {0:F2} (in hours).", updateChargeAmountInHours);
        }
        catch(ArgumentException argumentException)
        {
            Console.WriteLine(argumentException.Message);
        }

        Thread.Sleep(2000);
    }

    private void displayAllVehicleData()
    {
        bool isVehicleInGarage = m_Garage.CheckIfVehicleInTheGarage();

        if (isVehicleInGarage)
        {
            Console.WriteLine(m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber].ToString());
            Thread.Sleep(5000);
        }
        else
        {
            Console.WriteLine("There is no vehicle number {0} in the garage.", m_Garage.CurrentLicenseNumber);
            Thread.Sleep(2000);
        }
    }       
}
