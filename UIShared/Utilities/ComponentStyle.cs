﻿using ColossalFramework.UI;
using ModsCommon.Utilities;
using System;
using UnityEngine;
using static ColossalFramework.UI.UIDropDown;

namespace ModsCommon.UI
{
    public static class ComponentStyle
    {
        public static Color32 NormalBlue => new Color32(51, 153, 255, 255);
        public static Color32 HoveredBlue => new Color32(29, 143, 255, 255);
        public static Color32 PressedBlue => new Color32(7, 132, 255, 255);

        public static Color32 NormalGray => new Color32(129, 141, 145, 255);
        public static Color32 HoveredGray => new Color32(104, 116, 120, 255);
        public static Color32 PressedGray => new Color32(81, 90, 93, 255);
        public static Color32 DisabledGray => new Color32(35, 39, 41, 255);

        public static Color32 NormalGreen => new Color32(98, 179, 45, 255);
        public static Color32 HoveredGreen => new Color32(84, 153, 38, 255);
        public static Color32 PressedGreen => new Color32(70, 128, 32, 255);

        public static Color32 NormalSettingsGray => new Color32(48, 60, 74, 255);
        public static Color32 HoveredSettingsGray => new Color32(85, 99, 115, 255);
        public static Color32 HoveredSettingsGrayLight => new Color32(122, 138, 153, 255);
        public static Color32 PressedSettingsGray => new Color32(100, 113, 128, 255);
        public static Color32 DisabledSettingsGray => new Color32(29, 39, 51, 255);

        public static Color32 PanelColorDark => new Color32(17, 19, 22, 255);
        public static Color32 PanelColor => new Color32(34, 38, 44, 255);


        #region BUTTON

        public static Color32 ButtonNormalColor => NormalGray;
        public static Color32 ButtonHoveredColor => HoveredGray;
        public static Color32 ButtonPressedColor => PressedGray;
        public static Color32 ButtonFocusedColor => NormalBlue;
        public static Color32 ButtonDisabledColor => DisabledGray;
        public static Color32 ButtonSelectedNormalColor => NormalBlue;
        public static Color32 ButtonSelectedHoveredColor => HoveredBlue;
        public static Color32 ButtonSelectedPressedColor => PressedBlue;
        public static Color32 ButtonSelectedFocusedColor => NormalBlue;
        public static Color32 ButtonSelectedDisabledColor => DisabledGray;

        public static void CustomMessageBoxStyle(this CustomUIButton button)
        {
            button.atlas = CommonTextures.Atlas;
            button.SetBgSprite(new SpriteSet(CommonTextures.PanelBig));
            button.SetBgColor(new ColorSet(ButtonNormalColor, ButtonHoveredColor, ButtonPressedColor, ButtonFocusedColor, ButtonDisabledColor));
            button.SetSelectedBgColor(new ColorSet(ButtonSelectedNormalColor, ButtonSelectedHoveredColor, ButtonSelectedPressedColor, ButtonSelectedFocusedColor, ButtonSelectedDisabledColor));
            button.SetTextColor(new ColorSet(Color.white, Color.white, Color.white, null, null));

            button.horizontalAlignment = UIHorizontalAlignment.Center;
            button.verticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Center;
        }
        public static void CustomSettingsStyle(this CustomUIButton button)
        {
            button.atlas = CommonTextures.Atlas;
            button.SetBgSprite(new SpriteSet(CommonTextures.PanelBig));
            button.SetBgColor(new ColorSet(HoveredSettingsGrayLight, PressedSettingsGray, PressedSettingsGray, HoveredSettingsGrayLight, DisabledSettingsGray));
            button.SetTextColor(new ColorSet(Color.white, Color.white, Color.white, null, null));

            button.horizontalAlignment = UIHorizontalAlignment.Center;
            button.verticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Center;
        }

        #endregion

        #region DROPDOWN

