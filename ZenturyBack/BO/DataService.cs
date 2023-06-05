using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ZenturyBack.Context;
using ZenturyBack.Models;

namespace ZenturyBack.BO
{
    public class DataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<UsersPaginated>> GetAllUsers(UsersResource usersResource)
        {
            UsersPaginated usersPaginated = new UsersPaginated();
            usersPaginated.Users = await _context.Users.ToListAsync();
            usersPaginated.Users = usersPaginated.Users.OrderBy(x => x.Email).ToList();


            if (!String.IsNullOrEmpty(usersResource.FirstName))
            {
                usersPaginated.Users = usersPaginated.Users.Where(x => x.FirstName.Contains(usersResource.FirstName)).ToList();
            }

            if (!String.IsNullOrEmpty(usersResource.LastName))
            {
                usersPaginated.Users = usersPaginated.Users.Where(x => x.LastName.Contains(usersResource.LastName)).ToList();
            }

            if (!String.IsNullOrEmpty(usersResource.Email))
            {
                usersPaginated.Users = usersPaginated.Users.Where(x => x.Email.Contains(usersResource.Email)).ToList();
            }

            if (!String.IsNullOrEmpty(usersResource.SortOrder))
            {
                switch (usersResource.SortOrder)
                {
                    case "firstName_desc":
                        usersPaginated.Users = usersPaginated.Users.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case "lastName":
                        usersPaginated.Users = usersPaginated.Users.OrderBy(s => s.LastName).ToList();
                        break;
                    case "lastName_desc":
                        usersPaginated.Users = usersPaginated.Users.OrderByDescending(s => s.LastName).ToList();
                        break;
                    case "email":
                        usersPaginated.Users = usersPaginated.Users.OrderBy(s => s.Email).ToList();
                        break;
                    case "email_desc":
                        usersPaginated.Users = usersPaginated.Users.OrderByDescending(s => s.Email).ToList();
                        break;
                    default:
                        usersPaginated.Users = usersPaginated.Users.OrderBy(s => s.FirstName).ToList();
                        break;
                }
            }

            usersPaginated.totalItemsCount = usersPaginated.Users.Count;

            usersPaginated.Users = usersPaginated.Users.Skip(usersResource.PageIndex*usersResource.PageSize).Take(usersResource.PageSize).ToList();

            return usersPaginated;
        }

        public async Task<ActionResult<LoginsPaginated>> GetAllLogins(LoginResource loginResource)
        {
            LoginsPaginated loginsPaginated= new LoginsPaginated();
            loginsPaginated.Logins = await _context.Logins.ToListAsync();
            loginsPaginated.Logins = loginsPaginated.Logins.OrderBy(x => x.Email).ToList();

            if (!String.IsNullOrEmpty(loginResource.Email))
            {
                loginsPaginated.Logins = loginsPaginated.Logins.Where(x => x.Email.Contains(loginResource.Email)).ToList();
            }

            if (!String.IsNullOrEmpty(loginResource.SortOrder))
            {
                switch (loginResource.SortOrder)
                {
                    
                    case "email_desc":
                        loginsPaginated.Logins = loginsPaginated.Logins.OrderByDescending(s => s.Email).ToList();
                        break;
                    default:
                        loginsPaginated.Logins = loginsPaginated.Logins.OrderBy(s => s.Email).ToList();
                        break;
                }
            }

            loginsPaginated.totalItemsCount = loginsPaginated.Logins.Count;

            loginsPaginated.Logins = loginsPaginated.Logins.Skip(loginResource.PageIndex * loginResource.PageSize).Take(loginResource.PageSize).ToList();

            return loginsPaginated;
        }

        public async Task<ActionResult<User>> AddUser(User user)
        {
            user.Password = ComputeSHA256(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public User AuthenticateUser(Signin signin)
        {
            var user = _context.Users.Where(x => x.Email == signin.Email && x.Password == ComputeSHA256(signin.Password)).FirstOrDefault();
            return user;
        }

        public async Task<ActionResult<string>> testAsync()
        {
            for(int i = 0; i<95; i++)
            {
                await this.AddUser(new User
                {
                    FirstName = "kerim" + i,
                    LastName = "nurikic" + (95-i),
                    Email = "kerim" + i + "@gmail.com",
                    Password = "test"
                });
            }
            return "oi";
        }

        static string ComputeSHA256(string s)
        {
            string hash = String.Empty;

            // Initialize a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }
    }
}
