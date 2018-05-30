using System;
using System.Collections.Generic;

using PharmacyBackend.Models;

namespace PharmacyBackend.Repositories
{
    public interface IPharmacyRepository : IDisposable
    {
        IEnumerable<Pharmacy> GetPharmacies();

        Pharmacy GetPharmacyByID(int id);
    }
}
