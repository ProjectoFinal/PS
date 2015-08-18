namespace Logic
{
    public class Permission
    {
        public Permission(string method, string resource)
        {
            Resource = resource;
            Method = method;
        }

        public string Method { get; private set; }
        public string Resource { get; private set; }
    }
}