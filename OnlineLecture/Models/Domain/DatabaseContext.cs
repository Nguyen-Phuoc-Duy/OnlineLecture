using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLecture.Models.DTO;

namespace OnlineLecture.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base (options)
        {
            
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<SubjectModel> SubjectModel { get; set; }
        public DbSet<LectureModel> LectureModel { get; set; }
        public DbSet<SubjectLectureModel> SubjectLectureModel { get; set; }
        public DbSet<UserSubjectModel> UserSubjectModel { get; set; }
      

    }
}
