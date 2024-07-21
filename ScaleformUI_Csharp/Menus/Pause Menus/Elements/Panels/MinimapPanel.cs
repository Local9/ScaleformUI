using CitizenFX.Core;
using ScaleformUI.LobbyMenu;
using ScaleformUI.PauseMenu;
using ScaleformUI.PauseMenus.Elements.Items;

namespace ScaleformUI.PauseMenus.Elements.Panels
{
    public class MinimapPanel
    {
        internal PauseMenuBase Parent { get; set; }
        internal BaseTab ParentTab { get; set; }
        internal Vector2 mapPosition = new Vector2();
        internal float zoomDistance = 0;
        internal bool enabled;
        private bool turnedOn = false;
        internal int localCoronaMapStage = 0;

        public MinimapRoute MinimapRoute;
        public List<FakeBlip> MinimapBlips { get; internal set; }

        public MinimapPanel(PauseMenuBase parent, BaseTab parenttab)
        {
            MinimapBlips = new List<FakeBlip>();
            MinimapRoute = new MinimapRoute();
            Parent = parent;
            ParentTab = parenttab;
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (Parent != null && Parent.Visible)
                {
                    if (Parent is MainView lobby)
                    {
                        if (lobby.listCol[lobby.FocusLevel].Type == "players")
                        {
                            if (lobby.PlayersColumn.Items[lobby.PlayersColumn.CurrentSelection].KeepPanelVisible)
                            {
                                return;
                            }
                        }
                    }
                    else if (Parent is TabView)
                    {
                        if (ParentTab is PlayerListTab plTab)
                        {
                            if (plTab.listCol[plTab.Focus].Type == "players")
                            {
                                if (plTab.PlayersColumn.Items[plTab.PlayersColumn.CurrentSelection].KeepPanelVisible)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                enabled = value;
                if (enabled)
                {
                    if (localCoronaMapStage == -1)
                        localCoronaMapStage = 0;
                }
                else
                {
                    localCoronaMapStage = -1;
                    if (turnedOn)
                    {
                        Parent.Natives.DisplayRadar(false);
                        Parent.Natives.SetMapFullScreen(false);
                        turnedOn = false;
                    }
                }
                if (Parent != null && Parent.Visible && ParentTab.Visible)
                {
                    if (Parent is MainView lobby)
                    {
                        lobby._pause._lobby.CallFunction("HIDE_MISSION_PANEL", !enabled);
                    }
                    else if (Parent is TabView pause)
                    {
                        pause._pause._pause.CallFunction("HIDE_PLAYERS_TAB_MISSION_PANEL", !enabled);
                    }
                }
            }
        }

        internal void InitializeMapSize()
        {
            int iMaxNodesToCheck = 202;
            Vector3 vNodeMax = new Vector3();
            Vector3 vNodeMin = new Vector3();

            for (int i = 0; i < iMaxNodesToCheck; i++)
            {
                Vector3 vectorNode = GetVectorToCheck(i);

                if (MinimapBlips.Count > i)
                {
                    if (MinimapBlips[i].Position.LengthSquared() > vectorNode.LengthSquared())
                    {
                        vectorNode = MinimapBlips[i].Position;
                    }
                }

                if (i == 0)
                {

                    vNodeMax = vectorNode;
                    vNodeMin = vectorNode;
                }
                else
                {
                    if (vectorNode.X > vNodeMax.X)
                        vNodeMax.X = vectorNode.X;
                    if (vectorNode.X < vNodeMin.X)
                        vNodeMin.X = vectorNode.X;
                    if (vectorNode.Y > vNodeMax.Y)
                        vNodeMax.Y = vectorNode.Y;
                    if (vectorNode.Y < vNodeMin.Y)
                        vNodeMin.Y = vectorNode.Y;
                }
            }

            // Calculate our range and get the correct zoom.
            mapPosition = new Vector2((vNodeMax.X + vNodeMin.X) / 2f, (vNodeMax.Y + vNodeMin.Y) / 2f);

            float DistanceX = vNodeMax.X - vNodeMin.X;
            float DistanceY = vNodeMax.Y - vNodeMin.Y;

            if (DistanceX > DistanceY)
                zoomDistance = DistanceX / 1.5f;
            else
                zoomDistance = DistanceY / 1.5f;

            RefreshMapPosition(mapPosition);
            Parent.Natives.LockMinimapAngle(0);
        }

        public void RefreshMapPosition(Vector2 position)
        {
            mapPosition = new Vector2(position);
            if (ParentTab is GalleryTab g)
                zoomDistance = g.bigPic ? 600 : 1200;
        }

        internal Vector3 GetVectorToCheck(int i)
        {
            if (i == 0)
                return MinimapRoute.StartPoint.Position;
            else if (MinimapRoute.CheckPoints.Count > i)
                return MinimapRoute.CheckPoints[i].Position;
            else if (i == MinimapRoute.CheckPoints.Count)
                return MinimapRoute.EndPoint.Position;
            else return Vector3.Zero;
        }

        internal void SetupBlips()
        {
            foreach (FakeBlip blip in MinimapBlips)
            {
                Parent.Natives.RaceGalleryNextBlipSprite(blip.Sprite);
                int b = Parent.Natives.RaceGalleryAddBlip(blip.Position.X, blip.Position.Y, blip.Position.Z);
                if (blip.Scale > 0)
                    Parent.Natives.SetBlipScale(b, blip.Scale);
                Parent.Natives.SetBlipColour(b, (int)blip.Color);
            }
        }

        public void MaintainMap()
        {
            switch (localCoronaMapStage)
            {
                case 0:
                    InitializeMap();
                    break;
                case 1:
                    ProcessMap();
                    break;
            }
        }
        public void ProcessMap()
        {
            if (enabled)
            {
                if (!turnedOn)
                {
                    Parent.Natives.DisplayRadar(true);
                    Parent.Natives.SetMapFullScreen(true);
                    turnedOn = true;
                }
            }
            else
            {
                if (turnedOn)
                {
                    Parent.Natives.DisplayRadar(false);
                    Parent.Natives.SetMapFullScreen(false);
                    turnedOn = false;
                    Dispose();
                }
            }
            Parent.Natives.SetPlayerBlipPositionThisFrame(-5000, -5000);
            RefreshZoom();
        }

        internal void InitializeMapDisplay()
        {
            Parent.Natives.DeleteWaypoint();
            Parent.Natives.SetWaypointOff();
            Parent.Natives.ClearGpsCustomRoute();
            Parent.Natives.ClearGpsMultiRoute();
            Parent.Natives.SetPoliceRadarBlips(false);

            MinimapRoute.SetupCustomRoute();
            SetupBlips();
        }

        internal void InitializeMap()
        {
            // Use the data to set up our map.
            InitializeMapSize();
            // Initialise all our blips from data
            InitializeMapDisplay();
            // Set the zoom now
            RefreshZoom();
            localCoronaMapStage = 1;
        }

        /// <summary>
        /// To be called every tick
        /// </summary>
        internal void RefreshZoom()
        {
            if (zoomDistance != 0)
                Parent.Natives.SetRadarZoomToDistance(zoomDistance);
            Parent.Natives.LockMinimapPosition(mapPosition.X, mapPosition.Y);
        }

        internal void Dispose()
        {
            localCoronaMapStage = 0;
            enabled = false;
            Parent.Natives.PauseToggleFullscreenMap(true);
            Parent.Natives.DisplayRadar(false);
            Parent.Natives.RaceGalleryFullscreen(false);
            Parent.Natives.ClearRaceGalleryBlips();
            zoomDistance = 0;
            Parent.Natives.SetRadarZoom(0);
            Parent.Natives.SetGpsCustomRouteRender(false, 18, 30);
            Parent.Natives.SetGpsMultiRouteRender(false);
            Parent.Natives.UnlockMinimapPosition();
            Parent.Natives.UnlockMinimapAngle();
            Parent.Natives.DeleteWaypoint();
            Parent.Natives.ClearGpsCustomRoute();
            Parent.Natives.ClearGpsFlags();
            MinimapBlips.Clear();
            MinimapRoute = new MinimapRoute();
        }

        public void ClearMinimap()
        {
            MinimapBlips.Clear();
            MinimapRoute = new MinimapRoute();
            localCoronaMapStage = 0;
            zoomDistance = 0;
            Parent.Natives.ClearRaceGalleryBlips();
            Parent.Natives.SetRadarZoom(0);
            Parent.Natives.SetGpsCustomRouteRender(false, 18, 30);
            Parent.Natives.DeleteWaypoint();
            Parent.Natives.ClearGpsCustomRoute();
            Parent.Natives.ClearGpsFlags();
        }
    }
}
