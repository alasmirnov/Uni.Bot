namespace Service.Model
{
    public class User
    {
        public int Id { get; }
        public string Name { get; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => $"{Name} ({Id})";
    }
}