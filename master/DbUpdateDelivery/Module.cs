namespace DbUpdateDelivery
{
    public class Module
    {

        public const string ConnectionString =
            @"Data Source=DEV-PC\DEXLAB_SQLSERVER; Initial Catalog=DrugHouse; User Id=dev; Password=dev1234;";

        public const string SqlText = @"
insert into Drug values ('Tester', 2,'','Drug')
";
    }
}
