using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BrainStew.Models
{
    public class Account:Common
    {
        public string LoginId { get; set; }
        public string PackageId { get; set; }
        public string TopUpDate { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        //public string FK_UserId { get; set; }
        public string PaymentMode { get; set; }
        //public string BankName { get; set; }
        public string BankBranch { get; set; }
        //public string TransactionNo { get; set; }
        //public string TransactionDate { get; set; }
        //public string AddedBy { get; set; }
        public string TotalAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Account> lstTopUp { get; set; }
        public string InvestmentId { get; set; }
        public string Name { get; set; }
        public string PinAmount { get; set; }
        public string UsedFor { get; set; }
        public string BV { get; set; }
        public string IsCalculated { get; set; }
        public string TransactionBy { get; set; }
        public string Status { get; set; }
        public string ROIPercentage { get; set; }
        public string Pk_userId { get; set; }
        public string PaymentType { get; set; }
        public string ProductName { get; set; }
        public string Remark { get; set; }
        public string PackageDays { get; set; }
        public string PrimaryAmount { get; set; }
        public string BonusPercentage { get; set; }
        public string BVPercentage { get; set; }
        public string BonusAmount { get; set; }
        //public string Monthid { get; set; }
        public string ROI { get; set; }
        public string DonationAmount { get; set; }
        public string DonationPlanId { get; set; }
        public string UpdatedDonationPlanId { get; set; }
        public DataSet TopUp()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@AddedBy", Fk_UserId),
                                        new SqlParameter("@Fk_ProductId",PackageId),
                                        new SqlParameter("@TotalAmount",TotalAmount),
                                        new SqlParameter("@BonusAmount",BonusAmount),
                                        new SqlParameter("@Amount", Amount),
                                          new SqlParameter("@Fk_PPPId", Monthid),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("TopUp", para);
            return ds;
        }
        public DataSet GetTopUpDetails()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@FK_UserId", Fk_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetTopUpDetails", para);
            return ds;
        }
        public DataSet DonationByWallet()
        {
            SqlParameter[] para =
            {
                     new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@Amount",Amount),
                     new SqlParameter("@DonationPlanId",DonationPlanId)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUserByWallet", para);
            return ds;
        }
        public DataSet GetDonationAmount()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetDonationAmount", para);

            return ds;

        }
        public DataSet GetBrainMatrixPlanAmount()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainMatrixPlanAmount", para);

            return ds;

        }
        public DataSet DonationBrainMatrixPlan()
        {
            SqlParameter[] para =
            {
                     new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@Amount",Amount),
                     new SqlParameter("@BrainMatrixPlanId",DonationPlanId)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveBrainMatrixPlanDetails", para);
            return ds;
        }
        public DataSet GetStewMatrixPlanAmount()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixPlanAmount", para);

            return ds;

        }
        public DataSet DonationStewMatrixPlan()
        {
            SqlParameter[] para =
            {
                     new SqlParameter("@Fk_UserId",Fk_UserId),
                     new SqlParameter("@Amount",Amount),
                     new SqlParameter("@StewMatrixPlanId",DonationPlanId)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveStewMatrixPlanDetails", para);
            return ds;
        }
    }
}