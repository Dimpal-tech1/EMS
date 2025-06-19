using App_Rev.Core;
using App_Rev.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace App_Rev.Infrastructure.Repositories
{
    public class UserRepository:IUser
    {
        private readonly AppDBContext _db;

        public UserRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var user=await _db.UserMaster.ToListAsync();
            return user;
        }
        public async Task<User> GetById(int Id)
        {
            var user =await _db.UserMaster.FindAsync(Id);
            return user;
        }
        public async Task AddOrUpdate(User user)
        {
            var users = await _db.UserMaster.FirstOrDefaultAsync(x=>x.EmpId==user.EmpId);
            if (users == null)
            {
                await _db.UserMaster.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            else
            {
                users.EmpId = user.EmpId;
                users.Name = user.Name;
                users.Email = user.Email;
                users.Password = user.Password;
                users.Mobile = user.Mobile;
                users.Role = user.Role;
                users.Status = user.Status;
                _db.UserMaster.Update(users);
                await _db.SaveChangesAsync();
            }

        }
    }
}
