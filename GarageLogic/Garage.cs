
using System.Windows.Markup;

namespace GarageLogic;

public class Garage
{
    private string m_CurrentLicenseNumber;
    private Factory m_Factory;
    private Dictionary<string, Vehicle> m_VehiclesInGarage;

    public enum eStatus
    {
        In_Repair = 1,
        Fixed,
        Paid
    }

    public Garage()
    {
        m_Factory = new Factory();
        m_VehiclesInGarage = new Dictionary<string, Vehicle>();
    }

    public string CurrentLicenseNumber
    {
      get { return m_CurrentLicenseNumber; }
      set { m_CurrentLicenseNumber = value; }
    }

    public Factory Factory
    {
      get { return m_Factory; }
      set { m_Factory = value; }
    }

    public Dictionary<string, Vehicle> VehiclesInGarage
    {
        get { return m_VehiclesInGarage; }
    }

    public bool CheckIfVehicleInTheGarage()
    {
        bool isVehicleInGarage = true;

        if(!m_VehiclesInGarage.ContainsKey(m_CurrentLicenseNumber))
        {
            isVehicleInGarage = false;
            throw new ArgumentException(string.Format("There is no vehicle number {0} in the garage.", m_CurrentLicenseNumber));
        }

        return isVehicleInGarage;
    }

    public void AddNewVehicleToTheGarage(string i_ModelName, string i_LicenseNumber, Owner i_Owner)
    {
        Vehicle newVehicle = m_Factory.CreateNewVehicle(i_ModelName, i_LicenseNumber, i_Owner);
        m_VehiclesInGarage.Add(i_LicenseNumber, newVehicle);
    }

    public void changeStatus(Garage.eStatus newStatus)
    {
        m_VehiclesInGarage[m_CurrentLicenseNumber].Status = newStatus;
    }

    public List<string> GetLicenseNumbersListByFilter(int i_DesiredFilter)
    {
        List<string> filteredLicenseNumbers = new List<string>();

        foreach(string licenseNumber in m_VehiclesInGarage.Keys)
        {
            eStatus currentStatus = m_VehiclesInGarage[licenseNumber].Status;

            if((int)currentStatus == i_DesiredFilter || i_DesiredFilter == 4)
            {
                filteredLicenseNumbers.Add(licenseNumber);
            }
        }

        if(filteredLicenseNumbers.Count == 0)
        {
            throw new ArgumentException("No vehicles found.");
        }

        return filteredLicenseNumbers;
    }

    public void InflateWheelsToMaxAirPressure()
    {   
        Vehicle currentVehicle = m_VehiclesInGarage[m_CurrentLicenseNumber];
        foreach(Wheel wheel in currentVehicle.Wheels)
        {
            wheel.CurrentAirPressure = wheel.MaxAirPressure;
        }
    }

    public void CheckIfFuelOrElectricCompatibility(string i_powerUnitType)
    {
        Vehicle currentVehicle = m_VehiclesInGarage[m_CurrentLicenseNumber];

        if((currentVehicle.PowerUnit is FuelEngine) && i_powerUnitType == "Electric")
        {
            throw new ArgumentException(string.Format("Vehicle number {0} is not electric.", m_CurrentLicenseNumber));
        }
        else if((currentVehicle.PowerUnit is ElectricBattery) && i_powerUnitType == "Fuel")
        {
            throw new ArgumentException(string.Format("Vehicle number {0} is not fuel operated.", m_CurrentLicenseNumber));
        }
    }

    public void CheckFuelTypeCompatibility(FuelEngine.eFuelType i_ChosenFuelType, out bool o_IsFuelTypeCompatible)
    {
        Vehicle currentVehicle = VehiclesInGarage[CurrentLicenseNumber];
        FuelEngine.eFuelType currentFuelType = (currentVehicle.PowerUnit as FuelEngine).FuelType;
        o_IsFuelTypeCompatible = true;

        if(currentFuelType != i_ChosenFuelType)
        {
            o_IsFuelTypeCompatible = false;
            throw new ArgumentException(string.Format("Vehicle number {0} needs {1} fuel.", m_CurrentLicenseNumber, currentFuelType));
        }
    }

    public void RefuelVehicleAccordingToUserInput(float i_ChosenFuelAmount, out float o_UpdateFuelAmount)
    {
        Vehicle currentVehicle = m_VehiclesInGarage[m_CurrentLicenseNumber];
        if(currentVehicle.PowerUnit.CurrentEnergyAmount + i_ChosenFuelAmount > currentVehicle.PowerUnit.MaxEnergyCapacity)
        {
            currentVehicle.PowerUnit.CurrentEnergyAmount = currentVehicle.PowerUnit.MaxEnergyCapacity;
            o_UpdateFuelAmount = currentVehicle.PowerUnit.MaxEnergyCapacity;
        }
        else
        {
            currentVehicle.PowerUnit.CurrentEnergyAmount += i_ChosenFuelAmount;
            o_UpdateFuelAmount = currentVehicle.PowerUnit.CurrentEnergyAmount;
        }
    }

    public void RechargeVehicleAccordingToUserInput(float i_ChosenAmountOfMinutesToCharge, out float o_UpdateChargeAmountInHours)
    {
        Vehicle currentVehicle = m_VehiclesInGarage[m_CurrentLicenseNumber];
        float chosenAmountToChargeInHours = i_ChosenAmountOfMinutesToCharge / 60;

        if(currentVehicle.PowerUnit.CurrentEnergyAmount + chosenAmountToChargeInHours > currentVehicle.PowerUnit.MaxEnergyCapacity)
        {
            currentVehicle.PowerUnit.CurrentEnergyAmount = currentVehicle.PowerUnit.MaxEnergyCapacity;
            o_UpdateChargeAmountInHours = currentVehicle.PowerUnit.MaxEnergyCapacity;
        }
        else
        {
            currentVehicle.PowerUnit.CurrentEnergyAmount += chosenAmountToChargeInHours;
            o_UpdateChargeAmountInHours = currentVehicle.PowerUnit.CurrentEnergyAmount;
        }
    }
}

