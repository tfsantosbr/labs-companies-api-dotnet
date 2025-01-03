namespace Companies.Application.Abstractions.ValueObjects;

public record Cnpj
{
    public Cnpj(string number)
    {
        Number = number;
    }

    private Cnpj()
    {
    }

    public string Number { get; private set; } = default!;

    public override string ToString()
    {
        return $"{Number}";
    }
}
