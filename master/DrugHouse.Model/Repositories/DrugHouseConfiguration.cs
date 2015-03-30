/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DrugHouse.Model.Repositories
{
    class DrugHouseConfiguration : DbConfiguration
    {
        public DrugHouseConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient",
                () => new DefaultExecutionStrategy()
                );
        }
    }
}
