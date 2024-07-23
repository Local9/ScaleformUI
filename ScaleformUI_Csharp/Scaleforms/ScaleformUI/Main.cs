using CitizenFX.Core;
using Microsoft.Extensions.DependencyInjection;
using ScaleformUI.Scaleforms;
using ScaleformUI.Scaleforms.ScaleformUI.Controllers;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI
{
    public class Main : BaseScript
    {
        /// <summary>
        /// Provides the current game time in milliseconds.
        /// </summary>
        public static int GameTime = 0;

        private static ServiceProvider _serviceProvider;
        private static IRageNatives _natives;

        public static PauseMenuScaleform PauseMenu { get; internal set; }
        public static MediumMessageHandler MedMessageInstance { get; internal set; }
        public static InstructionalButtonsScaleform InstructionalButtons { get; internal set; }
        public static BigMessageHandler BigMessageInstance { get; internal set; }
        public static PopupWarning Warning { get; internal set; }
        public static PlayerListHandler PlayerListInstance { get; internal set; }
        public static MissionSelectorHandler JobMissionSelection { get; internal set; }
        public static BigFeedHandler BigFeed { get; internal set; }
        public static RankBarHandler RankBarInstance { get; internal set; }
        public static CountdownHandler CountdownInstance { get; internal set; }
        public static MultiplayerChatHandler MultiplayerChat { get; internal set; }


        internal static ScaleformWideScreen scaleformUI { get; set; }
        internal static ScaleformWideScreen radialMenu { get; set; }
        internal static ScaleformWideScreen radioMenu { get; set; }
        public Main()
        {
            SetupServices();

            _natives = GetNativesHandler();

            Warning = new();
            MedMessageInstance = new();
            BigMessageInstance = new();
            PlayerListInstance = new();
            JobMissionSelection = new();
            BigFeed = new();
            PauseMenu = new();
            InstructionalButtons = new();
            InstructionalButtons.Load();
            RankBarInstance = new();
            CountdownInstance = new();
            MultiplayerChat = new();
            scaleformUI = new("scaleformui");
            radialMenu = new("radialmenu");
            radioMenu = new("radiomenu");
            Tick += ScaleformUIThread_Tick;
            Tick += OnUpdateGlobalGameTimerAsync;
            MinimapOverlays.Load();
            EventHandlers["onResourceStop"] += new Action<string>((resName) =>
            {
                if (resName == _natives.GetCurrentResourceName())
                {
                    if (MenuHandler.IsAnyMenuOpen || MenuHandler.IsAnyPauseMenuOpen)
                        MenuHandler.CloseAndClearHistory();
                    radialMenu?.CallFunction("CLEAR_ALL");
                    radialMenu?.Dispose();
                    radioMenu?.CallFunction("CLEAR_ALL");
                    radioMenu?.Dispose();
                    scaleformUI?.CallFunction("CLEAR_ALL");
                    scaleformUI?.Dispose();
                    PauseMenu?.Dispose();
                    _natives.PauseToggleFullscreenMap(true);
                    _natives.RaceGalleryFullscreen(false);
                    _natives.ClearRaceGalleryBlips();
                    _natives.SetRadarZoom(0);
                    _natives.SetGpsCustomRouteRender(false, 18, 30);
                    _natives.SetGpsMultiRouteRender(false);
                    _natives.UnlockMinimapPosition();
                    _natives.UnlockMinimapAngle();
                    _natives.DeleteWaypoint();
                    _natives.ClearGpsCustomRoute();
                    _natives.ClearGpsFlags();
                }
            });
        }

        static void SetupServices()
        {
            ServiceCollection services = new ServiceCollection();
#if FIVEM
            services.AddSingleton<IRageNatives, FivemNativesHandler>();
#elif ALTV
            services.AddSingleton<IRageNatives, AltvNativesHandler>();
#endif
            _serviceProvider = services.BuildServiceProvider();
        }

        public static IRageNatives GetNativesHandler()
        {
            return _serviceProvider.GetRequiredService<IRageNatives>();
        }

        private async Task ScaleformUIThread_Tick()
        {
            if (_natives == null)
                _natives = GetNativesHandler();

            if (MenuHandler.ableToDraw && !(_natives.IsWarningMessageActive() || Warning.IsShowing))
            {
                MenuHandler.ProcessMenus();
                if (_natives.GetCurrentFrontendMenuVersion() == _natives.GetHashKey("FE_MENU_VERSION_CORONA"))
                {
                    _natives.BeginScaleformMovieMethodOnFrontend("INSTRUCTIONAL_BUTTONS");
                    _natives.ScaleformMovieMethodAddParamPlayerNameString("SET_DATA_SLOT_EMPTY");
                    _natives.EndScaleformMovieMethod();
                    _natives.BeginScaleformMovieMethodOnFrontendHeader("SHOW_MENU");
                    _natives.ScaleformMovieMethodAddParamBool(false);
                    _natives.EndScaleformMovieMethod();
                    _natives.BeginScaleformMovieMethodOnFrontendHeader("SHOW_HEADING_DETAILS");
                    _natives.ScaleformMovieMethodAddParamBool(false);
                    _natives.EndScaleformMovieMethod();
                }
            }

            if (Warning._sc != null)
                Warning.Update();
            if (InstructionalButtons._sc != null && (InstructionalButtons.ControlButtons != null && InstructionalButtons.ControlButtons.Count != 0))
                InstructionalButtons.Update();
            if (_natives.IsPaused) return;
            if (MedMessageInstance._sc != null)
                MedMessageInstance.Update();
            if (BigMessageInstance._sc != null)
                BigMessageInstance.Update();
            if (PlayerListInstance._sc != null && PlayerListInstance.Enabled)
                PlayerListInstance.Update();
            if (JobMissionSelection._sc != null && JobMissionSelection.Enabled)
                JobMissionSelection.Update();
            if (MultiplayerChat.IsTyping())
                MultiplayerChat.Update();
            if (BigFeed._sc != null)
                BigFeed.Update();
            scaleformUI ??= new("ScaleformUI");
            radialMenu ??= new("RadialMenu");
            if (!PauseMenu.Loaded)
                PauseMenu.Load();
            await Task.FromResult(0);
        }

        /// <summary>
        /// Updates the game time.
        /// </summary>
        /// <returns></returns>
        public async Task OnUpdateGlobalGameTimerAsync()
        {
            if (_natives == null)
                _natives = GetNativesHandler();

            GameTime = _natives.GetNetworkTimeAccurate();
        }
    }
}
