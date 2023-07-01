using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace aspm.Models
{
    public class ASPMDBContext:DbContext
    {
        public virtual DbSet<Banner> HomeBanners { get; set; }
        public virtual DbSet<Tcs> TCs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<NewsEvents> NewsEvents { get; set; }

        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<SubCategory> SubCategorys { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }

        public virtual DbSet<NoticeBoard> NoticeBoards { get; set; }
        public virtual DbSet<CategoryType> CategoryTypes { get; set; }

        public virtual DbSet<Vacancies> Vacancies { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        public virtual DbSet<Facilities> Facilities { get; set; }


        public virtual DbSet<NCRET> NCRETs { get; set; }
        public virtual DbSet<Video> Videos { get; set; }

        public virtual DbSet<AWESBook> AWESBooks { get; set; }

        public virtual DbSet<Emagazine> Emagazines { get; set; }

        public virtual DbSet<Downloads> Downloads { get; set; }

        public virtual DbSet<FeeStructure> fees { get; set; }

        public virtual DbSet<TopBanners> TopBanners { get; set; }

        public virtual DbSet<EventNGallarys> EventNGallarys { get; set; }
    }
}