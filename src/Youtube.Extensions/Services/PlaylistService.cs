using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Youtube.Extensions.Models;

namespace Youtube.Extensions.Services
{
    public record Video(string? Name,string Id);
    public class PlaylistService
    {
        private readonly HttpClient httpClient;
        private const string PlaylistUrl = "https://www.googleapis.com/youtube/v3/playlistItems";

        public PlaylistService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Video>?> GetAllVideosFromPlaylist(string playlistId, string apiKey)
        {
            List<Video> videos = new List<Video>();
            var playlist = await httpClient.GetFromJsonAsync<PlaylistModel>(PlaylistUrl + $"?part=snippet&maxResults=50&playlistId={playlistId}&key={apiKey}");
            if (playlist == null)
                return default;
            if (playlist.Items != null)
                videos.AddRange(playlist.Items.Select(p => new Video(p.Snippet?.Title, p.Snippet!.ResourceId!.VideoId!)));
            while (playlist!=null && !string.IsNullOrEmpty(playlist?.NextPageToken))
            {
                playlist = await httpClient.GetFromJsonAsync<PlaylistModel>(PlaylistUrl + $"?part=snippet&maxResults=50&playlistId={playlistId}&key={apiKey}&pageToken={playlist.NextPageToken}");
                if(playlist != null)
                videos.AddRange(playlist.Items!.Select(p => new Video(p.Snippet?.Title, p.Snippet!.ResourceId!.VideoId!)));
            }
            return videos;
        }
    }
}
