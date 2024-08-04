using ParkAPI_12.Data;
using ParkAPI_12.Models;
using ParkAPI_12.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_12.Repository
{
    public class NationalParkRepository:INationalParkRepository
    {
        private readonly ApplicationDbContext _context;
        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _context.NationalParks.Find(nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _context.NationalParks.ToList();
        }

        public bool NationalParkExists(int nationalParkId)
        {
            return _context.NationalParks.Any(np => np.Id == nationalParkId);
        }

        public bool NationalParkExists(string nationalParkName)
        {
            return _context.NationalParks.Any(np => np.Name == nationalParkName);
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Update(nationalPark);
            return Save();

        }
    }
}
