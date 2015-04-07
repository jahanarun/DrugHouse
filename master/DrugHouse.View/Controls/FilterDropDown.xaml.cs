using System;
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
    /// Interaction logic for FilterDropDown.xaml
    /// </summary>
    public partial class FilterDropDown : UserControl, INotifyPropertyChanged 
    {

        public FilterDropDown()
        {
            InitializeComponent();
            FilterText = string.Empty;

            //SelectedItemEvent += (sender, args) => ClosePopupMenu();
            ItemSourceEvent += (sender, args) => FilterList();
        }

        #region Properties

        private string FilterTextValue;
        public string FilterText
        {
            get { return FilterTextValue; }
            set
            {
                FilterTextValue = value;
                FilterList();
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ToggleButtonItem> ItemsSource { get; set; }

        public ToggleButtonItem SelectedItem
        {
            get { return (ToggleButtonItem) GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        #region Private Members

        private void FilterList()
        {
            var list = (List<ToggleButtonItem>) GetValue(ItemsSourceProperty);

            if (list == null)
                return;

            Func<ToggleButtonItem, bool> condition = item => true;

            if (FilterText != string.Empty)
                condition = item => item.ToString().ToUpper().Contains(FilterText.ToUpper());


            var myItemsSource = new ObservableCollection<ToggleButtonItem>(list.Where(condition));
            ItemsListBox.ItemsSource = myItemsSource;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            OpenPopupMenu();
        }
        private void OpenPopupMenu()
        {
            if (ListPopup.IsOpen) return;
            FilterText = string.Empty;
            ListPopup.IsOpen = true;
        }
        private void ClosePopupMenu()
        {
            ListPopup.IsOpen = false;
        }
        private static void TriggerMyEvent(PropertyChangedEventHandler e, PropertyChangedEventArgs args)
        {
            var handler = e;
            if (handler != null) handler(null, args);
        }

        #endregion 

        #region Event Handlers

        public static event PropertyChangedEventHandler SelectedItemEvent;
        public static event PropertyChangedEventHandler ItemSourceEvent;

        private static void OnSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TriggerMyEvent(SelectedItemEvent, new PropertyChangedEventArgs("SelectedItem"));
        }

        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TriggerMyEvent(ItemSourceEvent, new PropertyChangedEventArgs("ItemSource"));
        }


        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource"
                                    , typeof(List<ToggleButtonItem>)
                                    , typeof(FilterDropDown)
                                    , new PropertyMetadata(OnItemSourceChanged));


        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem"
                                        , typeof(ToggleButtonItem)
                                        , typeof(FilterDropDown)
                                        , new PropertyMetadata(OnSelectedItemPropertyChanged));

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FilterDropUserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            OpenPopupMenu();
        }

        private void FilterTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                if (ListPopup.IsOpen)
                {
                    //var lbi = ItemsListBox.ItemContainerGenerator.ContainerFromIndex(ItemsListBox.SelectedIndex) as ListBoxItem;
                    //if (lbi != null)  lbi.Focus();

                    ItemsListBox.Focus();
                    
                }
        }

        private void ListPopup_Opened(object sender, EventArgs e)
        {
            FilterTextBox.Focus();
        }

        private void ItemsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                ClosePopupMenu();
        }

        private void ItemsListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //ClosePopupMenu();
        }

        private void FilterDropUserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
                ClosePopupMenu();
        }
    }

}
