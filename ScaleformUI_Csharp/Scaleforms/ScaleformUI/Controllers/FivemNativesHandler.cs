#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using ScaleformUI.Elements;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;
using System.Drawing;

namespace ScaleformUI.Scaleforms.ScaleformUI.Controllers
{
    public class FivemNativesHandler : IRageNatives
    {
        private GameCursorSprite? _cursorSprite;

        public float ScreenWidth => Screen.Width;
        public float ScreenHeight => Screen.Height;

        public float GameplayCameraRelativeHeading
        {
            get => API.GetGameplayCamRelativeHeading();
            set => API.SetGameplayCamRelativeHeading(value);
        }

        public GameCursorSprite CursorSprite
        {
            get => _cursorSprite ?? GameCursorSprite.Normal;
            set
            {
                API.SetCursorSprite((int)value);
                _cursorSprite = value;
            }
        }

        public void ActivateFrontendMenu(uint menuhash, bool togglePause, int component)
        {
            API.ActivateFrontendMenu(menuhash, togglePause, component);
        }

        public int AddMinimapOverlay(string name)
        {
            return API.AddMinimapOverlay(name);
        }

        public void AddPointToGpsCustomRoute(float x, float y, float z)
        {
            API.AddPointToGpsCustomRoute(x, y, z);
        }

        public void AddTextComponentInteger(int value)
        {
            API.AddTextComponentInteger(value);
        }

        public void AddTextComponentScaleform(string value)
        {
            API.AddTextComponentScaleform(value);
        }

        public void AddTextComponentString(string text)
        {
            API.AddTextComponentString(text);
        }

        public void AddTextComponentSubstringPlayerName(string text)
        {
            API.AddTextComponentSubstringPlayerName(text);
        }

        public void AddTextComponentSubstringTextLabelHashKey(uint hashKey)
        {
            API.AddTextComponentSubstringTextLabelHashKey(hashKey);
        }

        public void AddTextEntry(string entryKey, string entryText)
        {
            API.AddTextEntry(entryKey, entryText);
        }

        public bool AnimpostfxIsRunning(string effectName)
        {
            return API.AnimpostfxIsRunning(effectName);
        }

        public void AnimpostfxPlay(string effectName, int duration, bool looped)
        {
            API.AnimpostfxPlay(effectName, duration, looped);
        }

        public void AnimpostfxStop(string effectName)
        {
            API.AnimpostfxStop(effectName);
        }

        public void BeginScaleformMovieMethod(int handle, string methodName)
        {
            API.BeginScaleformMovieMethod(handle, methodName);
        }

        public void BeginScaleformMovieMethodHudComponent(int hudComponent, string methodName)
        {
            API.BeginScaleformMovieMethodHudComponent(hudComponent, methodName);
        }

        public void BeginScaleformMovieMethodOnFrontend(string methodName)
        {
            API.BeginScaleformMovieMethodOnFrontend(methodName);
        }

        public void BeginScaleformMovieMethodOnFrontendHeader(string methodName)
        {
            API.BeginScaleformMovieMethodOnFrontendHeader(methodName);
        }

        public void BeginScaleformScriptHudMovieMethod(int hudComponent, string methodName)
        {
            API.BeginScaleformScriptHudMovieMethod(hudComponent, methodName);
        }

        public void BeginTextCommandDisplayHelp(string inputType)
        {
            API.BeginTextCommandDisplayHelp(inputType);
        }

        public void BeginTextCommandDisplayText(string text)
        {
            API.BeginTextCommandDisplayText(text);
        }

        public void BeginTextCommandLineCount(string entry)
        {
            API.BeginTextCommandLineCount(entry);
        }

        public void BeginTextCommandScaleformString(string value)
        {
            API.BeginTextCommandScaleformString(value);
        }

        public void BeginTextCommandThefeedPost(string text)
        {
            API.BeginTextCommandThefeedPost(text);
        }

        public void CallMinimapScaleformFunction(int handle, string fnName)
        {
            API.CallMinimapScaleformFunction(handle, fnName);
        }

        public void ClearDrawOrigin()
        {
            API.ClearDrawOrigin();
        }

        public void ClearGpsCustomRoute()
        {
            API.ClearGpsCustomRoute();
        }

        public void ClearGpsFlags()
        {
            API.ClearGpsFlags();
        }

        public void ClearGpsMultiRoute()
        {
            API.ClearGpsMultiRoute();
        }

