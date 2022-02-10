namespace Domain.Objects
{
    public class Tag
    {
        public string RecommendationDId { get; set; }
        public string Word { get; private set; }


        private Tag(string recommendationDId, string word)
        {
            RecommendationDId = recommendationDId;
            Word = word;
        }

        public static Tag Create(string recommendationDId, string word)
        {
            return new Tag(recommendationDId, word);
        }
    }
}
