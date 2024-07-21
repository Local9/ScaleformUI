﻿using CitizenFX.Core;
using CitizenFX.Core.UI;
using ScaleformUI.Elements;
using ScaleformUI.Scaleforms;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;
using Font = CitizenFX.Core.UI.Font;
namespace ScaleformUI
{
    public enum BusySpinner
    {
        Left,
        Left2,
        Left3,
        Save,
        Right,
    };

    public enum NotificationType : int
    {
        Default = 0,
        Bubble = 1,
        Mail = 2,
        FriendRequest = 3,
        Default2 = 4,
        Reply = 7,
        ReputationPoints = 8,
        Money = 9
    }

    public enum NotificationColor
    {
        Red = 27,
        Yellow = 50,
        Gold = 12,
        GreenLight = 46,
        GreenDark = 47,
        Cyan = 48,
        Blue = 51,
        Purple = 49,
        Rose = 45
    }

    // Had to copy from Cfx API or i wouldn't have been able to use it
    public sealed class ScaleformUINotification
    {
        private readonly IRageNatives _natives;

        #region Fields
        int _handle;
        #endregion

        internal ScaleformUINotification(int handle)
        {
            _natives = Main.GetNativesHandler();
            _handle = handle;
        }

        /// <summary>
        /// Hides this <see cref="ScaleformUINotification"/> instantly
        /// </summary>
        public void Hide()
        {
            _natives.ThefeedRemoveItem(_handle);
        }
    }


    public class Notifications
    {
        /// <summary>
        /// Shows a simple notification
        /// </summary>
        /// <param name="msg"> Notification text </param>
        /// <param name="blink"> if true the notification will blink </param>
        /// <param name="showBriefing"> if true the notification will visible in briefing page in pause menu </param>
        /// <returns></returns>
        public static ScaleformUINotification ShowNotification(string msg, bool blink = false, bool showBriefing = true)
        {
            IRageNatives natives = Main.GetNativesHandler();
            natives.AddTextEntry("ScaleformUINotification", msg);
            natives.BeginTextCommandThefeedPost("ScaleformUINotification");
            return new ScaleformUINotification(natives.EndTextCommandThefeedPostTicker(blink, showBriefing));
        }

        /// <summary>
        /// Shows a simple colored notification
        /// </summary>
        /// <param name="msg"> Notification text </param>
        /// <param name="color"> Notification color </param>
        /// <param name="blink"> if true the notification will blink </param>
        /// <param name="showBriefing"> if true the notification will visible in briefing page in pause menu </param>
        /// <returns></returns>
        public static ScaleformUINotification ShowNotification(string msg, NotificationColor color, bool blink = false, bool showBriefing = true)
        {
            IRageNatives natives = Main.GetNativesHandler();
            natives.AddTextEntry("ScaleformUINotification", msg);
            natives.BeginTextCommandThefeedPost("ScaleformUINotification");
            natives.ThefeedNextPostBackgroundColor((int)color);
            return new ScaleformUINotification(natives.EndTextCommandThefeedPostTicker(blink, showBriefing));
        }

        /// <summary>
        /// Shows the classic Help Text in Top-Left on the screen, call this every frame (on Tick)
        /// </summary>
        /// <param name="helpText">Text to be shown</param>
        public static void ShowHelpNotification(string helpText)
        {
            IRageNatives natives = Main.GetNativesHandler();
            natives.AddTextEntry("ScaleformUIHelpText", helpText);
            natives.DisplayHelpTextThisFrame("ScaleformUIHelpText", false);
        }

        /// <summary>
        /// Shows the classic Help Text in Top-Left on the screen for the amount of time specified (Max 5 seconds, NO NEED TO CALL THIS PER FRAME)
        /// </summary>
        /// <param name="helpText">Text to be shown</param>
        /// <param name="time">Amount of time (milliseconds)</param>
        public static void ShowHelpNotification(string helpText, int time)
        {
            IRageNatives natives = Main.GetNativesHandler();
            if (time > 5000) time = 5000;
            natives.AddTextEntry("ScaleformUIHelpText", helpText);
            natives.BeginTextCommandDisplayHelp("ScaleformUIHelpText");
            natives.EndTextCommandDisplayHelp(0, false, true, time);
        }

