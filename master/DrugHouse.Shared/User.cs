using System;

namespace DrugHouse.Shared
{
    public class User:IComparable
    {
        public User()
        {
            
        }

        public virtual string Id
        {
            get { return null; }
            set { }
        }

        public virtual  string LastName
        {
            get { return null; }
            set {  }
        }

        public virtual string FirstName
        {
            get { return null; }
            set { }
        }

        public virtual string FullName
        {
            get { return null; }
            set { }
        }

        public string Email;

        public string Role;


        public static User Empty = new UserEmpty();
        public int CompareTo(object obj)
        {
            if (obj ==null) throw new ArgumentException("obj");
            if (obj.GetType() != typeof (User)) throw new InvalidOperationException(string.Format("Cannot compare User with {0}",obj.GetType().Name));

            return String.CompareOrdinal(FullName, ((User) obj).FullName);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof (User)) return false;

            return Id == ((User) obj).Id;
        }

        public override string ToString()
        {
            return FullName;
        }
    }

}
