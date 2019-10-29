# Playlist File Format - Technical Specifications

## File Encoding
The playlist data is serialized using [BSON](http://bsonspec.org/), and then compressed using the [GZip](https://www.gzip.org/) format.  
Before the GZipped data is the file's Magic Number, 8 bytes that are UTF-8 (or ASCII) encoded text `Blist.v2`  
The extension for playlist files is `.blist`

## BSON Structure
### Playlist
| Key | Type | Extra Info |
| - | - | - |
| `title` | `string` | n/a |
| `author` | `string` | n/a |
| `description` | `string,null` | n/a |
| `cover` | `binary` | Can be PNG / JPEG, should be 1:1 aspect ratio |
| `maps` | `Beatmap[]` | See [Beatmap](#beatmap) for structure |

### Beatmap
| Key | Type | Extra Info |
| - | - | - |
| `type` | `uint enum` | See [Beatmap Types](#beatmap-types) for valid enum values |
| `dateAdded` | `date` | Date when this map was added to the playlist |
| `key` | `uint` | Only present when `type == "key"` |
| `hash` | `byte[20]` | Only present when `type == "hash"` |
| `bytes` | `binary` | Only present when `type == "zip"` |
| `levelID` | `string` | Only present when `type == "levelID"` |

### Beatmap Types
| Type | Usage |
| - | - |
| `0` | Uses a [BeatSaver key](#beatsaver-key-format) to reference a beatmap |
| `1` | Uses a [Beatmap hash](#beatmap-hash-format) to reference a beatmap |
| `2` | Embeds a [Beatmap zip](#beatmap-zips) in the playlist |
| `3` | Uses a Level ID to reference a beatmap.<br />For custom beatmaps this is derived from the hash, so it is preferred to use `hash`<br />This is best used to reference OST beatmaps. |

The enum system was adopted to enable the extension of this specification to add more map referencing systems. **Additional types may be added at a later date.**

## Addendum
### BeatSaver Key Format
BeatSaver keys are integers. They use a lowercased hex-encoded representation on BeatSaver. When converting from old playlists, you may encounter old BeatSaver keys in the format `XXX-XXX` where `X` represents a decimal digit.

To convert from old to new, take the last number in the old key (after the `-`).

### Beatmap Hash Format
Beatmap hashes are calculated using the `sha1` hashing algorithm.

To calculate beatmap hashes, concatenate `info.dat` and every difficulty `.dat` in the order they appear in `info.dat`, then hash those concatenated bytes.

### Beatmap Zips
Beatmap Zips use the same rules as BeatSaver uploads.
* Files must be at the root of the zip
* Zips should not contain any extraneous files, unrelated to the beatmap
* Beatmaps should be valid and in the 2.x.x format
