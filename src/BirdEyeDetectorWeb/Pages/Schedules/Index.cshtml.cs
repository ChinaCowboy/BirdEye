using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BirdEyeDetector.Models;
using BirdEyeDetector.Utilities;
using System.IO;
using System.Threading;

namespace BirdEyeDetector.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesAlbumContext _context;

        public IndexModel(RazorPagesAlbumContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        public IList<Schedule> Schedule { get; private set; }

        public async Task OnGetAsync()
        {
            Schedule = _context.Schedule;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Perform an initial check to catch FileUpload class attribute violations.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // var filePath = "<PATH-AND-FILE-NAME>";
            var filePath = _context.FilePath();
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await FileUpload.UploadPublicSchedule.CopyToAsync(fileStream);
            }

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostAsync2()
        {
            // Perform an initial check to catch FileUpload class
            // attribute violations.
            if (!ModelState.IsValid)
            {
                Schedule = _context.Schedule;
                return Page();
            }

            var publicScheduleData =
                await FileHelpers.ProcessFormFile(FileUpload.UploadPublicSchedule, ModelState);


            // Perform a second check to catch ProcessFormFile method
            // violations.
            if (!ModelState.IsValid)
            {
                Schedule =  _context.Schedule;
                return Page();
            }

            var schedule = new Schedule()
            {
                PublicSchedule = publicScheduleData,
                PublicScheduleSize = FileUpload.UploadPublicSchedule.Length,
                Title = FileUpload.Title,
                UploadDT = DateTime.UtcNow
            };

            _context.Schedule.Add(schedule);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}