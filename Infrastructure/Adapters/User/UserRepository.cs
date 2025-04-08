
using Domain.Base.ResponseEntity;
using Domain.Entity;
using Domain.Entity.Facultad;
using Domain.Entity.Program;
using Domain.Port.User;
using Infrastructure.Context.MongoDB;
using MongoDB.Driver;

namespace Infrastructure.Adapters.User
{
    public class UserRepository : IUserRepository
    {

        private readonly IMongoCollection<UserEntity> _collection;
        private readonly IMongoCollection<ProgramEntity> _collectionProgram;
        private readonly IMongoCollection<FacultyEntity> _collectionFaculty;

        public UserRepository(MongoDBContext context)
        {
            _collection = context._dbs.GetCollection<UserEntity>("User");
            _collectionProgram = context._dbs.GetCollection<ProgramEntity>("Program");
            _collectionFaculty = context._dbs.GetCollection<FacultyEntity>("Faculty");
        }

        public async Task<UserEntity> Add(UserEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            entity.Program = await _collectionProgram
            .Find(p => p.Id == entity.IdProgram)
            .FirstOrDefaultAsync();

            if (entity.Program != null)
            {
                entity.Program.Faculty = await _collectionFaculty
                          .Find(f => f.Id == entity.Program.IdFaculty)
                          .FirstOrDefaultAsync();
            }


            return entity;
        }

        public async Task<bool> ExistById(string id)
        {
            return await _collection.Find(c => c.Id == id).AnyAsync();
        }

        public async Task<bool> ExistByMail(string mail)
        {
            return await _collection.Find(u => u.Mail == mail).AnyAsync();
        }

        public async Task<ResponseEntity<UserEntity>> GetAll(int page, int pageSize)
        {
            var totalRecords = await _collection.CountDocumentsAsync(_ => true);
            var users = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            foreach (var user in users)
            {
                user.Program = await _collectionProgram
                    .Find(p => p.Id == user.IdProgram)
                    .FirstOrDefaultAsync();

                if (user.Program != null)
                {
                    user.Program.Faculty = await _collectionFaculty
                        .Find(f => f.Id == user.Program.IdFaculty)
                        .FirstOrDefaultAsync();
                }
            }

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            return new ResponseEntity<UserEntity>("Usuarios obtenidos", users)
            {
                totalRecords = (int)totalRecords,
                totalPages = totalPages
            };
        }


        public async Task<UserEntity?> GetById(string id)
        {
            var user = await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Program = await _collectionProgram
                    .Find(p => p.Id == user.IdProgram)
                    .FirstOrDefaultAsync();

                if (user.Program != null)
                {
                    user.Program.Faculty = await _collectionFaculty
                        .Find(f => f.Id == user.Program.IdFaculty)
                        .FirstOrDefaultAsync();
                }
            }
            return user;
        }

        public async Task<UserEntity?> GetByMail(string mail)
        {
            var user = await _collection.Find(u => u.Mail == mail).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Program = await _collectionProgram
                    .Find(p => p.Id == user.IdProgram)
                    .FirstOrDefaultAsync();

                if (user.Program != null)
                {
                    user.Program.Faculty = await _collectionFaculty
                        .Find(f => f.Id == user.Program.IdFaculty)
                        .FirstOrDefaultAsync();
                }
            }
            return user;
        }


        public async Task<UserEntity?> IsUserType(string userId, string type)
        {

            var user = await _collection.Find(u => u.Id == userId).FirstOrDefaultAsync();

            if (user.TypeUser == type && user != null)
            {
                return user;
            }


            return null;
        }

        public async Task<bool> Update(UserEntity entity)
        {
            var updateDefinition = Builders<UserEntity>.Update
                .Set(u => u.FirstName, entity.FirstName)
                .Set(u => u.LastName, entity.LastName)
                .Set(u => u.Identification, entity.Identification)
                .Set(u => u.TypeIdentification, entity.TypeIdentification)
                .Set(u => u.Gender, entity.Gender)
                .Set(u => u.Image, entity.Image)
                .Set(u => u.IdProgram, entity.IdProgram);

            var result = await _collection.UpdateOneAsync(u => u.Id == entity.Id, updateDefinition);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> UpdatePassword(string userId, string password)
        {
            var update = Builders<UserEntity>.Update.Set(u => u.Password, password);
            var result = await _collection.UpdateOneAsync(u => u.Id == userId, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> UpdateMail(string userId, string mail)
        {
            var update = Builders<UserEntity>.Update.Set(u => u.Mail, mail);
            var result = await _collection.UpdateOneAsync(u => u.Id == userId, update);
            return result.ModifiedCount > 0;
        }


    }
}