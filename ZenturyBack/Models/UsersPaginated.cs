namespace ZenturyBack.Models
{
    public class UsersPaginated
    {
        public List<User> Users { get; set; }

        public int totalItemsCount { get; set; }
    }
}
