using System;

namespace MusicMix.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
