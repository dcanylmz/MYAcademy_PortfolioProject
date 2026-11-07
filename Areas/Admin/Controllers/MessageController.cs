using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    // Gelen kutusu: mesaj içeriği düzenlenmez, yalnızca okunma durumu yönetilir.
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        // En yeniden eskiye listeler; gönderen adı veya konuya göre arama ve durum filtresi vardır.
        public IActionResult MessageList(string? search, string status = "all")
        {
            var query = _context.Messages.AsQueryable();

            var term = search?.Trim();
            if (!string.IsNullOrEmpty(term))
                query = query.Where(x => x.SenderName.Contains(term) || x.SenderSubject.Contains(term));

            query = status switch
            {
                "unread" => query.Where(x => !x.IsRead),
                "read" => query.Where(x => x.IsRead),
                _ => query
            };

            ViewBag.Search = term;
            ViewBag.Status = status;
            ViewBag.UnreadCount = _context.Messages.Count(x => !x.IsRead);

            var values = query
                .OrderByDescending(x => x.SendDate)
                .ThenByDescending(x => x.MessageID)
                .ToList();
            return View(values);
        }

        // Modal içeriği için JSON döner (sayfa yenilenmez). Durumu değiştirmez.
        [HttpGet]
        public async Task<IActionResult> MessageDetail(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            return Json(new
            {
                id = message.MessageID,
                senderName = message.SenderName,
                senderEmail = message.SenderEmail,
                senderSubject = message.SenderSubject,
                messageDetail = message.MessageDetail,
                sendDate = message.SendDate.ToString("dd.MM.yyyy HH:mm"),
                isRead = message.IsRead
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetReadStatus(int id, bool isRead)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            message.IsRead = isRead;
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                isRead = message.IsRead,
                unreadCount = _context.Messages.Count(x => !x.IsRead)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                TempData["ErrorMessage"] = "Silinecek mesaj bulunamadı.";
                return RedirectToAction(nameof(MessageList));
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mesaj silindi.";
            return RedirectToAction(nameof(MessageList));
        }
    }
}
