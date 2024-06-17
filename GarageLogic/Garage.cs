
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
        return m_VehiclesInGarage.ContainsKey(m_CurrentLicenseNumber);
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

    public List<string> GetLicenseNumbersListByFilter(eStatus i_Status)
    {
        List<string> filteredLicenseNumbers = new List<string>();
        foreach(string licenseNumber in m_VehiclesInGarage.Keys)
        {
            eStatus currentStatus = m_VehiclesInGarage[licenseNumber].Status;
            if(currentStatus == i_Status)
            {
                filteredLicenseNumbers.Add(licenseNumber);
            }
        }
        return filteredLicenseNumbers;
    }

    public void ChangeStatusAccordingUserInput(eStatus i_Status, out bool o_IsVehicleInGarage)
    {
        o_IsVehicleInGarage = CheckIfVehicleInTheGarage();
        if(o_IsVehicleInGarage)
        {
            changeStatus(i_Status);
        }
    }
}

