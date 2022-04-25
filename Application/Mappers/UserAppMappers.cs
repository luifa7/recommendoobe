using Domain.Objects;
using DTOs.Users;

namespace Application.Mappers
{
    public static class UserAppMappers
    {
        public static ReadUser FromDomainObjectToApiDto(User user)
        {
            return new ReadUser(
                dId: user.DId,
                userName: user.UserName,
                name: user.Name,
                shortFact1: user.ShortFact1,
                shortFact2: user.ShortFact2,
                shortFact3: user.ShortFact3,
                aboutMe: user.AboutMe,
                interestedIn: user.InterestedIn,
                photo: user.Photo
                );
        }
    }
}
