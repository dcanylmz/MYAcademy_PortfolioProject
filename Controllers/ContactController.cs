using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        // Ana sayfadaki iletişim formu bu action'a fetch ile (sayfa yenilenmeden) gönderilir.
        // Bind: SendDate, IsRead, MessageID gibi alanlar formdan gelemez (overposting koruması).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(
            [Bind("SenderName,SenderEmail,SenderSubject,MessageDetail")] Message message,
            string? website)
        {
            // Honeypot: gerçek kullanıcı görmediği bu alanı boş bırakır, botlar doldurur.
            // Bot'a hata göstermeden başarılı gibi yanıt verip kaydı atlıyoruz.
            if (!string.IsNullOrWhiteSpace(website))
                return Ok(new { success = true, message = "Mesajınız alındı." });

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value!.Errors.First().ErrorMessage);

                return BadRequest(new
                {
                    success = false,
                    message = "Lütfen işaretli alanları kontrol edin.",
                    errors
                });
            }

            message.SenderName = message.SenderName.Trim();
            message.SenderEmail = message.SenderEmail.Trim();
            message.SenderSubject = message.SenderSubject.Trim();
            message.MessageDetail = message.MessageDetail.Trim();
            message.SendDate = DateTime.Now;
            message.IsRead = false;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Mesajınız alındı, en kısa sürede size dönüş yapacağım." });
        }
    }
}
