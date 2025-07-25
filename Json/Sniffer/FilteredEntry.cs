﻿using System.Reflection;

namespace ZOPZ_SNIFF.Json.Sniffer
{
    [Obfuscation(Exclude = true)]
    public class FilteredEntry
    {
        public Image? GeoFlag { get; set; }
        public Image? ProtectedFlag { get; set; }
        public string? Label { get; set; }
        public string? Filters { get; set; }
        public string? IPAddress { get; set; }
        public ushort Port { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ISP { get; set; }
        public int Packets { get; set; }
    }
}
