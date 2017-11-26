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
            return View(await _context.Songs.ToListAsync());
        }

        [Route("api/song")]
        public async Task<IEnumerable<Song>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        [Route("/api/nextsong"), HttpPost]
        public async Task<Song> NextSong([FromBody]List<Song> history)
        {

            var historyIds = history.Select(x => x.Id);
            Vote highestVotedSong = _context.Votes.Include(x => x.Song).OrderBy(x => x.DateTime).FirstOrDefault(x => !historyIds.Contains(x.SongId));

            if (highestVotedSong != null)
            {
                return highestVotedSong.Song;
            }
            else
            {
                return await _context.Songs.Where(x => !historyIds.Contains(x.Id) && x.FileName != null).OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefaultAsync();
            }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Artist,FileName,StartTime,StopTime")] Song song)
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
