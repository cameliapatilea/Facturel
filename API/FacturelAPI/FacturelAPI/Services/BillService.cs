using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacturelAPI.Helpers;
using FacturelAPI.Models.Bills;
using System.Runtime.Serialization;


namespace FacturelAPI.Services
{
    public interface IBillService
    {
        IEnumerable<Bill> GetAll();
        Bill GetById(int id);
        Bill AddBill(Bill bill);
        Bill UpdateBill(Bill billParam);
        void Delete(int id);
    }
    public class BillService: IBillService
    {
        private DataContext _context;

        public BillService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Bill> GetAll()
        {
            return _context.Bills;
        }

        public Bill GetById(int id)
        {
            return _context.Bills.Find(id);
        }
        public Bill AddBill(Bill bill)
        { 
            if(string.IsNullOrWhiteSpace(bill.Name))
                throw new AppException("The name of the bill is required!");
            if(bill.Amount < 0)
                throw new AppException("The amount of money that is to be paid cannot be less than 0!");
            
            if (bill.DueDate < default(DateTime))
                throw new AppException("The due date cannot be before today's date!");
            if(string.IsNullOrWhiteSpace(bill.IssuedBy))
                throw new AppException("You must enter the name of company that issued the bill!");


            _context.Bills.Add(bill);
            _context.SaveChanges();

            return bill;
        }
        public Bill UpdateBill(Bill billParam)
        {
            var bill = _context.Bills.Find(billParam.Id);
            if(bill == null)
                throw new AppException("Bill not found!");

            

            if (!string.IsNullOrWhiteSpace(billParam.Name) && billParam.Name != bill.Name)
            {
                bill.Name = billParam.Name;
            }

            if (!string.IsNullOrWhiteSpace(billParam.IssuedBy) && billParam.IssuedBy != bill.IssuedBy)
            {
                bill.IssuedBy = billParam.IssuedBy;
            }

            if (billParam.Amount > 0 && (billParam.Amount != bill.Amount))
            {
                bill.Amount = billParam.Amount;
            }

            if (billParam.IssuedDate != bill.IssuedDate)
            {
                bill.IssuedDate = billParam.IssuedDate;
            }
            if(billParam.DueDate > default(DateTime) && billParam.DueDate != bill.DueDate)
            {
                bill.DueDate = billParam.DueDate;
            }

            _context.Bills.Update(bill);
            _context.SaveChanges();
            return bill;
        }

        public void Delete(int id)
        {
            var bill = _context.Bills.Find(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                _context.SaveChanges();
            }
        }
    }
}
