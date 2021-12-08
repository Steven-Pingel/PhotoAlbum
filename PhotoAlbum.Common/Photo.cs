namespace PhotoAlbum.Common
{
    public class Photo
    {
        public Photo(int id, string title, Uri url, Uri thumbnailUrl)
        {
            Id = id;
            Title = title;
            Url = url;
            ThumbnailUrl = thumbnailUrl;
        }

        public int Id { get; }
        public string Title { get; }
        public Uri Url { get; }
        public Uri ThumbnailUrl { get; }
    }
}