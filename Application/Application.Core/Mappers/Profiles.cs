using AutoMapper;
using Domain.Core.Objects;
using DTOs.Cities;
using DTOs.Friend;
using DTOs.Notifications;
using DTOs.Users;
using Infrastructure.Core.Database.Entities;

namespace Application.Core.Mappers;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, ReadCity>();
    }
}

public class FriendProfile : Profile
{
    public FriendProfile()
    {
        CreateMap<Friend, ReadFriend>();
    }
}

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, ReadNotification>();
    }
}

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ReadUser>();
    }
}

public class CityInfrastructureProfile : Profile
{
    public CityInfrastructureProfile()
    {
        CreateMap<City, Cities>();
        CreateMap<Cities, City>();
    }
}

public class FriendInfrastructureProfile : Profile
{
    public FriendInfrastructureProfile()
    {
        CreateMap<Friend, Friends>();
        CreateMap<Friends, Friend>();
    }
}

public class NotificationInfrastructureProfile : Profile
{
    public NotificationInfrastructureProfile()
    {
        CreateMap<Notification, Notifications>();
        CreateMap<Notifications, Notification>();
    }
}

public class RecommendationInfrastructureProfile : Profile
{
    public RecommendationInfrastructureProfile()
    {
        CreateMap<Recommendation, Recommendations>();
        CreateMap<Recommendations, Recommendation>();
    }
}

public class TagInfrastructureProfile : Profile
{
    public TagInfrastructureProfile()
    {
        CreateMap<Tag, Tags>();
        CreateMap<Tags, Tag>();
    }
}

public class UserInfrastructureProfile : Profile
{
    public UserInfrastructureProfile()
    {
        CreateMap<User, Users>();
        CreateMap<Users, User>();
    }
}

