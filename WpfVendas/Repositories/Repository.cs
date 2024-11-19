using System.Collections.Generic;
using WpfVendas.Repositories;
using System.Linq;

namespace WpfVendas.Repositories
{
    public class Repository<T> where T : class
    {
        protected List<T> items = new List<T>();

        public virtual void Add(T item)
        {
            items.Add(item);
        }

        public virtual void Remove(T item)
        {
            items.Remove(item);
        }

        public virtual void Update(T item, int index)
        {
            items[index] = item;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return items;
        }

        public virtual T GetById(int id)
        {
            return items.FirstOrDefault();
        }
    }
}