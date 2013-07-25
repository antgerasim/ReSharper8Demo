namespace MyNewName.MyNewFolder
{
    public class ThirdClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ThirdClass(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;


        }

        public void SomeMethod(string b)
        {
            var abc = string.Format("abc{0}ced{1}", b, 200);



        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Email: {1}", Name, Email);
        }

        public bool Equals(ThirdClass other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ThirdClass)) return false;
            return Equals((ThirdClass) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}