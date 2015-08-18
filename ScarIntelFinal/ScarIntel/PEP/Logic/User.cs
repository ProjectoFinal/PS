namespace Logic
{
    public class User
    {
        public readonly string Name;
        public readonly string Password;

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}