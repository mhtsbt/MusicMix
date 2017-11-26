using System.Collections.Generic;

namespace MusicMix.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string FileName { get; set; }
        public int StartTime { get; set; }
        public int StopTime { get; set; }
    
    }
}
