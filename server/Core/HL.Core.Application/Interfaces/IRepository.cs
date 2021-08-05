using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HL.Core.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
            void Create(TEntity entity);

            TEntity Read(int id);
            Task<TEntity> ReadAsync(int id);
            IEnumerable<TEntity> Read();
            Task<IEnumerable<TEntity>> ReadAsync();
            IEnumerable<TEntity> Read(Expression<Func<TEntity, bool>> predicate);
            Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> predicate);

            void Update(TEntity entity);
            void Update(int id, TEntity entity);

            void Delete(TEntity entity);
            void Delete(int id);

            /// <summary>
            /// შემოწმება, არსებობს თუ არა მსგავსი ჩანაწერი
            /// </summary>
            bool Check(Expression<Func<TEntity, bool>> predicate);
            Task<bool> CheckAsync(Expression<Func<TEntity, bool>> predicate);

            int Count(Expression<Func<TEntity, bool>> predicate);
        }
    }
