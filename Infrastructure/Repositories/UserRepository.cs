using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private DBContext _dbContext;

        public UserRepository()
        {
            _dbContext = new DBContext();
        }

        public User GetByDId(string dId)
        {
            Users userFromDB =
                _dbContext.Users.FirstOrDefault(u => u.DId == dId);

            if (userFromDB == null) return null;
            return UserMappers.FromDBEntityToDomainObject(userFromDB);
        }

        public List<User> GetUsersByDIdList(string[] dIds)
        {
            var usersFromDB = _dbContext.Users.Where(u => dIds.Contains(u.DId)).ToList();
            List<User> users = new();

            usersFromDB.ForEach(re => users.Add
            (UserMappers.FromDBEntityToDomainObject(re)));

            return users;

        }

        public List<User> GetAll()
        {
            List<Users> usersFromDB =
                _dbContext.Users.ToList();

            List<User> users = new();

            usersFromDB.ForEach(re => users.Add
            (UserMappers.FromDBEntityToDomainObject(re)));

            return users;
        }

        public Task PersistAsync(User user)
        {
            var userDBEntity =
                UserMappers.FromDomainObjectToDBEntity(user);
            _dbContext.Users.Add(userDBEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
