/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Windows;
using System.Windows.Controls;

namespace DrugHouse.View.Controls
{
    /// <summary>
    /// Interaction logic for LoadingOverlay.xaml
    /// </summary>
    public partial class LoadingOverlay : UserControl
    {
        public LoadingOverlay()
        {
            InitializeComponent();
        }



        public string TextProperty
        {
            get { return (string)GetValue(TextPropertyProperty); }
            set { SetValue(TextPropertyProperty, value); }
        }

        public int ZIndex
        {
            get { return (int) GetValue(ZIndexProperty); }
            set { SetValue(ZIndexProperty, value); }
        }

        public static readonly DependencyProperty TextPropertyProperty =
            DependencyProperty.Register("TextProperty", typeof(string), typeof(LoadingOverlay), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ZIndexProperty =
            DependencyProperty.Register("ZIndex", typeof (int), typeof (LoadingOverlay), new PropertyMetadata(default(int)));
    }
}
