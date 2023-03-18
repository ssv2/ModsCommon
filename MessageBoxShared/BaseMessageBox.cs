﻿using ColossalFramework;
using ColossalFramework.UI;
using ModsCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModsCommon.UI
{
    public static class MessageBox
    {
        public static T Show<T>() where T : UIComponent
        {
            var uiObject = new GameObject();
            uiObject.transform.parent = UIView.GetAView().transform;
            var messageBox = uiObject.AddComponent<T>();

            UIView.PushModal(messageBox);
            messageBox.Show(true);
            messageBox.Focus();

            if (UIView.GetAView().panelsLibraryModalEffect is UIComponent modalEffect)
            {
                modalEffect.FitTo(null);
                if (!modalEffect.isVisible || modalEffect.opacity != 1f)
                {
                    modalEffect.Show(false);
                    ValueAnimator.Cancel("ModalEffect67419");
                    ValueAnimator.Animate("ModalEffect67419", val => modalEffect.opacity = val, new AnimatedFloat(0f, 1f, 0.7f, EasingType.CubicEaseOut));
                }
            }

            return messageBox;
        }
        public static void Hide(UIComponent messageBox)
        {
            if (messageBox == null || UIView.GetModalComponent() != messageBox)
                return;

            UIView.PopModal();

            if (UIView.GetAView().panelsLibraryModalEffect is UIComponent modalEffect)
            {
                if (!UIView.HasModalInput())
                {
                    ValueAnimator.Cancel("ModalEffect67419");
                    ValueAnimator.Animate("ModalEffect67419", val => modalEffect.opacity = val, new AnimatedFloat(1f, 0f, 0.7f, EasingType.CubicEaseOut), () => modalEffect.Hide());
                }
                else
                    modalEffect.zOrder = UIView.GetModalComponent().zOrder - 1;
            }

            messageBox.Hide();
            UnityEngine.Object.Destroy(messageBox.gameObject);
            UnityEngine.Object.Destroy(messageBox);
        }
    }
    public abstract class MessageBoxBase : CustomUIPanel, IAutoLayoutPanel
    {
        public event Action OnCloseClick;
        public event Action OnClose;

        public static int DefaultWidth => 573;
        public static int DefaultHeight => 200;
        public static int ButtonHeight => 35;
        protected static int ButtonsSpace => 25;
        public static int Padding => 16;
        public Vector2 MaxContentSize
        {
            get
            {
                var resolution = GetUIView().GetScreenResolution();
                return new Vector2(DefaultWidth, resolution.y - 580f);
            }
        }
        protected virtual int ContentSpacing => 0;


        private CustomUIDragHandle Header { get; set; }
        private CustomUILabel Caption { get; set; }
        protected AutoSizeAdvancedScrollablePanel Panel { get; set; }
        private CustomUIPanel ButtonPanel { get; set; }
        private IEnumerable<CustomUIButton> Buttons => ButtonPanel.components.OfType<CustomUIButton>();

        private List<uint> ButtonsRatio { get; } = new List<uint>();
        public string CaptionText { set => Caption.text = value; }

        private int defaultButton = 0;
        public int DefaultButton
        {
            get => defaultButton;
            set => defaultButton = Mathf.Clamp(value, 0, Buttons.Count() - 1);
        }

        #region CONSTRUCTOR

        public MessageBoxBase()
        {
            isVisible = true;
            canFocus = true;
            isInteractive = true;
            size = new Vector2(DefaultWidth, DefaultHeight);
            Atlas = CommonTextures.Atlas;
            BackgroundSprite = CommonTextures.PanelBig;
            color = new Color32(17, 19, 22, 255);
            anchor = UIAnchorStyle.Left | UIAnchorStyle.Top | UIAnchorStyle.Proportional;

            AddHeader();
            AddContent();
            AddButtonPanel();

            SetSize();
            CenterToParent();
        }
        private void AddHeader()
        {
            Header = AddUIComponent<CustomUIDragHandle>();
            Header.relativePosition = new Vector2(0, 0);

            var background = Header.AddUIComponent<CustomUISlicedSprite>();
            background.atlas = CommonTextures.Atlas;
            background.spriteName = CommonTextures.PanelBig;
            background.color = new Color32(12, 12, 12, 255);

            Caption = Header.AddUIComponent<CustomUILabel>();
            Caption.textAlignment = UIHorizontalAlignment.Center;
            Caption.textScale = 1.3f;
            Caption.anchor = UIAnchorStyle.Top;

            Caption.eventTextChanged += (_, _) => Caption.CenterToParent();

            var cancel = Header.AddUIComponent<CustomUIButton>();
            cancel.atlas = CommonTextures.Atlas;
            cancel.normalBgSprite = CommonTextures.CloseButtonNormal;
            cancel.hoveredBgSprite = CommonTextures.CloseButtonHovered;
            cancel.pressedBgSprite = CommonTextures.CloseButtonPressed;
            cancel.size = new Vector2(24, 24);
            cancel.relativePosition = new Vector2(540, 9);
            cancel.eventClick += CloseClick;

            Header.eventSizeChanged += (component, size) =>
            {
                Caption.size = size;
                Caption.CenterToParent();

                background.size = size;
            };
            Header.size = new Vector2(DefaultWidth, 42);
        }
        private void AddContent()
        {
            Panel = AddUIComponent<AutoSizeAdvancedScrollablePanel>();
            Panel.MaxSize = MaxContentSize;
            Panel.size = new Vector2(DefaultWidth, 0f);
            Panel.relativePosition = new Vector2(0, Header.height + Padding);
            Panel.Content.autoLayoutPadding = new RectOffset(Padding, Padding, ContentSpacing, 0);
            Panel.Content.autoReset = true;
            Panel.eventSizeChanged += ContentSizeChanged;
        }
        private void AddButtonPanel()
        {
            ButtonPanel = AddUIComponent<CustomUIPanel>();
            ButtonPanel.size = new Vector2(DefaultWidth, ButtonHeight + 10);
        }

        #endregion

        #region EVENTS

        private Vector2 SizeBefore { get; set; } = new Vector2();
        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();

            var resolution = GetUIView().GetScreenResolution();
            var delta = (size - SizeBefore) / 2;
            SizeBefore = size;

            var newPosition = Vector2.Max(Vector2.Min((Vector2)relativePosition - delta, resolution - size), Vector2.zero);
            relativePosition = newPosition;
        }
        protected override void OnResolutionChanged(Vector2 previousResolution, Vector2 currentResolution)
        {
            base.OnResolutionChanged(previousResolution, currentResolution);

            Panel.MaxSize = MaxContentSize;
            Panel.size = Panel.size;
        }
        private void ContentSizeChanged(UIComponent component, Vector2 value) => SetSize();
        private void SetSize()
        {
            height = Mathf.Floor(Header.height + Padding + Panel.height + ButtonPanel.height + Padding);
            ButtonPanel.relativePosition = new Vector2(0, Header.height + Padding + Panel.height + Padding);
        }

        #endregion

        #region BUTTONS

        public override void Update()
        {
            base.Update();

            var buttons = Buttons.ToArray();

            for (var i = 0; i < buttons.Length; i += 1)
            {
                buttons[i].isSelected = (i == defaultButton);

                if (buttons[i].state == UIButton.ButtonState.Focused)
                    buttons[i].state = UIButton.ButtonState.Normal;
            }
        }

        protected CustomUIButton AddButton(Action action, uint ratio = 1)
        {
            var button = ButtonPanel.AddUIComponent<CustomUIButton>();
            button.ButtonMessageBoxStyle();
            button.eventClick += (UIComponent component, UIMouseEventParameter eventParam) => action?.Invoke();

            ButtonsRatio.Add(Math.Max(ratio, 1));
            ChangeButtons();

            return button;
        }
        public void SetButtonsRatio(params uint[] ratio)
        {
            for (var i = 0; i < ButtonsRatio.Count; i += 1)
                ButtonsRatio[i] = i < ratio.Length ? Math.Max(ratio[i], 1) : 1;

            ChangeButtons();
        }
        public void SetAutoButtonRatio()
        {
            var widths = Buttons.Select(b => b.MinimumAutoSize.x).ToArray();
            var allWidth = (int)(width - (widths.Length + 3) * (ButtonsSpace / 2));
            var sumWidth = widths.Sum();
            var allDelta = allWidth - sumWidth;

            for (var i = 0; i < widths.Length; i += 1)
            {
                var delta = allDelta / (widths.Length - i);
                allDelta += widths[i];
                widths[i] = Math.Max(widths[i] + delta, ButtonHeight);
                allDelta -= widths[i];
            }

            SetButtonsRatio(widths.Select(i => (uint)i).ToArray());
        }

        public void ChangeButtons()
        {
            var sum = 0u;
            var before = ButtonsRatio.Select(i => (sum += i) - i).ToArray();

            var buttons = Buttons.ToArray();
            for (var i = 0; i < buttons.Length; i += 1)
                ChangeButton(buttons[i], i + 1, buttons.Length, (float)before[i] / sum, (float)ButtonsRatio[i] / sum);
        }
        private void ChangeButton(CustomUIButton button, int i, int from, float? positionRatio = null, float? widthRatio = null)
        {
            var width = this.width - (ButtonsSpace * 2 + ButtonsSpace / 2 * (from - 1));
            button.size = new Vector2(width * (widthRatio ?? 1f / from), ButtonHeight);
            button.relativePosition = new Vector2(ButtonsSpace * (0.5f + i / 2f) + width * (positionRatio ?? 1f / from * (i - 1)), 0);
        }

        #endregion

        protected override void OnKeyDown(UIKeyEventParameter p)
        {
            if (!p.used)
            {
                if (p.keycode == KeyCode.Escape)
                {
                    p.Use();
                    Close();
                }
                else if (p.keycode == KeyCode.Return)
                {
                    p.Use();
                    if (Buttons.Skip(defaultButton).FirstOrDefault() is CustomUIButton button)
                        button.SimulateClick();
                }
                else if (p.keycode == KeyCode.RightArrow)
                {
                    p.Use();
                    DefaultButton += 1;
                }
                else if (p.keycode == KeyCode.LeftArrow)
                {
                    p.Use();
                    DefaultButton -= 1;
                }
            }
        }

        protected virtual void Close()
        {
            OnClose?.Invoke();
            MessageBox.Hide(this);
        }
        private void CloseClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            OnCloseClick?.Invoke();
            Close();
        }


        public new bool IsLayoutSuspended => Panel.IsLayoutSuspended;
        public new Vector2 ItemSize => Panel.ItemSize;
        public new RectOffset LayoutPadding => Panel.LayoutPadding;

        public override void StopLayout() => Panel.StopLayout();
        public override void StartLayout(bool layoutNow = true, bool force = false) => Panel.StartLayout(layoutNow, force);
        public override void PauseLayout(Action action) => Panel.PauseLayout(action);
    }
}
