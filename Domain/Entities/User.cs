using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }

    public string PhoneNumber { get; private set; }

    public Gender? Gender { get; private set; }

    public RelationShip? RelationShip { get; private set; }

    public string? Name { get; private set; }

    public DateOnly? DateOfBirth { get; private set; }

    public int? Age => DateOfBirth != null ? GetExactAge(DateOfBirth.Value) : null;

    public string? Description { get; private set; }

    public string? City { get; private set; }

    public List<string> Interests { get; private set; }

    public List<string> Photos { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public bool IsProfileComplete => Name != null;

    public User(string phoneNumber)
    {
        UserId = Guid.NewGuid();

        PhoneNumber = phoneNumber;

        CreatedAt = DateTimeOffset.UtcNow;

        Interests = new();

        Photos = new();
    }

    public void UpdateProfile(string name, Gender gender, RelationShip relationShip, 
        DateOnly dateOfBirth, string description, string city)
    {

        if (GetExactAge(dateOfBirth) < 18)
        {
            throw new ValidationException("Регистрация доступна только с 18 лет.");
        }

        Name = name;

        Gender = gender;

        RelationShip = relationShip;

        DateOfBirth = dateOfBirth;

        Description = description;

        City = city;
    }

    public void AddPhoto(string photoUrl) => Photos.Add(photoUrl);
    public void AddInterest(string interest) => Interests.Add(interest);

    private int GetExactAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var currentAge = today.Year - dateOfBirth.Year;

        if (dateOfBirth.AddYears(currentAge) > today)
        {
            currentAge--;
        }

        return currentAge;
    }
}
