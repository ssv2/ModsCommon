﻿using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModsCommon.UI
{
    public abstract class VariationProperty<ItemType, SegmentedType> : EditorPropertyPanel, IReusable
        where SegmentedType : UIOnceSegmented<ItemType>
    {
        bool IReusable.InCache { get; set; }

        private SegmentedType Selector { get; set; }
        protected ItemType SelectedObject
        {
            get => Selector.SelectedObject;
            set => Selector.SelectedObject = value;
        }

        public VariationProperty()
        {
            Selector = Content.AddUIComponent<SegmentedType>();
            Selector.SetDefaultStyle();
            Selector.name = nameof(Selector);
        }

        public override void Init()
        {
            Selector.AutoButtonSize = false;
            Selector.ButtonWidth = 30f;
            Selector.SetDefaultStyle();
            Selector.StopLayout();
            AddSelectorItems();
            Selector.StartLayout();
            Selector.OnSelectObjectChanged += SelectorChanged;

            base.Init();
        }

        public override void DeInit()
        {
            base.DeInit();
            Selector.DeInit();
        }

        protected abstract void AddSelectorItems();
        protected void AddItem(ItemType item, string label, UITextureAtlas atlas, string sprite) => Selector.AddItem(item, label: label, iconAtlas: atlas, iconSprite: sprite);

        private void SelectorChanged(ItemType selctedItem)
        {
            Refresh();
            SelectorChangedImpl(selctedItem);
        }
        protected abstract void SelectorChangedImpl(ItemType selctedItem);

        protected void Refresh()
        {
            RefreshImpl();
            Content.Refresh();
        }
        protected abstract void RefreshImpl();
    }
}