using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Blister.Types;
using Blister.Conversion.Types;

namespace Blister.Conversion
{
    public static class PlaylistConverter
    {
        public static LegacyPlaylist LoadLegacyPlaylist(byte[] bytes)
        {
            string text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return LoadLegacyPlaylist(text);
        }

        public static LegacyPlaylist LoadLegacyPlaylist(string text)
        {
            return JsonConvert.DeserializeObject<LegacyPlaylist>(text);
        }

        public static Playlist ConvertLegacyPlaylist(LegacyPlaylist legacy)
        {
            Playlist playlist = new Playlist
            {
                Title = legacy.PlaylistTitle,
                Author = legacy.PlaylistAuthor,
                Description = legacy.PlaylistDescription,

                Cover = Utils.ParseBase64Image(legacy.Image),
                Maps = new List<Beatmap>()
            };

            foreach (var song in legacy.Songs)
            {
                Beatmap map = new Beatmap();

                if (song.Hash != null)
                {
                    map.Type = "hash";
                    map.Hash = song.Hash;
                }
                else if (song.Key != null)
                {
                    map.Type = "key";
                    map.Key = Utils.ParseKey(song.Key);
                }
                else if (song.LevelID != null)
                {
                    map.Type = "levelID";
                    map.LevelID = song.LevelID;
                }

                playlist.Maps.Add(map);
            }

            return playlist;
        }
    }
}
