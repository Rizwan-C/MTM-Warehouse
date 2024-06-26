﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MTM_Warehouse.Entities
{
    public class AllDbContext : IdentityDbContext<User>
    {

        public AllDbContext( DbContextOptions<AllDbContext> dbContext) 
            : base (dbContext)
        {            
        }


        public DbSet<EmpData> EmpData_DbData { get; set; }
        public DbSet<LoginEmp> loginEmps_DbData { get; set; }
        public DbSet<WarehouseInfo> WarehouseInfo_DbData { get; set;}
        public DbSet<WarehouseItems> WarehouseItems_DbData { get; set;}
        public DbSet<Approvals> Approvals_DbData { get; set; }
        public DbSet<ApprovalJobs> ApprovalJobs_DbData { get; set; }
        public DbSet<JobProgress> JobProgress_DbData { get;set; }


        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            // Define roles
            string[] roleNames = { "super_user", "user", "restricted_user" };
            // User details
            (string username, string password, string role)[] userRoles = {
                ("superuser", "admin", "super_user"),
                ("user", "user", "user")
            };

            // Ensure all roles exist
            foreach (var roleName in roleNames)
            {
                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create users and assign roles
            foreach (var (username, password, role) in userRoles)
            {
                if (await userManager.FindByNameAsync(username) == null)
                {
                    User user = new User { UserName = username };
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
                       
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-Many relationship between WarehouseInfo and EmpData
            modelBuilder.Entity<WarehouseInfo>()
                .HasMany(wi => wi.EmpDatas)
                .WithOne(ed => ed.WarehouseInfo)
                .HasForeignKey(ed => ed.WarehouseInfoId);

            // One-to-Many relationship between LoginEmp and Approvals
            modelBuilder.Entity<LoginEmp>()
                .HasMany(le => le.Approvals)
                .WithOne(a => a.LoginEmp)
                .HasForeignKey(a => a.LoginEmpId);            

            // One-to-Many relationship between JobProgress and ApprovalJobs
            modelBuilder.Entity<JobProgress>()
                .HasMany(jp => jp.ApprovalJobs)
                .WithOne(aj => aj.JobProgress)
                .HasForeignKey(aj => aj.JobProgressId);

            // One-to-Many relationship between WarehouseInfo and WarehouseItems
            modelBuilder.Entity<WarehouseInfo>()
                .HasMany(wi => wi.WarehouseItems)
                .WithOne(wi => wi.WarehouseInfo)
                .HasForeignKey(wi => wi.WarehouseInfoId);

            // One-to-Many relationship between WarehouseInfo and LoginEmp
            modelBuilder.Entity<WarehouseInfo>()
                .HasMany(man => man.loginEmps)
                .WithOne(man => man.WarehouseInfo)
                .HasForeignKey(fk => fk.WarehouseInfoId);
            
            // One-to-One relationship between ApprovalJobs and Approvals            
            modelBuilder.Entity<ApprovalJobs>()
                .HasOne<Approvals>(aj => aj.Approvals)
                .WithOne(a => a.ApprovalJobs)
                .HasForeignKey<Approvals>(a => a.ApprovalJobsId);

            // Ensure that ApprovalJobsId in Approvals is a unique key, if the relationship is truly one-to-one.
            modelBuilder.Entity<Approvals>()
                .HasIndex(a => a.ApprovalJobsId)
                .IsUnique();
        }



    }
}


/*
 
Relations						
						
ONE	            TO 	      MANY		             ONE      TO     	ONE
WarehouseInfo		    EmpData		        ApprovalJobs		Approvals
LoginEmp		        Approvals				
JobProgress		        ApprovalJobs				
WarehouseInfo	        WarehouseItems				



modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne(s => s.Course)
            .HasForeignKey(s => s.CourseId);

modelBuilder.Entity<Casting>()
            .HasOne(c => c.Movie)
            .WithMany(m => m.Castings)
            .HasForeignKey(c => c.MovieId);
 
 */