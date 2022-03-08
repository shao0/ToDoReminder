using Microsoft.EntityFrameworkCore;
using ToDoReminder.Server.Entity;

namespace ToDoReminder.Server.DBContext
{
    public class ToDoReminderContext : DbContext
    {
        public ToDoReminderContext(DbContextOptions<ToDoReminderContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<ToDoReminderEntity> ToDoReminder { get; set; }
        public DbSet<MemoEntity> Memo { get; set; }


        public override int SaveChanges()
        {
            SaveSettings();
            return base.SaveChanges();
        }

        internal object Count<T>()
        {
            throw new NotImplementedException();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveSettings();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SaveSettings()
        {
            var list = ChangeTracker.Entries().ToList();
            foreach (var item in list)
            {
                if (item.State == EntityState.Modified)
                {
#warning 阻止跟新字段
                    Entry(item.Entity).Property(nameof(BaseEntity.CreateDateTime)).IsModified = false;
                }
                else if (item.State == EntityState.Added)
                {
                    Entry(item.Entity).Property(nameof(BaseEntity.CreateDateTime)).CurrentValue = DateTime.Now;
                }
                Entry(item.Entity).Property(nameof(BaseEntity.UpdateDateTime)).CurrentValue = DateTime.Now;
            }
        }
    }
}
