/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

namespace DrugHouse.Model.Types
{
    public class SimpleEntity  : ModelBase,ISimpleEntity
    {
        public SimpleEntity(string marker)
        {
            Name = string.Empty;
            Id = 0;
            Marker = marker;
        }

        public static readonly SimpleEntity Empty = new SimpleEntityEmpty();

        protected SimpleEntity()
        {
            Marker = Markers.Default;
        }

        public class Markers
        {
            public const string Location = "Location";
            public const string Diagnosis = "Diagnosis";
            public const string Default = "Default";
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Marker { get; private set; }


        public override bool Equals(object obj)
        {
            if (!(obj is SimpleEntity))
                return false;

            return ((SimpleEntity)obj).Id == Id;
        }

        public override object Clone()
        {
            var result = new SimpleEntity { Id = Id, Name = Name, Marker = Marker};

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    internal class SimpleEntityEmpty : SimpleEntity
    {
        public SimpleEntityEmpty()
        {
            Id = 0;
            Name = "None";
        }
    }
}
