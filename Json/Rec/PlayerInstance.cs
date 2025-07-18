namespace ZOPZ_SNIFF.Json.Rec
{
    public class PlayerInstance
    {
        public string appVersion { get; set; } = string.Empty;
        public int deviceClass { get; set; } = 0;
        public bool isOnline { get; set; } = false;
        public string lastOnline { get; set; } = string.Empty;
        public int platform { get; set; } = 0;
        public int playerId { get; set; } = 0;
        public RoomInstance? roomInstance { get; set; } = new RoomInstance();
        public int statusVisibility { get; set; } = 0;
        public int vrMovementMode { get; set; } = 0;
    }
}