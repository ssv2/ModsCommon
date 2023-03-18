﻿using ColossalFramework.UI;
using ModsCommon.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModsCommon.UI
{
    public class CustomUIPanel : UIComponent, IAutoLayoutPanel
    {
        private Vector3 positionBefore;
        public override void ResetLayout() => positionBefore = relativePosition;
        public override void PerformLayout()
        {
            if ((relativePosition - positionBefore).sqrMagnitude > 0.001)
                relativePosition = positionBefore;
        }

        private bool initialized;
        private bool resetNeeded;

        private void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                if ((resetNeeded || autoLayout != AutoLayout.Disabled) && !IsLayoutSuspended)
                    Reset();

                Invalidate();
            }
        }
        public override void OnEnable()
        {
            base.OnEnable();

            if (autoLayout != AutoLayout.Disabled && !IsLayoutSuspended)
                AutoArrange();
        }
        public override void OnDisable()
        {
            BgRenderData = null;
            FgRenderData = null;

            base.OnDisable();
        }
        public override void Update()
        {
            base.Update();

            if (m_IsComponentInvalidated && autoLayout != AutoLayout.Disabled && !IsLayoutSuspended && isVisible)
                AutoArrange();
        }
        public override void LateUpdate()
        {
            base.LateUpdate();

            Initialize();
            if (resetNeeded && isVisible)
            {
                resetNeeded = false;
                Reset();
            }
        }


        #region STYLE

        protected UITextureAtlas atlas;
        public UITextureAtlas Atlas
        {
            get => atlas ??= GetUIView()?.defaultAtlas;
            set
            {
                if (!Equals(value, atlas))
                {
                    atlas = value;
                    Invalidate();
                }
            }
        }

        protected UITextureAtlas atlasBackground;
        public UITextureAtlas AtlasBackground
        {
            get => atlasBackground ?? Atlas;
            set
            {
                if (!Equals(value, atlasBackground))
                {
                    atlasBackground = value;
                    Invalidate();
                }
            }
        }

        protected UITextureAtlas atlasForeground;
        public UITextureAtlas AtlasForeground
        {
            get => atlasForeground ?? Atlas;
            set
            {
                if (!Equals(value, atlasForeground))
                {
                    atlasForeground = value;
                    Invalidate();
                }
            }
        }

        Color32? normalBgColor;
        public Color32 NormalBgColor
        {
            get => normalBgColor ?? base.color;
            set
            {
                normalBgColor = value;
                Invalidate();
            }
        }

        Color32? disabledBgColor;
        public Color32 DisabledBgColor
        {
            get => disabledBgColor ?? base.disabledColor;
            set
            {
                disabledBgColor = value;
                Invalidate();
            }
        }

        Color32? normalFgColor;
        public Color32 NormalFgColor
        {
            get => normalFgColor ?? base.color;
            set
            {
                normalFgColor = value;
                Invalidate();
            }
        }

        Color32? disabledFgColor;
        public Color32 DisabledFgColor
        {
            get => disabledFgColor ?? base.disabledColor;
            set
            {
                disabledFgColor = value;
                Invalidate();
            }
        }

        string backgroundSprite;
        public string BackgroundSprite
        {
            get => backgroundSprite;
            set
            {
                if (value != backgroundSprite)
                {
                    backgroundSprite = value;
                    Invalidate();
                }
            }
        }

        string foregroundSprite;
        public string ForegroundSprite
        {
            get => foregroundSprite;
            set
            {
                if (value != foregroundSprite)
                {
                    foregroundSprite = value;
                    Invalidate();
                }
            }
        }

        private RectOffset spritePadding;
        public RectOffset SpritePadding
        {
            get => spritePadding ??= new RectOffset();
            set
            {
                if (!Equals(value, spritePadding))
                {
                    spritePadding = value;
                    Invalidate();
                }
            }
        }

        protected UISpriteFlip spriteFlip;
        public UISpriteFlip SpriteFlip
        {
            get => spriteFlip;
            set
            {
                if (value != spriteFlip)
                {
                    spriteFlip = value;
                    Invalidate();
                }
            }
        }


        #endregion

        #region LAYOUT

        protected RectOffset padding;
        public RectOffset Padding
        {
            get => padding ??= new RectOffset();
            set
            {
                value = value.ConstrainPadding();
                if (!Equals(value, padding))
                {
                    padding = value;
                    Reset();
                }
            }
        }
        public int PaddingRight
        {
            get => Padding.right;
            set
            {
                var old = Padding;
                Padding = new RectOffset(old.left, value, old.top, old.bottom);
            }
        }
        public int PaddingLeft
        {
            get => Padding.left;
            set
            {
                var old = Padding;
                Padding = new RectOffset(value, old.right, old.top, old.bottom);
            }
        }
        public int PaddingTop
        {
            get => Padding.top;
            set
            {
                var old = Padding;
                Padding = new RectOffset(old.left, old.right, value, old.bottom);
            }
        }
        public int PaddingButtom
        {
            get => Padding.bottom;
            set
            {
                var old = Padding;
                Padding = new RectOffset(old.left, old.right, old.top, value);
            }
        }

        protected AutoLayout autoLayout;
        public AutoLayout AutoLayout
        {
            get => autoLayout;
            set
            {
                if (value != autoLayout)
                {
                    autoLayout = value;
                    Reset();
                }
            }
        }

        protected LayoutStart autoLayoutStart;
        public LayoutStart AutoLayoutStart
        {
            get => autoLayoutStart;
            set
            {
                if (value != autoLayoutStart)
                {
                    autoLayoutStart = value;
                    Reset();
                }
            }
        }

        protected int autoLayoutSpace;
        public int AutoLayoutSpace
        {
            get => autoLayoutSpace;
            set
            {
                if (value != autoLayoutSpace)
                {
                    autoLayoutSpace = value;
                    Reset();
                }
            }
        }

        protected bool autoLayoutCenter;
        public bool AutoLayoutCenter
        {
            get => autoLayoutCenter;
            set
            {
                if (value != autoLayoutCenter)
                {
                    autoLayoutCenter = value;
                    Reset();
                }
            }
        }

        protected bool autoFitChildrenHorizontally;
        public bool AutoFitChildrenHorizontally
        {
            get => autoFitChildrenHorizontally;
            set
            {
                if (value != autoFitChildrenHorizontally)
                {
                    autoFitChildrenHorizontally = value;
                    Reset();
                }
            }
        }

        protected bool autoFitChildrenVertically;
        public bool AutoFitChildrenVertically
        {
            get => autoFitChildrenVertically;
            set
            {
                if (value != autoFitChildrenVertically)
                {
                    autoFitChildrenVertically = value;
                    Reset();
                }
            }
        }

        private Dictionary<UIComponent, RectOffset> itemPadding = new Dictionary<UIComponent, RectOffset>();
        public void SetItemPadding(UIComponent component, RectOffset padding)
        {
            padding = padding.ConstrainPadding();
            if (itemPadding.TryGetValue(component, out var oldPadding) && Equals(oldPadding, padding))
                return;

            itemPadding[component] = padding;
            Reset();
        }
        public RectOffset GetItemPadding(UIComponent component) => itemPadding.TryGetValue(component, out var padding) ? padding : new RectOffset();

        private int layoutSuspend;
        public bool IsLayoutSuspended => layoutSuspend != 0;

        public Vector2 ItemSize => new Vector2(width - -Padding.horizontal, height - Padding.vertical);
        public RectOffset LayoutPadding => Padding;

        public virtual void StopLayout()
        {
            layoutSuspend += 1;
        }
        public virtual void StartLayout(bool layoutNow = true, bool force = false)
        {
            layoutSuspend = force ? 0 : Mathf.Max(layoutSuspend - 1, 0);

            if (layoutSuspend == 0 && layoutNow)
                Reset();
        }
        public virtual void PauseLayout(Action action)
        {
            if (action != null)
            {
                try
                {
                    StopLayout();
                    action();
                }
                finally
                {
                    StartLayout();
                }
            }
        }

        public void Reset()
        {
            if (!IsLayoutSuspended)
            {
                if (autoLayout != AutoLayout.Disabled)
                    AutoArrange();

                Invalidate();
            }
        }

        protected virtual void AutoArrange()
        {
            try
            {
                StopLayout();

                FitChildren(autoFitChildrenHorizontally, autoFitChildrenVertically);

                var offset = Vector2.zero;
                var padding = Padding;

                if (autoLayoutStart.StartLeft())
                    offset.x = padding.left;
                else if (autoLayoutStart.StartRight())
                    offset.x = padding.right;

                if (autoLayoutStart.StartTop())
                    offset.y = padding.top;
                else if (autoLayoutStart.StartBottom())
                    offset.y = padding.bottom;

                for (int i = 0; i < childCount; i += 1)
                {
                    var child = autoLayoutStart switch
                    {
                        LayoutStart.TopLeft or LayoutStart.BottomLeft when autoLayout == AutoLayout.Horizontal => m_ChildComponents[i],
                        LayoutStart.TopRight or LayoutStart.BottomRight when autoLayout == AutoLayout.Horizontal => m_ChildComponents[childCount - 1 - i],
                        LayoutStart.TopLeft or LayoutStart.TopRight when autoLayout == AutoLayout.Vertical => m_ChildComponents[i],
                        LayoutStart.BottomLeft or LayoutStart.BottomRight when autoLayout == AutoLayout.Vertical => m_ChildComponents[childCount - 1 - i],
                        _ => m_ChildComponents[i],
                    };

                    if (!child.isVisible || !child.enabled || !child.gameObject.activeSelf)
                        continue;

                    var childPos = Vector2.zero;
                    var childPadding = GetItemPadding(child);

                    switch (autoLayout)
                    {
                        case AutoLayout.Horizontal:
                            if (autoLayoutStart.StartRight())
                                childPos.x = width - offset.x - child.width - childPadding.right;
                            else
                                childPos.x = offset.x + childPadding.left;

                            if (autoLayoutCenter)
                                childPos.y = offset.y + childPadding.top + (height - padding.vertical - child.height - childPadding.vertical) * 0.5f;
                            else if (autoLayoutStart.StartBottom())
                                childPos.y = height - offset.y - child.height - childPadding.bottom;
                            else
                                childPos.y = offset.y + childPadding.top;

                            offset.x += child.width + childPadding.horizontal + autoLayoutSpace;
                            break;
                        case AutoLayout.Vertical:
                            if (autoLayoutStart.StartBottom())
                                childPos.y = height - offset.y - child.height - childPadding.bottom;
                            else
                                childPos.y = offset.y + childPadding.top;

                            if (autoLayoutCenter)
                                childPos.x = offset.x + childPadding.left + (width - padding.horizontal - child.width - childPadding.horizontal) * 0.5f;
                            else if (autoLayoutStart.StartRight())
                                childPos.x = width - offset.x - child.width - childPadding.right;
                            else
                                childPos.x = offset.x + childPadding.left;

                            offset.y += child.height + childPadding.vertical + autoLayoutSpace;
                            break;
                    }

                    child.relativePosition = childPos;
                }
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                StartLayout(false);
            }
        }

        public new void FitChildrenHorizontally() => FitChildren(true, false);
        public new void FitChildrenVertically() => FitChildren(false, true);
        public new void FitChildren() => FitChildren(true, true);
        protected virtual void FitChildren(bool horizontally, bool vertically)
        {
            var newSize = size;
            var padding = Padding;

            if (horizontally)
            {
                switch (autoLayout)
                {
                    case AutoLayout.Disabled:
                        var offset = 0f;
                        for (int i = 0; i < childCount; i += 1)
                        {
                            var child = m_ChildComponents[i];
                            if (child.isVisibleSelf)
                                offset = Mathf.Max(offset, child.relativePosition.x + child.width);
                        }
                        newSize.x = Mathf.Max(offset, padding.left) + padding.right;
                        break;
                    case AutoLayout.Horizontal:
                        var totalWidth = 0f;
                        var count = 0;
                        for (int i = 0; i < childCount; i += 1)
                        {
                            var child = m_ChildComponents[i];
                            if (child.isVisibleSelf)
                            {
                                count += 1;
                                totalWidth += child.width + GetItemPadding(child).horizontal;
                            }
                        }
                        newSize.x = padding.horizontal + totalWidth + Math.Max(0, count - 1) * autoLayoutSpace;
                        break;
                    case AutoLayout.Vertical:
                        var maxWidth = 0f;
                        for (int i = 0; i < childCount; i += 1)
                        {
                            var child = m_ChildComponents[i];
                            if (child.isVisibleSelf)
                                maxWidth = Mathf.Max(maxWidth, child.width + GetItemPadding(child).horizontal);
                        }
                        newSize.x = padding.horizontal + maxWidth;
                        break;
                }
            }

            if (vertically)
            {
                switch (autoLayout)
                {
                    case AutoLayout.Disabled:
                        var offset = 0f;
                        for (int i = 0; i < childCount; i += 1)
                        {
                            var child = m_ChildComponents[i];
                            if (child.isVisibleSelf)
                                offset = Mathf.Max(offset, child.relativePosition.y + child.height);
                        }
                        newSize.x = Mathf.Max(offset, padding.top) + padding.bottom;
                        break;
                    case AutoLayout.Vertical:
                        var totalHeight = 0f;
                        var count = 0;
                        for (int i = 0; i < childCount; i += 1)
                        {
                            var child = m_ChildComponents[i];
                            if (child.isVisibleSelf)
                            {
                                count += 1;
                                totalHeight += child.height + GetItemPadding(child).vertical;
                            }
                        }
                        newSize.y = padding.vertical + totalHeight + Math.Max(0, count - 1) * autoLayoutSpace;
                        break;
                    case AutoLayout.Horizontal:
                        var maxHeight = 0f;
                        for (int i = 0; i < childCount; i += 1)
                        {
                            var child = m_ChildComponents[i];
                            if (child.isVisibleSelf)
                                maxHeight = Mathf.Max(maxHeight, child.height + GetItemPadding(child).vertical);
                        }
                        newSize.y = padding.vertical + maxHeight;
                        break;
                }
            }

            size = newSize;
        }

        #endregion

        #region HANDLERS

        protected override void OnComponentAdded(UIComponent child)
        {
            base.OnComponentAdded(child);

            if (child != null)
                AttachEvents(child);

            if (autoLayout != AutoLayout.Disabled && !IsLayoutSuspended)
                AutoArrange();
        }

        protected override void OnComponentRemoved(UIComponent child)
        {
            base.OnComponentRemoved(child);

            if (child != null)
                DetachEvents(child);

            if (autoLayout != AutoLayout.Disabled && !IsLayoutSuspended)
                AutoArrange();
        }

        private void AttachEvents(UIComponent child)
        {
            child.eventVisibilityChanged += ChildIsVisibleChanged;
            child.eventPositionChanged += ChildInvalidated;
            child.eventSizeChanged += ChildInvalidated;
            child.eventZOrderChanged += ChildZOrderChanged;
        }

        private void DetachEvents(UIComponent child)
        {
            child.eventVisibilityChanged -= ChildIsVisibleChanged;
            child.eventPositionChanged -= ChildInvalidated;
            child.eventSizeChanged -= ChildInvalidated;
            child.eventZOrderChanged -= ChildZOrderChanged;
        }

        private void ChildZOrderChanged(UIComponent child, int value) => Reset();
        private void ChildIsVisibleChanged(UIComponent child, bool value) => Reset();
        private void ChildInvalidated(UIComponent child, Vector2 value) => Reset();

        protected override void OnResolutionChanged(Vector2 previousResolution, Vector2 currentResolution)
        {
            base.OnResolutionChanged(previousResolution, currentResolution);
            resetNeeded = true;
        }

        #endregion

        #region RENDER

        protected UIRenderData BgRenderData { get; set; }
        protected UIRenderData FgRenderData { get; set; }


        protected override void OnRebuildRenderData()
        {
            if (BgRenderData == null)
            {
                BgRenderData = UIRenderData.Obtain();
                m_RenderData.Add(BgRenderData);
            }
            else
                BgRenderData.Clear();

            if (FgRenderData == null)
            {
                FgRenderData = UIRenderData.Obtain();
                m_RenderData.Add(FgRenderData);
            }
            else
                FgRenderData.Clear();

            if (AtlasBackground is UITextureAtlas bgAtlas && AtlasForeground is UITextureAtlas fgAtlas)
            {
                BgRenderData.material = bgAtlas.material;
                FgRenderData.material = fgAtlas.material;

                RenderBackground();
                RenderForeground();
            }
        }
        private void RenderBackground()
        {
            if (AtlasBackground[BackgroundSprite] is UITextureAtlas.SpriteInfo backgroundSprite)
            {
                var renderOptions = new RenderOptions()
                {
                    atlas = AtlasBackground,
                    color = isEnabled ? NormalBgColor : DisabledBgColor,
                    fillAmount = 1f,
                    flip = spriteFlip,
                    offset = pivot.TransformToUpperLeft(size, arbitraryPivotOffset),
                    pixelsToUnits = PixelsToUnits(),
                    size = size,
                    spriteInfo = backgroundSprite,
                };

                if (backgroundSprite.isSliced)
                    Render.RenderSlicedSprite(BgRenderData, renderOptions);
                else
                    Render.RenderSprite(BgRenderData, renderOptions);
            }
        }
        private void RenderForeground()
        {
            if (AtlasForeground[ForegroundSprite] is UITextureAtlas.SpriteInfo foregroundSprite)
            {
                var foregroundRenderSize = GetForegroundRenderSize(foregroundSprite);
                var foregroundRenderOffset = GetForegroundRenderOffset(foregroundRenderSize);

                var renderOptions = new RenderOptions()
                {
                    atlas = AtlasForeground,
                    color = isEnabled ? NormalFgColor : DisabledFgColor,
                    fillAmount = 1f,
                    flip = spriteFlip,
                    offset = foregroundRenderOffset,
                    pixelsToUnits = PixelsToUnits(),
                    size = foregroundRenderSize,
                    spriteInfo = foregroundSprite,
                };

                if (foregroundSprite.isSliced)
                    Render.RenderSlicedSprite(BgRenderData, renderOptions);
                else
                    Render.RenderSprite(BgRenderData, renderOptions);
            }
        }
        protected virtual Vector2 GetForegroundRenderSize(UITextureAtlas.SpriteInfo spriteInfo)
        {
            if (spriteInfo == null)
                return Vector2.zero;

            return new Vector2(width - SpritePadding.horizontal, height - SpritePadding.vertical);
        }
        protected virtual Vector2 GetForegroundRenderOffset(Vector2 renderSize)
        {
            Vector2 result = pivot.TransformToUpperLeft(size, arbitraryPivotOffset);

            result.x += SpritePadding.left;
            result.y -= SpritePadding.top;

            return result;
        }
        protected override Plane[] GetClippingPlanes()
        {
            if (clipChildren)
            {
                var corners = GetCorners();
                var right = transform.TransformDirection(Vector3.right);
                var left = transform.TransformDirection(Vector3.left);
                var up = transform.TransformDirection(Vector3.up);
                var down = transform.TransformDirection(Vector3.down);
                var ratio = PixelsToUnits();

                var padding = Padding;
                corners[0] += right * padding.left * ratio + down * padding.top * ratio;
                corners[1] += left * padding.right * ratio + down * padding.top * ratio;
                corners[2] += left * padding.right * ratio + up * padding.bottom * ratio;

                m_CachedClippingPlanes[0] = new Plane(right, corners[0]);
                m_CachedClippingPlanes[1] = new Plane(left, corners[1]);
                m_CachedClippingPlanes[2] = new Plane(up, corners[2]);
                m_CachedClippingPlanes[3] = new Plane(down, corners[0]);

                return m_CachedClippingPlanes;
            }
            else
                return null;
        }

        #endregion
    }
}
