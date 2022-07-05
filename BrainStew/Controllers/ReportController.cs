using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrainStew.Models;
using System.Data.SqlClient;
using BrainStew.Controllers;
using System.Data;
using BrainStew.Filter;

namespace BrainStew.Controllers
{
    public class ReportController : UserBaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PayoutWalletLedger()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
           
            DataSet ds = model.PayoutWalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.PK_PayoutWalletId = r["PK_PayoutWalletId"].ToString();
                    obj.FK_UserId = r["FK_UserId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            User obj1 = new User();
            obj1.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds11 = obj1.GetPayoutBalance();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                ViewBag.BalanceAmount = ds11.Tables[0].Rows[0]["Balance"].ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PayoutWalletLedger(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.PayoutWalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.PK_PayoutWalletId = r["PK_PayoutWalletId"].ToString();
                    obj.FK_UserId = r["FK_UserId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult BrainMatrixDonationList()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBrainMatrixDonation();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["Name"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["BrainMatrixLevel"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("BrainMatrixDonationList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetBrainMatrixDonationReport(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBrainMatrixDonation();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["Name"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["BrainMatrixLevel"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult LevelIncomeTr1()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.LevelIncomeTr1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult LevelIncomeTr1(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.LevelIncomeTr1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult LevelIncomeTr2()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.LevelIncomeTr2();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult LevelIncomeTr2(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.LevelIncomeTr2();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        public ActionResult PayoutDetail()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.PayoutDetail();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FK_UserId = r["Fk_Userid"].ToString();
                    obj.LevelIncomeTR1 = r["LevelIncomeTR1"].ToString();
                    obj.LevelIncomeTR2 = r["LevelIncomeTR2"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.ProcessingFee = r["AdminFee"].ToString();
                    obj.TDSAmount = r["TDSAmount"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PayoutDetail(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.PayoutDetail();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FK_UserId = r["Fk_Userid"].ToString();
                    obj.LevelIncomeTR1 = r["LevelIncomeTR1"].ToString();
                    obj.LevelIncomeTR2 = r["LevelIncomeTR2"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.ProcessingFee = r["AdminFee"].ToString();
                    obj.TDSAmount = r["TDSAmount"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult TransferotherWallet(string LoginId,string Amount)
        {
            User model = new User();
            try
            {
                model.Amount = Convert.ToDecimal(Amount);
                model.LoginId = LoginId;
                model.AddedBy = Session["Pk_UserId"].ToString();
                DataSet ds = model.TransfertoOther();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlacementBenefits()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            //model.Status = "0";
            DataSet ds = model.PlacementBenefits();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PlacementBenefits")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult PlacementBenefits(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.PlacementBenefits();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        public ActionResult UpgradeBenefits()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            model.Fk_IncomeTypeId = "4";
            //model.Status = "0";
            DataSet ds = model.GetbenefitsReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
          
        }

        [HttpPost]
        [ActionName("UpgradeBenefits")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult UpgradeBenefitsReport(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            model.Fk_IncomeTypeId = "4";
            DataSet ds = model.GetbenefitsReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult BrainMatrixBenefits()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            //model.Status = "0";
            model.Fk_IncomeTypeId = "6";
            DataSet ds = model.GetbenefitReportNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    //obj.FromName = r["FromName"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    //obj.BusinessAmount = r["BusinessAmount"].ToString();
                    //obj.Percentage = r["CommissionPercentage"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    //obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
                ViewBag.Total = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            return View(model);

        }

        [HttpPost]
        [ActionName("BrainMatrixBenefits")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetBrainMatrixbenefitsReport(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            model.Fk_IncomeTypeId = "6";
            DataSet ds = model.GetbenefitReportNew();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    //obj.FromName = r["FromName"].ToString();
                    //obj.FromLoginId = r["LoginId"].ToString();
                    //obj.BusinessAmount = r["BusinessAmount"].ToString();
                    //obj.Percentage = r["CommissionPercentage"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    //obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult BrainLevelBenefits()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            //model.Status = "0";
            DataSet ds = model.GetBrainMatixLevelBenefits();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.ToName= r["ToName"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
                
            }
            return View(model);

        }

        [HttpPost]
        [ActionName("BrainLevelBenefits")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult BrainLevelBenefitsReport(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBrainMatixLevelBenefits();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult StewMatrixDonationList()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetStewMatrixDonation();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["Name"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["stewMatrixLevel"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GetStewMatrixDonationReport(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetStewMatrixDonation();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["Name"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["stewMatrixLevel"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult StewMatrixBenefits()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            //model.Status = "0";
            model.Fk_IncomeTypeId = "8";
            DataSet ds = model.getStewbenefitsReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
                ViewBag.Total = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult StewMatrixLevelBenefits()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            //model.Status = "0";
            DataSet ds = model.GetStewMatrixLevelBenefits();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;

            }
            return View(model);

        }


        [HttpPost]
        [ActionName("StewMatrixLevelBenefits")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult StewMatrixLevelBenefits(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetStewMatrixLevelBenefits();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }


    }
}


