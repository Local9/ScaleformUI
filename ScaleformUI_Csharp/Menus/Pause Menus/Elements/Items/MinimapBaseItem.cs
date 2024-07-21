using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.PauseMenus.Elements.Items
{
    public class MinimapBaseItem
    {
        private readonly IRageNatives _natives;

        public MinimapBaseItem()
        {
            _natives = Main.GetNativesHandler();
        }

        public IRageNatives Natives => _natives;
    }
}
