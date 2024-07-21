﻿using CitizenFX.Core;
using ScaleformUI.Elements;
using ScaleformUI.LobbyMenu;
using ScaleformUI.Menu;
using ScaleformUI.PauseMenu;

namespace ScaleformUI.PauseMenus.Elements.Columns
{
    public delegate void SettingItemSelected(UIMenuItem item, int index);
    public class SettingsListColumn : Column
    {
        public event IndexChanged OnIndexChanged;
        public List<UIMenuItem> Items { get; internal set; }
        private List<UIMenuItem> _unfilteredItems;
        public event SettingItemSelected OnSettingItemActivated;
        public ScrollingType ScrollingType { get => Pagination.scrollType; set => Pagination.scrollType = value; }
        public SettingsListColumn(string label, SColor color, ScrollingType scrollType = ScrollingType.CLASSIC, int maxItems = 16) : base(label, color)
        {
            Items = new List<UIMenuItem>();
            Type = "settings";
            _maxItems = maxItems;
            Pagination = new PaginationHandler
            {
                ItemsPerPage = _maxItems,
                scrollType = scrollType
            };
        }
        public void AddSettings(UIMenuItem item)
        {
            item.ParentColumn = this;
            Items.Add(item);
            Pagination.TotalItems = Items.Count;
            if (Parent != null && Parent.Visible)
            {
                if (Pagination.TotalItems <= Pagination.ItemsPerPage)
                {
                    int sel = CurrentSelection;
                    Pagination.MinItem = Pagination.CurrentPageStartIndex;
                    if (Pagination.scrollType == ScrollingType.CLASSIC && Pagination.TotalPages > 1)
                    {
                        int missingItems = Pagination.GetMissingItems();
                        if (missingItems > 0)
                        {
                            Pagination.ScaleformIndex = Pagination.GetPageIndexFromMenuIndex(Pagination.CurrentPageEndIndex) + missingItems;
                            Pagination.MinItem = Pagination.CurrentPageStartIndex - missingItems;
                        }
                    }
                    Pagination.MaxItem = Pagination.CurrentPageEndIndex;
                    _itemCreation(0, Items.Count - 1, false);
                    if (Parent is TabView pause && ParentTab.Visible)
                    {
                        if (ParentTab.listCol[ParentTab.Focus] == this)
                            CurrentSelection = sel;
                    }
                    else if (Parent is MainView)
                    {
                        CurrentSelection = sel;
                    }
                }
            }
        }

