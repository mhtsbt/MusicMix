using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicMix.Data;
using MusicMix.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MusicMix.Controllers
{
    public class SongController : Controller
    {
        private readonly AppDataContext _context;

        public SongController(AppDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Songs.OrderBy(x => x.Artist).ThenBy(x => x.Title).ToListAsync());
        }

        public async Task<IActionResult> IndexSorted()
        {
            return View("Index", await _context.Songs.OrderBy(x => x.Position).ToListAsync());
        }

        [Route("api/song")]
        public async Task<IEnumerable<Song>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        [Route("/api/nextsong"), HttpPost]
        public async Task<Song> NextSong([FromBody]List<Song> history)
        {

            Song outputSong;

            var historyIds = history.Select(x => x.Id);
            Vote highestVotedSong = _context.Votes.Include(x => x.Song).OrderBy(x => x.DateTime).FirstOrDefault(x => !historyIds.Contains(x.SongId));

            if (highestVotedSong != null)
            {
                outputSong = highestVotedSong.Song;
            }
            else
            {
                outputSong = await _context.Songs.Where(x => !historyIds.Contains(x.Id) && x.FileName != null).OrderBy(x => x.Position).Take(1).FirstOrDefaultAsync();
            }

            if (outputSong.StopTime == 0)
            {
                outputSong.StopTime = 100;
            }

            return outputSong;

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Artist,FileName,StartTime,StopTime")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            song.Position = _context.Songs.Count();

            return View(song);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.SingleOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Artist,FileName,StartTime,StopTime,Position")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }

    }
}