        public void ClearPedInPauseMenu()
        {
            API.ClearPedInPauseMenu();
        }

        public void ClearRaceGalleryBlips()
        {
            API.ClearRaceGalleryBlips();
        }

        public int ClonePed(int ped, float heading, bool isNetwork, bool p3)
        {
            return API.ClonePed(ped, heading, isNetwork, p3);
        }

        public void DeleteWaypoint()
        {
            API.DeleteWaypoint();
        }

        public void DisableControlAction(int inputGroup, int control, bool disable)
        {
            API.DisableControlAction(inputGroup, control, disable);
        }

        public void DisableControlThisFrame(int inputGroup, GameControl control)
        {
            Game.DisableControlThisFrame(inputGroup, (Control)control);
        }

        public void DisableControlThisFrame(int inputGroup, int control)
        {
            Game.DisableControlThisFrame(inputGroup, (Control)control);
        }

        public void DisplayHelpTextThisFrame(string text, bool curvedWindow)
        {
            API.DisplayHelpTextThisFrame(text, curvedWindow);
        }

        public int DisplayRadar(bool toggle)
        {
            return API.DisplayRadar(toggle);
        }

        public void DrawRect(float x, float y, float width, float height, int r, int g, int b, int a)
        {
            API.DrawRect(x, y, width, height, r, g, b, a);
        }

        public void DrawScaleformMovie(int handle, float x, float y, float width, float height, int red, int green, int blue, int alpha, int unk)
        {
            API.DrawScaleformMovie(handle, x, y, width, height, red, green, blue, alpha, unk);
        }

        public void DrawScaleformMovieFullscreen(int handle, int red, int green, int blue, int alpha, int unk)
        {
            API.DrawScaleformMovieFullscreen(handle, red, green, blue, alpha, unk);
        }

        public void DrawScaleformMovie_3d(int handle, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float p7, float sharpness, float p9, float scaleX, float scaleY, float scaleZ, int p13)
        {
            API.DrawScaleformMovie_3d(handle, posX, posY, posZ, rotX, rotY, rotZ, p7, sharpness, p9, scaleX, scaleY, scaleZ, p13);
        }

        public void DrawScaleformMovie_3dNonAdditive(int handle, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float p7, float p8, float p9, float scaleX, float scaleY, float scaleZ, int p13)
        {
            API.DrawScaleformMovie_3dNonAdditive(handle, posX, posY, posZ, rotX, rotY, rotZ, p7, p8, p9, scaleX, scaleY, scaleZ, p13);
        }

        public void DrawSprite(string textureDict, string textureName, float screenX, float screenY, float width, float height, float heading, int r, int g, int b, int a)
        {
            API.DrawSprite(textureDict, textureName, screenX, screenY, width, height, heading, r, g, b, a);
        }

        public void DrawText(float x, float y)
        {
            API.DrawText(x, y);
        }

        public void EnableControlAction(int inputGroup, int control, bool enable)
        {
            API.EnableControlAction(inputGroup, control, enable);
        }

        public void EnableControlThisFrame(int inputGroup, GameControl control)
        {
            Game.EnableControlThisFrame(inputGroup, (Control)control);
        }

        public void EnableControlThisFrame(int inputGroup, int control)
        {
            Game.EnableControlThisFrame(inputGroup, (Control)control);
        }

        public void EndScaleformMovieMethod()
        {
            API.EndScaleformMovieMethod();
        }

        public int EndScaleformMovieMethodReturnValue()
        {
            return API.EndScaleformMovieMethodReturnValue();
        }

        public void EndTextCommandDisplayHelp(int shape, bool loop, bool beep, int duration)
        {
            API.EndTextCommandDisplayHelp(0, false, false, 0);
        }

        public void EndTextCommandDisplayText(float x, float y)
        {
            API.EndTextCommandDisplayText(x, y);
        }

        public void EndTextCommandScaleformString()
        {
            API.EndTextCommandScaleformString();
        }

        public void EndTextCommandScaleformString_2()
        {
            API.EndTextCommandScaleformString_2();
        }

        public int EndTextCommandThefeedPostMessagetext(string textureDict, string textureName, bool flash, int iconType, string sender, string subject)
        {
            return API.EndTextCommandThefeedPostMessagetext(textureDict, textureName, flash, iconType, sender, subject);
        }

        public void EndTextCommandThefeedPostStats(string statTitle, int icon, int newProgress, int oldProgress, bool isImportant, string picTxd, string picTxn)
        {
            Function.Call(Hash.END_TEXT_COMMAND_THEFEED_POST_STATS, statTitle, 2, newProgress, oldProgress, false, picTxd, picTxn);
        }

