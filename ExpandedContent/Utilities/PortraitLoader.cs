using ExpandedContent.Config;
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
    }
}
