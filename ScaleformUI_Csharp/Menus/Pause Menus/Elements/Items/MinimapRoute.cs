﻿using ScaleformUI.Scaleforms;

namespace ScaleformUI.PauseMenus.Elements.Items
{
    [Flags]
    public enum GPSFlags
    {
        NONE = 0,
        IGNORE_ONE_WAY = 1,
        FOLLOW_RULES = 2,
        AVOID_HIGHWAY = 4,
        NO_ROUTE_SHIFT = 8,
        CUSTOM_PROXIMITY = 16,
        NO_PULL_PATH_TO_RIGHT_LANE = 32,
        AVOID_OFF_ROAD = 64,
        IGNORE_DESTINATION_Z = 128
    }

    public class MinimapRoute : MinimapBaseItem
    {
        public MinimapRaceCheckpoint StartPoint = new();
        public MinimapRaceCheckpoint EndPoint = new();
        public List<MinimapRaceCheckpoint> CheckPoints = new List<MinimapRaceCheckpoint>();
        public int RadarThickness = 18;
        public int MapThickness = 30;
        public bool FollowStreet = true;
        public HudColor RouteColor { get; set; } = HudColor.HUD_COLOUR_FREEMODE;
        public void SetupCustomRoute()
        {
            if (StartPoint.Position.IsZero) return;
            Natives.ClearGpsFlags();
            Natives.SetGpsFlags(8, 0f);
            Natives.StartGpsCustomRoute((int)RouteColor, true, true);

            Natives.RaceGalleryNextBlipSprite(StartPoint.BlipSprite);
            Natives.RaceGalleryAddBlip(StartPoint.Position.X, StartPoint.Position.Y, StartPoint.Position.Z);

            Natives.AddPointToGpsCustomRoute(StartPoint.Position.X, StartPoint.Position.Y, StartPoint.Position.Z);

            for (int i = 0; i < CheckPoints.Count; i++)
            {
                MinimapRaceCheckpoint checkPoint = CheckPoints[i];
                Natives.RaceGalleryNextBlipSprite(checkPoint.BlipSprite);
                int blip = Natives.RaceGalleryAddBlip(checkPoint.Position.X, checkPoint.Position.Y, checkPoint.Position.Z);
                if (checkPoint.Scale > 0)
                    Natives.SetBlipScale(blip, checkPoint.Scale);
                Natives.SetBlipColour(blip, (int)checkPoint.Color);
                Natives.AddPointToGpsCustomRoute(checkPoint.Position.X, checkPoint.Position.Y, checkPoint.Position.Z);
            }

            Natives.RaceGalleryNextBlipSprite(EndPoint.BlipSprite);
            Natives.RaceGalleryAddBlip(EndPoint.Position.X, EndPoint.Position.Y, EndPoint.Position.Z);
            Natives.AddPointToGpsCustomRoute(EndPoint.Position.X, EndPoint.Position.Y, EndPoint.Position.Z);

            Natives.SetGpsCustomRouteRender(true, 18, MapThickness); ;
        }
    }
}
