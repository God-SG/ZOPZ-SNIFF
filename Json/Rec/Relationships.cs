using System.Text.Json.Serialization;

namespace ZOPZ_SNIFF.Json.Rec
{
    public class Relationships
    {
        public long Id { get; set; } = 0;
        public long PlayerID { get; set; } = 0;
        public long OtherPlayerID { get; set; } = 0;
        public long RelationshipType { get; set; } = 0;
        public long Favorited { get; set; } = 0;
        public long Muted { get; set; } = 0;
        public long Ignored { get; set; } = 0;
        public long VoiceVolume { get; set; } = 0;
    }
}