        /// <summary>
        /// Shows a 3D Help Notification in the desired coordinates (call this onTick)
        /// </summary>
        /// <param name="msg">The text of the notification</param>
        /// <param name="coords">World coordinates</param>
        /// <param name="time"></param>
        public static void ShowFloatingHelpNotification(string msg, Vector3 coords)
        {
            IRageNatives natives = Main.GetNativesHandler();
            natives.AddTextEntry("ScaleformUIFloatingHelpText", msg);
            natives.SetFloatingHelpTextWorldPosition(1, coords.X, coords.Y, coords.Z);
            natives.SetFloatingHelpTextStyle(1, 1, 2, -1, 3, 0);
            natives.BeginTextCommandDisplayHelp("ScaleformUIFloatingHelpText");
            natives.EndTextCommandDisplayHelp(2, false, false, -1);
        }

        /// <summary>
        /// Shows a notification cellphone style with picture
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="subtitle">Subtitle of the notification</param>
        /// <param name="text">Main text of the notification</param>
        /// <param name="iconSet">The texture dictionary for the icon (see <see cref="NotificationChar"/>for a list of default icons)</param>
        /// <param name="icon">The texture name for the icon</param>
        /// <param name="bgColor">The background color for the notification, defaults to NONE</param>
        /// <param name="flashColor">Set a <see cref="SColor"/> to flash the notification to, if blink is <see langword="true"/> this is ignored</param>
        /// <param name="blink">Wether to blink or not the notification</param>
        /// <param name="type">The <see cref="NotificationType"/></param>
        /// <param name="showInBrief">True to show it in Briefing page in Pause Menu</param>
        /// <param name="sound">If true a sound will be played on notification appearing</param>
        public static ScaleformUINotification ShowAdvancedNotification(string title, string subtitle, string text, string iconSet = "Default", string icon = "Default", HudColor bgColor = HudColor.NONE, SColor flashColor = default, bool blink = false, NotificationType type = NotificationType.Default, bool showInBrief = true, bool sound = true)
        {
            IRageNatives natives = Main.GetNativesHandler();
            natives.AddTextEntry("ScaleformUIAdvancedNotification", text);
            natives.BeginTextCommandThefeedPost("ScaleformUIAdvancedNotification");
            natives.AddTextComponentSubstringPlayerName(text);
            if (bgColor != HudColor.NONE)
                natives.SetNotificationBackgroundColor((int)bgColor);
            if (!flashColor.IsEmpty && !blink)
                natives.SetNotificationFlashColor(flashColor.R, flashColor.G, flashColor.B, flashColor.A);
            if (sound) Audio.PlaySoundFrontend("DELETE", "HUD_DEATHMATCH_SOUNDSET");
            return new ScaleformUINotification(natives.EndTextCommandThefeedPostMessagetext(iconSet, icon, true, (int)type, title, subtitle));
            //return new Notification(EndTextCommandThefeedPostTicker(blink, showInBrief));
        }

        /// <summary>
        /// Shows a statistic increase notification
        /// </summary>
        /// <param name="newProgress">New increased value</param>
        /// <param name="oldProgress">Old value before new value</param>
        /// <param name="title">Title of the notification</param>
        /// <param name="blink">Notification Blink</param>
        /// <param name="showBrief">Show or not in Pause Menu briefing page</param>
        public static async Task<ScaleformUINotification> ShowStatNotification(int newProgress, int oldProgress, string title, bool blink = false, bool showBrief = true)
        {
            IRageNatives natives = Main.GetNativesHandler();
            natives.AddTextEntry("ScaleformUIStatsNotification", title);
            Tuple<int, string> mug = await GetPedMugshotAsync(Game.PlayerPed);
            natives.BeginTextCommandThefeedPost("PS_UPDATE");
            natives.AddTextComponentInteger(newProgress);
            natives.EndTextCommandThefeedPostStats("ScaleformUIStatsNotification", 2, newProgress, oldProgress, false, mug.Item2, mug.Item2);
            int not = natives.EndTextCommandThefeedPostTicker(blink, showBrief);
            natives.UnregisterPedheadshot(mug.Item1);
            return new ScaleformUINotification(not);
        }

