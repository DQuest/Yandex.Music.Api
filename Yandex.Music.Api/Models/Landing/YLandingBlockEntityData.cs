﻿using Yandex.Music.Api.Models.Common;
using Yandex.Music.Api.Models.Playlist;

namespace Yandex.Music.Api.Models.Landing
{
    public class YLandingBlockEntityData
    {
        #region Свойства

        public YPlaylist Data { get; set; }
        public YDescription Description { get; set; }
        public bool Notify { get; set; }
        public bool Ready { get; set; }
        public YGeneratedPlaylistType Type { get; set; }

        #endregion
    }
}