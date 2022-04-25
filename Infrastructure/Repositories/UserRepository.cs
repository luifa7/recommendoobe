using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using DbContext = Infrastructure.Database.DbContext;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;
        private readonly IFriendRepository _friendRepository;

        public UserRepository(IFriendRepository friendRepository)
        {
            _dbContext = new DbContext();
            _friendRepository = friendRepository;
        }

        public User GetByDId(string dId)
        {
            var userFromDb = _dbContext.Users.FirstOrDefault(u => u.DId == dId);
            return userFromDb == null ? null : UserMappers.FromDbEntityToDomainObject(userFromDb);
        }

        public User GetByUserName(string username)
        {
            var userFromDb =
                _dbContext.Users.FirstOrDefault(u => u.UserName == username);
            return userFromDb == null ? null : UserMappers.FromDbEntityToDomainObject(userFromDb);
        }

        public List<User> GetUsersByDIdList(string[] dIds)
        {
            var usersFromDb = _dbContext.Users.Where(u => dIds.Contains(u.DId)).ToList();
            List<User> users = new();

            usersFromDb.ForEach(re => users.Add(
            UserMappers.FromDbEntityToDomainObject(re)));

            return users;
        }

        public List<City> GetCitiesByUserDId(string dId)
        {
            var citiesFromDb = _dbContext.Cities.Include(c => c.User)
                .Where(c => c.User.DId == dId).ToList();
            List<City> cities = new();

            citiesFromDb.ForEach(re => cities.Add(
            CityMappers.FromDbEntityToDomainObject(re)));

            return cities;
        }

        public List<User> GetAll()
        {
            var usersFromDb =
                _dbContext.Users.ToList();

            List<User> users = new();

            usersFromDb.ForEach(re => users.Add(
            UserMappers.FromDbEntityToDomainObject(re)));

            return users;
        }

        public Task PersistAsync(User user)
        {
            var userDbEntity =
                UserMappers.FromDomainObjectToDbEntity(user);
            _dbContext.Users.Add(userDbEntity);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateUser(
            string dId,
            string name,
            string shortFact1,
            string shortFact2,
            string shortFact3,
            string aboutMe,
            string interestedIn,
            string photo)
        {
            var user = _dbContext.Users.First(u => u.DId == dId);
            user.Name = name;
            user.ShortFact1 = shortFact1;
            user.ShortFact2 = shortFact2;
            user.ShortFact3 = shortFact3;
            user.AboutMe = aboutMe;
            user.InterestedIn = interestedIn;
            user.Photo = photo;
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteUser(string dId)
        {
            _friendRepository.DeleteAllFriendForUser(dId);
            _dbContext.Remove(_dbContext.Users.Single(u => u.DId == dId));
            return _dbContext.SaveChangesAsync();
        }
    }
}
