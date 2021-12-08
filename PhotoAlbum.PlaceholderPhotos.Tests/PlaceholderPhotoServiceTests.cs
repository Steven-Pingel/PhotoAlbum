using FluentAssertions;
using PhotoAlbum.Common;
using RestSharp;
using Xunit;

namespace PhotoAlbum.PlaceholderPhotos.Tests
{
    public class PlaceholderPhotoServiceTests
    {
        // Ideal: convert from integration tests that actually hit the internet, to tests that use static mocked data.
        // Placeholder data is static.  Could abstract out a client that returns DTOs, and then mock that client but that seems excessive
        [Fact]
        public async Task GetAlbum_should_return_collection_of_photos_for_valid_album_id()
        {
            var sut = new PlaceholderPhotoService(new RestClient());

            var result = await sut.GetAlbum(1);
            result.Id.Should().Be(1);
            result.Title.Should().Be("quidem molestiae enim");
            result.Photos.Count.Should().Be(50);
        }

        [Fact]
        public async Task GetPhoto_should_return_photo_details_for_valid_photo_id()
        {
            var sut = new PlaceholderPhotoService(new RestClient());

            var result = await sut.GetPhoto(1);
            result.Id.Should().Be(1);
            result.Title.Should().Be("accusamus beatae ad facilis cum similique qui sunt");
            result.Url.Should().Be("https://via.placeholder.com/600/92c952");
            result.ThumbnailUrl.Should().Be("https://via.placeholder.com/150/92c952");
        }

        [Fact]
        public async Task GetAlbum_should_throw_not_found_error_when_album_not_found()
        {
            var sut = new PlaceholderPhotoService(new RestClient());

            await sut.Invoking(x => x.GetAlbum(1000000))
                .Should().ThrowAsync<Exception>()
                .WithMessage("Unable to locate results for album id: 1000000");
        }

        [Fact]
        public void AlbumDto_toAlbum_can_be_built_with_empty_photo_collection()
        {
            var sut = new PlaceholderAlbumDto()
            {
                Id = 1,
                Title = "Trip to mars"
            };
            sut.Invoking(x => x.ToAlbum(new List<Photo>())).Should().NotThrow();
        }

        [Fact]
        public void PhotoDto_toPhoto_throws_with_invalid_thumbnail_url()
        {
            var sut = new PlaceholderPhotoDto()
            {
                Id = 1,
                AlbumId = 1,
                ThumbnailUrl = "obviouslynotaurl",
                Title = "Mountain",
                Url = "www.test.com"
            };

            sut.Invoking(x => x.ToPhoto()).Should().Throw<UriFormatException>();
        }
    
        [Fact]
        public void PhotoDto_toPhoto_throws_with_invalid_url()
        {
            var sut = new PlaceholderPhotoDto()
            {
                Id = 1,
                AlbumId = 1,
                ThumbnailUrl = "www.test.com",
                Title = "Mountain",
                Url = "obviouslynotaurl"
            };

            sut.Invoking(x => x.ToPhoto()).Should().Throw<UriFormatException>();
        }

        [Fact]
        public void PhotoDto_toPhoto_builds_with_valid_inputs()
        {
            var sut = new PlaceholderPhotoDto()
            {
                Id = 1,
                AlbumId = 1,
                ThumbnailUrl = "http://www.thumbnails.com/1",
                Title = "Mountain",
                Url = "http://www.photos.com/1"
            };

            var result = sut.ToPhoto();
            result.Id.Should().Be(1);
            result.Title.Should().Be("Mountain");
            result.ThumbnailUrl.ToString().Should().Be("http://www.thumbnails.com/1");
            result.Url.ToString().Should().Be("http://www.photos.com/1");
        }
    }
}