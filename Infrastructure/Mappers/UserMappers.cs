using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public static class UserMappers
    {
        public static Users FromDomainObjectToDbEntity(User user)
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
            };
        }

        public static User FromDbEntityToDomainObject(Users userDbEntity)
        {
            return new User(
                dId: userDbEntity.DId,
                userName: userDbEntity.UserName,
                name: userDbEntity.Name,
                shortFact1: userDbEntity.ShortFact1,
                shortFact2: userDbEntity.ShortFact2,
                shortFact3: userDbEntity.ShortFact3,
                aboutMe: userDbEntity.AboutMe,
                interestedIn: userDbEntity.InterestedIn,
                photo: userDbEntity.Photo
                );
        }
    }
}
