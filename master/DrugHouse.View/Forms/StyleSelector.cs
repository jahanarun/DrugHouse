/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Windows;
using System.Windows.Controls;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.View.Forms
{
    class TabViewStyleSelector : StyleSelector
    {
        public Style PatientStyle { get; set; }
        public Style PatientListStyle { get; set; }
        public Style AdminScreentStyle { get; set; }
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item != null)
            {
                if (typeof(PatientViewModel) == item.GetType())
                {
                    Application.Current.Resources["DynamicBg"] = Application.Current.Resources["PatientTabBrush"];
                    return PatientStyle;
                }
                if (typeof(PatientMasterViewModel) == item.GetType())
                {
                    Application.Current.Resources["DynamicBg"] = Application.Current.Resources["PatientListTabBrush"];
                    return PatientListStyle;
                }
                if (typeof(AdminScreenMasterViewModel) == item.GetType())
                {
                    Application.Current.Resources["DynamicBg"] = Application.Current.Resources["AdminTabBrush"];
                    return AdminScreentStyle;
                }
            }
            return base.SelectStyle(item, container);
        }
    }

}
