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

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty) // verificando se Guid esta vazio 
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

            // da o retorno do item
            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                // recebe um item(entidade) do usuario, vai buscar esse Id de usuario no banco de dados,
                // caso ache o Id igual ao recebido ele vai atribuir a variavel result, caso nao
                // ache ele atribui o valor null a variavel result
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                // se result == null ele vai retornar como resposta o valor null
                if (result == null) 
                    return null;

                item.UpdateAt = DateTime.UtcNow; // retorna a data atual
                item.CreateAt = result.CreateAt; // força o CreateAt a receber sempre do result

                // o contexto vai pegar o result e vai setar os dados conrrentes com os valores de item
                _context.Entry(result).CurrentValues.SetValues(item);

                // salvando alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // da o retorno do item
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                // recebe um id do usuario, vai buscar esse Id de usuario no banco de dados,
                // caso ache o Id igual ao recebido ele vai atribuir a variavel result, caso nao
                // ache ele atribui o valor false a variavel result
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                    return false;

                _dataset.Remove(result); // setando o remove no result
                await _context.SaveChangesAsync(); // salvando mudanças
                return true; // retorno verdadeiro para a exclusão da tabela
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExistAsync ( Guid id) 
        {
            // verifica se no banco existe qualquer id recebido, devolvendo auma task verdadeiro ou falso
            return await _dataset.AnyAsync (p => p.Id.Equals(id));
        }


        public async Task<T> SelectAsyn(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync(); // select sem o where
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
