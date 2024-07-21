using CitizenFX.Core;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.Scaleforms
{
    public class RankBarHandler : IScaleformModule
    {
        private readonly IRageNatives _natives;
        private const int HUD_COMPONENT_ID = 19;

        private HudColor _rankBarColor = HudColor.HUD_COLOUR_FREEMODE;

        /// <summary>
        /// Set the color of the Rank Bar when displayed
        /// </summary>
        public HudColor Color
        {
            get => _rankBarColor;
            set
            {
                _rankBarColor = value;
            }
        }

        public RankBarHandler()
        {
            _natives = Main.GetNativesHandler();
        }

        public async Task Load()
        {
            int timeout = 1000;

            int start = Main.GameTime;
            _natives.RequestHudScaleform(HUD_COMPONENT_ID);
            while (!_natives.HasHudScaleformLoaded(HUD_COMPONENT_ID) && Main.GameTime - start < timeout)
            {
                await BaseScript.Delay(0);
            }
        }

        /// <summary>
        /// Shows the rank bar
        /// </summary>
        /// <param name="limitStart">Floor value of experience e.g. 0</param>
        /// <param name="limitEnd">Ceiling value of experience e.g. 800</param>
        /// <param name="previousValue">Previous Experience value </param>
        /// <param name="currentValue">New Experience value</param>
        /// <param name="currentRank">Current rank</param>
        public async void SetScores(int limitStart, int limitEnd, int previousValue, int currentValue, int currentRank)
        {
            await Load();

            // Color has to be set else it will be white by default
            _natives.BeginScaleformMovieMethodHudComponent(HUD_COMPONENT_ID, "SET_COLOUR");
            _natives.PushScaleformMovieFunctionParameterInt((int)_rankBarColor);
            _natives.EndScaleformMovieMethod();

            // this will set an update the score
            _natives.BeginScaleformMovieMethodHudComponent(HUD_COMPONENT_ID, "SET_RANK_SCORES");
            _natives.PushScaleformMovieFunctionParameterInt(limitStart);
            _natives.PushScaleformMovieFunctionParameterInt(limitEnd);
            _natives.PushScaleformMovieFunctionParameterInt(previousValue);
            _natives.PushScaleformMovieFunctionParameterInt(currentValue);
            _natives.PushScaleformMovieFunctionParameterInt(currentRank);
            _natives.PushScaleformMovieFunctionParameterInt(100);
            _natives.EndScaleformMovieMethod();
        }

        public void Remove()
        {
            if (_natives.HasHudScaleformLoaded(HUD_COMPONENT_ID))
            {
                _natives.BeginScaleformScriptHudMovieMethod(HUD_COMPONENT_ID, "REMOVE");
                _natives.EndScaleformMovieMethod();
            }
        }

        public async void OverrideAnimationSpeed(int speed = 1000)
        {
            await Load();
            _natives.BeginScaleformScriptHudMovieMethod(HUD_COMPONENT_ID, "OVERRIDE_ANIMATION_SPEED");
            _natives.PushScaleformMovieFunctionParameterInt(speed);
            _natives.EndScaleformMovieMethod();
        }

        public async void OverrideOnscreenDuration(int duration = 4000)
        {
            await Load();
            _natives.BeginScaleformScriptHudMovieMethod(HUD_COMPONENT_ID, "OVERRIDE_ONSCREEN_DURATION");
            _natives.PushScaleformMovieFunctionParameterInt(duration);
            _natives.EndScaleformMovieMethod();
        }
    }
}
