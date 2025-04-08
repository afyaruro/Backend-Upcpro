using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base.PasswordEncryption;
using Domain.Entity;
using Domain.Port.User;

namespace Infrastructure.Extensions.Start
{
    public class StartMongoDB
    {
        private readonly IUserRepository _repository;
        public StartMongoDB(IUserRepository repository) => _repository = repository;
        public async Task CreateAdminStart()
        {
            UserEntity userAdmin = new UserEntity();
            userAdmin.DateUpdate = DateTime.Now;
            userAdmin.Mail = "admin@admin.com";
            userAdmin.TypeUser = "admin";
            var helper = new PasswordEncryptionHelper();
            userAdmin.Mail = userAdmin.Mail.ToUpper();
            userAdmin.Password = helper.HashPassword("Admin2025*", userAdmin.Mail);
            if (!await _repository.ExistByMail(userAdmin.Mail))
            {
                var resp = await _repository.Add(userAdmin);
                if (resp != null)
                {
                    Console.WriteLine("Usuario Creado");
                }
                else
                {
                    Console.WriteLine("Error al crear");
                }
            }
            else
            {
                Console.WriteLine("El usuario ya existe");

            }



        }
    }
}