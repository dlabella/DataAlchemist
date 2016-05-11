using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public static class FileExtensions
    {
        public enum MediaTypeEnum
        {
            None,
            Image,
            Video,
            Document
        }

        public static MediaTypeEnum GetMediaType(this FileInfo finfo)
        {
            if (!finfo.Exists) return MediaTypeEnum.None;
            var ext = finfo.Extension.ToLower();
            if (ext=="jpg" || ext=="jpeg" || ext=="gif" || ext == "png")
            {
                return MediaTypeEnum.Image;
            }else if (ext=="mp4" || ext=="mpg" || ext=="flv" || ext=="avi" || ext == "xvid")
            {
                return MediaTypeEnum.Video;
            }
            else
            {
                return MediaTypeEnum.Document;
            }
        }

        public static MediaTypeEnum GetMediaType(string extension)
        {
            string ext;
            if (string.IsNullOrEmpty(extension)) return MediaTypeEnum.None;

            if (extension.Trim()[0]=='.')
            {
                ext = extension.Substring(1);
            }
            else
            {
                ext = extension;
            }
            if (ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "png")
            {
                return MediaTypeEnum.Image;
            }
            else if (ext == "mp4" || ext == "mpg" || ext == "flv" || ext == "avi" || ext == "xvid")
            {
                return MediaTypeEnum.Video;
            }
            else
            {
                return MediaTypeEnum.Document;
            }
        }
    }
}
