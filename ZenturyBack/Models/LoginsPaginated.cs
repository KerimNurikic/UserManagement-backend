namespace ZenturyBack.Models
{
    public class LoginsPaginated
    {
        public List<Login> Logins { get; set; }

        public int totalItemsCount { get; set; }
    }
}
