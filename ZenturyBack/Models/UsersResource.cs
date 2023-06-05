using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZenturyBack.Models
{
    public class UsersResource
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public string? FirstName { get; set; } = string.Empty;   
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? SortOrder { get; set; } = string.Empty;

    }
}
