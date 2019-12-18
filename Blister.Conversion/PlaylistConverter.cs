using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Blister.Types;
using Blister.Conversion.Types;

namespace Blister.Conversion
{
    /// <summary>
    /// Main methods for Deserializing legacy playlists
    /// </summary>
    public static class PlaylistConverter
    {
        /// <summary>
        /// Deserialize legacy playlist JSON
        /// </summary>
        /// <param name="bytes">Legacy playlist JSON bytes</param>
        /// <returns></returns>
        public static LegacyPlaylist DeserializeLegacyPlaylist(byte[] bytes)
        {
            string text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return DeserializeLegacyPlaylist(text);
        }

        /// <summary>
        /// Deserialize legacy playlist JSON
        /// </summary>
        /// <param name="text">Legacy playlist JSON text</param>
        /// <returns></returns>
        public static LegacyPlaylist DeserializeLegacyPlaylist(string text)
        {
            return JsonConvert.DeserializeObject<LegacyPlaylist>(text);
        }

        /// <summary>
        /// Convert a legacy playlist to a v2 playlist struct
        /// </summary>
        /// <param name="legacy">Legacy playlist</param>
        /// <returns></returns>
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
                Beatmap map = new Beatmap()
                {
                    DateAdded = DateTime.Now,
                };

                if (song.Hash != null)
                {
                    map.Type = BeatmapType.Hash;

                    string hash = song.Hash.ToLower();
                    bool isValid = Utils.ValidHash(hash);
                    if (!isValid) throw new InvalidMapHashException(hash);

                    map.Hash = Utils.StringToByteArray(hash);
                }
                else if (song.Key != null)
                {
                    map.Type = BeatmapType.Key;

                    string key = Utils.ParseKey(song.Key);
                    if (key == null) throw new InvalidMapKeyException(song.Key);

                    map.Key = Convert.ToUInt32(key, 16);
                }
                else if (song.LevelID != null)
                {
                    map.Type = BeatmapType.LevelID;
                    map.LevelID = song.LevelID;
                }

                playlist.Maps.Add(map);
            }

            return playlist;
        }
    }
}
