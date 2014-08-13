using System.Collections.Generic;
using PageMetrics.PersistentDataStore.Models;

namespace PageMetrics.PersistentDataStore
{
    public interface IPageRepository
    {
        IList<PageModel> GetAll();
        PageModel Get(string id);
        PageModel Store(PageModel page);
    }
}
