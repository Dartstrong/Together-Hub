namespace Domain.ValueObjects;

public record Location
{
    private Location(string country, string city, string street)
    {
        Country = country;
        City = city;
        Street = street;
    }
    public string Country { get; } = default!;
    public string City { get; } = default!;
    public string Street { get; } = default!;

    public static Location Of(string country, string city, string street)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(street);

        return new Location(country, city, street);
    }
}