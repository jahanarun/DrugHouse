/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

namespace DrugHouse.Model.Types
{
    public interface ISimpleEntity: IEntity
    {
        long Id { get; set; } 
        string Name { get; set; } 
        string Marker { get; }  
    }
}