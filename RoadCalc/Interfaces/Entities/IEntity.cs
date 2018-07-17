using System;

namespace RoadCalc.Interfaces.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime DateIncluded { get; set; }
        DateTime DateAltered { get; set; }
    }
}