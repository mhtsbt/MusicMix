﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicMix.Data;
using MusicMix.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IO;

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

        public void AddLastSongToHistory(Song lastSong)
        {
            _context.History.Add(new History()
            {
                SongId = lastSong.Id
            });

            _context.SaveChanges();

        }

        [Route("/api/nextsong"), HttpPost]
        public async Task<Song> NextSong([FromBody]List<Song> history)
        {

            if (history != null && history.Count > 0)
            {
                AddLastSongToHistory(history.Last());
            }

            Song outputSong;

            var historyIds = _context.History.Select(x => x.SongId).ToList();
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
        [Route("/export"), HttpGet]
        public string Export()
        {

            var orderedSongs = _context.Songs.OrderBy(x => x.Position);

            int i = 0;

            Directory.CreateDirectory("Export");

            foreach (var song in orderedSongs)
            {
                string newFileName = i + "_" + song.FileName;
                System.IO.File.Copy(Path.Combine("Music", song.FileName), Path.Combine("Export", newFileName));
                i++;
            }

            return "OK!";
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
