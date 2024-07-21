using ScaleformUI.Elements;
using ScaleformUI.Menu;
using ScaleformUI.PauseMenu;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.PauseMenus.Elements
{
    public delegate void IndexChanged(int index);
    public class Column
    {
        private readonly IRageNatives _natives;

        internal bool isBuilding = false;
        internal int _maxItems = 16;
        public PlayerListTab ParentTab { get; internal set; }
        public PauseMenuBase Parent { get; internal set; }
        internal PaginationHandler Pagination { get; set; }
        public string Label { get; internal set; }
        public SColor Color { get; internal set; }
        public int Order { get; internal set; }
        public string Type { get; internal set; }

        public Column(string label, SColor color)
        {
            _natives = Main.GetNativesHandler();
            Label = label;
            Color = color;
        }

        public IRageNatives Natives => _natives;
    }
}
