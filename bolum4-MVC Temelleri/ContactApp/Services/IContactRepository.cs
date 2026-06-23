using ContactApp.Models;

namespace ContactApp.Services;

public interface IContactRepository
{
    //Contactların Listesi
    IEnumerable<Contact> GetAll();//"Birden fazla Contact nesnesinden oluşan bir koleksiyon."

    //Contact objesi olmayabileceği için NullCheck (Contact?) yaptık
    //"Bu metot bir Contact döndürür, ama isterse null da döndürebilir."
    Contact? GetById(int id);//imza Tanımı
                             //bir metot bildirimi(method declaration).
                             //Interface içinde olduğu için gövdesi({ }) yok.
                             //"Bana bir id ver, ben sana bir Contact döndüreyim. Eğer bulamazsam null döndürebilirim."



    //Geriye Contact Nesnesi döndüren ve Parametre olarak Contact nesnesi alan bir fonksiyon tanımı yaptık aslında
    Contact Add(Contact contact);
    bool Update(Contact contact);
    bool Delete(int id);
}
