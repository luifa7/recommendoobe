namespace Domain.Core.Objects
{
    public class Recommendation
    {
        public string DId { get; private set; }
        public string PlaceName { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Address { get; private set; }
        public string Maps { get; private set; }
        public string Website { get; private set; }
        public string Instagram { get; private set; }
        public string Facebook { get; private set; }
        public string OtherLink { get; private set; }
        public string Photo { get; private set; }
        public long CreatedOn { get; private set; }
        public string CityDId { get; private set; }
        public string FromUserDId { get; private set; }
        public string ToUserDId { get; private set; }

        public Recommendation(
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
            FromUserDId = fromUserDId;
            ToUserDId = toUserDId;
        }

        public static Recommendation Create(
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
            string cityDId,
            string fromUserDId,
            string toUserDId)
        {
            var dId = Guid.NewGuid().ToString();
            DateTime foo = DateTime.Now;
            long createdOn = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            return new Recommendation(
                dId,
                placeName,
                title,
                text,
                address,
                maps,
                website,
                instagram,
                facebook,
                otherLink,
                photo,
                createdOn,
                cityDId,
                fromUserDId,
                toUserDId);
        }
    }
}
