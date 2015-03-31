using DrugHouse.Model.Types;

namespace DrugHouse.ViewModel.RowItems
{
    public class ToggleButtonItem 
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
    }
}
