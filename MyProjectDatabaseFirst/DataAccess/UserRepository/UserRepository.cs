using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UserRepository
{
    public class UserRepository<T> : IRepository<T> where T : class
    {
        private MyProjectEntities db = null;
        private DbSet<T> User = null;

        public UserRepository()
        {
            this.db = new MyProjectEntities();
            User = db.Set<T>();
        }

        public UserRepository(MyProjectEntities db)
        {
            this.db = db;
            User = db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return User.ToList();
        }

        public T Find(object id)
        {
            return User.Find(id);
        }

        public void Insert(T obj)
        {
            User.Add(obj);
        }

        public void Update(T obj)
        {
            User.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = User.Find(id);
            User.Remove(existing);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
