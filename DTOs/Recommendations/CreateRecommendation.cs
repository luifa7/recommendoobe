namespace DTOs.Recommendations
{
    public class CreateRecommendation
    {
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
        public int CreatedOn { get; set; }
        public string CityDId { get; set; }
        public string[] Tags { get; set; }
        public string FromUserDId { get; set; }
        public string ToUserDId { get; set; }
    }
}
