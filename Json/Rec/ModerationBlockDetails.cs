namespace ZOPZ_SNIFF.Json.Rec
{
    public class ModerationBlockDetails
    {
        public int ReportCategory { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public int GameSessionId { get; set; } = 0;
        public bool IsHostKick {  get; set; } = false;
        public bool IsBan { get; set; } = false;
        public bool IsVoiceModAutoban { get; set; } = false;
        public bool IsDeviceBan { get; set; } = false;
        public bool IsWarning { get; set; } = false;
        public string VoteKickReason { get; set; } = string.Empty;
        public string TimeoutStartedAt {  get; set; } = string.Empty;
        public string AssociatedAccountUsername {  get; set; } = string.Empty;
        // Not Sure these are still in use as api doesn't show it
        public string Message { get; set; } = string.Empty;
        public string PlayerIdReporter { get; set; } = string.Empty;
        ////////////////////////////////////////////////////////////////
    }
}
