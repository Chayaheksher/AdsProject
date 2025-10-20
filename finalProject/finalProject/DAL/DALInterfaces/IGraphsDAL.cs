using finalProject;

public interface IGraphsDAL
{
    List<GraphsDAL.ApprovalUsersClass> ApprovalUsers(string selectedMonth);
    List<GraphsDAL.WhereStatusAdsClass> WhereStatusAds(string selectedMonth);
    List<GraphsDAL.AdCategoryClass> AdCategory(string selectedMonth);
    List<GraphsDAL.sumCustomerCharge> SumCustomerCharge(string selectedMonth);
}
