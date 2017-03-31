using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Pontinho.Domain;
using Pontinho.Domain.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Pontinho.Data
{
    public partial class PontinhoDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly CurrentUserService _currentUserService;
        public PontinhoDbContext(CurrentUserService currentUserService, DbContextOptions<PontinhoDbContext> options) : base(options) // base("name=PontinhoEntities")
        {
            _currentUserService = currentUserService;
        }
        public PontinhoDbContext(DbContextOptions<PontinhoDbContext> options) : base(options) { }

        public static PontinhoDbContext Create()
        {
            return new PontinhoDbContext(null);
        }

        public override int SaveChanges()
        {
            var changedTrackableEntities = ChangeTracker.Entries()
                   .Where(x => x.Entity is ITrackedPersistentEntity &&
                   (x.State == EntityState.Added || x.State == EntityState.Modified))
                   .ToList();

            foreach (var entity in changedTrackableEntities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ITrackedPersistentEntity)entity.Entity).CreatedUtc = DateTime.UtcNow;
                    ((ITrackedPersistentEntity)entity.Entity).CreatedBy = _currentUserService?.CurrentPrincipal?.Identity?.Name ?? "System";
                }

                if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
                {
                    ((ITrackedPersistentEntity)entity.Entity).ModifiedUtc = DateTime.UtcNow;
                    ((ITrackedPersistentEntity)entity.Entity).ModifiedBy = _currentUserService?.CurrentPrincipal?.Identity?.Name ?? "System";
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Competition>().HasMany<Player>(c => c.Players);//.WithMany(c => c.Competitions)
            builder.Entity<Player>().HasMany(p => p.Competitions);
            //.Map(cp =>
                //{
                //    cp.MapLeftKey("CompetitionId");
                //    cp.MapRightKey("PlayerId");
                //    cp.ToTable("CompetitionPlayers");
                //});

            //builder.Entity<Match>()
            //    .HasRequired(m => m.Competition)
            //    .WithMany(c => c.Matches)
            //    .WillCascadeOnDelete(true);
            //builder.Entity<Competition>().HasMany(c=>c.Players)

            builder.Entity<Match>().HasOne(c => c.Winner);
            builder.Entity<Competition>().HasOne(c => c.Winner);

            base.OnModelCreating(builder);
        }
    }

    //public class ApplicationUserManager : UserManager<ApplicationUser>
    //{
    //    public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<PontinhoDbContext>()));
    //        return manager;
    //    }
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser user)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
    //    {
    //    }

    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    //    }
    //}

    //public class ApplicationUserStore : IUserStore<ApplicationUser>
    //{
    //    private PontinhoDbContext database;
    //    public ApplicationUserStore()
    //    {
    //        database = new PontinhoDbContext();

    //    }
    //    public void Dispose()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task CreateAsync(ApplicationUser user)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task UpdateAsync(ApplicationUser user)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task DeleteAsync(ApplicationUser user)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<ApplicationUser> FindByIdAsync(string userId)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<ApplicationUser> FindByNameAsync(string userName)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}