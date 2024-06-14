namespace ConsoleUI;
using GargeLogic;

public class UserInterface
{
    public enum eVehicleType
    {
        Truck,
        ElectricCar,
        FuelCar, 
        ElectricMotorcycle,
        FuelMotorcycle
    }

    public void OpenGarage()
    {

    }

    public void Menu()
    {

    }

    public void test()
    {
        string licenseNumber = "abcd123";
        eVehicleType vehicleType = eVehicleType.Truck;
        float amountOfFuel = 4.15;
        float airPrussure = 20;
        bool isDangerous = true;
        
        m_GarageLogic = new GarageLogic();
        m_GarageLogic.Truck newTruck = new m_GarageLogic.Truck(85, isDangerous);
    }
}