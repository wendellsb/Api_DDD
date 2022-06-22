using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<UserEntity> Get(Guid Id); // retornando um UserEntity

        Task<IEnumerable<UserEntity>> GetAll(); // pega todos os usuarios e devolve uma lista de usuarios

        Task<UserEntity> Post(UserEntity user);

        Task<UserEntity> Put(UserEntity user);

        Task<bool> Delete(Guid id);



    }
}
