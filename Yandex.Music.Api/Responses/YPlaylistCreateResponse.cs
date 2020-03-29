using Yandex.Music.Api.Common;
using Yandex.Music.Api.Common.YPlaylist;

namespace Yandex.Music.Api.Responses
{
    public class YPlaylistCreateResponse
    {
        public bool Success { get; set; }
        public YPlaylist Playlist { get; set; }
    }
}