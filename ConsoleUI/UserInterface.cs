using GarageLogic;

namespace ConsoleUI;

public class UserInterface
{ 
    public void OpenGarage()
    {
        Menu();
        test();
    }

    public void Menu()
    {
        string output = @"Please selet one of the following actions:
(1) To add a new vehicle to the garage.
(2) To view the license numbers of all the vehicles in the garage.
(3) To change a vehicle status.
(4) To inflate a vehicle's tires to the maximum.
(5) To refuel a fuel operated vehicle.
(6) To charge an electric vehicle.
(7) To view a specific vehicle full data.
(8) Exit.";

        Console.WriteLine(output);
        string input = Console.ReadLine();

        //switch case
    }

    public void test()
    {
        string licenseNumber = "abcd123";
        eVehicleType vehicleType = eVehicleType.Truck;
        float amountOfFuel = 4.15f;
        float airPrussure = 20;
        bool isDangerous = true;
        float cargoVolume = 70;
        int numOfWheels = 12;

        //Truck truck = new Truck(cargoVolume, isDangerous, numOfWheels, licenseNumber, vehicleType);
        Motorcycle motti = new Motorcycle(20, Motorcycle.eLicenseType.B1, "romi", "abc1234566", "rotem", 20,1, eVehicleType.ElectricMotorcycle);
        Car newCar = new Car(Car.eColor.Red, Car.eDoorAmount.Five,"romi", "abc12345", "rotem", 20, 2, eVehicleType.FuelCar);
        
    }
}