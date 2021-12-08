using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoAlbum.Common;
using PhotoAlbum.PlaceholderPhotos;
using RestSharp;
using Spectre.Console;

namespace PhotoAlbum.Console
{
    public class Program {
        public static async Task Main() {
            var host = CreateHostBuilder().Build();
            var photoService = host.Services.GetService<IPhotoService>();
            if(photoService == null) {
                System.Console.WriteLine("Photo service not registered, ensure you have an implementation of IPhotoService registered in your DI container.");
                return;
            }

            var albumId = AnsiConsole.Ask<int>("What [green]album Id[/] do you want to view?");
            var mode = AnsiConsole.Prompt(
                new TextPrompt<string>("Do you want [green]summary[/] or [green]detailed[/] information?")
                    .DefaultValue("summary")
                    .AddChoice("summary")
                    .AddChoice("detailed"));
            try
            {
                var album = await photoService.GetAlbum(albumId);
                switch (mode)
                {
                    case "summary":
                        System.Console.WriteLine($"Album {album.Id}:{album.Title} has {album.Photos.Count} photos.");
                        break;
                    case "detailed":
                        var table = new Table();
                        table.Title = new TableTitle($"Album {album.Id}:{album.Title}");
                        table.AddColumns("Id", "Title", "ThumbnailUrl", "Url");
                        foreach (var photo in album.Photos)
                        {
                            table.AddRow(photo.Id.ToString(), photo.Title, photo.ThumbnailUrl.ToString(),
                                photo.Url.ToString());
                        }

                        AnsiConsole.Write(table);
                        break;
                }
            }
            catch(Exception e)
            {
                AnsiConsole.WriteException(e);
            }
            
        }

        private static IHostBuilder CreateHostBuilder() {
            return 
                Host.CreateDefaultBuilder()
                    .ConfigureServices((hostContext, services) => {
                        services.AddTransient<IRestClient, RestClient>();
                        services.AddTransient<IPhotoService, PlaceholderPhotoService>();
                    });
        }
    }
}