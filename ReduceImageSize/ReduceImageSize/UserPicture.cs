using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduceImageSize
{
    public class UserPicture
    {
        public int Id { get; set; }

        public int UserId { get; set; }


        public virtual byte[] Picture
        {
            get;
            set;
        }

        public virtual DateTime? UploadDate
        {
            get;
            set;
        }

        public virtual bool? IsUseAsAvator
        {
            get;
            set;
        }

        public virtual string MoodMessage
        {
            get;
            set;
        }

    }
}
