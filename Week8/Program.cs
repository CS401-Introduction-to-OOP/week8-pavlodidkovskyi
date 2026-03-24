public abstract class DeliveryItem
{
    public string TrackingNumber { get; private set; }
    public double Weight { get; private set; }

    public DeliveryItem(string trackingNumber, double weight)
    {
        TrackingNumber = trackingNumber;
        Weight = weight;
    }

    public abstract double CalculateCost();

    public virtual void PrintInfo()
    {
        Console.WriteLine($"Track - {TrackingNumber}, Weight - {Weight} kg (not N)");
    }
}

public class Letter : DeliveryItem
{
    public Letter(string trackingNumber, double weight) : base(trackingNumber, weight)
    {
    }

    public override double CalculateCost()
    {
        return 15 + (Weight * 10);
    }
}

public class Parcel : DeliveryItem
{
    public string Dimensions { get; private set; }

    public Parcel(string trackingNumber, double weight, string dimensions) : base(trackingNumber, weight)
    {
        Dimensions = dimensions;
    }

    public override double CalculateCost()
    {
        return 50 + (Weight * 25);
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Dimensions - {Dimensions}");
    }
}

public class CargoContainer<T> where T : DeliveryItem
{
    private List<T> _items;

    public CargoContainer()
    {
        _items = new List<T>();
    }

    public void AddItem(T item)
    {
        if (item != null)
        {
            _items.Add(item);
        }
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (T item in _items)
        {
            total += item.CalculateCost();
        }
        return total;
    }
}
class Program
{
    static void Main()
    {
        Letter letter1 = new Letter("L-001", 0.5);
        Letter letter2 = new Letter("L-002", 0.2);

        Parcel parcel1 = new Parcel("P-001", 5.0, "30x20x15");
        Parcel parcel2 = new Parcel("P-002", 12.5, "50x40x30");

        Console.WriteLine("test PrintInfo");
        letter1.PrintInfo();
        Console.WriteLine();
        parcel1.PrintInfo();
        Console.WriteLine();

        CargoContainer<DeliveryItem> myCargo = new CargoContainer<DeliveryItem>();
        
        myCargo.AddItem(letter1);
        myCargo.AddItem(letter2);
        myCargo.AddItem(parcel1);
        myCargo.AddItem(parcel2);

        Console.WriteLine("Загальна вартість доставки ");
        Console.WriteLine(myCargo.GetTotalCost() + " грн");
    }
}