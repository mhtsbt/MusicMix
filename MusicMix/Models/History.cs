using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMix.Models
{
    public class History
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
