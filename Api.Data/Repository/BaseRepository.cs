using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataset;
        public BaseRepository(MyContext context) // injeção de dependencia
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if(item.Id == Guid.Empty) // verificando se Guid esta vazio 
                {
                    item.Id = Guid.NewGuid(); // caso esteja atribuindo um novo Guid ao Id de item
                }

                item.CreateAt = DateTime.UtcNow; // recebendo data atual do servidor ou maquina
                _dataset.Add(item); // add a tabela trabalhada ao contexto

                await _context.SaveChangesAsync(); // salvando alterações de contexto
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public Task<T> SelectAsyn(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
