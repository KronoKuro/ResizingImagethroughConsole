using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReduceImageSize
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() : base("PictureContext")
        { }
        public DbSet<UserPicture> UserPictures { get; set; }
    }
}
