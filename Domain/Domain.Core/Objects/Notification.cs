using System;
using System.Collections.Generic;

namespace Domain.Core.Objects
{
    public class Notification
    {
        public string DId { get; private set; }
        public string UserDId { get; private set; }
        public string Type { get; private set; }
        public string Text { get; private set; }
        public bool WasOpen { get; private set; }
        public string RelatedDId { get; private set; }

        public static string TypeFriendRequestReceived { get; } = "FriendRequestReceived";
        public static string TypeFriendRequestAccepted { get; } = "FriendRequestAccepted";
        public static string TypeRecommendationReceived { get; } = "RecommendationReceived";
        public static string TypeFriendWillVisitCity { get; } = "FriendWillVisitCity";

        private static readonly Dictionary<string, string> NotificationTexts = new()
        {
            { TypeFriendRequestReceived, "You have a new friend request" },
            { TypeFriendRequestAccepted, "Your friend request was accepted" },
            { TypeRecommendationReceived, "You got a new recommendation" },
            { TypeFriendWillVisitCity, "One of your friends will visit a new city" }
        };

        public Notification(
            string dId,
            string userDId,
            string type,
            string text,
            bool wasOpen,
            string relatedDId)
        {
            DId = dId;
            UserDId = userDId;
            Type = type;
            Text = text;
            WasOpen = wasOpen;
            RelatedDId = relatedDId;
        }

        public static Notification Create(
            string userDId,
            string type,
            bool wasOpen,
            string relatedDId)
        {
            var dId = Guid.NewGuid().ToString();

            return new Notification(
                dId,
                userDId,
                type,
                NotificationTexts[type],
                wasOpen,
                relatedDId);
        }
    }
}
