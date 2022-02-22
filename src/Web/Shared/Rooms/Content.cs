using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Rooms
{
    public class Content
    {
        public Content()
        {
            _url = "";
        }
        [Required(ErrorMessage = "Введите ссылку")]
        [Url]
        public string Url { get => _url; set => _url = UrlConverter.Convert(value); }
        private string _url;
        public long Id { get; set; }
        public string? Name { get; set; }
       
    }
    public static class UrlConverter
    {
        public static string Convert(string url)
        {
            switch (url)
            {
                case string u when u.Contains("youtube.com/watch"):
                    return YoutubeEmbedConvert(u);
                default:
                    return url;
            }
        }
        private static string YoutubeEmbedConvert(string url) 
        {
            var youtubeVideoId = url[(url.IndexOf("?v=")+3)..url.IndexOf("&")];
            return "https://www.youtube.com/embed/" + youtubeVideoId;
        }
    }
}
