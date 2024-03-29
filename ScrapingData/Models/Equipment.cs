﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentCode { get; set; }
        public string Url { get; set; }
        public string? Date { get; set; }
        public string? Engine { get; set; }
        public string? Body { get; set; }
        public string? Grade { get; set; }
        public string? Atm { get; set; }
        public string? GearShiftType { get; set; }
        public string? Cab { get; set; }
        public string? TransmissionModel { get; set; }
        public string? LoadingCapacity { get; set; }
        public string? RearTire { get; set; }
        public string? Destination { get; set; }
        public string? FuelInduction { get; set; }
        public string? BuildingCondition { get; set; }
        public ICollection<GroupOfPart>? GroupOfParts { get; set; }

        public int ModelId { get; set; }
        public SubModel? Model { get; set; }

    }
}
