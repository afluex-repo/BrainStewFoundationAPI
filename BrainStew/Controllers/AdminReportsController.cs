using BrainStew.Filter;
using BrainStew.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrainStew.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        // GET: AdminReports
        #region AssociateList
        #endregion
        public ActionResult AssociateList(AdminReports model, string Status)
        {
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;
            if (Status != "" && Status != null)
            {
                model.Status = Status;
            }
            List<AdminReports> lst = new List<AdminReports>();

            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["isBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AssociateList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AssociateListBy(AdminReports model)
        {
            if (model.LoginId == null)
            {
                model.ToLoginID = null;
            }
            List<AdminReports> lst = new List<AdminReports>();
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.LoginId = model.ToLoginID;
            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["isBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            return View(model);
        }
        public ActionResult BlockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.BlockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User Blocked";
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult UnblockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UnblockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User Blocked";
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ActivateUser(string FK_UserID)
        {
            Profile model = new Profile();
            try
            {
                model.Fk_UserId = FK_UserID;
                model.ProductID = "1";
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ActivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User activated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        public ActionResult DeactivateUser(string lid)
        {
            Profile model = new Profile();
            try
            {
                model.LoginId = lid;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeactivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User deactivated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        #region topupreport
        public ActionResult TopupReport()
        {
            AdminReports newdata = new AdminReports();
            List<AdminReports> lst1 = new List<AdminReports>();
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    AdminReports Obj = new AdminReports();
                    Obj.ToLoginID = r["Pk_InvestmentId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["Name"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.TopupBy = r["TopupBy"].ToString();
                    Obj.Status = r["Status"].ToString();
                    Obj.PrintingDate = r["PrintingDate"].ToString();
                    Obj.Description = r["Description"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    ViewBag.Total = ds11.Tables[1].Rows[0]["Total"].ToString();
                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            return View(newdata);
        }
        [HttpPost]
        [ActionName("TopupReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TopupReportBy(AdminReports newdata)
        {
            if (newdata.LoginId == null)
            {
                newdata.ToLoginID = null;
            }
            List<AdminReports> lst1 = new List<AdminReports>();

            newdata.BusinessType = newdata.BusinessType == "" ? null : newdata.BusinessType;
            newdata.FromDate = string.IsNullOrEmpty(newdata.FromDate) ? null : Common.ConvertToSystemDate(newdata.FromDate, "dd/MM/yyyy");
            newdata.ToDate = string.IsNullOrEmpty(newdata.ToDate) ? null : Common.ConvertToSystemDate(newdata.ToDate, "dd/MM/yyyy");
            //newdata.LoginId = newdata.ToLoginID;
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    AdminReports Obj = new AdminReports();
                    Obj.ToLoginID = r["Pk_InvestmentId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["Name"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.TopupBy = r["TopupBy"].ToString();
                    Obj.Status = r["Status"].ToString();
                    Obj.PrintingDate = r["PrintingDate"].ToString();
                    Obj.Description = r["Description"].ToString();
                    //Obj.ReceiptNo = r["ReceiptNo"].ToString();
                    //Obj.BusinessType = r["Business"].ToString();
                    ViewBag.Total = ds11.Tables[1].Rows[0]["Total"].ToString();
                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            return View(newdata);
        }
        #endregion


        public ActionResult DirectListForAdmin()
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;

            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.GetDirectList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.SponsorId = (r["LoginId"].ToString());
                    obj.SponsorName = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lstDirect = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("DirectListForAdmin")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DirectListForAdmin(AdminReports model)
        {

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<AdminReports> lst = new List<AdminReports>();
            //model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.SponsorId = (r["LoginId"].ToString());
                    obj.SponsorName = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lstDirect = lst;
            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;
            return View(model);
        }
        public ActionResult ViewProfileForAdmin(string Id)
        {
            AdminReports model = new AdminReports();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            if (Id != null)
            {
                model.Fk_UserId = Id;
                DataSet ds = model.GetAdminProfileDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    model.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = ds.Tables[0].Rows[0]["SponserName"].ToString();
                    model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                    model.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
                    model.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ViewProfileForAdmin")]
        [OnAction(ButtonName = "Update")]
        public ActionResult ViewProfileForAdmin(AdminReports model, HttpPostedFileBase Image)
        {
            try
            {
                List<SelectListItem> Gender = Common.BindGender();
                ViewBag.Gender = Gender;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                if (Image != null)
                {
                    model.Image = "/PanUpload/" + Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                DataSet ds = model.UpdateAdminProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AdminProfile"] = "User profile updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["AdminProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AdminProfile"] = ex.Message;
            }
            return RedirectToAction("ViewProfileForAdmin", "AdminReports", new { Id = model.Fk_UserId });
        }
        public ActionResult DeleteUerDetails(string Id)
        {
            try
            {
                AdminReports model = new AdminReports();
                model.Fk_UserId = Id;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteUerDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "User deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        public ActionResult ViewProfile(string Id)
        {
            AdminReports model = new AdminReports();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            if (Id != null)
            {
                model.Fk_UserId = Id;
                DataSet ds = model.GetAdminProfileDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    ViewBag.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    ViewBag.SponsorName = ds.Tables[0].Rows[0]["SponserName"].ToString();
                    ViewBag.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    ViewBag.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    ViewBag.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    ViewBag.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    ViewBag.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                    ViewBag.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
                    ViewBag.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    ViewBag.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                    ViewBag.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    ViewBag.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    ViewBag.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
            }
            return View(model);
        }
        public ActionResult ViewProfileVeriFy(string Id)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Fk_UserId = Id;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ViewProfileVeriFy();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["verify"] = "Profile verify successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["verify"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["verify"] = ex.Message;
            }
            return RedirectToAction("ViewProfile","AdminReports",new { Id = Id });
        }
        public ActionResult BrainMatrixBenefits()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
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
        [ActionName("BrainMatrixBenefits")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetBrainMatrixDonationReport(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.LoginId = Session["LoginId"].ToString();
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
        public ActionResult DirectBenefitsList()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "2";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("DirectBenefitsList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DirectBenefitsListReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_IncomeTypeId = "2";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult LevelBenefitsList()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "1";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("LevelBenefitsList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult LevelBenefitsListReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_IncomeTypeId = "1";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult UpgradeBenefitsList()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "4";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UpgradeBenefitsList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult UpgradeBenefitsListReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_IncomeTypeId = "4";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult PlacementBenefitsList()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "3";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PlacementBenefitsList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult PlacementBenefitsListReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_IncomeTypeId = "3";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }


        public ActionResult BraintBenefitsList()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "6";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("BraintBenefitsList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult BraintBenefitsListReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_IncomeTypeId = "6";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }


        public ActionResult BrainLevelBenefitsList()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "7";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId ="7";
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("BrainLevelBenefitsList")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult BrainLevelBenefitsListReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_IncomeTypeId = "7";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = "7";
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model); 
        }
        public ActionResult ViewBenefitsListByUser(string id,string Incomeid)
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.LoginId = id;
            model.Fk_IncomeTypeId = Incomeid;
            if(Incomeid== "1")
            {
                @TempData["BenefitsName"] = "LEVEL BENEFITS LIST";
                @TempData["BenefitsNameList"] = "LEVELBENEFITSLIST";
            }
           else if (Incomeid == "2")
            {
                @TempData["BenefitsName"] = "DIRECT BENEFITS LIST";
                @TempData["BenefitsNameList"] = "DIRECTBENEFITSLIST";
            }
           else if (Incomeid == "4")
            {
                @TempData["BenefitsName"] = "UPGRADE BENEFITS LIST";
                @TempData["BenefitsNameList"] = "UPGRADEBENEFITSLIST";
            }
            else if (Incomeid == "3")
            {
                @TempData["BenefitsName"] = "PLACEMENT BENEFITS LIST";
                @TempData["BenefitsNameList"] = "PLACEMENTBENEFITSLIST";

            }
            else if (Incomeid == "6")
            {
                @TempData["BenefitsName"] = "BRAIN BENEFITS LIST";
                @TempData["BenefitsNameList"] = "BRAINTBENEFITSLIST";

            }
            else if (Incomeid == "7")
            {
                @TempData["BenefitsName"] ="BRAIN LEVEL BENEFITS LIST";
                @TempData["BenefitsNameList"] = "BRAINLEVELBENEFITSLIST";

            }
            else if (Incomeid == "9")
            {
                @TempData["BenefitsName"] = "STEW MATRIX LEVEL BENEFITS";
                @TempData["BenefitsNameList"] = "StewMatrixLevelBenefitsForAdmin";

            }
            DataSet ds = model.GetBraintBenefitsList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.FromLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult WalletLedger()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.WalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.AvailableBalance = r["AvailableBalance"].ToString();
                    lst.Add(obj);
                }
                ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.AvailableBalance = double.Parse(ds.Tables[0].Compute("sum(AvailableBalance)", "").ToString()).ToString("n2");
                model.lstWalletLedger = lst;

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("WalletLedger")]
        [OnAction(ButtonName = "Search")]
        public ActionResult WalletLedger(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.WalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.AvailableBalance = r["AvailableBalance"].ToString();
                    lst.Add(obj);
                }
                ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.AvailableBalance = double.Parse(ds.Tables[0].Compute("sum(AvailableBalance)", "").ToString()).ToString("n2");
                model.lstWalletLedger = lst;
            }
            return View(model);
        }
        public ActionResult StewMatrixDonationListForAdmin()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
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
        [ActionName("StewMatrixDonationListForAdmin")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult GetStewnMatrixDonationReport(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.LoginId = Session["LoginId"].ToString();
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



        public ActionResult TopUpWallet()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.TopUpWallet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.AvailableBalance = r["AvailableBalance"].ToString();
                    lst.Add(obj);
                }
                ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.AvailableBalance = double.Parse(ds.Tables[0].Compute("sum(AvailableBalance)", "").ToString()).ToString("n2");
                model.lstWalletLedger = lst;

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("TopUpWallet")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TopUpWallet(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.TopUpWallet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["FK_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.AvailableBalance = r["AvailableBalance"].ToString();
                    lst.Add(obj);
                }
                ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.AvailableBalance = double.Parse(ds.Tables[0].Compute("sum(AvailableBalance)", "").ToString()).ToString("n2");
                model.lstWalletLedger = lst;

            }
            return View(model);
        }


        public ActionResult StewMatrixLevelBenefitsForAdmin()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "9";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = "9";
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("StewMatrixLevelBenefitsForAdmin")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult StewMatrixLevelBenefitsReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.Fk_IncomeTypeId = "9";
            DataSet ds = model.getBenefitlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = "9";
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult StewMatrixBenefitLists()
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.Fk_IncomeTypeId = "8";
            DataSet ds = model.GetStewMatrixBenefitsList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lstStewMatrix = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("StewMatrixBenefitLists")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult StewMatrixBenefitLists(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Fk_IncomeTypeId = "8";
            DataSet ds = model.GetStewMatrixBenefitsList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["TotalBusiness"].ToString();
                    obj.Amount = r["TotalBenefits"].ToString();
                    obj.Fk_IncomeTypeId = r["Fk_IncomeTypeId"].ToString();
                    lst.Add(obj);
                }
                model.lstStewMatrix = lst;
            }
            return View(model);
        }
        public ActionResult ViewStewMatrixBenefitsListByUser(string id, string Incomeid)
        {
            List<AdminReports> lst = new List<AdminReports>();
            AdminReports model = new AdminReports();
            model.LoginId = id;
            model.Fk_IncomeTypeId = Incomeid;
            if (Incomeid == "1")
            {
                @TempData["BenefitsName"] = "LEVEL BENEFITS LIST";
                @TempData["BenefitsNameList"] = "LEVELBENEFITSLIST";
            }
            else if (Incomeid == "2")
            {
                @TempData["BenefitsName"] = "DIRECT BENEFITS LIST";
                @TempData["BenefitsNameList"] = "DIRECTBENEFITSLIST";
            }
            else if (Incomeid == "4")
            {
                @TempData["BenefitsName"] = "UPGRADE BENEFITS LIST";
                @TempData["BenefitsNameList"] = "UPGRADEBENEFITSLIST";
            }
            else if (Incomeid == "3")
            {
                @TempData["BenefitsName"] = "PLACEMENT BENEFITS LIST";
                @TempData["BenefitsNameList"] = "PLACEMENTBENEFITSLIST";

            }
            else if (Incomeid == "6")
            {
                @TempData["BenefitsName"] = "BRAIN BENEFITS LIST";
                @TempData["BenefitsNameList"] = "BRAINTBENEFITSLIST";

            }
            else if (Incomeid == "7")
            {
                @TempData["BenefitsName"] = "BRAIN LEVEL BENEFITS LIST";
                @TempData["BenefitsNameList"] = "BRAINLEVELBENEFITSLIST";

            }
            else if (Incomeid == "8")
            {
                @TempData["BenefitsName"] = "STEW MATRIX BENEFITS LIST"; 
                @TempData["BenefitsNameList"] = "STEWMATRIXBENEFITLISTS";

            }
            DataSet ds = model.GetStewMatrixBenefitsLists();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.FromLoginID = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
    }
}