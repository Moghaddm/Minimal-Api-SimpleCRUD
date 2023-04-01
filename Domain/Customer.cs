namespace Domain;

public class Customer
{
    public Customer(string firstName, string lastName, bool gender, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Age = age;
    }

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Gender { get; set; }
    public int Age { get; set; }
    private string _NationalCode = default!;
    public string NationalCode
    {
        get => _NationalCode;
        set { NationalCode = _NationalCode; }
    }
}
