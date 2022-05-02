using System;
using System.Collections.Generic;

namespace DTOs.Recommendations
{
    public class ReadRecommendation
    {
        public string DId { get; set; }
        public string PlaceName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Address { get; set; }
        public string Maps { get; set; }
        public string Website { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string OtherLink { get; set; }
        public string Photo { get; set; }
        public long CreatedOn { get; set; }
        public string CityDId { get; set; }
        public string[] Tags { get; set; }
        public string FromUserDId { get; set; }
        public string ToUserDId { get; set; }

        public ReadRecommendation(
            string dId,
            string placeName,
            string title,
            string text,
            string address,
            string maps,
            string website,
            string instagram,
            string facebook,
            string otherLink,
            string photo,
            long createdOn,
            string cityDId,
            string[] tags,
            string fromUserDId,
            string toUserDId)
        {
            DId = dId;
            PlaceName = placeName;
            Title = title;
            Text = text;
            Address = address;
            Maps = maps;
            Website = website;
            Instagram = instagram;
            Facebook = facebook;
            OtherLink = otherLink;
            Photo = photo;
            CreatedOn = createdOn;
            CityDId = cityDId;
            Tags = tags;
            FromUserDId = fromUserDId;
            ToUserDId = toUserDId;
        }
    }
}
