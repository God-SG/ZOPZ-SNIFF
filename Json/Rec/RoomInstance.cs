namespace ZOPZ_SNIFF.Json.Rec
{
    public class RoomInstance
    {
        public bool isFull { get; set; } = false;
        public bool isInProgress { get; set; } = false;
        public bool isPrivate { get; set; } = false;
        public string location { get; set; } = string.Empty;
        public int matchmakingPolicy { get; set; } = 0;
        public int maxCapacity { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public double roomId { get; set; } = 0;
        public double roomInstanceId { get; set; } = 0;
        public int roomInstanceType { get; set; } = 0;
        public double subRoomId { get; set; } = 0;
        public string voiceAuthId { get; set; } = string.Empty;
        public string voiceServerId { get; set; } = string.Empty;

    }
}