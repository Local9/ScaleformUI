#if FIVEM
using CitizenFX.Core;
#endif

#if ALTV
using System.Numerics;
#endif

using ScaleformUI.Elements;
using System.Drawing;

namespace ScaleformUI.Scaleforms.ScaleformUI.Interfaces
{
    public interface IRageNatives
    {
        public float ScreenWidth { get; }
        public float ScreenScaledWidth { get; }
        public int ScreenResolutionWidth { get; }
        public float ScreenHeight { get; }
        public int ScreenResolutionHeight { get; }
        public float GameplayCameraRelativeHeading { get; set; }
        public Vector3 GameplayCameraPosition { get; }
        public float GameplayCameraFieldOfView { get; }

        public bool IsPaused { get; }
        public GameCursorSprite CursorSprite { get; set; }
        public void RequestHudScaleform(int hudComponent);
        public bool HasHudScaleformLoaded(int hudComponent);
        public int RequestScaleformMovieInstance(string handle);
        public void SetScaleformMovieAsNoLongerNeeded(ref int handle);
        public bool HasScaleformMovieLoaded(int handle);
        public void BeginScaleformMovieMethod(int handle, string methodName);
        public void EndScaleformMovieMethod();
        public int EndScaleformMovieMethodReturnValue();
        public void PushScaleformMovieMethodParameterInt(int value);
        public void PushScaleformMovieMethodParameterFloat(float value);
        public void PushScaleformMovieMethodParameterBool(bool value);
        public void PushScaleformMovieMethodParameterString(string value);
        public void ScaleformMovieMethodAddParamPlayerNameString(string value);
        public void BeginTextCommandScaleformString(string value);
        public void EndTextCommandScaleformString();
        public void ScaleformMovieMethodAddParamTextureNameString_2(string value);
        public bool IsScaleformMovieMethodReturnValueReady(int handle);
        public int GetScaleformMovieFunctionReturnInt(int handle);
        public bool GetScaleformMovieMethodReturnValueBool(int handle);
        public string GetScaleformMovieFunctionReturnString(int handle);
        public void DrawScaleformMovieFullscreen(int handle, int red, int green, int blue, int alpha, int unk);
        public void DrawScaleformMovie(int handle, float x, float y, float width, float height, int red, int green, int blue, int alpha, int unk);
        public void DrawScaleformMovie_3dNonAdditive(int handle, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float p7, float p8, float p9, float scaleX, float scaleY, float scaleZ, int p13);
        public void DrawScaleformMovie_3d(int handle, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float p7, float sharpness, float p9, float scaleX, float scaleY, float scaleZ, int p13);
        public void BeginScaleformMovieMethodOnFrontend(string methodName);
        public void BeginScaleformMovieMethodOnFrontendHeader(string methodName);
        public void ScaleformMovieMethodAddParamBool(bool value);
        public int GetCurrentFrontendMenuVersion();
        public int GetHashKey(string value);
        public bool IsWarningMessageActive();

