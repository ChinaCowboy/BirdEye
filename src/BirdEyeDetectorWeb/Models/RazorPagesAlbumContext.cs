﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BirdEyeDetector.Models
{
    public class RazorPagesAlbumContext 
    {
        private IHostingEnvironment _environment;
        private static readonly string[] _extensions = { ".jpg", ".jpeg", ".gif", ".png", ".mp4", ".mov" };

        public RazorPagesAlbumContext(IHostingEnvironment environment)
        {
            _environment = environment;
            Schedule = new List<Schedule>();
            Initialize(environment.WebRootPath);
        }
        public string FilePath()
        {
            var root = Path.Combine(_environment.WebRootPath, "albums");
            if (!Directory.Exists(root))
                return string.Empty;
            return root;
        }
        private void Initialize(string contentPath)
        {

            var directory = new DirectoryInfo(FilePath());
            var schedules = directory.EnumerateDirectories()
                //.Where(f => IsFolder(f.FileName))
                //.OrderByDescending(f => f.LastWriteTime)
                .Select(a => new Schedule() { Title = a.Name , UploadDT= a.CreationTime, PublicScheduleSize= DirSize(a)});
            Schedule = schedules.ToList();

        }
        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        public bool IsImageFile(string file)
        {
            string ext = Path.GetExtension(file);
            return _extensions.Contains(ext, StringComparer.OrdinalIgnoreCase);
        }

        public List<Movie> Movie { get; set; }
        public List<Schedule> Schedule { get; set; }

    }

}