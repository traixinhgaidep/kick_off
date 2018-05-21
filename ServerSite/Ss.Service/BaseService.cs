using Ss.Data.Models;
using Ss.Data.ModelViews;
using Ss.Data.Repository.Interfaces;
using Ss.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Service
{
    public abstract class BaseService<TModel, TView> : IService<TModel, TView> 
        where TModel : BaseEntity, new() 
        where TView : BaseViewEntity, new()
    {
        protected readonly IRepository<TModel> BaseRepository;
        public BaseService(IRepository<TModel> repository)
        {
            BaseRepository = repository;
        }
        public IEnumerable<TView> Get(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<TModel> results = BaseRepository.Get(filter, orderBy, includeProperties);
            return results.Select(ConvertToView);
        }

        #region Abstraction

        public virtual TView ConvertToView(TModel model)
        {
            var view = new TView
            {
                Id = model.Id
            };
            return view;
        }

        public TModel GetSingle(Expression<Func<TModel, bool>> predicate)
        {
            return BaseRepository.GetSingle(predicate);
        }

        #endregion
    }
}
