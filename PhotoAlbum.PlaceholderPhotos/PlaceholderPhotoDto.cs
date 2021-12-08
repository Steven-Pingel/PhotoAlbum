using System;
using PhotoAlbum.Common;

namespace PhotoAlbum.PlaceholderPhotos
{
    public class PlaceholderPhotoDto
    {
        public int AlbumId {get; set;}
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Url { get; set; } = "";
        public string ThumbnailUrl { get; set; } = "";

        public Photo ToPhoto(){
            return new Photo(Id, Title, new Uri(Url), new Uri(ThumbnailUrl));
        }
    }
}