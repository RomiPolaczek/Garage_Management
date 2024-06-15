
using System.Windows.Markup;

namespace GarageLogic;

public class Garage
{
    private string m_CurrentLicenseNumber;
    private Factory m_Factory;
    //private Factory.eVehicleType m_CurrentVehicleType;
    private Dictionary<string, Vehicle> m_VehiclesInGarage;

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

    // public Factory.eVehicleType CurrentVehicleType
    // {
    //   get { return m_CurrentVehicleType; }
    //   set { m_CurrentVehicleType = value; }
    // }

    public Dictionary<string, Vehicle> VehiclesInGarage
    {
        get { return m_VehiclesInGarage; }
      //  set {  m_VehiclesInGarage = value;}
    }

    public bool CheckIfVehicleInTheGarage()
    {
        return m_VehiclesInGarage.ContainsKey(m_CurrentLicenseNumber);
    }

    public void AddNewVehicleToTheGarage(string i_ModelName, string i_LicenseNumber, string i_ManufacturerName)
    {
        Vehicle newVehicle = m_Factory.CreateNewVehicle(i_ModelName, i_LicenseNumber, i_ManufacturerName);
        m_VehiclesInGarage.Add(i_LicenseNumber, newVehicle);
    }
}

