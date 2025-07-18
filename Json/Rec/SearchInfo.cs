namespace ZOPZ_SNIFF.Json.Rec
{
    public class SearchInfo
    {
        public int accountId { get; set; } = 0;
        public string createdAt { get; set; } = string.Empty;
        public string displayName { get; set; } = string.Empty;
        public int identityFlags { get; set; } = 0;
        public bool isJunior { get; set; } = false;
        public bool isMetaPlatformBlocked { get; set; } = false;
        public int personalPronouns { get; set; } = 0;
        public int platforms { get; set; } = 0;
        public string profileImage { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
    }
}
