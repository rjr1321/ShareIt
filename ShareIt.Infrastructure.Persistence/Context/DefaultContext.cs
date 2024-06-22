using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Infrastructure.Persistence
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

        DbSet<Photo> Photos { get; set; }
        DbSet<AppProfile> Profiles { get; set; }
        DbSet<Publication> Publications { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Friendship> friendships { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);


            #region Tables

            builder.Entity<Photo>()
                .ToTable("Photos");

            builder.Entity<AppProfile>()
            .ToTable("Profiles");

            builder.Entity<Publication>()
            .ToTable("Publications");

            builder.Entity<Comment>()
            .ToTable("Coments");

            builder.Entity<Friendship>()
           .ToTable("Friendship");
            #endregion


            #region Primary keys

            builder.Entity<Photo>()
                .HasKey(pho => pho.Id);

            builder.Entity<AppProfile>(entity =>
            {
                entity.HasKey(pro => pro.IdUser);
                entity.Property(pro => pro.IdUser)
                    .ValueGeneratedNever();
            });

            builder.Entity<Publication>()
            .HasKey(k => k.Id);

            builder.Entity<Comment>()
            .HasKey(k => k.Id);

            builder.Entity<Friendship>()
       .HasKey(f => new { f.AppProfileId, f.FriendId });



            #endregion


            #region Relationships



            builder.Entity<AppProfile>(entity =>
            {
                entity.HasMany(pro => pro.Friends)
          .WithOne(f => f.AppProfile)
          .HasForeignKey(f => f.AppProfileId)
          .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Friendship>()
    .HasOne(f => f.AppProfile)
    .WithMany(p => p.Friends)
    .HasForeignKey(f => f.AppProfileId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<Publication>(entity =>
            {
                entity.HasKey(pub => pub.Id);

                entity.HasOne(pub => pub.Profile)
                    .WithMany(profile => profile.Publications)
                    .HasForeignKey(pub => pub.IdProfile)
                    .OnDelete(DeleteBehavior.Cascade);

   
                entity.HasMany(pub => pub.Photos)
                    .WithOne(photo => photo.Publication)
                    .HasForeignKey(photo => photo.PublicationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Photo>(entity =>
            {
              
                entity.HasOne(pho => pho.Publication)
                .WithMany(pu => pu.Photos)
                .HasForeignKey(photo => photo.Id)
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Comment>()
               .HasOne(c => c.ParentComment)
               .WithMany(c => c.Replies)
               .HasForeignKey(c => c.IdParentComment)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
           .HasOne(c => c.publication)
           .WithMany(p => p.Comments)
           .HasForeignKey(c => c.IdPublication)
           .OnDelete(DeleteBehavior.Restrict);

        
            #endregion

            



        }
    }
}