        public int EndTextCommandThefeedPostTicker(bool blink, bool showBriefing)
        {
            return API.EndTextCommandThefeedPostTicker(blink, showBriefing);
        }

        public int EndTextCommandThefeedPostVersusTu(string mugshotOneTxd, string mugshotOneTxn, int leftScore, string mugshotTwoTxd, string mugshotTwoTxn, int rightScore, int leftColor, int rightColor)
        {
            return Function.Call<int>(Hash.END_TEXT_COMMAND_THEFEED_POST_VERSUS_TU, mugshotOneTxd, mugshotOneTxn, leftScore, mugshotTwoTxd, mugshotTwoTxn, rightScore, leftColor, rightColor);
        }

        public string GetControlInstructionalButton(int inputGroup, GameControl control)
        {
            return API.GetControlInstructionalButton(inputGroup, (int)control, 1);
        }

        public int GetCurrentFrontendMenuVersion()
        {
            return API.GetCurrentFrontendMenuVersion();
        }

        public string GetCurrentResourceName()
        {
            return API.GetCurrentResourceName();
        }

        public float GetDisabledControlNormal(int inputGroup, int control)
        {
            return API.GetDisabledControlNormal(inputGroup, control);
        }

        public bool GetGroundZFor_3dCoord(float x, float y, float z, ref float groundZ, bool unk)
        {
            return API.GetGroundZFor_3dCoord(x, y, z, ref groundZ, unk);
        }

        public string GetGXTEntry(string entry)
        {
            return Game.GetGXTEntry(entry);
        }

        public int GetHashKey(string value)
        {
            return API.GetHashKey(value);
        }

        public void GetHudColour(int hudColorIndex, ref int r, ref int g, ref int b, ref int a)
        {
            API.GetHudColour(hudColorIndex, ref r, ref g, ref b, ref a);
        }

        public int GetNetworkTime()
        {
            return API.GetNetworkTime();
        }

        public int GetNetworkTimeAccurate()
        {
            return API.GetNetworkTimeAccurate();
        }

        public string GetPedheadshotTxdString(int mugshotHandle)
        {
            return API.GetPedheadshotTxdString(mugshotHandle);
        }

        public float GetSafeZoneSize()
        {
            return API.GetSafeZoneSize();
        }

        public bool GetScaleformMovieCursorSelection(int handle, ref int eventType, ref int context, ref int itemId, ref int unused)
        {
            return API.GetScaleformMovieCursorSelection(handle, ref eventType, ref context, ref itemId, ref unused);
        }

        public int GetScaleformMovieFunctionReturnInt(int handle)
        {
            return API.GetScaleformMovieFunctionReturnInt(handle);
        }

        public string GetScaleformMovieFunctionReturnString(int handle)
        {
            return API.GetScaleformMovieFunctionReturnString(handle);
        }

        public bool GetScaleformMovieMethodReturnValueBool(int handle)
        {
            return API.GetScaleformMovieMethodReturnValueBool(handle);
        }

        public int GetScaleformMovieMethodReturnValueInt(int handle)
        {
            return API.GetScaleformMovieMethodReturnValueInt(handle);
        }

        public string GetScaleformMovieMethodReturnValueString(int handle)
        {
            return API.GetScaleformMovieMethodReturnValueString(handle);
        }

        public Size GetScreenResolution()
        {
            return Screen.Resolution;
        }

        public int GetSoundId()
        {
            return API.GetSoundId();
        }

        public int GetTextScreenLineCount(float x, float y)
        {
            return API.GetTextScreenLineCount(x, y);
        }

        public float GetTextScreenWidth(bool p0)
        {
            return API.GetTextScreenWidth(p0);
        }

        public void GivePedToPauseMenu(int ped, int flags)
        {
            API.GivePedToPauseMenu(ped, flags);
        }

        public bool HasHudScaleformLoaded(int hudComponent)
        {
            return API.HasHudScaleformLoaded(hudComponent);
        }

        public bool HasMinimapOverlayLoaded(int handle)
        {
            return API.HasMinimapOverlayLoaded(handle);
        }

        public bool HasScaleformMovieLoaded(int handle)
        {
            return API.HasScaleformMovieLoaded(handle);
        }

        public bool HasSoundFinished(int soundId)
        {
            return API.HasSoundFinished(soundId);
        }

