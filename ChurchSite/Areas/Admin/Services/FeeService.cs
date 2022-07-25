using ChurchSite.Areas.Admin.Interfaces;
using ChurchSite.Areas.Admin.ViewModels;
using ChurchSite.DAL.DataConnection;
using ChurchSite.DAL.Entity;
using eLibrarySystem.Areas.Admin.Helpers;
using MonnifyPaymentGateway.Entities;
using MonnifyPaymentGateway.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ChurchSite.Areas.Admin.Services
{
    public class FeeService : IFeeService
    {
        // Instanciation Process
        #region Instanciation
        readonly DatabaseEntities _db;
        MonifyRestService _MonnifyRestService;
        public FeeService()
        {
            _db = new DatabaseEntities();
            _MonnifyRestService = new MonifyRestService();
        }
        public FeeService(DatabaseEntities db)
        {
            _db = db;
            _MonnifyRestService = new MonifyRestService();
        }
        #endregion

        public List<FeeVM> GetFees()
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            var model = _db.Fees.Where(x => x.IsDeleted == false).Select(b => new FeeVM()
            {
                Id = b.Id,
                Description = b.Description,
                Amount = b.Amount
            }).ToList();
            foreach (var item in model)
            {
                item.AmountString = item.Amount.ToString("N", nfi);
            }
            return model;
        }

        public bool AddFee(FeeVM vmodel)
        {
            var model = new Fee()
            {
                Description = vmodel.Description,
                Amount = CustomSerializer.UnMaskString(vmodel.AmountString),
                IsDeleted = false,
                DateCreated = DateTime.Now
            };
            _db.Fees.Add(model);
            _db.SaveChanges();
            return true;
        }

        public FeeVM GetFee(int id)
        {
            var model = _db.Fees.Where(x => x.Id == id).Select(b => new FeeVM()
            {
                Id = b.Id,
                Description = b.Description,
                Amount = b.Amount
            }).FirstOrDefault();
            return model;
        }

        public bool UpdateFee(FeeVM vmodel)
        {
            var model = _db.Fees.FirstOrDefault(x => x.Id == vmodel.Id);
            model.Amount = CustomSerializer.UnMaskString(vmodel.AmountString);
            model.Description = vmodel.Description;
            model.LastModified = DateTime.Now;

            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }

        public bool DeleteFee(int id)
        {
            var model = _db.Fees.FirstOrDefault(x => x.Id == id);
            model.IsDeleted = true;
            _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return true;
        }

        //Monnify
        public InvoiceVM CreateBookMassInvoice(string redirectURL, BookMassVM vmodel)
        {
            InvoiceVM vM = new InvoiceVM();
            var feeName = "Mass booking";


            var senderEmail = "";
            var senderName = "";
            senderEmail = "";
            senderName = vmodel.Name;

            var splitPercentage = Convert.ToDecimal(_db.ApplicationSettings.FirstOrDefault().MonnifySplitPercentage);
            var _incomeSplitConfigs = _db.MonnifySubAccountRecords.Where(x => x.IsDeleted == false).ToArray();
            var incomeSplitConfig = (from config in _incomeSplitConfigs
                                     select new IncomeSplitConfig
                                     {
                                         subAccountCode = config.SubAccountCode,
                                         splitPercentage = config.SplitPercentage,
                                         feeBearer = config.FeeBearer,
                                         feePercentage = config.FeePercentage
                                     }).ToList();
            var invoice = new Invoice();
            invoice.Amount = Convert.ToDecimal(CustomSerializer.UnMaskString(vmodel.AmountStr));
            invoice.Description = feeName;
            invoice.CurrencyCode = "NGN";
            invoice.ContractCode = "";
            invoice.CustomerEmail = senderEmail;
            invoice.CustomerName = senderName;
            invoice.ExpiryDate = DateTime.Now.AddHours(5).ToString("yyy-MM-dd HH:mm:ss");
            invoice.PaymentMethods = new string[] { "ACCOUNT_TRANSFER", "CARD" };
            invoice.IncomeSplitConfig = incomeSplitConfig;
            invoice.RedirectUrl = redirectURL;
            var FeeRecordID = StoreBookMassInvoice(invoice, vmodel);

            var rand = Guid.NewGuid();
            invoice.InvoiceReference = "CKPGRAENUGU" + "|" + "DON" + DateTime.Now.ToString("yyyyMMdd") + rand.ToString().ToUpper();
            var _invoice = _MonnifyRestService.CreateInvoice(invoice);
            if (_invoice.RequestSuccessful == true)
                return UpdateBookMassInvoice(_invoice.ResponseBody, vmodel, FeeRecordID);

            vM.Message = _invoice.ResponseMessage;
            return vM;

        }
        public int StoreBookMassInvoice(Invoice invoice, BookMassVM vmodel)
        {

            DonationStorage payment = new DonationStorage();
            payment.PaymentDate = DateTime.Now;
            payment.FeeDescription = invoice.Description;
            payment.HasPaid = false;
            payment.InvoiceNumber = invoice.InvoiceReference;
            payment.BankName = invoice.BankName;
            payment.SenderName = vmodel.Name;
            payment.SenderReason = vmodel.For;
            payment.CheckOutURL = invoice.CheckoutUrl;
            payment.VirtualAccountNumber = invoice.AccountNumber;
            payment.BankCode = invoice.BankCode;
            payment.InvoiceExpiryDate = Convert.ToDateTime(invoice.ExpiryDate);
            payment.AmountToPaid = invoice.Amount;
            _db.DonationStorages.Add(payment);
            _db.SaveChanges();
            return payment.Id;
        }
        public InvoiceVM UpdateBookMassInvoice(Invoice invoice, BookMassVM vmodel, int FeeStorageID)
        {
            InvoiceVM invoicevm = new InvoiceVM();

            var payment = _db.DonationStorages.FirstOrDefault(x => x.Id == FeeStorageID);
            payment.InvoiceNumber = invoice.InvoiceReference;
            payment.BankName = invoice.BankName;
            payment.BankCode = invoice.BankCode;
            payment.InvoiceExpiryDate = Convert.ToDateTime(invoice.ExpiryDate);
            payment.PaymentDate = DateTime.Now;

            payment.CheckOutURL = invoice.CheckoutUrl;
            payment.VirtualAccountNumber = invoice.AccountNumber;
            _db.SaveChanges();

            //Return Update
            invoicevm.AccountName = invoice.AccountName;
            invoicevm.AccountNumber = invoice.AccountNumber;
            invoicevm.BankName = invoice.BankName;
            invoicevm.BankCode = invoice.BankCode;
            invoicevm.CheckoutUrl = invoice.CheckoutUrl;
            invoicevm.CustomerEmail = invoice.CustomerEmail;
            invoicevm.CustomerName = invoice.CustomerName;
            invoicevm.ExpiryDate = invoice.ExpiryDate;
            invoicevm.InvoiceReference = invoice.InvoiceReference;
            invoicevm.Description = invoice.Description;
            invoicevm.storageID = payment.Id;
            invoicevm.Message = "MONNIFY Payment Invoice generation was successful.";

            return invoicevm;
        }
        public InvoiceVM CreateInvoice(string redirectURL, DonationVM donationVM)
        {
            InvoiceVM vM = new InvoiceVM();
            var feeName = "Parish donation";


            var senderEmail = "";
            var senderName = "";
            senderEmail = donationVM.Email;
            senderName = donationVM.Name;

            var splitPercentage = Convert.ToDecimal(_db.ApplicationSettings.FirstOrDefault().MonnifySplitPercentage);
            var _incomeSplitConfigs = _db.MonnifySubAccountRecords.Where(x => x.IsDeleted == false).ToArray();
            var incomeSplitConfig = (from config in _incomeSplitConfigs
                                     select new IncomeSplitConfig
                                     {
                                         subAccountCode = config.SubAccountCode,
                                         splitPercentage = config.SplitPercentage,
                                         feeBearer = config.FeeBearer,
                                         feePercentage = config.FeePercentage
                                     }).ToList();
            var invoice = new Invoice();
            invoice.Amount = Convert.ToDecimal(CustomSerializer.UnMaskString(donationVM.Amount));
            invoice.Description = feeName;
            invoice.CurrencyCode = "NGN";
            invoice.ContractCode = "";
            invoice.CustomerEmail = senderEmail;
            invoice.CustomerName = senderName;
            invoice.ExpiryDate = DateTime.Now.AddHours(5).ToString("yyy-MM-dd HH:mm:ss");
            invoice.PaymentMethods = new string[] { "ACCOUNT_TRANSFER", "CARD" };
            invoice.IncomeSplitConfig = incomeSplitConfig;
            invoice.RedirectUrl = redirectURL;
            var FeeRecordID = StoreInvoice(invoice, donationVM);

            var rand = Guid.NewGuid();
            invoice.InvoiceReference = "CKPGRAENUGU" + "|" + "DON" + DateTime.Now.ToString("yyyyMMdd") + rand.ToString().ToUpper();
            var _invoice = _MonnifyRestService.CreateInvoice(invoice);
            if (_invoice.RequestSuccessful == true)
                return UpdateInvoice(_invoice.ResponseBody, donationVM, FeeRecordID);

            vM.Message = _invoice.ResponseMessage;
            return vM;

        }


        public int StoreInvoice(Invoice invoice, DonationVM donationVM)
        {

            DonationStorage payment = new DonationStorage();
            payment.PaymentDate = DateTime.Now;
            payment.FeeDescription = invoice.Description;
            payment.HasPaid = false;
            payment.InvoiceNumber = invoice.InvoiceReference;
            payment.BankName = invoice.BankName;
            payment.SenderEmail = donationVM.Email;
            payment.SenderName = donationVM.Name;
            payment.SenderReason = donationVM.Reason;
            payment.CheckOutURL = invoice.CheckoutUrl;
            payment.VirtualAccountNumber = invoice.AccountNumber;
            payment.BankCode = invoice.BankCode;
            payment.InvoiceExpiryDate = Convert.ToDateTime(invoice.ExpiryDate);
            payment.AmountToPaid = invoice.Amount;
            _db.DonationStorages.Add(payment);
            _db.SaveChanges();
            return payment.Id;
        }
        public InvoiceVM UpdateInvoice(Invoice invoice, DonationVM donationVM, int FeeStorageID)
        {
            InvoiceVM invoicevm = new InvoiceVM();

            var payment = _db.DonationStorages.FirstOrDefault(x => x.Id == FeeStorageID);
            payment.InvoiceNumber = invoice.InvoiceReference;
            payment.BankName = invoice.BankName;
            payment.BankCode = invoice.BankCode;
            payment.InvoiceExpiryDate = Convert.ToDateTime(invoice.ExpiryDate);
            payment.PaymentDate = DateTime.Now;

            payment.CheckOutURL = invoice.CheckoutUrl;
            payment.VirtualAccountNumber = invoice.AccountNumber;
            _db.SaveChanges();

            //Return Update
            invoicevm.AccountName = invoice.AccountName;
            invoicevm.AccountNumber = invoice.AccountNumber;
            invoicevm.BankName = invoice.BankName;
            invoicevm.BankCode = invoice.BankCode;
            invoicevm.CheckoutUrl = invoice.CheckoutUrl;
            invoicevm.CustomerEmail = invoice.CustomerEmail;
            invoicevm.CustomerName = invoice.CustomerName;
            invoicevm.ExpiryDate = invoice.ExpiryDate;
            invoicevm.InvoiceReference = invoice.InvoiceReference;
            invoicevm.Description = invoice.Description;
            invoicevm.storageID = payment.Id;
            invoicevm.Message = "MONNIFY Payment Invoice generation was successful.";

            return invoicevm;
        }
        public string UpdateInvoiceByMonnify(MonnifyPaymentNotification payment)
        {
            switch (payment.paymentReference.Contains("CKPGRAENUGU"))
            {
                case true:
                    var sfee = _db.DonationStorages.Where(x => x.InvoiceNumber == payment.paymentReference).FirstOrDefault();
                    sfee.HasPaid = true;
                    sfee.TransactionReferenceNumber = payment.transactionReference;
                    sfee.RawPaymentDate = payment.paidOn;
                    sfee.DatePaid = Convert.ToDateTime(sfee.RawPaymentDate);
                    _db.SaveChanges();
                    break;
            }
     //(new Logger()).LogMonnify(new Exception(DateTime.Now.ToString() + ">> Notification For " + payment.paymentReference + "/ " + payment.transactionReference + "\r\n"));
            return "Successful";
        }
        //public List<PaymentVM> GetPayments(int feeID)
        //{
        //    var studentID = Global.AuthenticatedStudentID;
        //    var payments = _db.PaymentRecords.Where(x => x.FeeSetup.FeeID == feeID && x.StudentID == studentID).Select(b => new PaymentVM()
        //    {
        //        Id = b.Id,
        //        AmountPaid = b.AmountPaid,
        //        Fee = b.FeeSetup.Fee.Description,
        //        Installment = b.Installment,
        //        TransactionNumber = b.TransactionNumber,
        //        InvoiceNumber = b.InvoiceNumber,
        //        DatePaid = b.DatePaid,
        //    }).ToList();
        //    return payments;
        //}
        //public List<InvoiceVM> GetInvoiceForReport(int storageID)
        //{
        //    var record = _db.StudentPaymentStorages.Where(x => x.Id == storageID).Select(b => new InvoiceVM()
        //    {
        //        FeeDescription = b.FeeDescription,
        //        InvoiceNumber = b.InvoiceNumber,
        //        AccountNumber = b.VirtualAccountNumber,
        //        BankCode = b.BankCode,
        //        BankName = b.BankName,
        //        Amount = b.AmountToPaid,
        //        StudentClass = _db.ClassManagers.FirstOrDefault(x => x.SessionID == b.SessionID && x.StudentID == b.StudentID).LevelName + " " + _db.ClassManagers.FirstOrDefault(x => x.SessionID == b.SessionID && x.StudentID == b.StudentID).ClassName,
        //        StudentGender = b.Student.Gender.ToString(),
        //        StudentName = b.Student.Surname + " " + b.Student.Firstname + " " + b.Student.Othername,
        //        StudentRegistrationNumber = b.Student.RegistrationNumber,
        //        DateGene = b.PaymentDate,
        //        InvoiceExpDate = b.InvoiceExpiryDate
        //    }).ToList();
        //    foreach (var rec in record)
        //    {
        //        rec.AmountToPaid = "₦" + rec.Amount;
        //        rec.DateGenerated = rec.DateGene.ToString("yyy-MM-dd HH:mm:ss tt");
        //        rec.InvoiceExpiryDate = rec.InvoiceExpDate.ToString("yyy-MM-dd HH:mm:ss tt");
        //    }
        //    return record;
        //}
        //public List<PaymentVM> GetPaymentReciept(int storageID)
        //{
        //    var record = _db.PaymentRecords.Where(x => x.Id == storageID).Select(b => new PaymentVM()
        //    {
        //        TransactionNumber = b.TransactionNumber,
        //        InvoiceNumber = b.InvoiceNumber,
        //        DatePaid = b.DatePaid,
        //        FeeDescription = _db.FeeSetups.FirstOrDefault(x => x.Id == b.FeeSetupID).Fee.Description + "( " + b.Installment + " )",
        //        Session = b.Session.SessionName,
        //        Term = b.Term.TermName,
        //        StudentClass = _db.ClassManagers.FirstOrDefault(x => x.SessionID == b.SessionID && x.StudentID == b.StudentID).LevelName + " " + _db.ClassManagers.FirstOrDefault(x => x.SessionID == b.SessionID && x.StudentID == b.StudentID).ClassName,
        //        StudentGender = b.Student.Gender.ToString(),
        //        StudentName = b.Student.Surname + " " + b.Student.Firstname + " " + b.Student.Othername,
        //        RegistrationNumber = b.RegistrationNumber,
        //        AmountPaid = b.AmountPaid
        //    }).ToList();
        //    foreach (var rec in record)
        //    {
        //        rec.AmountPaidStr = "₦" + rec.AmountPaid;
        //        rec.DatePaidStr = rec.DatePaid.ToString("yyy-MM-dd HH:mm:ss tt");
        //    }
        //    return record;
        //}
    }

}
