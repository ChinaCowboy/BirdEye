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
using BirdEyeDetector.Services;
using Microsoft.Extensions.Logging;

namespace BirdEyeDetector.Pages.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesAlbumContext _context;
        private readonly IBackgroundTaskQueue _queue;
        private readonly ILogger _logger;

        public IndexModel(RazorPagesAlbumContext context, IBackgroundTaskQueue queue, ILoggerFactory loggerFactory)
        {
            _context = context;
            _queue = queue;
            _logger = loggerFactory.CreateLogger<QueuedHostedService>();

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
            var filefolder = _context.FilePath();
            string dir = Path.Combine(filefolder, FileUpload.Title);
            Directory.CreateDirectory(dir);
            string savePath = Path.Combine(dir, $"{FileUpload.UploadPublicSchedule.FileName}");

            using (var fileStream = new FileStream(savePath, FileMode.CreateNew, FileAccess.ReadWrite))
            {
                await FileUpload.UploadPublicSchedule.CopyToAsync(fileStream);
            }

            //Do Parse

            _queue.QueueBackgroundWorkItem(async token =>
            {
                var guid = Guid.NewGuid().ToString();

                for (int delayLoop = 0; delayLoop < 3; delayLoop++)
                {
                    _logger.LogInformation(
                        $"Queued Background Task {guid} is running. {delayLoop}/3");
                    await Task.Delay(TimeSpan.FromSeconds(5), token);
                }

                _logger.LogInformation(
                    $"Queued Background Task {guid} is complete. 3/3");
            });


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