        /// <summary>
        /// Original Method Name for FiveM : N_0x2de6c5e2e996f178
        /// </summary>
        /// <param name="enabled"></param>
        public void PauseToggleFullscreenMap(bool enabled);
        public int DisplayRadar(bool toggle);
        public void RaceGalleryFullscreen(bool enabled);
        public void ClearRaceGalleryBlips();
        public void SetRadarZoom(int zoomLevel);
        public void SetGpsCustomRouteRender(bool enabled, int color, int zoomLevel);
        public void SetGpsMultiRouteRender(bool enabled);
        public void UnlockMinimapPosition();
        public void UnlockMinimapAngle();
        public void DeleteWaypoint();
        public void ClearGpsCustomRoute();
        public void ClearGpsFlags();
        public string GetCurrentResourceName();
        public int GetNetworkTime();
        public int GetNetworkTimeAccurate();
        public void UnregisterPedheadshot(int handle);
        public void BeginScaleformMovieMethodHudComponent(int hudComponent, string methodName);
        public void PushScaleformMovieFunctionParameterInt(int value);
        public void BeginScaleformScriptHudMovieMethod(int hudComponent, string methodName);
        public void AddTextEntry(string entryKey, string entryText);
        public void ScaleformMovieMethodAddParamInt(int value);
        public void EndTextCommandScaleformString_2();
        public void PushScaleformMovieFunctionParameterString(string value);
        public void AddTextComponentScaleform(string value);
        public bool IsFrontendReadyForControl();
        public void SetScriptGfxDrawBehindPausemenu(bool enabled);
        public bool HasStreamedTextureDictLoaded(string textureDict);
        public void RequestStreamedTextureDict(string textureDict, bool p1);
        public void SetStreamedTextureDictAsNoLongerNeeded(string textureDict);
        public void GetHudColour(int hudColorIndex, ref int r, ref int g, ref int b, ref int a);
        public bool IsUsingKeyboard(int inputGroup);
        public void SetMouseCursorActiveThisFrame();
        public void SetInputExclusive(int inputGroup, int control);
        public bool GetScaleformMovieCursorSelection(int handle, ref int eventType, ref int context, ref int itemId, ref int unused);
        public int AddMinimapOverlay(string name);
        public bool HasMinimapOverlayLoaded(int handle);
        public void SetMinimapOverlayDisplay(int handle, float x, float y, float scaleX, float scaleY, float alpha);
        public void CallMinimapScaleformFunction(int handle, string fnName);
        public void ScaleformMovieMethodAddParamTextureNameString(string textureDict);
        public void ScaleformMovieMethodAddParamFloat(float value);
        public void PlaySound(string audioName, string audioRef);
        public void PlaySoundFrontend(string audioName, string audioRef);
        public void PlaySoundFrontend(int soundId, string audioName, string audioRef, bool p3);
        public void RequestScriptAudioBank(string audioBank, bool p2);
        public void ThefeedCommentTeleportPoolOn();
        public void ThefeedCommentTeleportPoolOff();
        public int PlayerPedId();
        public float GetDisabledControlNormal(int inputGroup, int control);
        public void HideHudComponentThisFrame(int hudComponent);
        public void HideHudComponentThisFrame(GameHudComponent hudComponent);
        public void SetMouseCursorSprite(int spriteId);
        public bool HasSoundFinished(int soundId);
        public int GetSoundId();
        public void ReleaseSoundId(int soundId);
        public void StopSound(int soundId);
        public int UpdateOnscreenKeyboard();
        public void SetCursorLocation(float x, float y);
        public void PushScaleformMovieFunctionParameterBool(bool value);
        public void AddTextComponentSubstringTextLabelHashKey(uint hashKey);
        public bool IsInputDisabled(int inputGroup);
        public string GetScaleformMovieMethodReturnValueString(int handle);
        public int GetScaleformMovieMethodReturnValueInt(int handle);
        public void DisableControlAction(int inputGroup, int control, bool disable);
        public void ActivateFrontendMenu(uint menuhash, bool Toggle_Pause, int component);
        public void SetPlayerControl(int player, bool toggle, int possiblyFlags);
        public void AnimpostfxStop(string effectName);
        public void AnimpostfxPlay(string effectName, int duration, bool looped);
        public bool AnimpostfxIsRunning(string effectName);
        public void SetPauseMenuPedLighting(bool state);
        public void ClearPedInPauseMenu();
        public void AddTextComponentInteger(int value);
        public void SetRadarZoomToDistance(float distance);
        public void LockMinimapPosition(float x, float y);
        public void LockMinimapAngle(int angle);
        public void SetWaypointOff();
        public void ClearGpsMultiRoute();
        public void SetPoliceRadarBlips(bool toggle);
        public void SetPlayerBlipPositionThisFrame(float x, float y);
        public void SetMapFullScreen(bool toggle);
        public void RaceGalleryNextBlipSprite(int sprite);
        public int RaceGalleryAddBlip(float x, float y, float z);
        public void SetBlipScale(int blip, float scale);
        public void SetBlipColour(int blip, int color);
        public void SetGpsFlags(int p0, float p1);
        public void StartGpsCustomRoute(int routeColour, bool displayOnFoot, bool followPlayer);
        public void AddPointToGpsCustomRoute(float x, float y, float z);
        public void SetPauseMenuPedSleepState(bool state);
        public int ClonePed(int ped, float heading, bool isNetwork, bool p3);
        public void GivePedToPauseMenu(int ped, int flags);
        public void ThefeedRemoveItem(int item);
        public void BeginTextCommandThefeedPost(string text);
        public int EndTextCommandThefeedPostTicker(bool blink, bool showBriefing);
        public void EndTextCommandThefeedPostStats(string statTitle, int icon, int newProgress, int oldProgress, bool isImportant, string picTxd, string picTxn);
        public int RegisterPedheadshot(int pedHandle);
        public int RegisterPedheadshotTransparent(int pedHandle);
        public bool IsPedheadshotReady(int mugshotHandle);
        public string GetPedheadshotTxdString(int mugshotHandle);
        public void ThefeedNextPostBackgroundColor(int notificationColor);
        public void DisplayHelpTextThisFrame(string text, bool curvedWindow);
        public void BeginTextCommandDisplayHelp(string inputType);
        public void EndTextCommandDisplayHelp(int shape, bool loop, bool beep, int duration);
        public void SetFloatingHelpTextWorldPosition(int hudIndex, float x, float y, float z);
        public void SetFloatingHelpTextStyle(int hudIndex, int style, int hudColor, int alpha, int arrowPosition, int boxOffset);
        public void AddTextComponentSubstringPlayerName(string text);
        public void SetNotificationBackgroundColor(int hudIndex);
        public void SetNotificationFlashColor(int red, int green, int blue, int alpha);
        public int EndTextCommandThefeedPostMessagetext(string textureDict, string textureName, bool flash, int iconType, string sender, string subject);
        public void SetTextScale(float scale, float size);
        public void SetTextCentre(bool align);
        public void SetTextDropshadow(int distance, int r, int g, int b, int a);
        public void SetTextEdge(int p0, int r, int g, int b, int a);
        public void SetTextFont(int fontType);
        public void SetTextColour(int r, int g, int b, int a);
        public void SetTextProportional(bool p);
        public void SetTextDropShadow();
        public void SetTextOutline();
        public void SetDrawOrigin(float x, float y, float z, int p3);
        public void BeginTextCommandDisplayText(string text);
        public void EndTextCommandDisplayText(float x, float y);
        public void ClearDrawOrigin();
        public void SetTextWrap(float start, float end);
        public void SetTextRightJustify(bool toggle);
        public void SetTextEntry(string entryType);
        public int EndTextCommandThefeedPostVersusTu(string mugshotOneTxd, string mugshotOneTxn, int leftScore, string mugshotTwoTxd, string mugshotTwoTxn, int rightScore, int leftColor, int rightColor);
        public bool GetGroundZFor_3dCoord(float x, float y, float z, ref float groundZ, bool unk);
        public void DrawText(float x, float y);
        public void AddTextComponentString(string text);
        public void DrawRect(float x, float y, float width, float height, int r, int g, int b, int a);
        public void DrawSprite(string textureDict, string textureName, float screenX, float screenY, float width, float height, float heading, int r, int g, int b, int a);
        public float GetSafeZoneSize();
        public void SetTextEntryForWidth(string text);
        public float GetTextScreenWidth(bool p0);
        public void BeginTextCommandLineCount(string entry);
        public int GetTextScreenLineCount(float x, float y);
        public void RegisterFontFile(string gfxName);
        public int RegisterFontId(string name);
        public void EnableControlAction(int inputGroup, int control, bool enable);
        public bool IsControlJustPressed(int inputGroup, GameControl control);
        public void ShowCursorThisFrame();
        public void ShowLoadingPrompt(string text, int type);
        public void ShowLoadingPrompt(string text, GameLoadingSpinnerType type);
        public void HideLoadingPrompt();
        public string GetControlInstructionalButton(int inputGroup, GameControl control);
        public Size GetScreenResolution();
        public void EnableControlThisFrame(int inputGroup, GameControl control);
        public void EnableControlThisFrame(int inputGroup, int control);
        public bool IsControlJustReleased(int inputGroup, GameControl control);
        public bool IsControlJustReleased(int inputGroup, int control);
        public bool IsControlPressed(int inputGroup, GameControl control);
        public bool IsControlPressed(int inputGroup, int control);
        public string GetGXTEntry(string entry);
        public void DisableControlThisFrame(int inputGroup, GameControl control);
        public void DisableControlThisFrame(int inputGroup, int control);
    }
}
