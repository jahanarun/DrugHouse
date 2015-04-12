using System;
using DrugHouse.Model.Types;

namespace DrugHouse.ViewModel.RowItems
{
    public class ToggleButtonItem : IComparable
    {
        public ToggleButtonItem(ModelBase obj)
        {
            Model = obj;

        }
        public readonly ModelBase Model;
        public override string ToString()
        {
            return Model.ToString();
        }
        public int CompareTo(object other)
        {
            var item = (ToggleButtonItem)other;
            return String.CompareOrdinal(Model.ToString(), item.Model.ToString());
        }
    }
}
