using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }

    public string PhoneNumber { get; private set; }

    public Gender Gender { get; private set; }

    public RelationShip RelationShip { get; private set; }

    public string Name { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public string Description { get; private set; }

    public string City { get; private set; }

    public List<string> Interests { get; private set; }

    public List<string> Photos { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }


    public User(string phoneNumber, string name, Gender gender, RelationShip relationShip, 
        DateOnly dateOfBirth, string discription, string city)
    {
        UserId = Guid.NewGuid();

        PhoneNumber = phoneNumber;

        Name = name;

        Gender = gender;

        RelationShip = relationShip;

        DateOfBirth = dateOfBirth;

        Description = discription;

        City = city;

        Interests = new();

        Photos = new();

        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void AddPhoto(string photoUrl) => Photos.Add(photoUrl);
    public void AddInterest(string interest) => Interests.Add(interest);
}