        [Obsolete]
        public static void DefaultStyle(this CustomUIDropDown dropDown, Vector2? size = null)
        {
            dropDown.atlasBackground = CommonTextures.Atlas;
            dropDown.normalBgSprite = CommonTextures.FieldSingle;
            dropDown.hoveredBgSprite = CommonTextures.FieldSingle;
            dropDown.disabledBgSprite = CommonTextures.FieldSingle;
            dropDown.color = FieldNormalColor;
            dropDown.hoveredBgColor = FieldHoveredColor;
            dropDown.disabledBgColor = FieldDisabledColor;

            dropDown.atlasForeground = TextureHelper.InGameAtlas;
            dropDown.normalFgSprite = "IconDownArrow";
            dropDown.hoveredFgSprite = "IconDownArrowHovered";
            dropDown.focusedFgSprite = "IconDownArrow";
            dropDown.disabledFgSprite = "IconDownArrowDisabled";

            dropDown.atlas = CommonTextures.Atlas;
            dropDown.listBackground = CommonTextures.FieldHovered;
            dropDown.itemHover = CommonTextures.FieldNormal;
            dropDown.itemHighlight = CommonTextures.FieldFocused;

            dropDown.itemHeight = 20;
            dropDown.listHeight = 700;
            dropDown.listPosition = PopupListPosition.Below;
            dropDown.clampListToScreen = true;
            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            dropDown.itemPadding = new RectOffset(14, 0, 5, 0);

            dropDown.popupColor = Color.white;
            dropDown.popupTextColor = Color.black;

            dropDown.textScale = 0.7f;
            dropDown.textFieldPadding = new RectOffset(8, 0, 6, 0);
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;
            dropDown.horizontalAlignment = UIHorizontalAlignment.Left;
            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Scale;
            dropDown.horizontalAlignment = UIHorizontalAlignment.Right;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;

            dropDown.triggerButton = dropDown;

            dropDown.size = size ?? new Vector2(230, 20);
        }

        public static void DefaultStyle<ObjectType, PopupType, EntityType>(this AdvancedDropDown<ObjectType, PopupType, EntityType> dropDown, Vector2? size = null)
            where PopupType : Popup<ObjectType, EntityType>
            where EntityType : PopupEntity<ObjectType>
        {
            dropDown.atlasBackground = CommonTextures.Atlas;
            dropDown.SetBgSprite(new SpriteSet(CommonTextures.FieldSingle));
            dropDown.SetBgColor(new ColorSet(FieldNormalColor, FieldHoveredColor, FieldHoveredColor, FieldNormalColor, FieldDisabledColor));

            dropDown.atlasForeground = CommonTextures.Atlas;
            dropDown.SetFgSprite(new SpriteSet(CommonTextures.ArrowDown));
            dropDown.SetFgColor(new ColorSet(new Color32(0, 0, 0, 255)));

            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Scale;
            dropDown.scaleFactor = 0.7f;
            dropDown.horizontalAlignment = UIHorizontalAlignment.Right;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;
            dropDown.spritePadding = new RectOffset(0, 5, 0, 0);

            dropDown.Entity.Padding = new RectOffset(0, 40, 0, 0);

            dropDown.size = size ?? new Vector2(230, 20);
        }
        public static void DefaultStyle<ObjectType, EntityType>(this Popup<ObjectType, EntityType> popup, float? entityHeight = null)
            where EntityType : PopupEntity<ObjectType>
        {
            popup.atlas = CommonTextures.Atlas;
            popup.backgroundSprite = CommonTextures.FieldSingle;
            popup.ItemHover = CommonTextures.FieldSingle;
            popup.ItemSelected = CommonTextures.FieldSingle;

            popup.color = FieldHoveredColor;
            popup.ColorHover = FieldNormalColor;
            popup.ColorSelected = FieldFocusedColor;

            popup.EntityHeight = entityHeight ?? 20f;
            popup.MaximumSize = new Vector2(230f, 700f);
        }


        public static void CustomMessageBoxStyle<ObjectType, PopupType, EntityType>(this AdvancedDropDown<ObjectType, PopupType, EntityType> dropDown, Vector2? size = null)
            where PopupType : Popup<ObjectType, EntityType>
            where EntityType : PopupEntity<ObjectType>
        {
            dropDown.atlasBackground = CommonTextures.Atlas;
            dropDown.SetBgSprite(new SpriteSet(CommonTextures.FieldSingle));
            dropDown.SetBgColor(new ColorSet(NormalGray, PressedGray, PressedGray, HoveredGray, DisabledGray));

            dropDown.atlasForeground = CommonTextures.Atlas;
            dropDown.SetFgSprite(new SpriteSet(CommonTextures.ArrowDown));
            dropDown.SetFgColor(new ColorSet(DisabledSettingsGray));

            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Scale;
            dropDown.scaleFactor = 0.7f;
            dropDown.spritePadding.right = 5;

            dropDown.textVerticalAlignment = UIVerticalAlignment.Middle;
            dropDown.textHorizontalAlignment = UIHorizontalAlignment.Left;

            dropDown.horizontalAlignment = UIHorizontalAlignment.Right;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;

            dropDown.size = size ?? new Vector2(230, 20);
        }
        public static void CustomMessageBoxStyle<ObjectType, EntityType>(this Popup<ObjectType, EntityType> popup, float? entityHeight = null)
            where EntityType : PopupEntity<ObjectType>
        {
            popup.atlas = CommonTextures.Atlas;
            popup.backgroundSprite = CommonTextures.FieldSingle;
            popup.ItemHover = CommonTextures.FieldSingle;
            popup.ItemSelected = CommonTextures.FieldSingle;

            popup.color = NormalGray;
            popup.ColorHover = HoveredGray;
            popup.ColorSelected = NormalBlue;

            popup.EntityHeight = entityHeight ?? 20f;
            popup.MaximumSize = new Vector2(230f, 700f);
            popup.ItemsPadding = new RectOffset(4, 4, 4, 4);
        }


