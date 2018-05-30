using System;

using TinyCsvParser.Mapping;

namespace PharmacyBackend.Models
{
    public class PharmacyMapping : CsvMapping<Pharmacy>
    {
        public PharmacyMapping() : base()
        {
            MapProperty(0, x => x.Name);
            MapProperty(1, x => x.Address);
            MapProperty(2, x => x.City);
            MapProperty(3, x => x.State);
            MapProperty(4, x => x.Zip);
            MapProperty(5, x => x.Latitude);
            MapProperty(6, x => x.Longitude);
        }
    }
}