        public bool HasStreamedTextureDictLoaded(string textureDict)
        {
            return API.HasStreamedTextureDictLoaded(textureDict);
        }

        public void HideHudComponentThisFrame(int hudComponent)
        {
            API.HideHudComponentThisFrame(hudComponent);
        }

        public void HideHudComponentThisFrame(GameHudComponent hudComponent)
        {
            API.HideHudComponentThisFrame((int)hudComponent);
        }

        public void HideLoadingPrompt()
        {
            Screen.LoadingPrompt.Hide();
        }

        public bool IsControlJustPressed(int inputGroup, GameControl control)
        {
            return API.IsControlJustPressed(inputGroup, (int)control);
        }

        public bool IsControlJustReleased(int inputGroup, GameControl control)
        {
            return API.IsControlJustReleased(inputGroup, (int)control);
        }

        public bool IsControlJustReleased(int inputGroup, int control)
        {
            return API.IsControlJustReleased(inputGroup, control);
        }

        public bool IsControlPressed(int inputGroup, GameControl control)
        {
            return API.IsControlPressed(inputGroup, (int)control);
        }

        public bool IsControlPressed(int inputGroup, int control)
        {
            return API.IsControlPressed(inputGroup, control);
        }

        public bool IsFrontendReadyForControl()
        {
            return API.IsFrontendReadyForControl();
        }

        public bool IsGamePaused()
        {
            return Game.IsPaused;
        }

        public bool IsInputDisabled(int inputGroup)
        {
            return API.IsInputDisabled(inputGroup);
        }

        public bool IsPedheadshotReady(int mugshotHandle)
        {
            return API.IsPedheadshotReady(mugshotHandle);
        }

        public bool IsScaleformMovieMethodReturnValueReady(int handle)
        {
            return API.IsScaleformMovieMethodReturnValueReady(handle);
        }

        public bool IsUsingKeyboard(int inputGroup)
        {
            return API.IsUsingKeyboard(inputGroup);
        }

        public bool IsWarningMessageActive()
        {
            return API.IsWarningMessageActive();
        }

        public void LockMinimapAngle(int angle)
        {
            API.LockMinimapAngle(angle);
        }

        public void LockMinimapPosition(float x, float y)
        {
            API.LockMinimapPosition(x, y);
        }

        public void PauseToggleFullscreenMap(bool enabled)
        {
            API.N_0x2de6c5e2e996f178(enabled ? 1 : 0);
        }

        public int PlayerPedId()
        {
            return API.PlayerPedId();
        }

        public void PlaySound(string audioName, string audioRef)
        {
            API.PlaySoundFrontend(-1, audioName, audioRef, false);
        }

        public void PlaySoundFrontend(int soundId, string audioName, string audioRef, bool p3)
        {
            API.PlaySoundFrontend(soundId, audioName, audioRef, p3);
        }

        public void PlaySoundFrontend(string audioName, string audioRef)
        {
            API.PlaySoundFrontend(-1, audioName, audioRef, false);
        }

        public void PushScaleformMovieFunctionParameterBool(bool value)
        {
            API.PushScaleformMovieFunctionParameterBool(value);
        }

        public void PushScaleformMovieFunctionParameterInt(int value)
        {
            API.PushScaleformMovieFunctionParameterInt(value);
        }

        public void PushScaleformMovieFunctionParameterString(string value)
        {
            API.PushScaleformMovieFunctionParameterString(value);
        }

        public void PushScaleformMovieMethodParameterBool(bool value)
        {
            API.PushScaleformMovieMethodParameterBool(value);
        }

        public void PushScaleformMovieMethodParameterFloat(float value)
        {
            API.PushScaleformMovieMethodParameterFloat(value);
        }

        public void PushScaleformMovieMethodParameterInt(int value)
        {
            API.PushScaleformMovieMethodParameterInt(value);
        }

        public void PushScaleformMovieMethodParameterString(string value)
        {
            API.PushScaleformMovieMethodParameterString(value);
        }

        public int RaceGalleryAddBlip(float x, float y, float z)
        {
            return API.RaceGalleryAddBlip(x, y, z);
        }

        public void RaceGalleryFullscreen(bool enabled)
        {
            API.RaceGalleryFullscreen(enabled);
        }

        public void RaceGalleryNextBlipSprite(int sprite)
        {
            API.RaceGalleryNextBlipSprite(sprite);
        }

        public void RegisterFontFile(string gfxName)
        {
            API.RegisterFontFile(gfxName);
        }

