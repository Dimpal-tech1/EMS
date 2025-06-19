using App_Rev.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Rev.Infrastructure.Interface
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task AddOrUpdate(User user);
    }
}
