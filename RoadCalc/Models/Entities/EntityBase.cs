using System;
using RoadCalc.Interfaces.Entities;

namespace RoadCalc.Models.Entities
{
    public class EntityBase : IEntity
    {
        public int Id { get; set; }
        public DateTime DateIncluded { get; set; }
        public DateTime DateAltered { get; set; }
    }
}