        public int RegisterFontId(string name)
        {
            return API.RegisterFontId(name);
        }

        public int RegisterPedheadshot(int pedHandle)
        {
            return API.RegisterPedheadshot(pedHandle);
        }

        public int RegisterPedheadshotTransparent(int pedHandle)
        {
            return API.RegisterPedheadshotTransparent(pedHandle);
        }

        public void ReleaseSoundId(int soundId)
        {
            API.ReleaseSoundId(soundId);
        }

        public void RequestHudScaleform(int hudComponent)
        {
            API.RequestHudScaleform(hudComponent);
        }

        public int RequestScaleformMovieInstance(string handle)
        {
            return API.RequestScaleformMovieInstance(handle);
        }

        public void RequestScriptAudioBank(string audioBank, bool p2)
        {
            API.RequestScriptAudioBank(audioBank, p2);
        }

        public void RequestStreamedTextureDict(string textureDict, bool p1)
        {
            API.RequestStreamedTextureDict(textureDict, p1);
        }

        public void ScaleformMovieMethodAddParamBool(bool value)
        {
            API.ScaleformMovieMethodAddParamBool(value);
        }

        public void ScaleformMovieMethodAddParamFloat(float value)
        {
            API.ScaleformMovieMethodAddParamFloat(value);
        }

        public void ScaleformMovieMethodAddParamInt(int value)
        {
            API.ScaleformMovieMethodAddParamInt(value);
        }

        public void ScaleformMovieMethodAddParamPlayerNameString(string value)
        {
            API.ScaleformMovieMethodAddParamPlayerNameString(value);
        }

        public void ScaleformMovieMethodAddParamTextureNameString(string textureDict)
        {
            API.ScaleformMovieMethodAddParamTextureNameString(textureDict);
        }

        public void ScaleformMovieMethodAddParamTextureNameString_2(string value)
        {
            API.ScaleformMovieMethodAddParamTextureNameString_2(value);
        }

        public void SetBlipColour(int blip, int color)
        {
            API.SetBlipColour(blip, color);
        }

        public void SetBlipScale(int blip, float scale)
        {
            API.SetBlipScale(blip, scale);
        }

        public void SetCursorLocation(float x, float y)
        {
            API.SetCursorLocation(x, y);
        }

        public void SetDrawOrigin(float x, float y, float z, int p3)
        {
            API.SetDrawOrigin(x, y, z, p3);
        }

        public void SetFloatingHelpTextStyle(int hudIndex, int style, int hudColor, int alpha, int arrowPosition, int boxOffset)
        {
            API.SetFloatingHelpTextStyle(hudIndex, style, hudColor, alpha, arrowPosition, boxOffset);
        }

        public void SetFloatingHelpTextWorldPosition(int hudIndex, float x, float y, float z)
        {
            API.SetFloatingHelpTextWorldPosition(hudIndex, x, y, z);
        }

        public void SetGpsCustomRouteRender(bool enabled, int color, int zoomLevel)
        {
            API.SetGpsCustomRouteRender(enabled, color, zoomLevel);
        }

        public void SetGpsFlags(int p0, float p1)
        {
            API.SetGpsFlags(p0, p1);
        }

        public void SetGpsMultiRouteRender(bool enabled)
        {
            API.SetGpsMultiRouteRender(enabled);
        }

        public void SetInputExclusive(int inputGroup, int control)
        {
            API.SetInputExclusive(inputGroup, control);
        }

        public void SetMapFullScreen(bool toggle)
        {
            API.SetMapFullScreen(toggle);
        }

        public void SetMinimapOverlayDisplay(int handle, float x, float y, float scaleX, float scaleY, float alpha)
        {
            API.SetMinimapOverlayDisplay(handle, x, y, scaleX, scaleY, alpha);
        }

        public void SetMouseCursorActiveThisFrame()
        {
            API.SetMouseCursorActiveThisFrame();
        }

        public void SetMouseCursorSprite(int spriteId)
        {
            API.SetMouseCursorSprite(spriteId);
        }

        public void SetNotificationBackgroundColor(int hudIndex)
        {
            API.SetNotificationBackgroundColor(hudIndex);
        }

        public void SetNotificationFlashColor(int red, int green, int blue, int alpha)
        {
            API.SetNotificationFlashColor(red, green, blue, alpha);
        }

        public void SetPauseMenuPedLighting(bool state)
        {
            API.SetPauseMenuPedLighting(state);
        }

