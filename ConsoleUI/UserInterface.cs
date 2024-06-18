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

    private void displayLicenseNumberWithFilterOption()
    {
        string displayLicenseOutputStr = string.Format("Please choose from the following options the desired filter for the license numbers list:\n(1) {0}\n(2) {1}\n(3) {2}", 
        Garage.eStatus.In_Repair, Garage.eStatus.Fixed, Garage.eStatus.Paid);
        int chosenFilter;
        HandleInputValidation(displayLicenseOutputStr, 1, 3, out chosenFilter);

        List<string> filteredLicenseNumbersList = m_Garage.GetLicenseNumbersListByFilter((Garage.eStatus)chosenFilter);

        if(filteredLicenseNumbersList.Count != 0)
        {
            Console.WriteLine("The filtered license numbers:");
            foreach(string licenseNumber in filteredLicenseNumbersList)
            {
                Console.WriteLine(licenseNumber);
            }
        }
        else
        {
            Console.WriteLine("There are no vehicles with the status {0} in the garage.",(Garage.eStatus)chosenFilter);
        }
        Thread.Sleep(2000);
    }

    private void changeVehicleStatusAccordingNewStatus()
    {
        bool isVehicleInGarage = false;
        int chosenStatus;
        string chooseNewStatusStr = string.Format("Please choose from the following options the desired new status for the vehicle:\n(1) {0}\n(2) {1}\n(3) {2}", 
        Garage.eStatus.In_Repair, Garage.eStatus.Fixed, Garage.eStatus.Paid);
        
        HandleInputValidation(chooseNewStatusStr, 1, 3, out chosenStatus);
        m_Garage.ChangeStatusAccordingUserInput((Garage.eStatus)chosenStatus, out isVehicleInGarage);

        if(isVehicleInGarage)
        {
            Console.WriteLine("Vehicle number {0} status was changed to {1}.", m_Garage.CurrentLicenseNumber, (Garage.eStatus)chosenStatus);
        }
        else
        {
            Console.WriteLine("There is no vehicle number {0} in the garage.", m_Garage.CurrentLicenseNumber);
        }
        Thread.Sleep(2000);
    }

    private void inflateWheelsToMaxAirPressure()
    {
        bool isVehicleInGarage = false;
        m_Garage.InflateWheelsToMaxAirPressure(out isVehicleInGarage);

        if(isVehicleInGarage)
        {
            Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
            Console.WriteLine("All wheels of vehicle number {0} were inflated to {1}.", m_Garage.CurrentLicenseNumber, currentVehicle.Wheels[0].MaxAirPressure);
        }
        else
        {
            Console.WriteLine("There is no vehicle number {0} in the garage.", m_Garage.CurrentLicenseNumber);
        }
        Thread.Sleep(2000);
    }

    private void refuelVehicleAccordingToUserInput()
    {
        bool isVehicleInGarage = m_Garage.CheckIfVehicleInTheGarage();
       
        if(isVehicleInGarage)
        {
            bool isFuelOperatedVehicle = m_Garage.CheckIfFuelOperatedVehicle();

            if(isFuelOperatedVehicle)
            {
                int chosenFuelType;
                float chosenFuelAmount;
                float updateFuelAmount;


                string enterFuelTypeOutput = string.Format("Please choose the appropriate fuel type::\n(1) {0}\n(2) {1}\n(3) {2}\n(4) {3}", 
                FuelEngine.eFuelType.Octan95, FuelEngine.eFuelType.Octan96, FuelEngine.eFuelType.Octan98, FuelEngine.eFuelType.Soler);

                HandleInputValidation(enterFuelTypeOutput, 1, 3, out chosenFuelType);

                // check fuel type

                string enterAmountOfFuelOutput = string.Format("Please enter the amount of fuel you want to fill:");

                Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
                HandleInputValidation(enterAmountOfFuelOutput, 0, currentVehicle.PowerUnit.MaxEnergyCapacity, out chosenFuelAmount);
                m_Garage.RefuelVehicleAccordingToUserInput(chosenFuelAmount, out updateFuelAmount);

                Console.WriteLine("The update fuel amount is {0}.", updateFuelAmount);
            }
            else
            {
                Console.WriteLine("Vehicle number {0} is not fuel operated.", m_Garage.CurrentLicenseNumber);
            }
        }
        else
        {
            Console.WriteLine("There is no vehicle number {0} in the garage.", m_Garage.CurrentLicenseNumber);
        }
        Thread.Sleep(2000);
    }

    private void chargeElectricVehicle()
    {
        bool isVehicleInGarage = m_Garage.CheckIfVehicleInTheGarage();
       
        if(isVehicleInGarage)
        {
            bool isFuelOperatedVehicle = m_Garage.CheckIfFuelOperatedVehicle();

            if(!isFuelOperatedVehicle)
            {
                float chosenAmountOfMinutesToCharge;
                float updateChargeAmountInHours;

                string enterAmountOfMinutesToChargeOutput = string.Format("Please enter the amount of minutes to charge:");

                Vehicle currentVehicle = m_Garage.VehiclesInGarage[m_Garage.CurrentLicenseNumber];
                HandleInputValidation(enterAmountOfMinutesToChargeOutput, 0, currentVehicle.PowerUnit.MaxEnergyCapacity * 60, out chosenAmountOfMinutesToCharge);
                m_Garage.RechargeVehicleAccordingToUserInput(chosenAmountOfMinutesToCharge, out updateChargeAmountInHours);

                Console.WriteLine("The update battery charge amount is {0}.", updateChargeAmountInHours);
            }
            else
            {
                Console.WriteLine("Vehicle number {0} is not electric vehicle.", m_Garage.CurrentLicenseNumber);
            }
        }
        else
        {
            Console.WriteLine("There is no vehicle number {0} in the garage.", m_Garage.CurrentLicenseNumber);
        }

        Thread.Sleep(2000);
    }

    private void displayAllVehicleData()
    {
        
    }
}
