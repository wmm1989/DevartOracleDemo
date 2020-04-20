using JQ.Common.IRepository;
using JQ.Common.Model;
using JQ.Base.IServices;

namespace JQ.Base.Services
{
    public class BasePRCAddressDictServices : BaseServices<Base_PRCAddress_Dict>,IBasePRCAddressDictServices
    {

		public IBaseRepository<Base_PRCAddress_Dict> dal;

        public BasePRCAddressDictServices(IBaseRepository<Base_PRCAddress_Dict> dal)
        {
		    this.dal = dal;
            base.baseDal = dal;
        }

	
    }
}
