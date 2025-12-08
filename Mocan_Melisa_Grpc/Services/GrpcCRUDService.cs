using Grpc.Core;
using DataAccess = Mocan_Melisa_MariaMVC.Data;
using ModelAccess = Mocan_Melisa_MariaMVC.Models;

namespace Mocan_Melisa_Grpc.Services
{
    public class GrpcCRUDService : BreedService.BreedServiceBase
    {
        private DataAccess.PetsAdoptionContext db = null;

        public GrpcCRUDService(DataAccess.PetsAdoptionContext db)
        {
            this.db = db;
        }

        public override Task<BreedList> GetAll(Empty empty, ServerCallContext context)
        {
            BreedList list = new BreedList();

            var query =
                from b in db.Breed
                select new Breed()
                {
                    Id = b.Id,
                    BreedName = b.BreedName,
                    AnimalType = b.AnimalType
                };

            list.Items.AddRange(query.ToArray());
            return Task.FromResult(list);
        }

        public override Task<Empty> Insert(Breed requestData, ServerCallContext context)
        {
            db.Breed.Add(new ModelAccess.Breed
            {
                //Id = requestData.Id,
                BreedName = requestData.BreedName,
                AnimalType = requestData.AnimalType
            });

            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Breed> Get(BreedId requestData, ServerCallContext context)
        {
            var data = db.Breed.Find(requestData.Id);

            Breed br = new Breed()
            {
                Id = data.Id,
                BreedName = data.BreedName,
                AnimalType = data.AnimalType
            };

            return Task.FromResult(br);
        }

        public override Task<Empty> Delete(BreedId requestData, ServerCallContext context)
        {
            var data = db.Breed.Find(requestData.Id);
            db.Breed.Remove(data);

            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Breed> Update(Breed requestData, ServerCallContext context)
        {
            var data = db.Breed.Find(requestData.Id);

            if (data == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Breed not found"));
            }
            else
            {
                data.BreedName = requestData.BreedName;
                data.AnimalType = requestData.AnimalType;

                db.SaveChanges();

                var updatedData = new Breed()
                {
                    Id = data.Id,
                    BreedName = data.BreedName,
                    AnimalType = data.AnimalType
                };

                return Task.FromResult(updatedData);
            }
        }
    }
}
