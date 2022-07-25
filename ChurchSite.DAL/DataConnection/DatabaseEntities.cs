using ChurchSite.DAL.Entity;
using ChurchSite.DAL.Migrations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ChurchSite.DAL.DataConnection
{
    public class DatabaseEntities : DbContext
    {
        public DatabaseEntities() : base("name=DatabaseEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseEntities, Configuration>());
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 5000;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ApplicationSettings> ApplicationSettings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleContent> ArticleContents { get; set; }
        public virtual DbSet<AfflilateBonusManager> AfflilateBonusManagers { get; set; }
        public virtual DbSet<Spiritualist> Spiritualists { get; set; }
        public virtual DbSet<SubCategoryXArticle> SubCategoryXArticles { get; set; }
        public virtual DbSet<Sacrament> Sacraments { get; set; }
        public virtual DbSet<Prayer> Prayers { get; set; }
        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<CustomMail> CustomMails { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Baptism> Baptisms { get; set; }
        public virtual DbSet<Eucharist> Eucharists { get; set; }
        public virtual DbSet<Matrimony> Matrimonies { get; set; }
        public virtual DbSet<BookMass> BookMasses { get; set; }
        public virtual DbSet<MonnifySubAccountRecords> MonnifySubAccountRecords { get; set; }
        public virtual DbSet<DonationStorage> DonationStorages { get; set; }
    }
}
