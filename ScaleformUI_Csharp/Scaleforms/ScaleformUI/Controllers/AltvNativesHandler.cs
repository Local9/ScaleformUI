#if ALTV
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.Scaleforms.ScaleformUI.Controllers
{
    public class AltvNativesHandler : IRageNatives
    {
        public void ActivateFrontendMenu(uint menuhash, bool Toggle_Pause, int component)
        {
            throw new NotImplementedException();
        }

        public int AddMinimapOverlay(string name)
        {
            throw new NotImplementedException();
        }

        public void AddPointToGpsCustomRoute(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void AddTextComponentInteger(int value)
        {
            throw new NotImplementedException();
        }

        public void AddTextComponentScaleform(string value)
        {
            throw new NotImplementedException();
        }

        public void AddTextComponentString(string text)
        {
            throw new NotImplementedException();
        }

        public void AddTextComponentSubstringPlayerName(string text)
        {
            throw new NotImplementedException();
        }

        public void AddTextComponentSubstringTextLabelHashKey(uint hashKey)
        {
            throw new NotImplementedException();
        }

        public void AddTextEntry(string entryKey, string entryText)
        {
            throw new NotImplementedException();
        }

        public bool AnimpostfxIsRunning(string effectName)
        {
            throw new NotImplementedException();
        }

        public void AnimpostfxPlay(string effectName, int duration, bool looped)
        {
            throw new NotImplementedException();
        }

        public void AnimpostfxStop(string effectName)
        {
            throw new NotImplementedException();
        }

        public void BeginScaleformMovieMethod(int handle, string methodName)
        {
            throw new NotImplementedException();
        }

        public void BeginScaleformMovieMethodHudComponent(int hudComponent, string methodName)
        {
            throw new NotImplementedException();
        }

        public void BeginScaleformMovieMethodOnFrontend(string methodName)
        {
            throw new NotImplementedException();
        }

        public void BeginScaleformMovieMethodOnFrontendHeader(string methodName)
        {
            throw new NotImplementedException();
        }

        public void BeginScaleformScriptHudMovieMethod(int hudComponent, string methodName)
        {
            throw new NotImplementedException();
        }

        public void BeginTextCommandDisplayHelp(string inputType)
        {
            throw new NotImplementedException();
        }

        public void BeginTextCommandDisplayText(string text)
        {
            throw new NotImplementedException();
        }

        public void BeginTextCommandLineCount(string entry)
        {
            throw new NotImplementedException();
        }

        public void BeginTextCommandScaleformString(string value)
        {
            throw new NotImplementedException();
        }

        public void BeginTextCommandThefeedPost(string text)
        {
            throw new NotImplementedException();
        }

        public void CallMinimapScaleformFunction(int handle, string fnName)
        {
            throw new NotImplementedException();
        }

        public void ClearDrawOrigin()
        {
            throw new NotImplementedException();
        }

        public void ClearGpsCustomRoute()
        {
            throw new NotImplementedException();
        }

        public void ClearGpsFlags()
        {
            throw new NotImplementedException();
        }

        public void ClearGpsMultiRoute()
        {
            throw new NotImplementedException();
        }

        public void ClearPedInPauseMenu()
        {
            throw new NotImplementedException();
        }

        public void ClearRaceGalleryBlips()
        {
            throw new NotImplementedException();
        }

        public int ClonePed(int ped, float heading, bool isNetwork, bool p3)
        {
            throw new NotImplementedException();
        }

        public void DeleteWaypoint()
        {
            throw new NotImplementedException();
        }

        public void DisableControlAction(int inputGroup, int control, bool disable)
        {
            throw new NotImplementedException();
        }

        public void DisplayHelpTextThisFrame(string text, bool curvedWindow)
        {
            throw new NotImplementedException();
        }

        public int DisplayRadar(bool toggle)
        {
            throw new NotImplementedException();
        }

        public void DrawRect(float x, float y, float width, float height, int r, int g, int b, int a)
        {
            throw new NotImplementedException();
        }

        public void DrawScaleformMovie(int handle, float x, float y, float width, float height, int red, int green, int blue, int alpha, int unk)
        {
            throw new NotImplementedException();
        }

        public void DrawScaleformMovieFullscreen(int handle, int red, int green, int blue, int alpha, int unk)
        {
            throw new NotImplementedException();
        }

        public void DrawScaleformMovie_3d(int handle, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float p7, float sharpness, float p9, float scaleX, float scaleY, float scaleZ, int p13)
        {
            throw new NotImplementedException();
        }

        public void DrawScaleformMovie_3dNonAdditive(int handle, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, float p7, float p8, float p9, float scaleX, float scaleY, float scaleZ, int p13)
        {
            throw new NotImplementedException();
        }

        public void DrawSprite(string textureDict, string textureName, float screenX, float screenY, float width, float height, float heading, int r, int g, int b, int a)
        {
            throw new NotImplementedException();
        }

        public void DrawText(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void EnableControlAction(int inputGroup, int control, bool enable)
        {
            throw new NotImplementedException();
        }

        public void EndScaleformMovieMethod()
        {
            throw new NotImplementedException();
        }

        public int EndScaleformMovieMethodReturnValue()
        {
            throw new NotImplementedException();
        }

        public void EndTextCommandDisplayHelp(int shape, bool loop, bool beep, int duration)
        {
            throw new NotImplementedException();
        }

        public void EndTextCommandDisplayText(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void EndTextCommandScaleformString()
        {
            throw new NotImplementedException();
        }

        public void EndTextCommandScaleformString_2()
        {
            throw new NotImplementedException();
        }

        public int EndTextCommandThefeedPostMessagetext(string textureDict, string textureName, bool flash, int iconType, string sender, string subject)
        {
            throw new NotImplementedException();
        }

        public void EndTextCommandThefeedPostStats(string statTitle, int icon, int newProgress, int oldProgress, bool isImportant, string picTxd, string picTxn)
        {
            throw new NotImplementedException();
        }

        public int EndTextCommandThefeedPostTicker(bool blink, bool showBriefing)
        {
            throw new NotImplementedException();
        }

        public int EndTextCommandThefeedPostVersusTu(string mugshotOneTxd, string mugshotOneTxn, int leftScore, string mugshotTwoTxd, string mugshotTwoTxn, int rightScore, int leftColor, int rightColor)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentFrontendMenuVersion()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentResourceName()
        {
            throw new NotImplementedException();
        }

        public float GetDisabledControlNormal(int inputGroup, int control)
        {
            throw new NotImplementedException();
        }

        public bool GetGroundZFor_3dCoord(float x, float y, float z, ref float groundZ, bool unk)
        {
            throw new NotImplementedException();
        }

        public int GetHashKey(string value)
        {
            throw new NotImplementedException();
        }

        public void GetHudColour(int hudColorIndex, ref int r, ref int g, ref int b, ref int a)
        {
            throw new NotImplementedException();
        }

        public int GetNetworkTime()
        {
            throw new NotImplementedException();
        }

        public int GetNetworkTimeAccurate()
        {
            throw new NotImplementedException();
        }

        public string GetPedheadshotTxdString(int mugshotHandle)
        {
            throw new NotImplementedException();
        }

        public float GetSafeZoneSize()
        {
            throw new NotImplementedException();
        }

        public bool GetScaleformMovieCursorSelection(int handle, ref int eventType, ref int context, ref int itemId, ref int unused)
        {
            throw new NotImplementedException();
        }

        public int GetScaleformMovieFunctionReturnInt(int handle)
        {
            throw new NotImplementedException();
        }

        public string GetScaleformMovieFunctionReturnString(int handle)
        {
            throw new NotImplementedException();
        }

        public bool GetScaleformMovieMethodReturnValueBool(int handle)
        {
            throw new NotImplementedException();
        }

        public int GetScaleformMovieMethodReturnValueInt(int handle)
        {
            throw new NotImplementedException();
        }

        public string GetScaleformMovieMethodReturnValueString(int handle)
        {
            throw new NotImplementedException();
        }

        public int GetSoundId()
        {
            throw new NotImplementedException();
        }

        public int GetTextScreenLineCount(float x, float y)
        {
            throw new NotImplementedException();
        }

        public float GetTextScreenWidth(bool p0)
        {
            throw new NotImplementedException();
        }

        public void GivePedToPauseMenu(int ped, int flags)
        {
            throw new NotImplementedException();
        }

        public bool HasHudScaleformLoaded(int hudComponent)
        {
            throw new NotImplementedException();
        }

        public bool HasMinimapOverlayLoaded(int handle)
        {
            throw new NotImplementedException();
        }

        public bool HasScaleformMovieLoaded(int handle)
        {
            throw new NotImplementedException();
        }

        public bool HasSoundFinished(int soundId)
        {
            throw new NotImplementedException();
        }

        public bool HasStreamedTextureDictLoaded(string textureDict)
        {
            throw new NotImplementedException();
        }

        public void HideHudComponentThisFrame(int hudComponent)
        {
            throw new NotImplementedException();
        }

        public bool IsFrontendReadyForControl()
        {
            throw new NotImplementedException();
        }

        public bool IsInputDisabled(int inputGroup)
        {
            throw new NotImplementedException();
        }

        public bool IsPedheadshotReady(int mugshotHandle)
        {
            throw new NotImplementedException();
        }

        public bool IsScaleformMovieMethodReturnValueReady(int handle)
        {
            throw new NotImplementedException();
        }

        public bool IsUsingKeyboard(int inputGroup)
        {
            throw new NotImplementedException();
        }

        public bool IsWarningMessageActive()
        {
            throw new NotImplementedException();
        }

        public void LockMinimapAngle(int angle)
        {
            throw new NotImplementedException();
        }

        public void LockMinimapPosition(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void PauseToggleFullscreenMap(bool enabled)
        {
            throw new NotImplementedException();
        }

        public int PlayerPedId()
        {
            throw new NotImplementedException();
        }

        public void PlaySoundFrontend(int soundId, string audioName, string audioRef, bool p3)
        {
            throw new NotImplementedException();
        }

        public void PushScaleformMovieFunctionParameterBool(bool value)
        {
            throw new NotImplementedException();
        }

        public void PushScaleformMovieFunctionParameterInt(int value)
        {
            throw new NotImplementedException();
        }

        public void PushScaleformMovieFunctionParameterString(string value)
        {
            throw new NotImplementedException();
        }

        public void PushScaleformMovieMethodParameterBool(bool value)
        {
            throw new NotImplementedException();
        }

        public void PushScaleformMovieMethodParameterFloat(float value)
        {
            throw new NotImplementedException();
        }

        public void PushScaleformMovieMethodParameterInt(int value)
        {
            throw new NotImplementedException();
        }

        public void PushScaleformMovieMethodParameterString(string value)
        {
            throw new NotImplementedException();
        }

        public int RaceGalleryAddBlip(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void RaceGalleryFullscreen(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void RaceGalleryNextBlipSprite(int sprite)
        {
            throw new NotImplementedException();
        }

        public void RegisterFontFile(string gfxName)
        {
            throw new NotImplementedException();
        }

        public int RegisterFontId(string name)
        {
            throw new NotImplementedException();
        }

        public int RegisterPedheadshot(int pedHandle)
        {
            throw new NotImplementedException();
        }

        public int RegisterPedheadshotTransparent(int pedHandle)
        {
            throw new NotImplementedException();
        }

        public void ReleaseSoundId(int soundId)
        {
            throw new NotImplementedException();
        }

        public void RequestHudScaleform(int hudComponent)
        {
            throw new NotImplementedException();
        }

        public int RequestScaleformMovieInstance(string handle)
        {
            throw new NotImplementedException();
        }

        public void RequestScriptAudioBank(string audioBank, bool p2)
        {
            throw new NotImplementedException();
        }

        public void RequestStreamedTextureDict(string textureDict, bool p1)
        {
            throw new NotImplementedException();
        }

        public void ScaleformMovieMethodAddParamBool(bool value)
        {
            throw new NotImplementedException();
        }

        public void ScaleformMovieMethodAddParamFloat(float value)
        {
            throw new NotImplementedException();
        }

        public void ScaleformMovieMethodAddParamInt(int value)
        {
            throw new NotImplementedException();
        }

        public void ScaleformMovieMethodAddParamPlayerNameString(string value)
        {
            throw new NotImplementedException();
        }

        public void ScaleformMovieMethodAddParamTextureNameString(string textureDict)
        {
            throw new NotImplementedException();
        }

        public void ScaleformMovieMethodAddParamTextureNameString_2(string value)
        {
            throw new NotImplementedException();
        }

        public void SetBlipColour(int blip, int color)
        {
            throw new NotImplementedException();
        }

        public void SetBlipScale(int blip, float scale)
        {
            throw new NotImplementedException();
        }

        public void SetCursorLocation(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void SetDrawOrigin(float x, float y, float z, int p3)
        {
            throw new NotImplementedException();
        }

        public void SetFloatingHelpTextStyle(int hudIndex, int style, int hudColor, int alpha, int arrowPosition, int boxOffset)
        {
            throw new NotImplementedException();
        }

        public void SetFloatingHelpTextWorldPosition(int hudIndex, float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void SetGpsCustomRouteRender(bool enabled, int color, int zoomLevel)
        {
            throw new NotImplementedException();
        }

        public void SetGpsFlags(int p0, float p1)
        {
            throw new NotImplementedException();
        }

        public void SetGpsMultiRouteRender(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void SetInputExclusive(int inputGroup, int control)
        {
            throw new NotImplementedException();
        }

        public void SetMapFullScreen(bool toggle)
        {
            throw new NotImplementedException();
        }

        public void SetMinimapOverlayDisplay(int handle, float x, float y, float scaleX, float scaleY, float alpha)
        {
            throw new NotImplementedException();
        }

        public void SetMouseCursorActiveThisFrame()
        {
            throw new NotImplementedException();
        }

        public void SetMouseCursorSprite(int spriteId)
        {
            throw new NotImplementedException();
        }

        public void SetNotificationBackgroundColor(int hudIndex)
        {
            throw new NotImplementedException();
        }

        public void SetNotificationFlashColor(int red, int green, int blue, int alpha)
        {
            throw new NotImplementedException();
        }

        public void SetPauseMenuPedLighting(bool state)
        {
            throw new NotImplementedException();
        }

        public void SetPauseMenuPedSleepState(bool state)
        {
            throw new NotImplementedException();
        }

        public void SetPlayerBlipPositionThisFrame(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void SetPlayerControl(int player, bool toggle, int possiblyFlags)
        {
            throw new NotImplementedException();
        }

        public void SetPoliceRadarBlips(bool toggle)
        {
            throw new NotImplementedException();
        }

        public void SetRadarZoom(int zoomLevel)
        {
            throw new NotImplementedException();
        }

        public void SetRadarZoomToDistance(float distance)
        {
            throw new NotImplementedException();
        }

        public void SetScaleformMovieAsNoLongerNeeded(ref int handle)
        {
            throw new NotImplementedException();
        }

        public void SetScriptGfxDrawBehindPausemenu(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void SetStreamedTextureDictAsNoLongerNeeded(string textureDict)
        {
            throw new NotImplementedException();
        }

        public void SetTextCentre(bool align)
        {
            throw new NotImplementedException();
        }

        public void SetTextColour(int r, int g, int b, int a)
        {
            throw new NotImplementedException();
        }

        public void SetTextDropshadow(int distance, int r, int g, int b, int a)
        {
            throw new NotImplementedException();
        }

        public void SetTextDropShadow()
        {
            throw new NotImplementedException();
        }

        public void SetTextEdge(int p0, int r, int g, int b, int a)
        {
            throw new NotImplementedException();
        }

        public void SetTextEntry(string entryType)
        {
            throw new NotImplementedException();
        }

        public void SetTextEntryForWidth(string text)
        {
            throw new NotImplementedException();
        }

        public void SetTextFont(int fontType)
        {
            throw new NotImplementedException();
        }

        public void SetTextOutline()
        {
            throw new NotImplementedException();
        }

        public void SetTextProportional(bool p)
        {
            throw new NotImplementedException();
        }

        public void SetTextRightJustify(bool toggle)
        {
            throw new NotImplementedException();
        }

        public void SetTextScale(float scale, float size)
        {
            throw new NotImplementedException();
        }

        public void SetTextWrap(float start, float end)
        {
            throw new NotImplementedException();
        }

        public void SetWaypointOff()
        {
            throw new NotImplementedException();
        }

        public void StartGpsCustomRoute(int routeColour, bool displayOnFoot, bool followPlayer)
        {
            throw new NotImplementedException();
        }

        public void StopSound(int soundId)
        {
            throw new NotImplementedException();
        }

        public void ThefeedCommentTeleportPoolOff()
        {
            throw new NotImplementedException();
        }

        public void ThefeedCommentTeleportPoolOn()
        {
            throw new NotImplementedException();
        }

        public void ThefeedNextPostBackgroundColor(int notificationColor)
        {
            throw new NotImplementedException();
        }

        public void ThefeedRemoveItem(int item)
        {
            throw new NotImplementedException();
        }

        public void UnlockMinimapAngle()
        {
            throw new NotImplementedException();
        }

        public void UnlockMinimapPosition()
        {
            throw new NotImplementedException();
        }

        public void UnregisterPedheadshot(int handle)
        {
            throw new NotImplementedException();
        }

        public int UpdateOnscreenKeyboard()
        {
            throw new NotImplementedException();
        }
    }
}
#endif