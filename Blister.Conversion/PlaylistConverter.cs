using System;
using System.Collections.Generic;
using System.IO;
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
        internal static JsonSerializer _serializer = new JsonSerializer();

        /// <summary>
        /// Deserialize legacy playlist JSON
        /// </summary>
        /// <param name="bytes">Legacy playlist JSON bytes</param>
        /// <returns></returns>
        public static LegacyPlaylist DeserializeLegacyPlaylist(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            return DeserializeLegacyPlaylist(ms);
        }

        /// <summary>
        /// Deserialize legacy playlist JSON
        /// </summary>
        /// <param name="text">Legacy playlist JSON text</param>
        /// <returns></returns>
        public static LegacyPlaylist DeserializeLegacyPlaylist(string text)
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(text));
            return DeserializeLegacyPlaylist(ms);
        }

        /// <summary>
        /// Deserialize legacy playlist JSON
        /// </summary>
        /// <param name="stream">Legacy playlist JSON stream</param>
        /// <returns></returns>
        public static LegacyPlaylist DeserializeLegacyPlaylist(Stream stream)
        {
            using var sr = new StreamReader(stream);
            return DeserializeLegacyPlaylist(sr);
        }

        /// <summary>
        /// Deserialize legacy playlist JSON
        /// </summary>
        /// <param name="reader">Legacy playlist JSON stream reader</param>
        /// <returns></returns>
        public static LegacyPlaylist DeserializeLegacyPlaylist(StreamReader reader)
        {
            using var r = new JsonTextReader(reader);
            return _serializer.Deserialize<LegacyPlaylist>(r);
        }

        /// <summary>
        /// Convert a legacy playlist to a v2 playlist struct
        /// </summary>
        /// <param name="legacy">Legacy playlist</param>
        /// <param name="flags">Flags used to control conversion logic</param>
        /// <returns></returns>
        public static Playlist ConvertLegacyPlaylist(LegacyPlaylist legacy, ConversionFlags flags = ConversionFlags.Default)
        {
            bool ignoreInvalidHashes = FlagUtils.HasFlag(flags, ConversionFlags.IgnoreInvalidHashes);
            bool ignoreInvalidKeys = FlagUtils.HasFlag(flags, ConversionFlags.IgnoreInvalidKeys);
            bool ignoreInvalidCover = FlagUtils.HasFlag(flags, ConversionFlags.IgnoreInvalidCover);

            Playlist playlist = new Playlist
            {
                Title = legacy.PlaylistTitle,
                Author = legacy.PlaylistAuthor,
                Description = legacy.PlaylistDescription,

                Maps = new List<Beatmap>()
            };

            try
            {
                byte[] cover = Utils.ParseBase64Image(legacy.Image);
                string mimeType = MimeType.GetMimeType(cover);

                if (mimeType != MimeType.PNG && mimeType != MimeType.JPG)
                {
                    throw new InvalidCoverException(mimeType);
                }

                playlist.Cover = cover;
            }
            catch (InvalidBase64Exception ex)
            {
                if (ignoreInvalidCover) playlist.Cover = null;
                else throw ex;
            }

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

                    if (isValid == false)
                    {
                        if (ignoreInvalidHashes) continue;
                        else throw new InvalidMapHashException(hash);
                    }

                    map.Hash = Utils.StringToByteArray(hash);
                }
                else if (song.Key != null)
                {
                    map.Type = BeatmapType.Key;

                    string key = Utils.ParseKey(song.Key);
                    if (key == null)
                    {
                        if (ignoreInvalidKeys) continue;
                        else throw new InvalidMapKeyException(song.Key);
                    }

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
