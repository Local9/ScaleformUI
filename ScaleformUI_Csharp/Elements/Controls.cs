using CitizenFX.Core;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.Elements
{
    /// <summary>
    /// Class that provides tools to handle the game controls.
    /// </summary>
    public static class Controls
    {
        private static IRageNatives _natives => Main.GetNativesHandler();

        /// <summary>
        /// All of the controls required for using a keyboard.
        /// </summary>
        private static readonly GameControl[] NecessaryControlsKeyboard = new GameControl[]
        {
            GameControl.FrontendAccept,
            GameControl.FrontendAxisX,
            GameControl.FrontendAxisY,
            GameControl.FrontendDown,
            GameControl.FrontendUp,
            GameControl.FrontendLeft,
            GameControl.FrontendRight,
            GameControl.FrontendCancel,
            GameControl.FrontendSelect,
            GameControl.CursorScrollDown,
            GameControl.CursorScrollUp,
            GameControl.CursorX,
            GameControl.CursorY,
            GameControl.CursorAccept,
            GameControl.CursorCancel,
            GameControl.MoveUpDown,
            GameControl.MoveLeftRight,
            GameControl.Sprint,
            GameControl.Jump,
            GameControl.Enter,
            GameControl.VehicleExit,
            GameControl.VehicleAccelerate,
            GameControl.VehicleBrake,
            GameControl.VehicleMoveLeftRight,
            GameControl.VehicleFlyYawLeft,
            GameControl.FlyLeftRight,
            GameControl.FlyUpDown,
            GameControl.VehicleFlyYawRight,
            GameControl.VehicleHandbrake,

        };
        /// <summary>
        /// All of the controls required for using a gamepad.
        /// </summary>
        private static readonly GameControl[] NecessaryControlsGamePad = NecessaryControlsKeyboard.Concat(new GameControl[]
        {
            GameControl.LookUpDown,
            GameControl.LookLeftRight,
            GameControl.Aim,
            GameControl.Attack,
            GameControl.VehicleAccelerate,
            GameControl.VehicleBrake,
            GameControl.VehicleMoveLeftRight,
            GameControl.MoveUpDown,
            GameControl.MoveLeftRight,
            GameControl.VehicleExit,
        }).ToArray();

        /// <summary>
        /// Toggles the availability of the controls.
        /// It does not disable the basic movement and frontend controls.
        /// </summary>
        /// <param name="toggle">If we want to enable or disable the controls.</param>
        public static void Toggle(bool toggle)
        {
            // If we want to enable the controls
            if (toggle)
            {
                // Enable all of them
                Game.EnableAllControlsThisFrame(0);
                Game.EnableAllControlsThisFrame(1);
                Game.EnableAllControlsThisFrame(2);
            }
            // If we don't need them
            else
            {
                // Disable all of the controls
                Game.DisableAllControlsThisFrame(2);

                // Now, re-enable the controls that are required for the game
                // First, pick the right controls for gamepad or keyboard and mouse
                GameControl[] list = Game.CurrentInputMode == InputMode.GamePad ? NecessaryControlsGamePad : NecessaryControlsKeyboard;
                // Then, enable all of the controls for that input mode
                foreach (GameControl control in list)
                    _natives.EnableControlAction(0, (int)control, true);
            }
        }
    }
}
