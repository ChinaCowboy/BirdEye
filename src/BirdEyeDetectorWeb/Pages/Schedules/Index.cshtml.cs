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
        private readonly BirdEyeDetector.Models.RazorPagesMovieContext _context;

        public IndexModel(BirdEyeDetector.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        public IList<Schedule> Schedule { get; private set; }

        public async Task OnGetAsync()
        {
            Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Perform an initial check to catch FileUpload class attribute violations.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var filePath = "<PATH-AND-FILE-NAME>";

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                var cancellationToken = new CancellationToken();
                await FileUpload.UploadPublicSchedule.CopyToAsync(fileStream, cancellationToken);
            }

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostAsync2()
        {
            // Perform an initial check to catch FileUpload class
            // attribute violations.
            if (!ModelState.IsValid)
            {
                Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
                return Page();
            }

            var publicScheduleData =
                await FileHelpers.ProcessFormFile(FileUpload.UploadPublicSchedule, ModelState);

            var privateScheduleData =
                await FileHelpers.ProcessFormFile(FileUpload.UploadPrivateSchedule, ModelState);

            // Perform a second check to catch ProcessFormFile method
            // violations.
            if (!ModelState.IsValid)
            {
                Schedule = await _context.Schedule.AsNoTracking().ToListAsync();
                return Page();
            }

            var schedule = new Schedule()
            {
                PublicSchedule = publicScheduleData,
                PublicScheduleSize = FileUpload.UploadPublicSchedule.Length,
                PrivateSchedule = privateScheduleData,
                PrivateScheduleSize = FileUpload.UploadPrivateSchedule.Length,
                Title = FileUpload.Title,
                UploadDT = DateTime.UtcNow
            };

            _context.Schedule.Add(schedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}