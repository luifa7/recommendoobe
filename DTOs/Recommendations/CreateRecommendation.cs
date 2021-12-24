using System;
using System.Collections.Generic;

namespace DTOs.Recommendations
{
    public class CreateRecommendation
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string MapLink { get; set; }
        public string Website { get; set; }
        public string Photo { get; set; }
    }
}
