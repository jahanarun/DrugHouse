namespace DrugHouse.Shared
{
    class UserEmpty:User
    {
        public override string Id
        {
            get { return string.Empty; }
            set { }
        }

        public override string FirstName
        {
            get { return string.Empty; }
            set { }
        }

        public override string FullName
        {
            get { return string.Empty; }
            set { }
        }

        public override string LastName
        {
            get { return string.Empty; }
            set { }
        }
    }
}
