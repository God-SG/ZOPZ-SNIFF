namespace ZOPZ_SNIFF.Json.Rec
{
    public class AccountInfo
    {
        public int accountId {  get; set; } = 0;
        public string username { get; set; } = string.Empty;
        public string displayName {  get; set; } = string.Empty;
        public string profileImage { get; set; } = string.Empty;
        public bool isJunior { get; set; } = false;
        public int platforms { get; set; } = 0;
        public int personalPronouns { get; set; } = 0;
        public int identityFlags { get; set; } = 0;
        public string createdAt { get; set; } = string.Empty;
        public bool isMetaPlatformBlocked { get; set; } = false;
    }
}
