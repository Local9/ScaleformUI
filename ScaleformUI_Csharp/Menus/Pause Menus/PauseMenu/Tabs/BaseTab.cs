using ScaleformUI.Elements;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.PauseMenu
{
    public class BaseTab
    {
        private readonly IRageNatives _natives;

        internal int _type;
        internal SColor TabColor;
        public BaseTab(string name, SColor color)
        {
            _natives = Main.GetNativesHandler();
            Title = name;
            TabColor = color;
        }

        public IRageNatives Natives => _natives;

        public bool Visible { get; set; }
        public virtual bool Focused { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public TabView Parent { get; internal set; }

        public List<TabLeftItem> LeftItemList = new List<TabLeftItem>();

        public event EventHandler Activated;
        public event EventHandler DrawInstructionalButtons;

        public void OnActivated()
        {
            Activated?.Invoke(this, EventArgs.Empty);
        }
    }
}