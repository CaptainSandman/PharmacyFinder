using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PharmacyBackend.Models;
using PharmacyBackend.Repositories;

namespace PharmacyBackend.Controllers
{
    [Route("api/[controller]")]
    public class PharmaciesController : Controller
    {
        private IPharmacyRepository myPharmaRepo;
        
        public PharmaciesController(IPharmacyRepository repository)
        {
            this.myPharmaRepo = repository;
        }

        // GET api/pharmacies
        [HttpGet]
        public IEnumerable<Pharmacy> Get()
        {
            return this.myPharmaRepo.GetPharmacies();
        }

        // GET api/pharmacies/69
        [HttpGet("{id}")]
        public Pharmacy Get(int id)
        {
            return this.myPharmaRepo.GetPharmacyByID(id);
        }

        [HttpGet("/fetchNearest/{lat}/{long}")]
        public Pharmacy FetchNearest(double latitude, double longitude)
        {
            return null;
        }
    }
}
