using eTicaretProjesi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTicaretProjesi.BL.Repository
{
    public class BaseRepository<T, TId> where T : class
    {
        protected internal static MyContext dbContext;

        public virtual List<T> Listele()
        {
            try
            {
                dbContext = new MyContext();
                return dbContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual T IDyeGoreBul(TId id)
        {
            try
            {
                dbContext = new MyContext();
                return dbContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Ekle(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Add(entity);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Sil(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Remove(entity);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int Guncelle()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
