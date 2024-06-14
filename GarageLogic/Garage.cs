
namespace GarageLogic;

internal class Garage
{
    private Dictionary<string, Vehicle> m_VehiclesInGarage;

    public Dictionary<string, Vehicle> VehiclesInGarage
    {
        get { return m_VehiclesInGarage; }
      //  set {  m_VehiclesInGarage = value;}
    }
}

