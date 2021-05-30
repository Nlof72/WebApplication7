using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication7.Controllers
{
    [Authorize]
    public class FoldersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FoldersController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Folder()
        {
            var userId = _userManager.GetUserId(User);
            var index = _context.Folders.Where(e => e.FolderId == null && e.ApplicationUserId == userId).ToList();

            return View(index);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();

            var folder = _context.Folders.Include(e => e.Files).Include(e => e.Folders)
                .SingleOrDefault(e => e.Id == id);
            ViewBag.Path = GetPath(id);
            ViewBag.Can = GetCount(id);

            return View(folder);
        }


        public IActionResult Create(Guid? id)
        {
            var model = new FolderView { FolderId = id };
            if(id == null) return View(model);
            ViewBag.Path = GetPath(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FolderView model, Guid? id)
        {
            if (!ModelState.IsValid) return View(model);


            var folder = new Folder
            {
                FolderId = id,
                Name = model.Name,
                ApplicationUserId = _userManager.GetUserId(User)
            };
            _context.Add(folder);
            _context.SaveChanges();
             
            if(id == null) return RedirectToAction(nameof(Folder));
            return RedirectToAction(nameof(Details), new { id });
        }


        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            ViewBag.Id = id;
            return View(new FolderView());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FolderView model, Guid? id)

        {
            if (!ModelState.IsValid) return View(model);

            var folder = _context.Folders.SingleOrDefault(e => e.Id == id);
            folder.Name = model.Name;
            _context.SaveChanges();
            return folder.FolderId != null
                ? RedirectToAction(nameof(Details), new { id = folder.FolderId })
                : RedirectToAction(nameof(Folder));
        }


        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var folder = _context.Folders.Include(e => e.Folders).Include(e => e.Files)
                           .SingleOrDefault(e => e.Id == id);
            _context.Folders.Remove(folder);
            _context.SaveChanges();
            return folder.FolderId != null
                ? RedirectToAction(nameof(Details), new { id = folder.FolderId })
                : RedirectToAction(nameof(Folder));
        }



        private List<Tuple<Guid?, string>> GetPath(Guid? id)
        {
            var folder = _context.Folders.SingleOrDefault(e => e.Id == id);
            var root = new List<Tuple<Guid?, string>> { new Tuple<Guid?, string>(id, folder?.Name) };
            while (folder?.FolderId != null)
            {
                folder = _context.Folders.SingleOrDefault(e => e.Id == folder.FolderId);
                root.Add(new Tuple<Guid?, string>(folder?.Id, folder?.Name));
            }

            root.Reverse();
            return root;
        }

        private bool GetCount(Guid? id)
        {
            var folder = _context.Folders.SingleOrDefault(e => e.Id == id);
            var count = 0;
            while (folder?.FolderId != null)
            {
                folder = _context.Folders.SingleOrDefault(e => e.Id == folder.FolderId);
                count++;
            }
            return count == 0;
        }
    }
}
