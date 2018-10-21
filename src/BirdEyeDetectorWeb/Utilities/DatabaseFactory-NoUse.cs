using BirdEyeDetector.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdEyeDetector.Utilities
{
    public class RazorPagesMovieContextFactory : IDesignTimeDbContextFactory<RazorPagesMovieContext>
    {
        public RazorPagesMovieContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RazorPagesMovieContext>();
            optionsBuilder.UseSqlite("Data Source=movies.db");

            return new RazorPagesMovieContext(optionsBuilder.Options);
        }
    }
}
