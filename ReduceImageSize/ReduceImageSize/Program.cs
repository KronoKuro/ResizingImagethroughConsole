using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace ReduceImageSize
{
    class Program
    {
       static void Main(string[] args)
       {

            ApplicationContext db = new ApplicationContext();

            /* collection Picture List 
             * var usersPicturesList = db.UserPictures.ToList();
              foreach (var picture in usersPicturesList)
              {
                  if (picture.Picture.Length > 0)
                  {
                      picture.Picture = Resize2Max50Kbytes(picture.Picture);

                  }
              }
             db.SaveChanges();*/
            //Call Add Image in database Program.AddImage();
            /*Проверка*/
            var imageObject = db.UserPictures.First();
            var img = FromArrayByteIntoImage(imageObject.Picture);
            String saveImagePath = @"c:\1\1.jpg";
            img.Save(saveImagePath);
            /*Проверка*/
       }

        //Add image in database
        public static void AddImage()
        {
            ApplicationContext db = new ApplicationContext();
            DirectoryInfo Folder;
            FileInfo[] Images;
            Folder = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + "\\photo\\");
            Images = Folder.GetFiles();
            List<System.IO.FileInfo> usersPicturesList = Images.ToList();
            Random rnd = new Random();
            foreach (var picture in usersPicturesList)
            {
                using (var img = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\photo\\" + picture.Name))
                {
                    var height = img.Height;
                    var width = img.Width;
                    var byteArrayImage = Program.ImageToByteArray(img);
                    db.UserPictures.Add(new UserPicture
                    {
                        Id = rnd.Next(1, 13),
                        UserId = rnd.Next(1, 13),
                        Picture = byteArrayImage,
                        MoodMessage = "new string description",
                    });
                    db.SaveChanges();
                }


            }
        }

        //from image into byte array
        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        //image from byte array
        public static Image FromArrayByteIntoImage(byte[] arrayByte)
        {
            return (Bitmap)((new ImageConverter()).ConvertFrom(arrayByte));
        }

        //Resize image
        public static byte[] Resize2Max50Kbytes(byte[] byteImageIn)
        {
            //1024000
            if (byteImageIn.Length > 320000)
            {
                byte[] currentByteImageArray = byteImageIn;

                MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
                System.Drawing.Image fullsizeImage = Image.FromStream(inputMemoryStream);
                if (fullsizeImage.Width > 250 || fullsizeImage.Height > 250)
                {
                    float ratioX = 200 / (float)fullsizeImage.Width;
                    float ratioY = 200 / (float)fullsizeImage.Height;
                    float ratio = Math.Min(ratioX, ratioY);


                    Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * ratio), (int)(fullsizeImage.Height * ratio)));
                    MemoryStream resultStream = new MemoryStream();

                    fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                    currentByteImageArray = resultStream.ToArray();
                    resultStream.Dispose();
                    resultStream.Close();
                }

                return currentByteImageArray;
            }
            else { return null; }
        }
    }
}


/*  
 *  пока оставь может пригодится
 *  DirectoryInfo Folder;
              FileInfo[] Images;
              Folder = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + "\\photo\\");
              Images = Folder.GetFiles();
              List<System.IO.FileInfo> usersPicturesList = Images.ToList();

              foreach (var picture in usersPicturesList)
              {
                  using (var img = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\photo\\" + picture.Name))
                  {
                      var height = img.Height;
                      var width = img.Width;
                      var byteArrayImage = Resize2Max50Kbytes(Program.ImageToByteArray(img));
                      Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImage));
                  }


              }*/

