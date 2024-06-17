using System.Collections;
using System.IO.Compression;

namespace GarageLogic;

public class Factory
{
    private eVehicleType m_VehicleType; 

    public enum eVehicleType
    {
        Truck = 1,
        Electric_Car,
        Fuel_Car,
        Electric_Motorcycle,
        Fuel_Motorcycle
    }

    public eVehicleType VehicleType
    {
        get { return m_VehicleType; }
        set { m_VehicleType = value; }
    }    

    public Vehicle CreateNewVehicle(string i_ModelName, string i_LicenseNumber, Owner i_Owner)
    {
        Vehicle vehicle;
        switch(m_VehicleType)
        {
            case eVehicleType.Truck:
            vehicle = new Truck(i_ModelName, i_LicenseNumber, i_Owner);
            break;
            case eVehicleType.Electric_Car:
            vehicle = new Car(i_ModelName, i_LicenseNumber, i_Owner, m_VehicleType);
            break;
            case eVehicleType.Fuel_Car:
            vehicle = new Car(i_ModelName, i_LicenseNumber, i_Owner, m_VehicleType);
            break;
            case eVehicleType.Electric_Motorcycle: 
            vehicle = new Motorcycle(i_ModelName, i_LicenseNumber, i_Owner, m_VehicleType);
            break;
            default:
            vehicle = new Motorcycle(i_ModelName, i_LicenseNumber, i_Owner, m_VehicleType);
            break;
        } 

        return vehicle;
    }
}