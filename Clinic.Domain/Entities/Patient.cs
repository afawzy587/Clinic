namespace Clinic.Domain.Entities;

public class Patient
{
    public int Id { get; set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Phone { get; private set; }

    public DateTime DateOfBirth { get; private set; }


    public bool IsActive { get; private set; }

    public Patient(string firstName, string lastName, string phone, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        DateOfBirth = dateOfBirth;
        IsActive = true;
    }

    public void Update(
        string firstName,
        string lastName,
        string phone,
        DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

}
