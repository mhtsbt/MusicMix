using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicMix.Data;
using MusicMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMix.Controllers
{
    public class VoteController : Controller
    {

        private readonly AppDataContext _context;

        public VoteController(AppDataContext context)
        {
            _context = context;
        }

        public IActionResult Add(int songId)
        {
            if (songId != 0)
            {
                _context.Votes.Add(new Models.Vote()
                {
                    DateTime = DateTime.UtcNow,
                    SongId = songId
                });

                _context.SaveChanges();
            }


            return View("VoteOk");
        }

        public IActionResult Confirm(int songId)
        {

            Song selectedSong = _context.Songs.FirstOrDefault(x => x.Id == songId);

            return View(selectedSong);
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Songs.Where(x => x.FileName != null).ToListAsync());
        }

    }
}
