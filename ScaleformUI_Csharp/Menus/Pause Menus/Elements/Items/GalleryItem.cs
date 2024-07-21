﻿using ScaleformUI.PauseMenu;
using ScaleformUI.Scaleforms.ScaleformUI.Interfaces;

namespace ScaleformUI.PauseMenus.Elements.Items
{
    public class GalleryItem
    {
        private readonly IRageNatives _natives;

        public string TextureDictionary { get; private set; }
        public string TextureName { get; private set; }
        public string Label1 { get; private set; }
        public string Label2 { get; private set; }
        public string Label3 { get; private set; }
        public string Label4 { get; private set; }
        public string RightPanelDescription { get; private set; }

        public event GalleryItemSelected Activated;
        public GalleryTab Parent { get; internal set; }

        public FakeBlip Blip { get; set; }

        public GalleryItem(string textureDictionary, string textureName)
        {
            _natives = Main.GetNativesHandler();
            TextureDictionary = textureDictionary;
            TextureName = textureName;
        }

        public void SetLabels(string label1, string label2, string label3, string label4)
        {
            Label1 = label1;
            Label2 = label2;
            Label3 = label3;
            Label4 = label4;
            if (Parent != null && Parent.Visible && Parent.IsItemVisible(Parent.GalleryItems.IndexOf(this)))
            {
                int gridPosition = Parent.GridIndexFromItemIndex(Parent.GalleryItems.IndexOf(this));
                Parent.Parent._pause._pause.CallFunction("UPDATE_GALLERY_ITEM", gridPosition, gridPosition, 33, 4, 0, 1, Label1, Label2, TextureDictionary, TextureName, 1, false, Label3, Label4);
            }
        }
        public void SetRightDescription(string description)
        {
            RightPanelDescription = description;
            if (Blip != null) return;
            if (Parent != null && Parent.Visible && Parent.IsItemVisible(Parent.GalleryItems.IndexOf(this)))
            {
                _natives.AddTextEntry("gallerytab_desc", RightPanelDescription);
                Parent.Parent._pause._pause.CallFunction("SET_GALLERY_PANEL_HIDDEN", false);
                _natives.BeginScaleformMovieMethod(Parent.Parent._pause._pause.Handle, "SET_GALLERY_PANEL_DESCRIPTION");
                _natives.BeginTextCommandScaleformString("gallerytab_desc");
                _natives.EndTextCommandScaleformString_2();
                _natives.EndScaleformMovieMethod();
            }
        }

        internal void ItemSelected(GalleryTab tab, GalleryItem item, int totalIndex, int gridIndex)
        {
            Activated?.Invoke(tab, item, totalIndex, gridIndex);
        }
    }
}
