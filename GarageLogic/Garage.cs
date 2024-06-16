
using System.Windows.Markup;

namespace GarageLogic;

public class Garage
{
    private string m_CurrentLicenseNumber;
    private Factory m_Factory;
    private Dictionary<string, Vehicle> m_VehiclesInGarage;

    public enum eStatus
    {
        In_Repair,
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

    public void AddNewVehicleToTheGarage(string i_ModelName, string i_LicenseNumber, string i_ManufacturerName, Owner i_Owner)
    {
        Vehicle newVehicle = m_Factory.CreateNewVehicle(i_ModelName, i_LicenseNumber, i_ManufacturerName, i_Owner);
        m_VehiclesInGarage.Add(i_LicenseNumber, newVehicle);
    }

    public void changeStatus(Garage.eStatus newStatus)
    {
        m_VehiclesInGarage[m_CurrentLicenseNumber].Status = newStatus;
    }
}

