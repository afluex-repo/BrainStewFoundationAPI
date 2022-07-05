using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BrainStew.Models
{
    public class UserReports
    {
        public List<UserReports> lst { get; set; }
        public string LoginId { get; set; }
        public string FK_UserId { get; set; }
        public string PK_PayoutWalletId { get; set; }
        public string Narration { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string TransactionDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromName { get; set; }
        public string FromLoginId { get; set; }
        public string BusinessAmount { get; set; }
        public string Amount { get; set; }
        public string Level { get; set; }
        public string ClosingDate { get; set; }
        public string PayoutNo { get; set; }
        public string LevelIncomeTR1 { get; set; }
        public string LevelIncomeTR2 { get; set; }
        public string GrossAmount { get; set; }
        public string ProcessingFee { get; set; }
        public string TDSAmount { get; set; }
        public string NetAmount { get; set; }
        public string Percentage { get; set; }
        public string Status { get; set; }
        public string ToName { get; set; }
        public DataSet PayoutWalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutWalletLedger", para);
            return ds;
        }
        public DataSet LevelIncomeTr1()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr1", para);
            return ds;
        }
        public DataSet LevelIncomeTr2()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                   new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr2", para);
            return ds;
        }
        public DataSet PayoutDetail()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_Userid", FK_UserId),
                new SqlParameter("@PayoutNo", PayoutNo),
                  new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutDetails", para);
            return ds;
        }
        public string Fk_IncomeTypeId { get; set; }
        public DataSet PlacementBenefits()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPlacementBenefitsReport", para);
            return ds;
        }

        public DataSet GetbenefitsReport()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBenefitsReports", para);
            return ds;
        }
        public DataSet GetBrainMatrixDonation()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainMatrixReport", para);
            return ds;
        }
        public DataSet GetBrainMatixLevelBenefits()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBrainLevelBenefits", para);
            return ds;
        }
        public DataSet GetStewMatrixDonation()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixReport", para);
            return ds;
        }
        public DataSet GetbenefitReportNew()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetbrainmatrixReportNew", para);
            return ds;
        }
        public DataSet getStewbenefitsReport()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixReportNew", para);
            return ds;
        }

        public DataSet GetStewMatrixLevelBenefits()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@Status", Status),
                  new SqlParameter("@Fk_IncomeTypeId",Fk_IncomeTypeId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetStewMatrixLevelBenefits", para);
            return ds;
        }


    }
}