        public static void CustomSettingsStyle<ObjectType, PopupType, EntityType>(this AdvancedDropDown<ObjectType, PopupType, EntityType> dropDown, Vector2? size = null)
            where PopupType : Popup<ObjectType, EntityType>
            where EntityType : PopupEntity<ObjectType>
        {
            dropDown.atlasBackground = CommonTextures.Atlas;
            dropDown.SetBgSprite(new SpriteSet(CommonTextures.FieldSingle));
            dropDown.SetBgColor(new ColorSet(HoveredSettingsGrayLight, PressedSettingsGray, PressedSettingsGray, HoveredSettingsGrayLight, DisabledSettingsGray));

            dropDown.atlasForeground = CommonTextures.Atlas;
            dropDown.SetFgSprite(new SpriteSet(CommonTextures.ArrowDown));
            dropDown.SetFgColor(new ColorSet(DisabledSettingsGray));

            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Scale;
            dropDown.scaleFactor = 0.7f;
            dropDown.spritePadding.right = 5;

            dropDown.textVerticalAlignment = UIVerticalAlignment.Middle;
            dropDown.textHorizontalAlignment = UIHorizontalAlignment.Left;

            dropDown.horizontalAlignment = UIHorizontalAlignment.Right;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;

            dropDown.size = size ?? new Vector2(230, 20);
        }

        public static void CustomSettingsStyle<ObjectType, EntityType>(this Popup<ObjectType, EntityType> popup, float? entityHeight = null)
            where EntityType : PopupEntity<ObjectType>
        {
            popup.atlas = CommonTextures.Atlas;
            popup.backgroundSprite = CommonTextures.FieldSingle;
            popup.ItemHover = CommonTextures.FieldSingle;
            popup.ItemSelected = CommonTextures.FieldSingle;

            popup.color = NormalSettingsGray;
            popup.ColorHover = HoveredSettingsGray;
            popup.ColorSelected = NormalBlue;

            popup.EntityHeight = entityHeight ?? 20f;
            popup.MaximumSize = new Vector2(230f, 700f);
            popup.ItemsPadding = new RectOffset(4, 4, 4, 4);
        }

        #endregion

        #region TEXT FIELD

        public static Color32 FieldNormalColor => new Color32(148, 161, 166, 255);
        public static Color32 FieldHoveredColor => new Color32(177, 189, 193, 255);
        public static Color32 FieldDisabledColor => new Color32(104, 110, 113, 255);
        public static Color32 FieldFocusedColor => new Color32(151, 202, 222, 255);
        public static Color32 FieldDisabledFocusedColor => new Color32(113, 151, 166, 255);

        public static void DefaultStyle(this CustomUITextField textField)
        {
            textField.atlas = CommonTextures.Atlas;
            textField.normalBgSprite = CommonTextures.FieldSingle;
            textField.hoveredBgSprite = CommonTextures.FieldSingle;
            textField.focusedBgSprite = CommonTextures.FieldSingle;
            textField.disabledBgSprite = CommonTextures.FieldSingle;
            textField.selectionSprite = CommonTextures.Empty;

            textField.color = FieldNormalColor;
            textField.hoveredColor = FieldHoveredColor;
            textField.focusedColor = FieldFocusedColor;
            textField.disabledColor = FieldDisabledColor;
            textField.selectionBackgroundColor = Color.black;

            textField.allowFloats = true;
            textField.isInteractive = true;
            textField.enabled = true;
            textField.readOnly = false;
            textField.builtinKeyNavigation = true;
            textField.cursorWidth = 1;
            textField.cursorBlinkTime = 0.45f;
            textField.selectOnFocus = true;

            textField.verticalAlignment = UIVerticalAlignment.Middle;
            textField.padding = new RectOffset(0, 0, 6, 0);
        }
        public static void CustomSettingsStyle(this CustomUITextField textField)
        {
            textField.atlas = CommonTextures.Atlas;
            textField.normalBgSprite = CommonTextures.FieldSingle;
            textField.hoveredBgSprite = CommonTextures.FieldSingle;
            textField.focusedBgSprite = CommonTextures.FieldSingle;
            textField.disabledBgSprite = CommonTextures.FieldSingle;
            textField.selectionSprite = CommonTextures.Empty;

            textField.color = HoveredSettingsGrayLight;
            textField.hoveredColor = PressedSettingsGray;
            textField.focusedColor = NormalBlue;
            textField.disabledColor = DisabledSettingsGray;
        }

