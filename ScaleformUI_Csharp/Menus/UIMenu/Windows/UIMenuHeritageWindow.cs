using CitizenFX.Core;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.Menu
{
    public class UIMenuHeritageWindow : UIMenuWindow
    {
        private readonly IRageNatives _natives;
        public int Mom { get; set; }
        public int Dad { get; set; }

        public UIMenuHeritageWindow(int mom, int dad)
        {
            _natives = Main.GetNativesHandler();
            id = 0;
            Mom = mom;
            Dad = dad;
        }

        public async void Index(int mom, int dad)
        {
            Mom = mom;
            Dad = dad;
            if (mom > 21) Mom = 21;
            if (mom < 0) Mom = 0;
            if (dad > 23) Dad = 23;
            if (dad < 0) Dad = 0;
            int wid = ParentMenu.Windows.IndexOf(this);
            while (!_natives.HasStreamedTextureDictLoaded("char_creator_portraits"))
            {
                await BaseScript.Delay(0);
                _natives.RequestStreamedTextureDict("char_creator_portraits", true);
            }
            Main.scaleformUI.CallFunction("UPDATE_HERITAGE_WINDOW", wid, Mom, Dad);
            _natives.SetStreamedTextureDictAsNoLongerNeeded("char_creator_portraits");
        }
    }
}
