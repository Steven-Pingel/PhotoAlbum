using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbum.Common;
using RestSharp;

namespace PhotoAlbum.PlaceholderPhotos
{
    public class PlaceholderPhotoService : IPhotoService
    {
        // This could be extracted to a config file in the event that the placeholder service moves or changes their api
        private const string BaseUrl = "http://jsonplaceholder.typicode.com/";
        private readonly IRestClient _restClient;

        public PlaceholderPhotoService(IRestClient restClient)
        {
            _restClient = restClient;
            restClient.BaseUrl = new Uri(BaseUrl);
        }

        public async Task<Photo> GetPhoto(int photoId)
        {
            var request = new RestRequest($"photos/{photoId}", Method.GET);
            var response = await _restClient.ExecuteAsync<PlaceholderPhotoDto>(request);
            var result = response.Data.ToPhoto();
            return result;
        }

        public async Task<Album> GetAlbum(int albumId)
        {
            var albumRequest = new RestRequest($"albums/{albumId}", Method.GET);
            var albumPhotoRequest = new RestRequest($"photos", Method.GET);
            albumPhotoRequest.AddQueryParameter("albumId", albumId.ToString());

            var albumTask = _restClient.ExecuteAsync<PlaceholderAlbumDto>(albumRequest);
            var albumPhotosTask = _restClient.ExecuteAsync<List<PlaceholderPhotoDto>>(albumPhotoRequest);
        
            var albumResult = await albumTask;
            var albumPhotosResult = await albumPhotosTask;
            
            if (albumResult.Data.Id == 0){
                throw new Exception($"Unable to locate results for album id: {albumId}");
            }

            var photos = albumPhotosResult.Data.Select(dto => dto.ToPhoto()).ToList();
            var album = albumResult.Data.ToAlbum(photos);
            return album;
        }
    }
}