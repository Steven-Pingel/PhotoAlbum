namespace PhotoAlbum.Common
{
    public interface IPhotoService
    {
        Task<Photo> GetPhoto(int photoId);
        Task<Album> GetAlbum(int albumId);
    }
}