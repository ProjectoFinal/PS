namespace Logic
{
    public class Role
    {
        public Role Junior;
        public string Name;

        public Role(Role junior, string role)
        {
            Name = role;
            Junior = junior;
        }
    }
}