        /// <summary>
        /// Show a VS notification (like in GTA:O) taking as left Ped the current PlayerPed
        /// </summary>
        /// <param name="leftScore">Left ped score</param>
        /// <param name="leftColor">Left ped color</param>
        /// <param name="rightPed">The ped to show on the Right side of the notification</param>
        /// <param name="rightScore">Left ped score</param>
        /// <param name="rightColor">Left ped color</param>
        public static async Task<ScaleformUINotification> ShowVSNotification(int leftScore, HudColor leftColor, Ped rightPed, int rightScore, HudColor rightColor)
        {
            return await ShowVSNotification(Game.PlayerPed, leftScore, leftColor, rightPed, rightScore, rightColor);
        }

        /// <summary>
        /// Show a VS notification (like in GTA:O) between 2 peds
        /// </summary>
        /// <param name="leftPed">Ped on the left</param>
        /// <param name="leftScore">Left ped score</param>
        /// <param name="leftColor">Color for Ped on the left</param>
        /// <param name="rightPed">Ped on the right</param>
        /// <param name="rightScore">Left ped score</param>
        /// <param name="rightColor">Color for ped on the right</param>
        public static async Task<ScaleformUINotification> ShowVSNotification(Ped leftPed, int leftScore, HudColor leftColor, Ped rightPed, int rightScore, HudColor rightColor)
        {
            IRageNatives natives = Main.GetNativesHandler();
            Tuple<int, string> mug = await GetPedMugshotAsync(leftPed);
            Tuple<int, string> otherMug = await GetPedMugshotAsync(rightPed);
            natives.BeginTextCommandThefeedPost("");
            return new(natives.EndTextCommandThefeedPostVersusTu(mug.Item2, mug.Item2, leftScore, otherMug.Item2, otherMug.Item2, rightScore, (int)leftColor, (int)rightColor));
        }

        private static void _drawText3d(Vector3 campos, float camfov, string text, Vector3 coord, SColor color, Font font = Font.ChaletComprimeCologne, float scale = 17f)
        {
            IRageNatives natives = Main.GetNativesHandler();
            float dist = Vector3.Distance(coord, campos);
            float scaleInternal = (1 / dist) * scale;
            float fov = (1 / camfov) * 100;
            float _scale = scaleInternal * fov;
            natives.SetTextScale(0.1f * _scale, 0.15f * _scale);
            natives.SetTextFont((int)font);
            natives.SetTextProportional(true);
            natives.SetTextColour(color.R, color.G, color.B, color.A);
            natives.SetTextDropshadow(5, 0, 0, 0, 255);
            natives.SetTextEdge(2, 0, 0, 0, 150);
            natives.SetTextDropShadow();
            natives.SetTextOutline();
            natives.SetTextCentre(true);
            natives.SetDrawOrigin(coord.X, coord.Y, coord.Z, 0);
            natives.BeginTextCommandDisplayText("STRING");
            natives.AddTextComponentSubstringPlayerName(text);
            natives.EndTextCommandDisplayText(0, 0);
            natives.ClearDrawOrigin();
        }

        /// <summary>
        /// Draws a 3D text on the screen at desired world coords relative to the <see cref="GameplayCamera"/>
        /// </summary>
        /// <param name="text">Main text</param>
        /// <param name="coord">World coordinates</param>
        /// <param name="color">Text color</param>
        /// <param name="font">Set the desired <see cref="Font"/></param>
        /// <param name="scale">Text scale (Default is 17.0f)</param>
        public static void DrawText3D(string text, Vector3 coord, SColor color, Font font = Font.ChaletComprimeCologne, float scale = 17f)
        {
            if (Game.IsPaused) return;
            _drawText3d(GameplayCamera.Position, GameplayCamera.FieldOfView, text, coord, color, font, scale);
        }

