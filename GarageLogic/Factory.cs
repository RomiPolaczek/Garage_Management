using System.Collections;

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

    public Vehicle CreateNewVehicle(string i_ModelName, string i_LicenseNumber, string i_ManufacturerName)
    {
        Vehicle vehicle;
        switch(m_VehicleType)
        {
            case eVehicleType.Truck:
            vehicle = new Truck(i_ModelName, i_LicenseNumber, i_ManufacturerName);
            break;

            default:
            vehicle = new Truck(i_ModelName, i_LicenseNumber, i_ManufacturerName);
            break;
        } 

        return vehicle;
    }
}