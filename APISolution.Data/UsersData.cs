using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APISolution.Data.Helpers;
using APISolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace APISolution.Data
{
    public class UsersData : IUserData
    {
        private readonly AppDbContext _context;
        public UsersData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Task> ChangePassword(string username, string newPassword)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }
                user.Password = Helpers.Md5Hash.GetHash(newPassword);
                await _context.SaveChangesAsync();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<IEnumerable<User>> GetAllWithRoles()
        {
            var users = await _context.Users.Include(u=> u.Roles).ToListAsync();

            foreach(var user in users)
            {
                await _context.Entry(user)
                    .Collection(u => u.Roles)
                    .LoadAsync();
            }

            //var users = await _context.Users.Include(u => u.Roles).ToListAsync();
            return users;
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return user;
        }

        public async Task<User> GetUserWithRoles(string username)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return user;
        }

        public async Task<User> Insert(User entity)
        {
            try
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == Helpers.Md5Hash.GetHash(password));
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return user;
        }

        public async Task<User> Update(User entity)
        {
            try
            {
                var user = await GetByUsername(entity.Username);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Email = entity.Email;
                user.Address = entity.Address;
                user.Telp = entity.Telp;
                user.SecurityQuestion = entity.SecurityQuestion;
                user.SecurityAnswer = entity.SecurityAnswer;

                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<User> Update(int id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
