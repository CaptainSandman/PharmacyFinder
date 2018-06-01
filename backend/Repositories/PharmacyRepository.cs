using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TinyCsvParser;

using PharmacyBackend;
using PharmacyBackend.Models;

namespace PharmacyBackend.Repositories
{
    public class PharmacyRepository : IPharmacyRepository, IDisposable
    {
        private List<Pharmacy> pharmacies;

        public PharmacyRepository()
        {
            this.pharmacies = new List<Pharmacy>();

            CsvParser<Pharmacy> parser = new CsvParser<Pharmacy>(new CsvParserOptions(true, ','), new PharmacyMapping());

            // lmao, this may be cheating... :^)
            var data = parser.ReadFromFile(@"./Data/pharmacies.csv", Encoding.ASCII);

            int idIterator = 0;
            foreach (var pharmaData in data.ToList())
            {
                Pharmacy pharma = new Pharmacy();

                pharma = pharmaData.Result;
                pharma.ID = idIterator++;

                this.pharmacies.Add(pharma);
            }
        }

        public IEnumerable<Pharmacy> GetPharmacies()
        {
            return this.pharmacies;
        }

        public Pharmacy GetPharmacyByID(int ID)
        {
            return (from Pharmacy p in pharmacies where p.ID == ID select p).First();
        }

        public IEnumerable<KeyValuePair<Pharmacy, double>> FetchNearestPharmacies(double latitude, double longitude)
        {
            // Where key is the pharmacy id, and double is the distance
            Dictionary<Pharmacy, double> nearestPharmacies = new Dictionary<Pharmacy, double>();

            foreach (Pharmacy p in this.pharmacies)
            {
                nearestPharmacies.Add(p, Utility.kilometersToMiles(Utility.haversine(latitude, longitude, p.Latitude, p.Longitude)));
            }

            var tempList = nearestPharmacies.ToList();

            return from KeyValuePair<Pharmacy, double> data in tempList orderby data.Value ascending select data;
        }

        public KeyValuePair<Pharmacy, double> FetchNearestPharmacy(double latitude, double longitude)
        {
            return FetchNearestPharmacies(latitude, longitude).First();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
