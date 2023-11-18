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

        public DbSet<SubjectModel> SubjectModel { get; set; }
        public DbSet<LectureModel> LectureModel { get; set; }

    }
}
