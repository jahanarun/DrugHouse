﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DrugHouse.View.Annotations;
using DrugHouse.ViewModel.RowItems;

namespace DrugHouse.View.Controls
{
    /// <summary>
    /// Interaction logic for DictionaryTextbox.xaml
    /// </summary>
    public partial class ArrowedDropDown : UserControl, INotifyPropertyChanged
    {
        #region Constructor
        public ArrowedDropDown()
        {
            InitializeComponent();
            ItemSourceEvent += (o, e) => InitializeList();
        }
        #endregion

        #region Static / Dependency Properties
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem"
                                , typeof(ToggleButtonItem)
                                , typeof(ArrowedDropDown)
                                , new PropertyMetadata(OnSelectedItemPropertyChanged));

        private static void OnSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var handler = SelectedItemEvent;
            if (handler != null) handler(null, new PropertyChangedEventArgs("ItemSourceEvent"));
        }


        private static void OnDictionaryItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var handler = ItemSourceEvent;
            if (handler != null) handler(null, new PropertyChangedEventArgs("ItemSourceEvent"));
        }


        public static readonly DependencyProperty DictionaryItemsProperty =
            DependencyProperty.Register("DictionaryItems"
            , typeof(List<ToggleButtonItem>)
            , typeof(ArrowedDropDown)
            , new PropertyMetadata(OnDictionaryItemsChanged));

        static event PropertyChangedEventHandler ItemSourceEvent;
        static event PropertyChangedEventHandler SelectedItemEvent;

        #endregion

        #region Properties

        public List<ToggleButtonItem> DictionaryItems
        {
            get { return (List<ToggleButtonItem>)GetValue(DictionaryItemsProperty); }
            set { SetValue(DictionaryItemsProperty, value); }
        }

        public ToggleButtonItem SelectedItem
        {
            get { return (ToggleButtonItem)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ToggleButtonItem> MatchedItemsValue;
        private string TextValue;

        public ObservableCollection<ToggleButtonItem> MatchedItems
        {
            get { return MatchedItemsValue; }
            private set
            {
                MatchedItemsValue = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return TextValue; }
            set
            {
                TextValue = value;
                Filter();
            }
        }

        #endregion

        #region Private Members
        private void InitializeList()
        {
            if (DictionaryItems == null)
                return;
            MatchedItems = new ObservableCollection<ToggleButtonItem>(DictionaryItems);
        }

        private void RefreshDictionaryList(string word)
        {
            if (word == null)
                return;
            List<ToggleButtonItem> items;

            GetMatchedItems(word, out items);
            MatchedItems.Clear();
            foreach (var item in items)
            {
                MatchedItems.Add(item);
            }
        }

        private void OpenPopup()
        {
            ListPopup.IsOpen = true;
            MainTextBox.Text = "";
        }
        private void ClosePopupMenu()
        {
            ListPopup.IsOpen = false;
        }

        private void GetMatchedItems(string word, out List<ToggleButtonItem> result)
        {
            Func<ToggleButtonItem, bool> condition;
            if (word.Length < 1)
                condition = (p) => true;
            else
                condition = (item) => item.ToString().ToUpper().Contains(word.ToUpper());

            result = DictionaryItems.Where(condition).OrderBy(s => s).ToList();
        }

        private void Filter()
        {
            RefreshDictionaryList(Text);
        }

        private static bool IsNavigationalKey(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    return true;
                case Key.Down:
                    return true;
                case Key.Left:
                    return true;
                case Key.Right:
                    return true;
                case Key.PageDown:
                    return true;
                case Key.PageUp:
                    return true;
                case Key.LeftShift:
                    return true;
                case Key.RightShift:
                    return true;
                case Key.Escape:
                    return true;
                case Key.Home:
                    return true;
                case Key.End:
                    return true;
            }
            return false;
        }

        private bool TrySelectionKeys(Key key)
        {
            switch (key)
            {
                case Key.Enter:
                    ClosePopupMenu();
                    return true;

                case Key.Space:
                    ClosePopupMenu();
                    return true;

                case Key.Tab:
                    ClosePopupMenu();
                    return true;
            }
            return false;
        }

        #endregion

        #region Event Handlers

        private void MainTextBox_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (IsNavigationalKey(e.Key))
                return;
            Filter();

        }
        private void MainTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {

            ListBoxItem listBoxItem;
            switch (e.Key)
            {
                case Key.Down:
                    listBoxItem = (ListBoxItem)ItemsListBox
                        .ItemContainerGenerator
                        .ContainerFromItem(ItemsListBox.Items[0]);

                    if (listBoxItem != null)
                        listBoxItem.Focus();
                    break;

                case Key.Up:
                    var count = ItemsListBox.Items.Count;
                    listBoxItem = (ListBoxItem)ItemsListBox
                        .ItemContainerGenerator
                        .ContainerFromItem(ItemsListBox.Items[count - 1]);

                    if (listBoxItem != null)
                        listBoxItem.Focus();
                    break;

                case Key.Escape:
                    ClosePopupMenu();
                    return;

            }
            if (TrySelectionKeys(e.Key))
                e.Handled = true;

        }
        private void ItemsListBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ClosePopupMenu();
                return;
            }
            if (TrySelectionKeys(e.Key))
            {
                e.Handled = true;
                return;
            }
            if (!IsNavigationalKey(e.Key))
                MainTextBox.Focus();

        }


        private void ItemsListBox_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ClosePopupMenu();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {

            OpenPopup();
        }

        private void ListPopup_OnOpened(object sender, EventArgs e)
        {
            MainTextBox.Focus();
        }

        private void ArrowedTextbox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            OpenPopup();
        }



        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


    }
}