namespace ZenturyBack.Models
{
    public class LoginResource
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public string? Email { get; set; } = string.Empty;
        public string? SortOrder { get; set; } = string.Empty;
    }
}
