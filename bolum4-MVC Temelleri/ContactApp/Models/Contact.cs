namespace ContactApp.Models; //buraya ; koyunca altındaki {} lerden kurtuluyoz sorun çıkmıyo

public class Contact
{
    public int Id { get; set; }
    public String FirstName { get; set; }//bunlar yeni nesil değişkenmiş araştırılacak bunlar
    public String LastName { get; set; }
    public String? Email { get; set; }//? olanlar boş olabilir anlamına geliyor. Nullable
    public String? Phone { get; set; }
    public String? Company { get; set; }
    public String? Title { get; set; }
    public String? Notes { get; set; }
}
