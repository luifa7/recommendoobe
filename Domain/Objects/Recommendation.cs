using System;
using System.Collections.Generic;

namespace Domain.Objects
{
    public class Recommendation
    {
        public string DId { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string MapLink { get; private set; }
        public string Website { get; private set; }
        public string Photo { get; private set; }


        public Recommendation(string dId, string title, string text, string mapLink,
            string website, string photo)
        {
            DId = dId;
            Title = title;
            Text = text;
            MapLink = mapLink;
            Website = website;
            Photo = photo;
        }

        public static Recommendation Create(string title, string text, string mapLink,
            string website, string photo)
        {
            var DId = Guid.NewGuid().ToString();
            return new Recommendation(DId, title, text, mapLink, website, photo);
        }
    }
}
