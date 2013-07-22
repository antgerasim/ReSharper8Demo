namespace CSharp.Formatter
{
  internal class Address
  {
    public Address(int houseNumber, string streetName)
    {
      HouseNumber = houseNumber;
      StreetName = streetName;
    }

    public string StreetName { get; set; }
    public int HouseNumber { get; set; }
  }
}