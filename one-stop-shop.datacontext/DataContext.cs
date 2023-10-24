using Microsoft.EntityFrameworkCore;
using one_stop_shop.model;

namespace one_stop_shop.datacontext;

public class DataContext: DbContext
{
    #region Constructor
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
    }

    #endregion

    #region Entities
    DbSet<User> Users { get; set; }
    #endregion

    #region Override Methods
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .Property(_ => _.Name)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasKey(_ => _.Id);
    }
    #endregion

}
