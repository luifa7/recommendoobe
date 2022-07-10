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
        CreateMap<City, Cities>();
        CreateMap<Cities, City>();
    }
}

public class FriendProfile : Profile
{
    public FriendProfile()
    {
        CreateMap<Friend, ReadFriend>();
        CreateMap<Friend, Friends>();
        CreateMap<Friends, Friend>();
    }
}

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, ReadNotification>();
        CreateMap<Notification, Notifications>();
        CreateMap<Notifications, Notification>();
    }
}

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ReadUser>();
        CreateMap<User, Users>();
        CreateMap<Users, User>();
    }
}

public class RecommendationProfile : Profile
{
    public RecommendationProfile()
    {
        CreateMap<Recommendation, Recommendations>();
        CreateMap<Recommendations, Recommendation>();
    }
}

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, Tags>();
        CreateMap<Tags, Tag>();
    }
}
