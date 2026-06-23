using ContactApp.Models;

namespace ContactApp.Services;

public class InMemoryContactRepository : IContactRepository
{
    private readonly List<Contact> _contacts;//bunu kullanabilmek için Constructor'da Newledik
    private int _nextId = 1;
    public InMemoryContactRepository()
    {
        _contacts = new List<Contact>();

        //Uygulama üzerinde doğrudan sonuç almak için SEED DATA Ekliyoruz yani bikaç tane default kullanıcı ekliyoz uygulamayı test ederken görünsün falan diye

        //Seed Data
        var seed = new List<Contact>()
        {
            //new(){FirstName="Ahmet".......} Şeklinde de yazılabilir aynı bokun laciverti
            new Contact(){FirstName="Hasan", LastName="Comak", Email="hasan.comak@example.com", Phone="+905551112233", Company="Comak Yazilim A.Ş.", Title="Yazılım Geliştirme Uzmanı", Notes=".Net"},
            new Contact(){FirstName="Ugurcan", LastName="Agbaba", Email="ugurcan.agbaba@example.com", Phone="+905552224455", Company="Agbaba Yazilim A.Ş.", Title="Bilgi Islem Personeli", Notes="ADO.Net"},
            new Contact(){FirstName="Salih", LastName="Dayıoglu", Email="salih.dayioglu@example.com", Phone="+905553336677", Company="Dayioglu Yazilim A.Ş.", Title="Yazılım Geliştirme Uzmanı", Notes=".Net"},
            new Contact(){FirstName="Volkan", LastName="Aslan", Email="volkan.aslan@example.com", Phone="+905554448899", Company="Aslan Yazilim A.Ş.", Title="Elektrik Teknisyeni", Notes="Topraklama Uzmanı"},
            new Contact(){FirstName="Mert", LastName="Tumel", Email="mert.tumel@example.com", Phone="+905556661232", Company="Tumel CNC A.Ş.", Title="CNC Geliştirme Uzmanı", Notes="CNC"},
            new Contact(){FirstName="Sefa", LastName="Gürbüz", Email="sefa.gurbuz@example.com", Phone="+905559994556", Company="Gurbuz Buro A.Ş.", Title="Buro Uzmanı", Notes="Buro"},
            new Contact(){FirstName="Yigit", LastName="Coban", Email="yigit.coban@example.com", Phone="+90357278734", Company="Coban Games A.Ş.", Title="3D Model Uzmanı", Notes="3D Artist "}
        };

        //IDleri Tanımladık
        foreach(var c in seed)
        {
            c.Id = _nextId++;
            _contacts.Add(c);//Listeye Kullanıcıyı ekledi
        }
    }

    public Contact Add(Contact contact)
    {
        contact.Id = _nextId++; //parametreden gelen Contact'a bir Id ataması yaptık
        _contacts.Add(contact);//Parametreden gelen Contactı(yani kişiyi işte amk) Listeye ekledik
        return contact;//Geriye Contactı Döndürdü. Yani Idsi olmayan bi kullanıcı geldi ve ona bir Id atayıp o Contactı geri Döndürdü
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);//Girilen Idye bağlı bir kayıt var mı diye kontrol ediyo varsa existing'e atıyo
        if(existing is null) //Eğer Existing nullsa false döndürüyo yani öyle bi kayıt yoksa false döndürür çünkü silemezsin
            return false;
        _contacts.Remove(existing);//burası da else yerine yazıldı yani existing null değilse listeden o kaydı siliyo ve true döndürüyo
        return true;
    }


    //ANASININ AMI AMINAKOYİM BUNLAR NEYMİŞ BİZ C# ÖĞRENMEMİŞİZ
    //Contactları sıralı şekilde döndürüyo "c" temsili bi takma ad foreach'te olduğu gibi. Soy isme göre sıralıyo eğer soy isim aynıysa isme göre sıralıyo 
    //Contactın içinden c nesnesi alıyo direk
    public IEnumerable<Contact> GetAll() => 
        _contacts
                .OrderBy(c  => c.LastName)
                .ThenBy(c => c.FirstName);


    public Contact? GetById(int id) =>
        _contacts.FirstOrDefault(c => c.Id.Equals(id)); //Contact içinde ilk gördüğü kaydı döner (FirstOrDefault)
                                                        //c => c.Id.Equals(id)   Contactın idsi parametreden gelen Id'ye eşit olacak

    public bool Update(Contact contact)
    {
       var existing = GetById(contact.Id);
        if (existing is null)
            return false;

        existing.FirstName = contact.FirstName;
        existing.LastName = contact.LastName;
        existing.Email = contact.Email;
        existing.Phone = contact.Phone;
        existing.Company = contact.Company;
        existing.Notes = contact.Notes;
        existing.Title = contact.Title;
        return true;
    }
}
