using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DrugHouse.View.Annotations;

namespace DrugHouse.View.Controls
{
    /// <summary>
    /// Interaction logic for DictionaryTextbox.xaml
    /// </summary>
    public partial class DictionaryTextbox : UserControl, INotifyPropertyChanged
    {
        private bool JustReplacedText;
        #region Constructor
        public DictionaryTextbox()
        {
            MatchedItems = new ObservableCollection<string>();
            InitializeComponent();
            ItemSourceEvent += (o, e) => InitializeList();
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text"
                                , typeof(string)
                                , typeof(DictionaryTextbox)
                                , new PropertyMetadata(default(string)));


        public static readonly DependencyProperty DictionaryItemsProperty =
            DependencyProperty.Register("DictionaryItems"
            , typeof(List<string>)
            , typeof(DictionaryTextbox)
            , new PropertyMetadata(OnDictionaryItemsChanged));

        static event PropertyChangedEventHandler ItemSourceEvent;

        #endregion

        #region Properties

        public List<string> DictionaryItems
        {
            get { return (List<string>)GetValue(DictionaryItemsProperty); }
            set { SetValue(DictionaryItemsProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> MatchedItemsValue;
        public ObservableCollection<string> MatchedItems
        {
            get { return MatchedItemsValue; }
            private set
            {
                MatchedItemsValue = value;
                OnPropertyChanged();
            }
        }

        private string SelectedItemValue;
        public string SelectedItem
        {
            get { return SelectedItemValue; }
            set
            {
                SelectedItemValue = value;
                OnPropertyChanged();
            }

        }
        #endregion

        #region Private Members
        private void InitializeList()
        {
            if (DictionaryItems == null)
                return;
            MatchedItems = new ObservableCollection<string>(DictionaryItems);
        }

        private bool TryInitializeDictionaryList(string word)
        {
            List<string> items;

            if (TryFindingWordMatch(word, out items))
            {
                MatchedItems.Clear();
                foreach (var item in items)
                {
                    MatchedItems.Add(item);
                }
                return true;


            }
            return false;
        }

        private void OpenPopup()
        {
            var listBoxItem = (ListBoxItem)ItemsListBox
                                .ItemContainerGenerator
                                .ContainerFromItem(ItemsListBox.Items[0]);

            var rowSize = listBoxItem == null ? 20 : listBoxItem.RenderSize.Height;

            //rowSize = 28;
            ListPopup.Height = (MatchedItems.Count + 1) * rowSize;

            var offsetHeight = ((ListPopup.Height < ListPopup.MaxHeight) ? ListPopup.Height : ListPopup.MaxHeight)
                                  + 5;

            var rect = MainTextBox.GetRectFromCharacterIndex(MainTextBox.CaretIndex);
            rect.Location = new Point(rect.Left, rect.Top + offsetHeight);
            ListPopup.PlacementRectangle = rect;
            ListPopup.IsOpen = true;
            ItemsListBox.SelectedItem = ItemsListBox.Items[0];


        }
        private void ClosePopupMenu()
        {
            ListPopup.IsOpen = false;
            MainTextBox.Focus();
        }

        private bool TryFindingWordMatch(string word, out List<string> result)
        {
            var matching = DictionaryItems.Where(p => p.ToUpper().StartsWith(word.ToUpper())).OrderBy(s => s);
            if (matching.Any())
            {
                result = matching.ToList();
                return true;
            }
            result = new List<string>();
            return false;
        }

        private string GetCurrentWord()
        {
            int startPosition, endPosition;
            GetWordPositionBasedOnCaret(out startPosition, out endPosition);

            return MainTextBox.Text.Substring(startPosition, (endPosition - startPosition));
        }

        private void GetWordPositionBasedOnCaret(out int startPosition, out int endPosition)
        {
            var position = MainTextBox.CaretIndex;
            if (position > 0)
                while (position < MainTextBox.Text.Length)
                {
                    var ctr = MainTextBox.Text[position - 1];
                    if (ctr == '\r' || ctr == ' ')
                    {
                        position--;
                        break;
                    }

                    position++;
                }
            endPosition = position;

            position = MainTextBox.CaretIndex;
            while (position > 0)
            {
                var ctr = MainTextBox.Text[position - 1];
                if (ctr == '\r' || ctr == ' ')
                    break;

                position--;
            }
            startPosition = position;

            if (endPosition < startPosition)
                endPosition = startPosition;

        }
        private void ReplaceText()
        {
            JustReplacedText = true;
            int startPosition, endPosition;
            GetWordPositionBasedOnCaret(out startPosition, out endPosition);

            MainTextBox.SelectionStart = startPosition;
            MainTextBox.SelectionLength = (endPosition - startPosition);


            MainTextBox.SelectedText = (string)ItemsListBox.SelectedItem;
            MainTextBox.CaretIndex = MainTextBox.CaretIndex + ((string)ItemsListBox.SelectedItem).Length;


        }

        private void MainTextBox_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (JustReplacedText)
            {
                JustReplacedText = false;
                return;
            }
            if (e.Key == Key.Escape)
                ClosePopupMenu();
            if (IsNavigationalKey(e.Key))
                return;
            var currentWord = GetCurrentWord().Trim();
            if (currentWord.Length <= 0)
            {
                ClosePopupMenu();
                return;
            }
            if (TryInitializeDictionaryList(currentWord))
                OpenPopup();
            else
                ClosePopupMenu();
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

        private void ReplaceTextWithOneSpace()
        {
            ReplaceText();
            MainTextBox.SelectedText = " ";
            MainTextBox.CaretIndex = MainTextBox.CaretIndex + 1;
            ClosePopupMenu();
        }

        private bool TrySelectionKeys(Key key)
        {
            switch (key)
            {
                case Key.Enter:
                    ReplaceText();
                    ClosePopupMenu();
                    return true;

                case Key.Space:
                    ReplaceTextWithOneSpace();
                    return true;

                case Key.Tab:
                    ReplaceText();
                    ClosePopupMenu();
                    return true;
            }
            return false;
        }

        #endregion

        #region Event Handlers
        private void MainTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (ListPopup.IsOpen)
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

        private static void OnDictionaryItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var handler = ItemSourceEvent;
            if (handler != null) handler(null, new PropertyChangedEventArgs("ItemSourceEvent"));
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

        private void ItemsListBox_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ReplaceText();
            ClosePopupMenu();
        }

        private void DictionaryTextbox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            MainTextBox.Focus();
        }
    }
}
