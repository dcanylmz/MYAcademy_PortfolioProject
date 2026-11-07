using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    // İletişim bilgileri tek kayıttır; sitedeki iletişim bölümünü besler.
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ContactList()
        {
            return View(_context.Contacts.FirstOrDefault());
        }

        [HttpGet]
        public IActionResult UpdateContact()
        {
            return View(_context.Contacts.FirstOrDefault() ?? new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateContact(Contact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);

            var existing = _context.Contacts.FirstOrDefault();
            if (existing == null)
            {
                existing = new Contact();
                _context.Contacts.Add(existing);
            }

            existing.Title = contact.Title;
            existing.Description = contact.Description;
            existing.Email = contact.Email;
            existing.Location = contact.Location;
            existing.PhoneNumber = contact.PhoneNumber;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "İletişim bilgileri kaydedildi.";
            return RedirectToAction(nameof(ContactList));
        }
    }
}
