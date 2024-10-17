using System.Collections.Generic;
using System.Threading.Tasks;

//namespace projectnew.Models
//{
//    public interface IRepository<TEntity>
//    {
//        Task Add(TEntity entity);
//        Task Delete(int id);
//        Task<TEntity> GetById(int id);
//        Task<List<TEntity>> GetAll();
//        //void Update(TEntity entity);
//        void UpdateById(int id, TEntity updatedEntity);
//    }
//}
namespace projectnew.Models
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void DeleteById(int id);
        void UpdateById(int id, TEntity updatedEntity);
        TEntity FindById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
