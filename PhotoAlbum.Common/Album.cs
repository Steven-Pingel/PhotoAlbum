namespace PhotoAlbum.Common
{
    public class Album {
        public Album(int id, string title, IReadOnlyCollection<Photo> photos)
        {
            Id = id;
            Title = title;
            Photos = photos;
        }

        public int Id { get; }

        public string Title { get; }

        public IReadOnlyCollection<Photo> Photos {get ; }
    }
}