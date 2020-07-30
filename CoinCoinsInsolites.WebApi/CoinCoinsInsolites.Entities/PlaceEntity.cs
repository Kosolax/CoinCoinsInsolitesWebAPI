// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlaceEntity.cs.cs" company="GameTurretCompany">
//   Copyright © GameTurretCompany. 2020
using System.ComponentModel.DataAnnotations;

namespace CoinCoinsInsolites.Entities
{
    public class PlaceEntity : BaseEntity
    {
        [Encrypted]
        public string Description { get; set; }

        public int Id { get; set; }

        [Encrypted]
        public double Latitude { get; set; }

        [Encrypted]
        public double Longitude { get; set; }

        [Encrypted]
        public string Title { get; set; }
    }
}