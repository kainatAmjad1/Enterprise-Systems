using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectnew.Data;  // Assuming ApplicationDbContext is defined in projectnew.Data

namespace projectnew.Models
{
    public class financerepository
    {
        private readonly ApplicationDbContext _context;

        public financerepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Addfin(finance f)
        {
            _context.finance.Add(f);
            _context.SaveChanges();
        }

        // Method to get all finances
        public List<finance> GetFinances()
        {
            return _context.finance.ToList();
        }
        public void UpdateFinance(finance updatedFinance)
        {
            var existingFinance = _context.finance.Find(updatedFinance.Id);
            if (existingFinance != null)
            {
                existingFinance.category = updatedFinance.category;
                existingFinance.amount = updatedFinance.amount;
                existingFinance.expenseDate = updatedFinance.expenseDate;
                existingFinance.description = updatedFinance.description;
                _context.SaveChanges();
            }
        }

        public void DeleteFinance(finance financeToDelete)
        {
            _context.finance.Remove(financeToDelete);
            _context.SaveChanges();
        }

        public void DeleteFinance(int id)
        {
            var financeToDelete = _context.finance.Find(id);
            if (financeToDelete != null)
            {
                _context.finance.Remove(financeToDelete);
                _context.SaveChanges();
            }
        }

        public void UpdateAll() { }
        public void UpdatefinanceByName(finance f) { }
    }
}


