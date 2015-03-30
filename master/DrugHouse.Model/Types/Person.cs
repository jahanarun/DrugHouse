/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.ComponentModel.DataAnnotations;
using DrugHouse.Model.Enum;
using DrugHouse.Shared.Enumerations;

namespace DrugHouse.Model.Types
{
    public class Person : ModelBase
    {
        #region "Constructors"

        protected Person()
        {

        }

        protected Person(Person person)
        {
            Address = person.Address;
            Age = person.Age;
            Email = person.Email;
            Gender = person.Gender;
            GuardianName = person.GuardianName;
            GuardianRelationship = person.GuardianRelationship;
            Id = person.Id;
            Location = person.Location;
            Name = person.Name;
            PhoneNumber = person.PhoneNumber;
            Remark = person.Remark;
        }
        #endregion
                  
        public static Person Empty = new PersonEmpty();

        public struct PersonData
        {
            public string Name;
            public GenderType Gender;
            public int Age;
            public string Address;
            public string Location;
            public string Email;
            public string PhoneNumber;

        }

        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public GenderType Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public SimpleEntity Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GuardianName { get; set; }
        public RelationshipType GuardianRelationship { get; set; }
        public string Remark { get; set; }

        public PersonType Category;

        public override object Clone()
        {
            var result = new Person();
            result.Address = Address;
            result.Age = Age;
            result.Email = Email;
            result.Gender = Gender;
            result.GuardianName = GuardianName;
            result.GuardianRelationship = GuardianRelationship;
            result.Id = Id;
            result.Location = Location;
            result.Name = Name;
            result.PhoneNumber = PhoneNumber;
            result.Remark = Remark;

            return result;
        }
    }

    public class PersonEmpty : Person
    {  
        public PersonEmpty()
        {
            Id = (long)decimal.Zero;
            Name = string.Empty;
            Address = string.Empty;
            Age = 0;
            Email = string.Empty;
            Gender = GenderType.NotSpecified;
            Location = SimpleEntity.Empty;
            PhoneNumber = string.Empty;
            GuardianName = string.Empty;
            GuardianRelationship = RelationshipType.None;
            Remark = string.Empty;
        }      
    }
}
