using System;
using System.Collections.Generic;

using PharmacyBackend.Models;

namespace PharmacyBackend.Repositories
{
    public interface IPharmacyRepository : IDisposable
    {
        IEnumerable<Pharmacy> GetPharmacies();

        Pharmacy GetPharmacyByID(int id);

        IEnumerable<KeyValuePair<Pharmacy, double>> FetchNearestPharmacies(double latitude, double longitude);

        KeyValuePair<Pharmacy, double> FetchNearestPharmacy(double latitude, double longitude);
    }
}
