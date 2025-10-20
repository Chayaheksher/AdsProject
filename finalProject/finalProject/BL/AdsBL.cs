
using finalProject.Models;
using finalProject.Models1;
using System.Text.Json;
using static finalProject.COMMON.Enums;
using static finalProject.AdsDAL;

namespace finalProject
{
    public class AdsBL:IAdsBL
    {
        private readonly IAdsDAL fromDal;

        public AdsBL(IAdsDAL adsDal)
        {
            fromDal = adsDal;
        }
        public List<IshurimForAdClass> AdToUser(int userId, DateTime? firstDate, DateTime? endDate)
        {
            return fromDal.AdToUser(userId, firstDate, endDate);
        }

        public List<IshurimForAdClass> AdToUserWithDate(int UserId, DateTime firstDate, DateTime endDate)
        {
            return fromDal.AdToUserWithDate(UserId, firstDate, endDate);
        }

        public List<AdDiaryClass> AdDiary(int adId)
        {
            return fromDal.AdDiary(adId);
        }

        private static StatusIshurEnum GetStatusIshur(AdStatusEnum currentAdStatus, bool isApproval)
        {
            StatusIshurEnum statusIshurAd;
            if (!isApproval)
            {
                {
                    statusIshurAd = StatusIshurEnum.rejected;
                }
            }
            else
            {
                statusIshurAd = StatusIshurEnum.approved;
            }

            return statusIshurAd;
        }
        public bool AdApproval(int adId, int userId, int currentAdStatus, bool isApproval, string ishurForAdNote)
        {
            if((currentAdStatus == (int)AdStatusEnum.CustomerMaterials || currentAdStatus==(int)AdStatusEnum.WillPublication)&& isApproval == true)
            {
                bool hasMaterial =  fromDal.hasMaterialOrPubDatePast(adId, currentAdStatus);
                if (hasMaterial == false) { return false; }
            }
            var isExistsPublicationDates = false;

            StatusIshurEnum statusIshurAd = GetStatusIshur((AdStatusEnum)currentAdStatus, isApproval);
            fromDal.UpdateApprovalAd(adId, userId, statusIshurAd, ishurForAdNote);

            if ((AdStatusEnum)currentAdStatus == AdStatusEnum.WillPublication && isApproval)
            {
                 fromDal.UpdateDateAd(adId, statusIshurAd);
                isExistsPublicationDates =  fromDal.IsSeveralPublicationDates(adId);
            }
            Helper.NextOrReverseStatusAndRoll nextOrReversStatus = isApproval ? Helper.GetNextStatus((AdStatusEnum)currentAdStatus, isExistsPublicationDates) : Helper.GetReversStatus((AdStatusEnum)currentAdStatus);
            fromDal.InsertNextOrReversAdStatus(nextOrReversStatus.AdStatusEnum, nextOrReversStatus.RolesEnum, adId, userId);
            return true;
        }

        public bool CancelApprovalOrRejection(int adRowToDelete, int adRowToUpdate, int adId, int userId)
        {
            try
            {
                bool deleteResult =  fromDal.DeleteTheNextStatus(adRowToDelete);

                if (!deleteResult)
                {
                    return false;
                }

                bool updateResult =  fromDal.UpdateTheCanceledStatus(adRowToUpdate, adId, userId);

                return updateResult;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Shearim ShaarAd()
        {
            return fromDal.ShaarAd();
        }

        public async Task<bool> OutApi(int userId, int charactersIDInsertRow)
        {
            string apiUrlEUR = "https://boi.org.il/PublicApi/GetExchangeRate?key=EUR";
            string apiUrlUSD = "https://boi.org.il/PublicApi/GetExchangeRate?Key=USD";
            string apiUrlInterest = "https://boi.org.il/PublicApi/GetInterest";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseEUR = await client.GetAsync(apiUrlEUR);
                    HttpResponseMessage responseUSD = await client.GetAsync(apiUrlUSD);
                    HttpResponseMessage responseInterest = await client.GetAsync(apiUrlInterest);

                    if (responseEUR.IsSuccessStatusCode && responseUSD.IsSuccessStatusCode && responseInterest.IsSuccessStatusCode)
                    {
                        string responseBodyEUR = await responseEUR.Content.ReadAsStringAsync();
                        string responseBodyUSD = await responseUSD.Content.ReadAsStringAsync();
                        string responseBodyInterest = await responseInterest.Content.ReadAsStringAsync();

                        var jsonEUR = JsonDocument.Parse(responseBodyEUR).RootElement;
                        var jsonUSD = JsonDocument.Parse(responseBodyUSD).RootElement;
                        var jsonInterest = JsonDocument.Parse(responseBodyInterest).RootElement;

                        var exchangeRateEUR = jsonEUR.GetProperty("currentExchangeRate").GetDecimal();
                        var exchangeRateUSD = jsonUSD.GetProperty("currentExchangeRate").GetDecimal();
                        var interestRate = jsonInterest.GetProperty("currentInterest").GetDecimal();

                        var result = new
                        {
                            currentExchangeRateEUR = exchangeRateEUR,
                            currentExchangeRateUSD = exchangeRateUSD,
                            currentInterest = interestRate
                        };

                        string shearim = $"שער הדולר {exchangeRateUSD} שער היורו {exchangeRateEUR} ריבית {interestRate}";
                         fromDal.OutApi(shearim, userId, charactersIDInsertRow);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<StatusForRejectionClass> StatusForRejection(int adId)
        {
            return fromDal.StatusForRejection(adId);
        }
    }
}
