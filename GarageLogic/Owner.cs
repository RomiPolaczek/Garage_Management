namespace GarageLogic;

public class Owner
{
    private string m_Name;
    private string m_PhoneNumer;


    public string Name
    {
        get { return m_Name; }
        set 
        { 
            if (string.IsNullOrWhiteSpace(value))
            {
              throw new ArgumentException("Name cannot be empty or whitespace.");
            } 
            m_Name = value; 
        }
    }   

    public string phoneNumber
    {
        get { return m_PhoneNumer; }
        set 
        { 
            if (string.IsNullOrWhiteSpace(value))
            {
              throw new ArgumentException("Phone number cannot be empty or whitespace.");
            }
            m_PhoneNumer = value;
        }
    }   
}