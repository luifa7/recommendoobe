namespace DTOs.Notifications
{
    public class ReadNotification
    {
        public string DId { get; set; }
        public string UserDId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public bool WasOpen { get; set; }
        public string RelatedDId { get; set; }

        public ReadNotification(
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
    }
}
