using JQ.Common.IRepository;
using JQ.Base.IServices;
using JQ.Common.Model;

namespace JQ.Base.Services
{
    public class BaseFeeItemClassServices : BaseServices<Base_Fee_Item_Class>,IBaseFeeItemClassServices
    {

		public IBaseRepository<Base_Fee_Item_Class> dal;

        public BaseFeeItemClassServices(IBaseRepository<Base_Fee_Item_Class> dal)
        {
		    this.dal = dal;
            base.baseDal = dal;
        }

	
    }
}