        public void SetPauseMenuPedSleepState(bool state)
        {
            API.SetPauseMenuPedSleepState(state);
        }

        public void SetPlayerBlipPositionThisFrame(float x, float y)
        {
            API.SetPlayerBlipPositionThisFrame(x, y);
        }

        public void SetPlayerControl(int player, bool toggle, int possiblyFlags)
        {
            API.SetPlayerControl(player, toggle, possiblyFlags);
        }

        public void SetPoliceRadarBlips(bool toggle)
        {
            API.SetPoliceRadarBlips(toggle);
        }

        public void SetRadarZoom(int zoomLevel)
        {
            API.SetRadarZoom(zoomLevel);
        }

        public void SetRadarZoomToDistance(float distance)
        {
            API.SetRadarZoomToDistance(distance);
        }

        public void SetScaleformMovieAsNoLongerNeeded(ref int handle)
        {
            API.SetScaleformMovieAsNoLongerNeeded(ref handle);
        }

        public void SetScriptGfxDrawBehindPausemenu(bool enabled)
        {
            API.SetScriptGfxDrawBehindPausemenu(enabled);
        }

        public void SetStreamedTextureDictAsNoLongerNeeded(string textureDict)
        {
            API.SetStreamedTextureDictAsNoLongerNeeded(textureDict);
        }

        public void SetTextCentre(bool align)
        {
            API.SetTextCentre(align);
        }

        public void SetTextColour(int r, int g, int b, int a)
        {
            API.SetTextColour(r, g, b, a);
        }

        public void SetTextDropshadow(int distance, int r, int g, int b, int a)
        {
            API.SetTextDropshadow(distance, r, g, b, a);
        }

        public void SetTextDropShadow()
        {
            API.SetTextDropShadow();
        }

        public void SetTextEdge(int p0, int r, int g, int b, int a)
        {
            API.SetTextEdge(p0, r, g, b, a);
        }

        public void SetTextEntry(string entryType)
        {
            API.SetTextEntry(entryType);
        }

        public void SetTextEntryForWidth(string text)
        {
            API.SetTextEntryForWidth(text);
        }

        public void SetTextFont(int fontType)
        {
            API.SetTextFont(fontType);
        }

        public void SetTextOutline()
        {
            API.SetTextOutline();
        }

        public void SetTextProportional(bool p)
        {
            API.SetTextProportional(p);
        }

        public void SetTextRightJustify(bool toggle)
        {
            API.SetTextRightJustify(toggle);
        }

        public void SetTextScale(float scale, float size)
        {
            API.SetTextScale(scale, size);
        }

        public void SetTextWrap(float start, float end)
        {
            API.SetTextWrap(start, end);
        }

        public void SetWaypointOff()
        {
            API.SetWaypointOff();
        }

        public void ShowCursorThisFrame()
        {
            API.ShowCursorThisFrame();
        }

        public void ShowLoadingPrompt(string text, int type)
        {
            Screen.LoadingPrompt.Show(text, (LoadingSpinnerType)type);
        }

        public void ShowLoadingPrompt(string text, GameLoadingSpinnerType type)
        {
            Screen.LoadingPrompt.Show(text, (LoadingSpinnerType)type);
        }

        public void StartGpsCustomRoute(int routeColour, bool displayOnFoot, bool followPlayer)
        {
            API.StartGpsCustomRoute(routeColour, displayOnFoot, followPlayer);
        }

        public void StopSound(int soundId)
        {
            API.StopSound(soundId);
        }

        public void ThefeedCommentTeleportPoolOff()
        {
            API.ThefeedCommentTeleportPoolOff();
        }

        public void ThefeedCommentTeleportPoolOn()
        {
            API.ThefeedCommentTeleportPoolOn();
        }

        public void ThefeedNextPostBackgroundColor(int notificationColor)
        {
            API.ThefeedNextPostBackgroundColor(notificationColor);
        }

        public void ThefeedRemoveItem(int item)
        {
            API.ThefeedRemoveItem(item);
        }

        public void UnlockMinimapAngle()
        {
            API.UnlockMinimapAngle();
        }

        public void UnlockMinimapPosition()
        {
            API.UnlockMinimapPosition();
        }

        public void UnregisterPedheadshot(int handle)
        {
            API.UnregisterPedheadshot(handle);
        }

        public int UpdateOnscreenKeyboard()
        {
            return API.UpdateOnscreenKeyboard();
        }
    }
}
#endif