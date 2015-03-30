/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Data.Entity.Infrastructure;

namespace DrugHouse.Model.Repositories
{
    class DrugHouseContextFactory : IDbContextFactory<DrugHouseContext>
    {
        public DrugHouseContext Create()
        {
            return new DrugHouseContext(Decoder.Decrypt(DataAccess.GetApplicationConnectionStringEncrypted()));
        }

        public static DrugHouseContextFactory Instance = new DrugHouseContextFactory();
    
    }
}
