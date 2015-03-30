using DrugHouse.Model.Types;

namespace DrugHouse.Model.PatientDetails
{
    public class PatientStatus : ModelBase
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public override object Clone()
        {
            var result = new PatientStatus() {Id = Id, Status = Status};
            return result;
        }
    }
}