        internal void _itemCreation(int page, int pageIndex, bool before, bool isOverflow = false)
        {
            int menuIndex = Pagination.GetMenuIndexFromPageIndex(page, pageIndex);
            if (!before)
            {
                if (Pagination.GetPageItemsCount(page) < Pagination.ItemsPerPage && Pagination.TotalPages > 1)
                {
                    if (Pagination.scrollType == ScrollingType.ENDLESS)
                    {
                        if (menuIndex > Pagination.TotalItems - 1)
                        {
                            menuIndex -= Pagination.TotalItems;
                            Pagination.MaxItem = menuIndex;
                        }
                    }
                    else if (Pagination.scrollType == ScrollingType.CLASSIC && isOverflow)
                    {
                        int missingItems = Pagination.ItemsPerPage - Pagination.GetPageItemsCount(page);
                        menuIndex -= missingItems;
                    }
                    else if (Pagination.scrollType == ScrollingType.PAGINATED)
                        if (menuIndex >= Items.Count) return;
                }
            }
            int scaleformIndex = Pagination.GetScaleformIndex(menuIndex);

            UIMenuItem item = Items[menuIndex];
            if (item.MainColor == SColor.HUD_Panel_light)
                item.MainColor = SColor.HUD_Pause_bg;
            if (Parent is MainView lobby)
            {
                Natives.AddTextEntry($"menu_lobby_desc_{menuIndex}", item.Description);
                Natives.BeginScaleformMovieMethod(lobby._pause._lobby.Handle, "ADD_LEFT_ITEM");
                Natives.PushScaleformMovieFunctionParameterBool(before);
                Natives.PushScaleformMovieFunctionParameterInt(menuIndex);
                Natives.PushScaleformMovieFunctionParameterInt(item._itemId);
                Natives.PushScaleformMovieMethodParameterString(item._formatLeftLabel);
                if (item.DescriptionHash != 0 && string.IsNullOrWhiteSpace(item.Description))
                {
                    Natives.BeginTextCommandScaleformString("STRTNM1");
                    Natives.AddTextComponentSubstringTextLabelHashKey(item.DescriptionHash);
                    Natives.EndTextCommandScaleformString_2();
                }
                else
                {
                    Natives.BeginTextCommandScaleformString($"menu_lobby_desc_{menuIndex}");
                    Natives.EndTextCommandScaleformString_2();
                }
                Natives.PushScaleformMovieFunctionParameterBool(item.Enabled);
                Natives.PushScaleformMovieFunctionParameterBool(item.BlinkDescription);
                switch (item)
                {
                    case UIMenuListItem:
                        UIMenuListItem it = (UIMenuListItem)item;
                        string joinedList = string.Join(",", it.Items.Cast<string>().Select(x =>
                            x = !it.Enabled ? x.ReplaceRstarColorsWith("~c~") : it.Selected ? (x.StartsWith("~") ? x : "~s~" + x).ToString().Replace("~w~", "~l~").Replace("~s~", "~l~") : (x.StartsWith("~") ? x : "~s~" + x).ToString().Replace("~l~", "~s~")
                        ));
                        Natives.AddTextEntry($"listitem_lobby_{menuIndex}_list", joinedList);
                        Natives.BeginTextCommandScaleformString($"listitem_lobby_{menuIndex}_list");
                        Natives.EndTextCommandScaleformString();
                        Natives.PushScaleformMovieFunctionParameterInt(it.Index);
                        Natives.PushScaleformMovieFunctionParameterInt(it.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(it.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(it.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(it.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuCheckboxItem:
                        UIMenuCheckboxItem check = (UIMenuCheckboxItem)item;
                        Natives.PushScaleformMovieFunctionParameterInt((int)check.Style);
                        Natives.PushScaleformMovieMethodParameterBool(check.Checked);
                        Natives.PushScaleformMovieFunctionParameterInt(check.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(check.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(check.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(check.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuSliderItem:
                        UIMenuSliderItem prItem = (UIMenuSliderItem)item;
                        Natives.PushScaleformMovieFunctionParameterInt(prItem._max);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem._multiplier);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.Value);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.HighlightedTextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.SliderColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterBool(prItem._heritage);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuProgressItem:
                        UIMenuProgressItem slItem = (UIMenuProgressItem)item;
                        Natives.PushScaleformMovieFunctionParameterInt(slItem._max);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem._multiplier);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.Value);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.HighlightedTextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.SliderColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuSeparatorItem:
                        UIMenuSeparatorItem separatorItem = (UIMenuSeparatorItem)item;
                        Natives.PushScaleformMovieFunctionParameterBool(separatorItem.Jumpable);
                        Natives.PushScaleformMovieFunctionParameterInt(item.MainColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    default:
                        Natives.PushScaleformMovieFunctionParameterInt(item.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(item.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        lobby._pause._lobby.CallFunction("UPDATE_SETTINGS_ITEM_LABEL_RIGHT", scaleformIndex, item._formatRightLabel);
                        if (item.RightBadge != BadgeIcon.NONE)
                        {
                            lobby._pause._lobby.CallFunction("SET_SETTINGS_ITEM_RIGHT_BADGE", scaleformIndex, (int)item.RightBadge);
                        }
                        break;
                }
                lobby._pause._lobby.CallFunction("SET_SETTINGS_ITEM_LABEL_FONT", scaleformIndex, item.labelFont.FontName, item.labelFont.FontID);
                lobby._pause._lobby.CallFunction("SET_SETTINGS_ITEM_RIGHT_LABEL_FONT", scaleformIndex, item.rightLabelFont.FontName, item.rightLabelFont.FontID);
                if (item.LeftBadge != BadgeIcon.NONE)
                    lobby._pause._lobby.CallFunction("SET_SETTINGS_ITEM_LEFT_BADGE", scaleformIndex, (int)item.LeftBadge);
            }
            else if (Parent is TabView pause && ParentTab.Visible)
            {
                Natives.AddTextEntry($"menu_pause_playerTab[{pause.Index}]_desc_{menuIndex}", item.Description);
                Natives.BeginScaleformMovieMethod(pause._pause._pause.Handle, "ADD_PLAYERS_TAB_SETTINGS_ITEM");
                Natives.PushScaleformMovieFunctionParameterBool(before);
                Natives.PushScaleformMovieFunctionParameterInt(menuIndex);
                Natives.PushScaleformMovieFunctionParameterInt(item._itemId);
                Natives.PushScaleformMovieMethodParameterString(item._formatLeftLabel);
                if (item.DescriptionHash != 0 && string.IsNullOrWhiteSpace(item.Description))
                {
                    Natives.BeginTextCommandScaleformString("STRTNM1");
                    Natives.AddTextComponentSubstringTextLabelHashKey(item.DescriptionHash);
                    Natives.EndTextCommandScaleformString_2();
                }
                else
                {
                    Natives.BeginTextCommandScaleformString($"menu_pause_playerTab[{pause.Index}]_desc_{menuIndex}");
                    Natives.EndTextCommandScaleformString_2();
                }
                Natives.PushScaleformMovieFunctionParameterBool(item.Enabled);
                Natives.PushScaleformMovieFunctionParameterBool(item.BlinkDescription);
                switch (item)
                {
                    case UIMenuListItem:
                        UIMenuListItem it = (UIMenuListItem)item;
                        string joinedList = string.Join(",", it.Items.Cast<string>().Select(x =>
                            x = !it.Enabled ? x.ReplaceRstarColorsWith("~c~") : it.Selected ? (x.StartsWith("~") ? x : "~s~" + x).ToString().Replace("~w~", "~l~").Replace("~s~", "~l~") : (x.StartsWith("~") ? x : "~s~" + x).ToString().Replace("~l~", "~s~")
                        ));
                        Natives.AddTextEntry($"listitem_menu_pause_playerTab[{pause.Index}]_{menuIndex}_list", joinedList);
                        Natives.BeginTextCommandScaleformString($"listitem_menu_pause_playerTab[{pause.Index}]_{menuIndex}_list");
                        Natives.EndTextCommandScaleformString();
                        Natives.PushScaleformMovieFunctionParameterInt(it.Index);
                        Natives.PushScaleformMovieFunctionParameterInt(it.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(it.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(it.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(it.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuCheckboxItem:
                        UIMenuCheckboxItem check = (UIMenuCheckboxItem)item;
                        Natives.PushScaleformMovieFunctionParameterInt((int)check.Style);
                        Natives.PushScaleformMovieMethodParameterBool(check.Checked);
                        Natives.PushScaleformMovieFunctionParameterInt(check.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(check.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(check.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(check.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuSliderItem:
                        UIMenuSliderItem prItem = (UIMenuSliderItem)item;
                        Natives.PushScaleformMovieFunctionParameterInt(prItem._max);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem._multiplier);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.Value);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.HighlightedTextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(prItem.SliderColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterBool(prItem._heritage);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuProgressItem:
                        UIMenuProgressItem slItem = (UIMenuProgressItem)item;
                        Natives.PushScaleformMovieFunctionParameterInt(slItem._max);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem._multiplier);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.Value);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.HighlightedTextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(slItem.SliderColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    case UIMenuSeparatorItem:
                        UIMenuSeparatorItem separatorItem = (UIMenuSeparatorItem)item;
                        Natives.PushScaleformMovieFunctionParameterBool(separatorItem.Jumpable);
                        Natives.PushScaleformMovieFunctionParameterInt(item.MainColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        break;
                    default:
                        Natives.PushScaleformMovieFunctionParameterInt(item.MainColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightColor.ArgbValue/**/);
                        Natives.PushScaleformMovieFunctionParameterInt(item.TextColor.ArgbValue);
                        Natives.PushScaleformMovieFunctionParameterInt(item.HighlightedTextColor.ArgbValue);
                        Natives.EndScaleformMovieMethod();
                        pause._pause._pause.CallFunction("UPDATE_PLAYERS_TAB_SETTINGS_ITEM_LABEL_RIGHT", scaleformIndex, item._formatRightLabel);
                        if (item.RightBadge != BadgeIcon.NONE)
                        {
                            pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_ITEM_RIGHT_BADGE", scaleformIndex, (int)item.RightBadge);
                        }
                        break;
                }
                pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_ITEM_LABEL_FONT", scaleformIndex, item.labelFont.FontName, item.labelFont.FontID);
                pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_ITEM_RIGHT_LABEL_FONT", scaleformIndex, item.rightLabelFont.FontName, item.rightLabelFont.FontID);
                if (item.LeftBadge != BadgeIcon.NONE)
                    pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_ITEM_LEFT_BADGE", scaleformIndex, (int)item.LeftBadge);

            }
        }

        internal async void GoUp()
        {
            try
            {
                Items[CurrentSelection].Selected = false;
                do
                {
                    await BaseScript.Delay(0);
                    bool overflow = CurrentSelection == 0 && Pagination.TotalPages > 1;
                    if (Pagination.GoUp())
                    {
                        if (Pagination.scrollType == ScrollingType.ENDLESS || (Pagination.scrollType == ScrollingType.CLASSIC && !overflow))
                        {
                            _itemCreation(Pagination.GetPage(CurrentSelection), Pagination.CurrentPageIndex, true);
                            if (Parent is MainView lobby)
                                await lobby._pause._lobby.CallFunctionReturnValueInt("SET_INPUT_EVENT", 8, 100);
                            else if (Parent is TabView pause && ParentTab.Visible)
                                await pause._pause._pause.CallFunctionReturnValueInt("SET_INPUT_EVENT", 8, 100);
                        }
                        else if (Pagination.scrollType == ScrollingType.PAGINATED || (Pagination.scrollType == ScrollingType.CLASSIC && overflow))
                        {
                            if (Parent is MainView lobby)
                                lobby._pause._lobby.CallFunction("CLEAR_SETTINGS_COLUMN");
                            else if (Parent is TabView pause && ParentTab.Visible)
                                pause._pause._pause.CallFunction("CLEAR_PLAYERS_TAB_SETTINGS_COLUMN");
                            int max = Pagination.ItemsPerPage;
                            isBuilding = true;
                            for (int i = 0; i < max; i++)
                            {
                                if (!Parent.Visible) return;
                                _itemCreation(Pagination.CurrentPage, i, false, true);
                            }
                            isBuilding = false;
                        }
                    }
                }
                while (Items[CurrentSelection] is UIMenuSeparatorItem sp && sp.Jumpable);

                if (Parent is MainView _lobby)
                {
                    _lobby._pause._lobby.CallFunction("SET_SETTINGS_SELECTION", Pagination.ScaleformIndex);
                    _lobby._pause._lobby.CallFunction("SET_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                }
                else if (Parent is TabView _pause && ParentTab.Visible)
                {
                    _pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_SELECTION", Pagination.ScaleformIndex);
                    _pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                }

                Items[CurrentSelection].Selected = true;
                IndexChangedEvent();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        internal async void GoDown()
        {
            try
            {
                Items[CurrentSelection].Selected = false;
                do
                {
                    bool overflow = CurrentSelection == Items.Count - 1 && Pagination.TotalPages > 1;
                    if (Pagination.GoDown())
                    {
                        if (Pagination.scrollType == ScrollingType.ENDLESS || (Pagination.scrollType == ScrollingType.CLASSIC && !overflow))
                        {
                            _itemCreation(Pagination.GetPage(CurrentSelection), Pagination.CurrentPageIndex, false);

                            if (Parent is MainView lobby)
                                lobby._pause._lobby.CallFunction("SET_INPUT_EVENT", 9, 100);
                            else if (Parent is TabView pause && ParentTab.Visible)
                                await pause._pause._pause.CallFunctionReturnValueInt("SET_INPUT_EVENT", 9, 100);
                        }
                        else if (Pagination.scrollType == ScrollingType.PAGINATED || (Pagination.scrollType == ScrollingType.CLASSIC && overflow))
                        {
                            if (Parent is MainView lobby)
                                lobby._pause._lobby.CallFunction("CLEAR_SETTINGS_COLUMN");
                            else if (Parent is TabView pause && ParentTab.Visible)
                                pause._pause._pause.CallFunction("CLEAR_PLAYERS_TAB_SETTINGS_COLUMN");
                            int i = 0;
                            int max = Pagination.ItemsPerPage;
                            isBuilding = true;
                            for (i = 0; i < max; i++)
                            {
                                if (!Parent.Visible) return;
                                _itemCreation(Pagination.CurrentPage, i, false, true);
                            }
                            isBuilding = false;
                        }
                    }
                }
                while (Items[CurrentSelection] is UIMenuSeparatorItem sp && sp.Jumpable);

                if (Parent is MainView _lobby)
                {
                    _lobby._pause._lobby.CallFunction("SET_SETTINGS_SELECTION", Pagination.ScaleformIndex);
                    _lobby._pause._lobby.CallFunction("SET_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                }
                else if (Parent is TabView _pause && ParentTab.Visible)
                {
                    _pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_SELECTION", Pagination.ScaleformIndex);
                    _pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                }

                Items[CurrentSelection].Selected = true;
                IndexChangedEvent();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public int CurrentSelection
        {
            get { return Items.Count == 0 ? 0 : Pagination.CurrentMenuIndex; }
            set
            {
                if (value == CurrentSelection)
                {
                    if (Parent != null && Parent.Visible)
                    {
                        if (Parent is MainView)
                        {
                            if (!Items[CurrentSelection].Selected)
                                Items[CurrentSelection].Selected = true;
                        }
                        else if (Parent is TabView pause && ParentTab.Visible)
                        {
                            if (pause.Index == pause.Tabs.IndexOf(ParentTab) && pause.FocusLevel == 1 && !Items[CurrentSelection].Selected)
                                Items[CurrentSelection].Selected = true;
                        }
                    }
                    return;
                }
                if (value < 0)
                {
                    Pagination.CurrentMenuIndex = 0;
                }
                else if (value >= Items.Count)
                {
                    Pagination.CurrentMenuIndex = Items.Count - 1;
                }
                if (Pagination.TotalItems > 0)
                {
                    Items[CurrentSelection].Selected = false;

                    Pagination.CurrentMenuIndex = value;
                    Pagination.CurrentPage = Pagination.GetPage(Pagination.CurrentMenuIndex);
                    Pagination.CurrentPageIndex = value;
                    Pagination.ScaleformIndex = Pagination.GetScaleformIndex(value);
                    if (value > Pagination.MaxItem || value < Pagination.MinItem)
                    {
                        RefreshColumn();
                    }

                    if (Parent != null && Parent.Visible)
                    {
                        if (Parent is MainView lobby)
                        {
                            lobby._pause._lobby.CallFunction("SET_SETTINGS_SELECTION", Pagination.GetScaleformIndex(Pagination.CurrentMenuIndex));
                            lobby._pause._lobby.CallFunction("SET_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                            Items[CurrentSelection].Selected = true;
                        }
                        else if (Parent is TabView pause && ParentTab.Visible)
                        {
                            pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_SELECTION", Pagination.GetScaleformIndex(Pagination.CurrentMenuIndex));
                            pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                            if (pause.Index == pause.Tabs.IndexOf(ParentTab) && pause.FocusLevel == 1)
                                Items[CurrentSelection].Selected = true;
                        }
                        IndexChangedEvent();
                    }
                    if (Items[CurrentSelection] is UIMenuSeparatorItem jp)
                    {
                        if (jp.Jumpable)
                            GoDown();
                        else
                        {
                            if (Pagination.TotalItems == 1)
                                Items[CurrentSelection].Selected = false;
                        }
                    }
                }
            }
        }

        public void UpdateItemLabels(int index, string leftLabel, string rightLabel)
        {
            if (Parent != null)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("UPDATE_SETTINGS_ITEM_LABELS", Pagination.GetScaleformIndex(index), leftLabel, rightLabel);
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("UPDATE_PLAYERS_TAB_SETTINGS_ITEM_LABELS", Pagination.GetScaleformIndex(index), leftLabel, rightLabel);
            }
        }

        public void UpdateItemBlinkDescription(int index, bool blink)
        {
            if (Parent != null)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("UPDATE_SETTINGS_ITEM_BLINK_DESC", Pagination.GetScaleformIndex(index), blink);
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("UPDATE_PLAYERS_TAB_SETTINGS_ITEM_BLINK_DESC", Pagination.GetScaleformIndex(index), blink);
            }
        }

        public void UpdateItemLabel(int index, string label)
        {
            if (Parent != null)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("UPDATE_SETTINGS_ITEM_LABEL", Pagination.GetScaleformIndex(index), label);
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("UPDATE_PLAYERS_TAB_SETTINGS_ITEM_LABEL", Pagination.GetScaleformIndex(index), label);
            }
        }

        public void UpdateItemRightLabel(int index, string label)
        {
            if (Parent != null)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("UPDATE_SETTINGS_ITEM_LABEL_RIGHT", Pagination.GetScaleformIndex(index), label);
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("UPDATE_PLAYERS_TAB_SETTINGS_ITEM_LABEL_RIGHT", Pagination.GetScaleformIndex(index), label);
            }
        }

        public void UpdateItemLeftBadge(int index, BadgeIcon badge)
        {
            if (Parent != null)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("SET_SETTINGS_ITEM_LEFT_BADGE", Pagination.GetScaleformIndex(index), (int)badge);
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_ITEM_LEFT_BADGE", Pagination.GetScaleformIndex(index), (int)badge);
            }
        }

        public void UpdateItemRightBadge(int index, BadgeIcon badge)
        {
            if (Parent != null)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("SET_SETTINGS_ITEM_RIGHT_BADGE", Pagination.GetScaleformIndex(index), (int)badge);
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_ITEM_RIGHT_BADGE", Pagination.GetScaleformIndex(index), (int)badge);
            }
        }

        public void EnableItem(int index, bool enable)
        {
            if (Parent != null && Parent.Visible)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("ENABLE_SETTINGS_ITEM", Pagination.GetScaleformIndex(index), enable);
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("ENABLE_PLAYERS_TAB_SETTINGS_ITEM", Pagination.GetScaleformIndex(index), enable);
            }
        }

        public void Clear()
        {
            if (Parent != null && Parent.Visible)
            {
                if (Parent is MainView lobby)
                    lobby._pause._lobby.CallFunction("CLEAR_SETTINGS_COLUMN");
                else if (Parent is TabView pause && ParentTab.Visible)
                    pause._pause._pause.CallFunction("CLEAR_PLAYERS_TAB_SETTINGS_COLUMN");
            }
            Items.Clear();
            Pagination.Reset();
        }

        public void SortSettings(Comparison<UIMenuItem> compare)
        {
            try
            {
                Items[CurrentSelection].Selected = false;
                if (_unfilteredItems == null || _unfilteredItems.Count == 0)
                {
                    _unfilteredItems = Items.ToList();
                }
                Clear();
                List<UIMenuItem> list = _unfilteredItems.ToList();
                list.Sort(compare);
                Items = list.ToList();
                Pagination.TotalItems = Items.Count;
                if (Parent != null && Parent.Visible)
                {
                    if (Parent is MainView lobby)
                        lobby.buildSettings();
                    else if (Parent is TabView pause && ParentTab.Visible)
                        pause.buildSettings(ParentTab);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ScaleformUI - " + ex.ToString());
            }
        }

        public void FilterSettings(Func<UIMenuItem, bool> predicate)
        {
            try
            {

                Items[CurrentSelection].Selected = false;
                if (_unfilteredItems == null || _unfilteredItems.Count == 0)
                {
                    _unfilteredItems = Items.ToList();
                }
                Clear();
                Items = _unfilteredItems.Where(predicate.Invoke).ToList();
                if (Items.Count == 0)
                    throw new Exception("Predicate resulted in a filtering of 0 items.. settings column cannot rebuild!");
                Pagination.TotalItems = Items.Count;
                if (Parent != null && Parent.Visible)
                {
                    if (Parent is MainView lobby)
                        lobby.buildSettings();
                    else if (Parent is TabView pause && ParentTab.Visible)
                        pause.buildSettings(ParentTab);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("^1ScaleformUI - " + ex.ToString());
            }
        }

        public void ResetFilter()
        {
            try
            {
                if (_unfilteredItems != null && _unfilteredItems.Count > 0)
                {
                    Items[CurrentSelection].Selected = false;
                    Clear();
                    Items = _unfilteredItems.ToList();
                    Pagination.TotalItems = Items.Count;
                    if (Parent != null && Parent.Visible)
                    {
                        if (Parent is MainView lobby)
                            lobby.buildSettings();
                        else if (Parent is TabView pause && ParentTab.Visible)
                            pause.buildSettings(ParentTab);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ScaleformUI - " + ex.ToString());
            }
        }

        private void RefreshColumn()
        {
            if (Parent is MainView lobby)
                lobby._pause._lobby.CallFunction("CLEAR_SETTINGS_COLUMN");
            else if (Parent is TabView pause && ParentTab.Visible)
                pause._pause._pause.CallFunction("CLEAR_PLAYERS_TAB_SETTINGS_COLUMN");
            if (Items.Count > 0)
            {
                isBuilding = true;
                int max = Pagination.ItemsPerPage;
                if (Items.Count < max)
                    max = Items.Count;

                Pagination.MinItem = Pagination.CurrentPageStartIndex;
                if (Pagination.scrollType == ScrollingType.CLASSIC && Pagination.TotalPages > 1)
                {
                    int missingItems = Pagination.GetMissingItems();
                    if (missingItems > 0)
                    {
                        Pagination.ScaleformIndex = Pagination.GetPageIndexFromMenuIndex(Pagination.CurrentPageEndIndex) + missingItems;
                        Pagination.MinItem = Pagination.CurrentPageStartIndex - missingItems;
                    }
                }
                Pagination.MaxItem = Pagination.CurrentPageEndIndex;

                for (int i = 0; i < max; i++)
                {
                    if (!Parent.Visible) return;
                    _itemCreation(Pagination.CurrentPage, i, false, true);
                }
                Pagination.ScaleformIndex = Pagination.GetScaleformIndex(CurrentSelection);
                if (Parent is MainView _lobby)
                {
                    _lobby._pause._lobby.CallFunction("SET_SETTINGS_SELECTION", Pagination.ScaleformIndex);
                    _lobby._pause._lobby.CallFunction("SET_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                }
                else if (Parent is TabView _pause && ParentTab.Visible)
                {
                    _pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_SELECTION", Pagination.ScaleformIndex);
                    _pause._pause._pause.CallFunction("SET_PLAYERS_TAB_SETTINGS_QTTY", CurrentSelection + 1, Items.Count);
                }
                isBuilding = false;
            }
        }


        public void SelectItem()
        {
            OnSettingItemActivated?.Invoke(Items[CurrentSelection], CurrentSelection);
        }
        public void IndexChangedEvent()
        {
            OnIndexChanged?.Invoke(CurrentSelection);
        }
    }
}
