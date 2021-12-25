using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public class UserMappers
    {

        public static Users FromDomainObjectToDBEntity(User user,
            List<Users> friends)
        {
            return new Users()
            {
                DId = user.DId,
                UserName = user.UserName,
                Name = user.Name,
                ShortFact1 = user.ShortFact1,
                ShortFact2 = user.ShortFact2,
                ShortFact3 = user.ShortFact3,
                AboutMe = user.AboutMe,
                InterestedIn = user.InterestedIn,
                Photo = user.Photo,
                Friends = friends,
        };

        }

        public static User FromDBEntityToDomainObject(Users userDBEntity)
        {
            return new User(
                dId:userDBEntity.DId,
                userName: userDBEntity.UserName,
                name:userDBEntity.Name,
                shortFact1: userDBEntity.ShortFact1,
                shortFact2: userDBEntity.ShortFact2,
                shortFact3: userDBEntity.ShortFact3,
                aboutMe: userDBEntity.AboutMe,
                interestedIn: userDBEntity.InterestedIn,
                photo: userDBEntity.Photo,
                friends: FromListUsersToArrStr(userDBEntity.Friends)
                );
        }

        private static string[] FromListUsersToArrStr(List<Users> users)
        {
            if (users == null) return System.Array.Empty<string>();
            return users.Select(user => user.DId).ToArray();
        }
    }
}
