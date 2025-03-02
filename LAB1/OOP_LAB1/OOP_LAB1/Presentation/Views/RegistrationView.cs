namespace OOP_LAB1.Presentation.Views;

public class RegistrationView
{
    public string GetRegistrationChoice()
    {
        string choice = string.Empty;
        while (true)
        {
            Console.WriteLine("Register as (1) Employee or (2) Client:");
            choice = Console.ReadLine();
            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Please enter a valid choice");
            }
            break;
        }

        return choice;
    }

    public string GetFirstName()
    {
        Console.WriteLine("Enter first name:");
        return Console.ReadLine();
    }
    
    public string GetLastName()
    {
        Console.WriteLine("Enter last name:");
        return Console.ReadLine();
    }
    
    public string GetMiddleName()
    {
        Console.WriteLine("Enter middle name:");
        return Console.ReadLine();
    }
    
    public string GetEmail()
    {
        Console.WriteLine("Enter email:");
        return Console.ReadLine();
    }

    public string GetPassword()
    {
        Console.WriteLine("Enter password:");
        return Console.ReadLine();
    }
    
    public string GetPhoneNumber()
    {
        Console.WriteLine("Enter phone number:");
        return Console.ReadLine();
    }
    
    public string GetIdentificationNumber()
    {
        Console.WriteLine("Enter identification number:");
        return Console.ReadLine();
    }
    
    public string GetPassportSeries()
    {
        Console.WriteLine("Enter passport series:");
        return Console.ReadLine();
    }

    public void DisplayRegistrationSuccess()
    {
        Console.WriteLine("application for registration successfully filed");
    }

    public void DisplayRegistrationFailure()
    {
        Console.WriteLine("User registered failed.");
    }

    
}
