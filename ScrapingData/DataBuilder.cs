using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    public class DataBuilder : IDataBuilder
    {
        private readonly List<ModelName> modelNames;
        private readonly List<Model> modelData;
        private readonly List<Equipment> equipmentData;
        private readonly List<GroupOfParts> groupOfPartsData;
        private readonly List<SubGroup> subGroupData;
        private readonly List<Part> partData;
        private readonly List<SubPart> subPartData;

        public DataBuilder()
        {
            modelNames = new List<ModelName>();
            modelData = new List<Model>();
            equipmentData = new List<Equipment>();
            groupOfPartsData = new List<GroupOfParts>();
            subGroupData = new List<SubGroup>();
            partData = new List<Part>();
            subPartData = new List<SubPart>();
        }
        public Data Build()
        {
            return new Data
            {
                ModelNames = modelNames,
                ModelData = modelData,
                EquipmentData = equipmentData,
                GroupOfPartsData = groupOfPartsData,
                SubGroupsData = subGroupData,
                PartsData = partData,
                SubPartsData = subPartData
            };
        }

        public void AddModelData(List<ModelName> models)
        {
            modelNames.AddRange(models);
            foreach (var innerModel in models)
            {
                modelData.AddRange(innerModel.Models);
            }
        }

        public void AddEquipmentData(List<Equipment> equipments)
        {
            equipmentData.AddRange(equipments);
        }

        public void AddGroupOfPartsData(List<GroupOfParts> groupOfParts)
        {
            groupOfPartsData.AddRange(groupOfParts);
        }

        public void AddSubGroupData(List<SubGroup> subGroups)
        {
            subGroupData.AddRange(subGroups);
        }

        public void AddPartData(List<Part> parts)
        {
            partData.AddRange(parts);
            foreach(var part in parts)
            {
                subPartData.AddRange(part.SubParts);
            }
        }
    }
}
