using ContactApp.Models;
using ContactApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    public class ContactsController : Controller
    {

        private readonly IContactRepository _repo; //bunun newlenmesi lazım
        private readonly ILogger<ContactsController> _logger;//bunu da newlememiz lazım //bu dalga console'a log basıyo amk

        //Constructor Injection yapmış olduk
        public ContactsController(IContactRepository repo, ILogger<ContactsController> logger)//o yüzden bu sınıfın Constructorunda newleme işlemi yaptık
        {
            _repo = repo;
            _logger = logger;
        }

        //Dependency Injection - Resolve
        public IActionResult Index()//index tanımı default geldi
        {
            var items = _repo.GetAll();
            return View(items.ToList());//Burda Controller üzerinden View'e veri gönderdim
        }
        public IActionResult Details(int id)//endpoint tanımı; Bir kişinin idye bağlı olarak gelecek olan bilgileri...
        {
            var contact = _repo.GetById(id);
            if (contact is null)
                return NotFoundView();

            ViewData["Title"] = "Kişi Güncelle";
            return View(contact);
        }

        private IActionResult NotFoundView()//Burası private olduğu için urlye direk bunu yazarak ulaşamıyoruz
        {
            Response.StatusCode = 404;
            ViewData["Title"] = "Bulunamadı";
            ViewBag.Message = "Kişi Bulunamadı";
            return View("NotFound");
        }

        [HttpGet("create")]//get işlemi için
        public IActionResult Create()//rehbere kişi eklemek için
        {
            ViewData["Title"] = "Yeni Kişi";
            return View(new Contact());
        }

        [HttpPost("create")]//post işlemi için
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)//rehbere kişi eklemek için
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Yeni Kişi";
                return View(contact);
            }
            _repo.Add(contact);
            _logger.LogInformation($"Kişi Eklendi: {contact.Id} {contact.FirstName} {contact.LastName}");
            TempData["Success"] = "Kayıt Eklendi";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]//süslü parantez içerisindeki kısım ROUTEDATA çünkü bunu url üzerinden alıcaz
        public IActionResult Edit(int id)//var olan kişiyi düzenlemek için
        {
            var contact = _repo.GetById(id);
            if (contact is null)
                return NotFoundView();
            ViewData["Title"] = "Kişi Görüntüleme";
            return View(contact);//kişiyi web sayfasına yani view'a gönderdik
        }

        [HttpPost("edit/{id}")]//idye göre çalışcak yani
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz istek!");
            }
            if (!ModelState.IsValid) 
            {
                ViewData["Title"] = "Kişi Güncelle";
                return View(contact);
            }

            var ok = _repo.Update(contact);

            if (!ok)
                return NotFoundView();

            _logger.LogInformation($"Kişi Güncellendi. Id: {contact.Id} {contact.FirstName} {contact.LastName}");
            TempData["Success"] = "Kayıt Güncellendi!";//_Layout.cshtml içinde var eğer Success doluysa şu şu mesajı göster anlamında

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)//kişi silme
        {
            var contact = _repo.GetById(id);
            if (contact is null)
                return NotFoundView();

            ViewData["Title"] = "Silme Onayı";
            return View(contact);
        }

        //burda ActionName kullandık çünkü üstteki fonskiyonla çakışıcaktı çünkü aynı parametreyeler sahipler diğerlerinde farklı parametrelere sahipler
        [HttpPost("delete/{id}")]
        [ActionName("Delete")]//üsttekiyle çakışmasın diye bunu yazdık ama nasıl çalışıyo idk
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)//kişi silme onay
        {
            var ok = _repo.Delete(id);
            if(!ok)
                return NotFoundView();

            TempData["Success"] = "Kayıt Silindi";//bununla galiba _Layout.cshtml'e mesaj gönderiyoz o da tarayıcıda mesaj atıyo

            return RedirectToAction(nameof(Index));
        }
    }
}
