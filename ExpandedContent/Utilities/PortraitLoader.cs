using ExpandedContent.Config;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.ResourceManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Utilities {
    public class PortraitLoader {
        public static Sprite LoadInternal(string folder, string file, Vector2Int size, TextureFormat format) {
            return Image2Sprite.Create($"{ModSettings.ModEntry.Path}Assets{Path.DirectorySeparatorChar}{folder}{Path.DirectorySeparatorChar}{file}", size, format);
        }
        // Loosely based on https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
        public static class Image2Sprite {
            public static string icons_folder = "";
            public static Sprite Create(string filePath, Vector2Int size, TextureFormat format) {
                var bytes = File.ReadAllBytes(icons_folder + filePath);
                var texture = new Texture2D(size.x, size.y, format, false);
                texture.mipMapBias = 15.0f;
                _ = texture.LoadImage(bytes);
                return Sprite.Create(texture, new Rect(0, 0, size.x, size.y), new Vector2(0, 0));
            }
        }

        public static PortraitData LoadPortraitData(string name) {
            var imageFolderPath = Path.Combine(ModSettings.ModEntry.Path, "Assets", "Portraits");
            var smallImagePath = Path.Combine(imageFolderPath, $"{name}Small.png");
            var mediumImagePath = Path.Combine(imageFolderPath, $"{name}Medium.png");
            var fullImagePath = Path.Combine(imageFolderPath, $"{name}FullLength.png");
            var smallPortraitHandle = new CustomPortraitHandle(smallImagePath, PortraitType.SmallPortrait, CustomPortraitsManager.Instance.Storage) {
                Request = new SpriteLoadingRequest(smallImagePath) {
                    Resource = Image2Sprite.Create(smallImagePath, new Vector2Int(185, 242), TextureFormat.RGBA32)
                }
            };
            var mediumPortraitHandle = new CustomPortraitHandle(mediumImagePath, PortraitType.HalfLengthPortrait, CustomPortraitsManager.Instance.Storage) {
                Request = new SpriteLoadingRequest(mediumImagePath) {
                    Resource = Image2Sprite.Create(mediumImagePath, new Vector2Int(330, 432), TextureFormat.RGBA32)
                }
            };
            var fullPortraitHandle = new CustomPortraitHandle(fullImagePath, PortraitType.FullLengthPortrait, CustomPortraitsManager.Instance.Storage) {
                Request = new SpriteLoadingRequest(fullImagePath) {
                    Resource = Image2Sprite.Create(fullImagePath, new Vector2Int(692, 1024), TextureFormat.RGBA32)
                }
            };
            return new PortraitData(name) {
                SmallPortraitHandle = smallPortraitHandle,
                HalfPortraitHandle = mediumPortraitHandle,
                FullPortraitHandle = fullPortraitHandle,
                PortraitCategory = PortraitCategory.None,
                IsDefault = false,
                InitiativePortrait = false
            };
        }
    }
}
