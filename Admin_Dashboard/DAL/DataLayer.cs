using Admin_Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Admin_Dashboard.DAL
{
    public class DataLayer:DbContext
    {
        public DataLayer(DbContextOptions<DataLayer> options):base(options) 
        {
            
        }
        public DbSet<CompanyUsers> Companyusers { get; set; }
        public DbSet<Company> companys { get; set; }
       
    }
}