        /// <summary>
        /// Draws a 3D text on the screen at desired world coords relative to any scripted <see cref="Camera"/>
        /// </summary>
        /// <param name="camera">The scripted Camera to refer</param>
        /// <param name="text">Main text</param>
        /// <param name="coord">World coordinates</param>
        /// <param name="color">Text color</param>
        /// <param name="font">Set the desired <see cref="Font"/></param>
        /// <param name="scale">Text scale (Default is 17.0f)</param>
        public static void DrawText3D(Camera camera, string text, Vector3 coord, SColor color, Font font = Font.ChaletComprimeCologne, float scale = 17f)
        {
            if (Game.IsPaused) return;
            _drawText3d(camera.Position, camera.FieldOfView, text, coord, color, font, scale);

        }

        /// <summary>
        /// Draws a 2D text on screen
        /// </summary>
        /// <param name="x">X position on the screen (default centered)</param>
        /// <param name="y">Y position on the screen (default 0.8f from top)</param>
        /// <param name="text">Text to draw</param>
        /// <param name="color">Color of the text (Default white)</param>
        /// <param name="font">Text font (default ChaletComprimeCologne)</param>
        /// <param name="TextAlignment">Text alignment (default center)</param>
        /// <param name="Outline">True to outline the text (default true)</param>
        /// <param name="Wrap">Wrap the text at desired boundries (default 0)</param>
        public static void DrawText(float x = 0.5f, float y = 0.8f, string text = "", SColor color = default, Font font = Font.ChaletComprimeCologne, Alignment TextAlignment = Alignment.Center, bool Outline = true, float Wrap = 0)
        {
            IRageNatives natives = Main.GetNativesHandler();
            if (Game.IsPaused || string.IsNullOrWhiteSpace(text)) return;
            int screenw = Screen.Resolution.Width;
            int screenh = Screen.Resolution.Height;
            const float height = 1080f;
            float ratio = (float)screenw / screenh;
            float width = height * ratio;

            natives.SetTextFont((int)font);
            natives.SetTextScale(0.0f, 0.5f);
            if (color.IsEmpty)
                natives.SetTextColour(255, 255, 255, 255);
            else
                natives.SetTextColour(color.R, color.G, color.B, color.A);
            if (Outline)
                natives.SetTextOutline();
            if (Wrap != 0)
            {
                float xsize = (x + Wrap) / width;
                natives.SetTextWrap(x, xsize);
            }
            switch (TextAlignment)
            {
                case Alignment.Center:
                    natives.SetTextCentre(true);
                    break;
                case Alignment.Right:
                    natives.SetTextRightJustify(true);
                    natives.SetTextWrap(0, x);
                    break;
            }
            natives.SetTextEntry("jamyfafi");
            natives.AddTextComponentSubstringPlayerName(text);
            natives.EndTextCommandDisplayText(x, y);
        }

        /// <summary>
        /// Creates a loading prompt in the lower right of the screen.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="busySpinner"></param>
        public static void StartLoadingMessage(string label, LoadingSpinnerType busySpinner = LoadingSpinnerType.SocialClubSaving, int savingTime = -1)
        {
            IRageNatives natives = Main.GetNativesHandler();
            string textOutput = Game.GetGXTEntry(label);
            if (string.IsNullOrEmpty(textOutput))
                textOutput = label;
            if (savingTime > 0)
                Main.InstructionalButtons.AddSavingText(busySpinner, textOutput, savingTime);
            else
                Main.InstructionalButtons.AddSavingText(busySpinner, textOutput);
        }

        /// <summary>
        /// Removes the loading prompt.
        /// </summary>
        public static void StopLoadingMessage()
        {
            Main.InstructionalButtons.HideSavingText();
        }

        internal static async Task<Tuple<int, string>> GetPedMugshotAsync(Ped ped, bool transparent = false)
        {
            IRageNatives natives = Main.GetNativesHandler();
            int mugshot = natives.RegisterPedheadshot(ped.Handle);
            if (transparent) mugshot = natives.RegisterPedheadshotTransparent(ped.Handle);
            while (!natives.IsPedheadshotReady(mugshot)) await BaseScript.Delay(1);
            string txd = natives.GetPedheadshotTxdString(mugshot);

            return new Tuple<int, string>(mugshot, txd);
        }
    }
}
