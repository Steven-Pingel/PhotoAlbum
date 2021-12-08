using PhotoAlbum.Common;

namespace PhotoAlbum.PlaceholderPhotos
{
    public class PlaceholderAlbumDto
        {
            public int Id { get;  set; }

            public string Title { get; set;} = "";

            public Album ToAlbum(IReadOnlyCollection<Photo> photos){
                return new Album(Id, Title, photos);
            }
        }
    }