using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Objects;
using Infrastructure.Core.Database;
using Infrastructure.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using DbContext = Infrastructure.Core.Database.DbContext;

namespace Infrastructure.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFriendRepository _friendRepository;

        public UserRepository(IFriendRepository friendRepository, IMapper mapper)
        {
            _dbContext = new DbContext();
            _mapper = mapper;
            _friendRepository = friendRepository;
        }

        public User GetByDId(string dId)
        {
            var userFromDb = _dbContext.Users.FirstOrDefault(u => u.DId == dId);
            return userFromDb == null ? null : _mapper.Map<User>(userFromDb);
        }

        public User GetByUserName(string username)
        {
            var userFromDb =
                _dbContext.Users.FirstOrDefault(u => u.UserName == username);
            return userFromDb == null ? null : _mapper.Map<User>(userFromDb);
        }

        public List<User> GetUsersByDIdList(string[] dIds)
        {
            var usersFromDb = _dbContext.Users.Where(u => dIds.Contains(u.DId)).ToList();
            List<User> users = new();

            usersFromDb.ForEach(userFromDb => users.Add(_mapper.Map<User>(userFromDb)));

            return users;
        }

        public List<City> GetCitiesByUserDId(string dId)
        {
            var citiesFromDb = _dbContext.Cities.Include(c => c.User)
                .Where(c => c.User.DId == dId).ToList();
            List<City> cities = new();

            citiesFromDb.ForEach(cityFromDb => cities.Add(_mapper.Map<City>(cityFromDb)));

            return cities;
        }

        public List<User> GetAll()
        {
            var usersFromDb =
                _dbContext.Users.ToList();

            List<User> users = new();

            usersFromDb.ForEach(userFromDb => users.Add(_mapper.Map<User>(userFromDb)));

            return users;
        }

        public Task PersistAsync(User user)
        {
            var userDbEntity = _mapper.Map<Users>(user);
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
