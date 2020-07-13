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
                _vetDbContext.AnimalsBasicInfos.Include(p => p.AvailableMedicines).Load();

                var queryResult = _vetDbContext.AnimalsBasicInfos.Local;

                return queryResult;
            }
        }

        public void Delete(string species)
        {
            _vetDbContext.AnimalsBasicInfos.Remove(_vetDbContext.AnimalsBasicInfos.Where(animal => animal.Species == species).FirstOrDefault());
        }

        //public override void Add(AnimalBasicInfo entity)
        //{
        //    //_vetDbContext.Entry(entity.AvailableMedicines).State = EntityState.Unchanged;
        //    foreach(var medicine in entity.AvailableMedicines)
        //    {
        //        _vetDbContext.Entry(medicine).State = EntityState.Unchanged;
        //    }
        //    base.Add(entity);
        //}

    }
}
