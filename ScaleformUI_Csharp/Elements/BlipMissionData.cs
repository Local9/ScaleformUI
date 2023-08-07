using CitizenFX.Core.Native;

namespace ScaleformUI.Elements
{
    public enum eBlipMissionInfoHandle
    {
        Text = 1,
        Icon = 2,
        Name = 3,
        Header = 4,
        LeftTextOnly = 5,
    }

    public class BlipMissionData
    {
        const int COLUMN_DISPLAYED = 1;
        const string LABEL_PREFIX = $"BMD_";

        public string Title { get; set; }
        public bool RockstarVerified { get; set; }
        public int Money { get; set; } = 0;
        public int RP { get; set; } = 0;
        public string TextureDictionary { get; set; }
        public string TextureName { get; set; }

        public List<BlipMissionInfo> Info { get; set; } = new();

        public BlipMissionData(string title, bool rockstarVerified, int money, int rp, string textureDictionary, string textureName)
        {
            Title = title;
            RockstarVerified = rockstarVerified;
            Money = money;
            RP = rp;
            TextureDictionary = textureDictionary;
            TextureName = textureName;
        }

        public void AddText(string left, string right)
        {
            eBlipMissionInfoHandle handle = eBlipMissionInfoHandle.Text;

            if (string.IsNullOrEmpty(right))
                handle = eBlipMissionInfoHandle.LeftTextOnly;

            BlipMissionInfo info = new();
            info.Handle = handle;
            info.LabelLeft = left;

            if (handle == eBlipMissionInfoHandle.Text)
                info.LabelRight = right;

            Info.Add(info);
        }

        public void AddIcon(string left, string right, int iconId = 0, int iconColor = 0, bool _checked = false)
        {
            BlipMissionInfo info = new();
            info.Handle = eBlipMissionInfoHandle.Icon;
            info.LabelLeft = left;
            info.LabelRight = right;

            info.IconId = iconId;
            info.IconColor = iconColor;
            info.Checked = _checked;

            Info.Add(info);
        }

        public void AddName(string left, string right)
        {
            BlipMissionInfo info = new();
            info.Handle = eBlipMissionInfoHandle.Name;
            info.LabelLeft = left;
            info.LabelRight = right;

            Info.Add(info);
        }

        public void AddHeader(string left, string right)
        {
            BlipMissionInfo info = new();
            info.Handle = eBlipMissionInfoHandle.Header;
            info.LabelLeft = left;
            info.LabelRight = right;

            Info.Add(info);
        }

        public void AddInfo(BlipMissionInfo info)
        {
            Info.Add(info);
        }

        private void SetScaleformString(string text)
        {
            API.BeginTextCommandScaleformString(text);
            API.EndTextCommandScaleformString();
        }

        private string CreateLabel(string text)
        {
            string label = $"{LABEL_PREFIX}{text}";
            API.AddTextEntry(label, text);
            return label;
        }

        private void SetTitle()
        {
            if (!API.PushScaleformMovieFunctionN("SET_COLUMN_TITLE")) return;

            API.PushScaleformMovieFunctionParameterInt(COLUMN_DISPLAYED);
            SetScaleformString(string.Empty);
            SetScaleformString(CreateLabel(Title));
            API.PushScaleformMovieFunctionParameterInt(RockstarVerified ? 1 : 0);
            API.PushScaleformMovieFunctionParameterString(TextureDictionary);
            API.PushScaleformMovieFunctionParameterString(TextureName);
            API.PushScaleformMovieFunctionParameterInt(0);
            API.PushScaleformMovieFunctionParameterInt(0);

            if (RP == 0)
                API.PushScaleformMovieFunctionParameterBool(false);
            else
                SetScaleformString(CreateLabel($"{RP}"));

            if (Money == 0)
                API.PushScaleformMovieFunctionParameterBool(false);
            else
                SetScaleformString(CreateLabel($"{Money}"));

            API.PopScaleformMovieFunctionVoid();
        }

        private void SetColumnVisiblity(bool visible) // Needs to be in a tick/handler
        {
            if (!API.PushScaleformMovieFunctionN("SHOW_COLUMN")) return;

            API.PushScaleformMovieFunctionParameterInt(COLUMN_DISPLAYED);
            API.PushScaleformMovieFunctionParameterBool(visible);
            API.PopScaleformMovieFunctionVoid();
        }

        public void CreateBlipMissionData() // Needs to be in a tick/handler
        {
            if (!API.IsFrontendReadyForControl()) return;
            if (!API.IsHoveringOverMissionCreatorBlip())
            {
                SetColumnVisiblity(false);
            }
        }
    }

    public class BlipMissionInfo
    {
        public eBlipMissionInfoHandle Handle { get; set; }
        public string LabelLeft { get; set; }
        public string LabelRight { get; set; }
        public int IconId { get; set; }
        public int IconColor { get; set; }
        public bool Checked { get; set; }
    }
}
