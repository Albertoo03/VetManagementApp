using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VetManagementApp.Model.DbContexts;
using static VetManagementApp.Interfaces.Interfaces;

namespace VetManagementApp.Model
{
    public class AnimalBasicInfoRepository : CommonRepository<AnimalBasicInfo>, IAnimalBasicInfoRepository
    {
        private readonly VetManagementAppDbContext _vetDbContext;

        public AnimalBasicInfoRepository(VetManagementAppDbContext dbContext) : base(dbContext)
        {
            this._vetDbContext = dbContext;
        }


        public override ICollection<AnimalBasicInfo> All
        {
            get
            {
                _vetDbContext.AnimalsBasicInfos.Include(p => p.AvailableMedicines).Include(p => p.AssignedAnimals).Load();

                var queryResult = _vetDbContext.AnimalsBasicInfos.Local;

                return queryResult;
            }
        }

        public void Delete(string species)
        {
            _vetDbContext.AnimalsBasicInfos.Remove(_vetDbContext.AnimalsBasicInfos.Where(animal => animal.Species == species).FirstOrDefault());
        }

        public AnimalBasicInfo GetBySpecies(string species)
        {
            var queryResult = _vetDbContext.AnimalsBasicInfos.Where(animal => animal.Species.Equals(species)).FirstOrDefault();

            return queryResult;
        }

    }
}
