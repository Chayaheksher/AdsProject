
using finalProject.Models;
using finalProject.Models1;
using System.Text.Json;
using static finalProject.COMMON.Enums;
using static finalProject.GraphsDAL;

namespace finalProject
{
    public class GraphsBL:IGraphsBL
    {
        private readonly IGraphsDAL fromDal;

        public GraphsBL(IGraphsDAL graphsDAL)
        {
            fromDal = graphsDAL;
        }

        public List<ApprovalUsersClass> ApprovalUsers(string selectedMonth)
        {
            return fromDal.ApprovalUsers(selectedMonth);
        }

        public List<WhereStatusAdsClass> WhereStatusAds(string selectedMonth)
        {
            return fromDal.WhereStatusAds(selectedMonth);
        }

        public List<AdCategoryClass> AdCategory(string selectedMonth)
        {
            return fromDal.AdCategory(selectedMonth);
        }

        public List<sumCustomerCharge> SumCustomerCharge(string selectedMonth)
        {
            return fromDal.SumCustomerCharge(selectedMonth);
        }
    }
}
