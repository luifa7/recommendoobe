using System;
using System.Collections.Generic;

namespace DTOs.Recommendations
{
    public class ReadRecommendation
    {
        public string DId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string MapLink { get; set; }
        public string Website { get; set; }
        public string Photo { get; set; }

        public ReadRecommendation(string dId, string title, string text, string mapLink,
            string website, string photo)
        {
            DId = dId;
            Title = title;
            Text = text;
            MapLink = mapLink;
            Website = website;
            Photo = photo;
        }
    }
}