        #endregion

        #region TAB STRIPE

        public static void CustomStyle<TabType>(this TabStrip<TabType> tabStrip)
            where TabType : Tab
        {
            tabStrip.atlas = CommonTextures.Atlas;
            tabStrip.backgroundSprite = CommonTextures.PanelBig;

            tabStrip.TabSpacingHorizontal = 4;
            tabStrip.TabSpacingVertical = 4;

            tabStrip.TabAtlas = CommonTextures.Atlas;
            tabStrip.TabNormalSprite = CommonTextures.PanelSmall;
            tabStrip.TabHoveredSprite = CommonTextures.PanelSmall;
            tabStrip.TabPressedSprite = CommonTextures.PanelSmall;
            tabStrip.TabFocusedSprite = CommonTextures.PanelSmall;
            tabStrip.TabDisabledSprite = CommonTextures.PanelSmall;
        }
        public static void CustomSettingsStyle<TabType>(this TabStrip<TabType> tabStrip)
            where TabType : Tab
        {
            tabStrip.atlas = CommonTextures.Atlas;
            tabStrip.backgroundSprite = CommonTextures.PanelBig;
            tabStrip.color = NormalSettingsGray;

            tabStrip.TabSpacingHorizontal = 4;
            tabStrip.TabSpacingVertical = 4;

            tabStrip.TabAtlas = CommonTextures.Atlas;
            tabStrip.TabNormalSprite = CommonTextures.PanelSmall;
            tabStrip.TabHoveredSprite = CommonTextures.PanelSmall;
            tabStrip.TabPressedSprite = CommonTextures.PanelSmall;
            tabStrip.TabFocusedSprite = CommonTextures.PanelSmall;
            tabStrip.TabDisabledSprite = CommonTextures.PanelSmall;

            tabStrip.TabColor = NormalSettingsGray;
            tabStrip.TabHoveredColor = HoveredSettingsGray;
            tabStrip.TabPressedColor = PressedSettingsGray;
            tabStrip.TabFocusedColor = NormalBlue;
        }

        #endregion

        #region TOGGLE

        public static Color32 ToggleOnNormalColor => NormalGreen;
        public static Color32 ToggleOnHoveredColor => HoveredGreen;
        public static Color32 ToggleOnPressedColor => PressedGreen;

        public static Color32 ToggleOffNormalColor => NormalGray;
        public static Color32 ToggleOffHoveredColor => HoveredGray;
        public static Color32 ToggleOffPressedColor => PressedGray;


        public static void CustomStyle(this CustomUIToggle toggle)
        {
            toggle.atlas = CommonTextures.Atlas;
            toggle.SetBgSprite(new SpriteSet(CommonTextures.ToggleBackgroundSmall));
            toggle.SetFgSprite(new SpriteSet(CommonTextures.ToggleCircle));

            toggle.OnColor = FieldFocusedColor;
            toggle.OnHoverColor = FieldFocusedColor;
            toggle.OnPressedColor = FieldFocusedColor;
            toggle.OnDisabledColor = FieldDisabledFocusedColor;

            toggle.OffColor = FieldNormalColor;
            toggle.OffHoverColor = FieldHoveredColor;
            toggle.OffPressedColor = FieldHoveredColor;
            toggle.OffDisabledColor = FieldDisabledColor;

            toggle.textScale = 0.8f;
            toggle.CircleScale = 0.7f;
            toggle.ShowMark = true;
            toggle.textPadding = new RectOffset(12, 6, 5, 0);
            toggle.size = new Vector2(42f, 22f);
        }

        public static void CustomSettingsStyle(this CustomUIToggle toggle)
        {
            toggle.atlas = CommonTextures.Atlas;
            toggle.SetBgSprite(new SpriteSet(CommonTextures.ToggleBackground));
            toggle.SetFgSprite(new SpriteSet(CommonTextures.ToggleCircle));

            toggle.OnColor = ToggleOnNormalColor;
            toggle.OnHoverColor = ToggleOnHoveredColor;
            toggle.OnPressedColor = ToggleOnHoveredColor;

            toggle.OffColor = HoveredSettingsGrayLight;
            toggle.OffHoverColor = PressedSettingsGray;
            toggle.OffPressedColor = PressedSettingsGray;

            toggle.CircleScale = 0.7f;
            toggle.ShowMark = true;
            toggle.textPadding = new RectOffset(17, 11, 5, 0);
            toggle.size = new Vector2(60f, 30f);
        }

        #endregion
    }
}
