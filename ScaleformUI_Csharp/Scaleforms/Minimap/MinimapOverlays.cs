using CitizenFX.Core;
using ScaleformUI.Elements;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;
using System.Drawing;

namespace ScaleformUI.Scaleforms
{
    public class MinimapOverlay
    {
        internal int handle;
        internal string txd;
        internal string txn;
        internal SColor color;
        internal Vector2 position;
        internal float rotation;
        internal SizeF size;
        internal float alpha;
        internal bool centered;

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                MinimapOverlays.HideOverlay(Handle, !visible);
            }
        }

        public int Handle { get => handle; set => handle = value; }
        public string Txd { get => txd; set => txd = value; }
        public string Txn { get => txn; set => txn = value; }
        public SColor Color
        {
            get => color;
            set
            {
                color = value;
                MinimapOverlays.SetOverlayColor(Handle, color);
            }
        }
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                MinimapOverlays.SetOverlayPosition(Handle, position);
            }
        }
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                MinimapOverlays.SetOverlayRotation(Handle, rotation);
            }
        }
        public SizeF Size
        {
            get => size;
            set
            {
                size = value;
                MinimapOverlays.SetOverlaySizeOrScale(Handle, size);
            }
        }

        internal bool visible;

        public float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
                MinimapOverlays.SetOverlayAlpha(Handle, alpha);
            }
        }

        public bool Centered { get => centered; set => centered = value; }

        public MinimapOverlay() { }

        public MinimapOverlay(int handle, string txd, string txn, Vector2 pos, float r, SizeF size, int a, bool centered)
        {
            this.handle = handle;
            this.txd = txd;
            this.txn = txn;
            this.color = Color;
            this.position = pos;
            this.size = size;
            this.rotation = r;
            this.centered = centered;
        }
    }

    public static class MinimapOverlays
    {
        private static IRageNatives _natives;

        internal static List<MinimapOverlay> minimaps = new();
        internal static int overlay = 0;

        internal static async Task Load()
        {
            if (_natives == null)
                _natives = Main.GetNativesHandler();

            overlay = _natives.AddMinimapOverlay("files/MINIMAP_LOADER.gfx");
            while (!_natives.HasMinimapOverlayLoaded(overlay)) await BaseScript.Delay(0);
            _natives.SetMinimapOverlayDisplay(overlay, 0f, 0f, 100f, 100f, 100f);
        }

        private static async Task<MinimapOverlay> addOverlay(string method, string txd, string txn, float x, float y, float r, float w, float h, int a, bool centered)
        {
            if (overlay == 0) await Load();

            if (!_natives.HasStreamedTextureDictLoaded(txd))
            {
                _natives.RequestStreamedTextureDict(txd, false);
                while (!_natives.HasStreamedTextureDictLoaded(txd)) await BaseScript.Delay(0);
            }

            _natives.CallMinimapScaleformFunction(overlay, method);
            _natives.ScaleformMovieMethodAddParamTextureNameString(txd);
            _natives.ScaleformMovieMethodAddParamTextureNameString(txn);
            _natives.ScaleformMovieMethodAddParamFloat(x);
            _natives.ScaleformMovieMethodAddParamFloat(y);
            _natives.ScaleformMovieMethodAddParamFloat(r);
            _natives.ScaleformMovieMethodAddParamFloat(w);
            _natives.ScaleformMovieMethodAddParamFloat(h);
            _natives.ScaleformMovieMethodAddParamInt(a);
            _natives.ScaleformMovieMethodAddParamBool(centered);
            _natives.EndScaleformMovieMethod();

            _natives.SetStreamedTextureDictAsNoLongerNeeded(txd);

            MinimapOverlay minOv = new MinimapOverlay(minimaps.Count + 1, txd, txn, new Vector2(x, y), r, new SizeF(w, h), a, centered);
            minimaps.Add(minOv);
            return minOv;

        }

        /// <summary>
        /// Adds a custom minimap in any place of the map specified
        /// </summary>
        /// <param name="textureDict">The texture dictionary</param>
        /// <param name="textureName">The texture name</param>
        /// <param name="x">X world coordinate</param>
        /// <param name="y">Y world coordinate</param>
        /// <param name="rotation">Rotation of the texture in degrees (default 0)</param>
        /// <param name="width">The width of the texture in pixels (default -1 to leave on original size, else this will override original width)</param>
        /// <param name="height">The height of the texture in pixels (default -1 to leave on original size, else this will override original height)</param>
        /// <param name="alpha">The alpha transparency value of the texture (default 100)</param>
        /// <param name="centered"><see langword="true"/> to center the texture to the given coordinates, <see langword="false"/> to center it on its top-left corner</param>
        /// <returns>The overlay ID in Dictionary</returns>
        public static async Task<MinimapOverlay> AddSizedOverlayToMap(string textureDict, string textureName, float x, float y, float rotation = 0, float width = -1, float height = -1, int alpha = 100, bool centered = false)
        {
            return await addOverlay("ADD_SIZED_OVERLAY", textureDict, textureName, x, y, rotation, width, height, alpha, centered);
        }

        /// <summary>
        /// Adds a custom minimap in any place of the map specified
        /// </summary>
        /// <param name="textureDict">The texture dictionary</param>
        /// <param name="textureName">The texture name</param>
        /// <param name="x">X world coordinate</param>
        /// <param name="y">Y world coordinate</param>
        /// <param name="rotation">Rotation of the texture in degrees (default 0)</param>
        /// <param name="xScale">The horizontal scale (percentage) of the texture (default 100)</param>
        /// <param name="yScale">The vertical scale (percentage) of the texture (default 100)</param>
        /// <param name="alpha">The alpha transparency value of the texture (default 100)</param>
        /// <param name="centered"><see langword="true"/> to center the texture to the given coordinates, <see langword="false"/> to center it on its top-left corner</param>
        /// <returns>The overlay ID in Dictionary</returns>
        public static async Task<MinimapOverlay> AddScaledOverlayToMap(string textureDict, string textureName, float x, float y, float rotation = 0, float xScale = 100f, float yScale = 100f, int alpha = 100, bool centered = false)
        {
            return await addOverlay("ADD_SCALED_OVERLAY", textureDict, textureName, x, y, rotation, xScale, yScale, alpha, centered);
        }

        /// <summary>
        /// Sets the selected overlay's color (argb)
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="color"></param>
        public static void SetOverlayColor(int overlayId, SColor color)
        {
            if (overlay == 0) return;
            _natives.CallMinimapScaleformFunction(overlay, "SET_OVERLAY_COLOR");
            _natives.ScaleformMovieMethodAddParamInt(overlayId - 1);
            _natives.ScaleformMovieMethodAddParamInt(color.A);
            _natives.ScaleformMovieMethodAddParamInt(color.R);
            _natives.ScaleformMovieMethodAddParamInt(color.G);
            _natives.ScaleformMovieMethodAddParamInt(color.B);
            _natives.EndScaleformMovieMethod();
            minimaps[overlayId - 1].color = color;
        }

        /// <summary>
        /// Hides the selected overlay or shows it
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="hide"></param>
        public static void HideOverlay(int overlayId, bool hide)
        {
            if (overlay == 0) return;
            _natives.CallMinimapScaleformFunction(overlay, "HIDE_OVERLAY");
            _natives.ScaleformMovieMethodAddParamInt(overlayId - 1);
            _natives.ScaleformMovieMethodAddParamBool(hide);
            _natives.EndScaleformMovieMethod();
            minimaps[overlayId - 1].visible = !hide;
        }

        /// <summary>
        /// Changes overlay's Alpha
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="hide"></param>
        public static void SetOverlayAlpha(int overlayId, float alpha)
        {
            if (overlay == 0) return;
            _natives.CallMinimapScaleformFunction(overlay, "SET_OVERLAY_ALPHA");
            _natives.ScaleformMovieMethodAddParamInt(overlayId - 1);
            _natives.ScaleformMovieMethodAddParamFloat(alpha);
            _natives.EndScaleformMovieMethod();
            minimaps[overlayId - 1].alpha = alpha;
        }

        /// <summary>
        /// Changes the overlay coordinates
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="pos"></param>
        public static void SetOverlayPosition(int overlayId, Vector2 pos)
        {
            overlayPos(overlayId, pos.X, pos.Y);
        }

        /// <summary>
        /// Changes the overlay coordinates
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="pos"></param>
        public static void SetOverlayPosition(int overlayId, float x, float y)
        {
            overlayPos(overlayId, x, y);
        }

        /// <summary>
        /// Changes the overlay coordinates
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="pos"></param>
        public static void SetOverlayPosition(int overlayId, float[] pos)
        {
            overlayPos(overlayId, pos[0], pos[1]);
        }

        /// <summary>
        /// Changes the overlay size, if the overlay is scaled then the values must be in percentage
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="size"></param>
        public static void SetOverlaySizeOrScale(int overlayId, SizeF size)
        {
            overlaySize(overlayId, size.Width, size.Height);
        }

        /// <summary>
        /// Changes the overlay size, if the overlay is scaled then the values must be in percentage
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="size"></param>
        public static void SetOverlaySizeOrScale(int overlayId, float w, float h)
        {
            overlaySize(overlayId, w, h);
        }

        /// <summary>
        /// Changes the overlay size, if the overlay is scaled then the values must be in percentage
        /// </summary>
        /// <param name="overlayId"></param>
        /// <param name="size"></param>
        public static void SetOverlaySizeOrScale(int overlayId, float[] size)
        {
            overlaySize(overlayId, size[0], size[1]);
        }

        public static void SetOverlayRotation(int overlayId, float rotation)
        {
            if (overlay == 0) return;
            _natives.CallMinimapScaleformFunction(overlay, "UPDATE_OVERLAY_ROTATION");
            _natives.ScaleformMovieMethodAddParamInt(overlayId - 1);
            _natives.ScaleformMovieMethodAddParamFloat(rotation);
            _natives.EndScaleformMovieMethod();
            minimaps[overlayId - 1].rotation = rotation;
        }

        private static void overlayPos(int overlayId, float x, float y)
        {
            if (overlay == 0) return;
            _natives.CallMinimapScaleformFunction(overlay, "UPDATE_OVERLAY_POSITION");
            _natives.ScaleformMovieMethodAddParamInt(overlayId - 1);
            _natives.ScaleformMovieMethodAddParamFloat(x);
            _natives.ScaleformMovieMethodAddParamFloat(y);
            _natives.EndScaleformMovieMethod();
            minimaps[overlayId - 1].position = new Vector2(x, y);
        }

        private static void overlaySize(int overlayId, float w, float h)
        {
            if (overlay == 0) return;
            _natives.CallMinimapScaleformFunction(overlay, "UPDATE_OVERLAY_SIZE_OR_SCALE");
            _natives.ScaleformMovieMethodAddParamInt(overlayId - 1);
            _natives.ScaleformMovieMethodAddParamFloat(w);
            _natives.ScaleformMovieMethodAddParamFloat(h);
            _natives.EndScaleformMovieMethod();
            minimaps[overlayId - 1].size = new SizeF(w, h);
        }


        /// <summary>
        /// Remove an overlay from the ingame Map
        /// </summary>
        /// <param name="overlayId">Id of the overlay to remove</param>
        public static async void RemoveOverlayFromMinimap(int overlayId)
        {
            if (overlay == 0) await Load();
            _natives.CallMinimapScaleformFunction(overlay, "REM_OVERLAY");
            _natives.ScaleformMovieMethodAddParamInt(overlayId - 1);
            _natives.EndScaleformMovieMethod();
            minimaps.RemoveAt(overlayId - 1);
        }

        /// <summary>
        /// Removes all the overlays from the minimap
        /// </summary>
        public static void ClearAll()
        {
            if (overlay == 0) return;
            _natives.CallMinimapScaleformFunction(overlay, "CLEAR_ALL");
            _natives.EndScaleformMovieMethod();
            minimaps.Clear();
        }
    }
}
