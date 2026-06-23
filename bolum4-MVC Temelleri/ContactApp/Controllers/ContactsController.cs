using ContactApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    public class ContactsController : Controller
    {
        
        private readonly IContactRepository _repo; //bunun newlenmesi lazım
        private readonly ILogger<ContactsController> _logger;//bunu da newlememiz lazım

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
            return View();
        }
        public IActionResult Create()//rehbere kişi eklemek için
        {
            return View();
        }
        public IActionResult Edit(int id)//var olan kişiyi düzenlemek için
        {
            return View();
        }
        public IActionResult Delete(int id)//kişi silme
        {
            return View();
        }
    }
}
