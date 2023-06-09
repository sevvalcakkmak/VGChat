Gazi Üniversitesi 2022 - 2023 Bahar Dönemi Bilgisayar Programlama II (BM-102) Dönem Ödevi

1. Proje Ekibi
Nilüfer Beyza Gülep 22118080014
Said Berk 21118080070
Şevval Çakmak 22118080010

2. Proje İsmi
VGChat – https://vgchat.azurewebsites.net

3. Proje Konusu ve Amacı
Kullanıcıların birbiriyle gerçek zamanlı olarak iletişim kurmasını sağlayan estetik, güvenilir,
kullanımı kolay ve geliştirilebilir bir mesajlaşma platformu oluşturmak. Bu platformu dünyanın
her yerinden erişilebilecek şekilde gerçek dünyaya sunmak. Kullanıcıların güvenlik ve
gizliliklerini koruyacak biçimde, bulut bilişim teknolojilerinin güvenlik ve hız faktörlerini göz
önünde bulundurarak gerçek dünyaya hazır bir uygulama meydana getirmek.

4. Projede Kullanılan Yöntem, Teknik ve Araçlar
Projemizde ASP.NET Core Web App (Model-View-Controller) mimarisini kullanarak bir web
uygulaması geliştirdik. MVC mimarisi, web uygulamalarının sorumluluklarını parçalara ayırarak
bu projemizdeki tasarım düzenimizi oluşturmamızı sağladı. Kısaca ASP.NET Core MVC, üç ana
bileşeni içerir:

Model: Veriye erişimi sağlar, temel verileri yönetir ve kullanıcının arayüzle
etkileşimine yardımcı olur.
View: Kullanıcı arayüzü için HTML, CSS ve JavaScript gibi istemci taraflı
teknolojileri sağlar ve kullanıcının görebileceği içerikler oluşturur. Views sayesinde
Model’den gelen veriler kullanıcıya rahatlıkla gönderilir.
Controller: Model ile View arasında ileişimi sağlar ve kullanıcının taleplerini
karşılar. Modelden verileri alarak istediğimiz View’e göndermemizde yardımcı olur.

Veri tabanı için Microsoft tarafından geliştirilen ve yaygın olarak kullanılan Entity
Framework ’ü ve bununla uyumlu olarak çalışan SQL Server ’ı kullandık. EF, veri tabanı
işlemlerini kolaylaştırmamızı ve nesne odaklı bir şekilde gerçekleştirmemizi sağladı.

MVC projesinin Models klasöründe Message.cs ile veri tabanı modelimizi oluşturduk. Modeli
oluştururken detaylar için data annotations kullandık.

Veri tabanı bağlantısını kurmak için projemizdeki appsettings.json dosyasına connection
string imizi ekledik ve veri tabanımızı adlandırdık.

Bu connection string ve Message modelimiz ile veri tabanında tablo oluşturabilmek için
EF ile DbContext sınıfını kullandık. Burada constructor ve veri tabanına eklemek istediğimiz
Message.cs modelimizin “get;”, “set;” metodlarını oluşturduk. Böylelikle önce kod sonra veri
tabanı yaklaşımını kullandık.

Program.cs dosyasında DbContext’i kullanabilmek için ayarlamalar yaptık, iki tane
DbContext ’i uygulamaya ekledik. (Mesaj işlemleri için ApplicationDbContext ve giriş işlemleri
için eklediğimiz Identity Framework için gerekli olan VGChatAuthDbContext ).

“Kayıt ol” ve “Giriş yap” ekranları için projemize Scaffolded öğesi içinden Identity
kütüphanesini ekleyip DbContext olarak VGChatAuthDbContext.cs ve model olarak
VGChatUser.cs sınıflarını oluşturduk. Scaffolded öğesi bizim için gerekli paketleri yüklemiş oldu.
Ayrıca Login.cshtml ve Register.cshtml dosyalarımızı ve bu dosyalar için _ AuthLayout.cshtml
adında layout dosyası oluşturduk. Bu layout dosyasında gerekli “.cshtml” uzantılı dosyalarımızda
arayüz tasarımını yaptık.

Identity kütüphanesinin bize sağladığı özellikler dışında “FirstName” , “LastName ”
özelliklerini modelimize ekledik ve kayıt ol ekranında bu bilgileri zorunlu veri olarak talep ettik.

Ayrıca kaydolurken; kullanılan mail adresinin geçerli olup olmadığı, şifrenin uzunluğu,
büyük-küçük harf, rakam ve özel karakter içerme durumları kontrol edildi.

Hyperlink yoluyla giriş yapmadan diğer sayfalara erişim gibi bazı güvenlik sorunları
projemizdeki tüm controller’lara eklenen “[Authorize]” tagi sayesinde erişime engellendi.

Böylelikle veri tabanı bağlantılarımızı yaptık ve tüm modellerimizi oluşturduk. Bu
değişiklikleri veri tabanına tablo şeklinde aktarmak için Package Manager Console’a aşağıdaki
komutları yazarak migration dosyaları oluşturduk:

add-migration AddMessagesToDatabase -Context ApplicationDbContext
add-migration AddUsersToDatabase -Context AuthDbContext
Bu migration’ları veri tabanında işlevsel hale getirmek için aşağıdaki komutları kullandık:

update-database -Context ApplicationDbContext
update-database -Context VGChatAuthDbContext
Ayrıca gerçek zamanlı mesajlaşma uygulaması geliştirdiğimiz için SignalR teknolojisi
kullandık. SignalR kütüphanesi ile bütün istemcilerin birbiriyle canlı zamanlı olarak
haberleşmesini ve arayüz üzerinde değişikliklerin görülmesini sağladık.

SignalR kurulumunu yapmak için terminale aşağıdaki komutları girdik:

“npm init -y” ve “npm install @microsot/signalr”
Resim 5 .1: Kurulum sonrası package.json dosyasının içeriği
İstemcilerden aldığımız mesajları diğer kullanıcılara iletmek ve tüm istemcileri birbirine
backend tarafında bağlamak için Hub sınıfından kalıtılan ChatHub.cs sınıfımızı oluşturduk.

SignalR ve ChatHub sınıflarını ekledikten sonra .cshtml dosyasının içinden script tagi açtık ve bu
arayüzdeki SignalR bağlantısını Resim 6 .1’deki kodlar ile oluşturduk.

Javascript kodundaki sendMessage() fonksiyonu ile mesajımızı ve kullanıcı adımızı
gönderdikten sonra bu bilgiler sayfa yenilendiğinde kaybolmaması için veri tabanına
kaydedilmektedir. Canlı gönderilen veriler anlık olarak ekrana bastırılır, mevcut veriler ise veri
tabanından çekilerek sayfada görüntülenir.

Giriş yaptığımız zaman veri tabanında kendimiz dışında kayıtlı olan tüm kullanıcılar ana
sayfada listelenir.

Uygulamamızda estetiği artırmak adına bootstrap.com sitesinden seçtiğimiz css kodlarıyla
temamızı oluşturup kendi seçtiğimiz renk paletleri ile tasarımımızı tamamladık. Logo tasarımını
da ortak beğenilerimiz doğrultusunda kendimiz oluşturduk.

5. Proje Ekibi İş Bölümü

Projemizde giriş ekranını Şevval Çakmak, ana sayfa Nilüfer Beyza Gülep ve mesajlar kısmını
Said Berk yapacak şekilde iş bölümü oluşturduk.

Giriş ekranında öncelikli olarak üye olma kısmı ve oluşturulan şifrenin gerektirdiği şartlar (en
az bir rakam olması, en az bir büyük harf olması ve en az bir alfasayısal karakterin olması), “beni
hatırla” butonu ile girişlerimizin kaydedilmesi ve navbar” kısmındaki ayarlamalar Şevval
Çakmak tarafından yapıldı.

Giriş yaptıktan sonraki kısımda karşımıza çıkan “ana sayfa” kısmında da aktif kullanıcılar
listesinden oluşan tablo, uygulamamızın yapım yılı ve tüm hakların gizliliğini içeren sayfa alt
bilgisi Nilüfer Beyza Gülep tarafından düzenlendi. Mesajlar kısmında scrollbarın ayarlanması,
mesajlar tablosu, mesajların gönderim saati ve tarihi için gerekli fonksiyonlar, boş karakterin
mesaj olarak gönderimini engelleme ve çıkış yap butonu da Said Berk tarafından yapıldı.

Her birimizin yaptığı güncellemeleri takip etmek ve üzerine eklemek amacıyla Git yöntemini
kullandık. Eklediğimiz özellikleri uzak kod repoları(GitHub) vasıtasıyla birbirimizin kullanımına
sunduk.

SQL veri tabanının bağlanması ile ilişkili kodlar, connection strings eklenmesi Said Berk
tarafından yapıldı.

Migrationların yani veri tabanı şemasındaki değişiklikleri denetleyen, bu değişikliklerin veri
tabanına uygulanmasını sağlayan işlemlerin düzenlenmesi de Şevval Çakmak tarafından yapıldı.

Bu sayede atılan mesajlar, kaydolan üyeler gibi değişiklikler veri tabanına aktarılabilecek yapıda
oldu.

Web uygulamalarında gerçek zamanlı iletişimi sağlayan SignalR teknolojisini de ortak bir
şekilde projemize bağladık ve böylece mesajlaşma uygulamamızın temeli atılmış oldu.

6. Proje Veri tabanı (Ayrıntılı Olarak – Veri Tipleri Tablolar Tutulan Kayıtlar)

Projemizde SQL Server kullandık. Oluşturduğumuz veri tabanını “vgchat_db” olarak
adlandırdık.

Resim 7.2’deki tabloda: int türünde “Id”, string türünde “Content”, string türünde “UserName”,
int türünde “GroupId”, TimeStamp türünde “MessageSentTime” değerleri tutulmuştur. Bu
değerler:

“Content” Mesajlaşma ekranından arayüz ile aldığımız kullanıcı mesajlarıdır.
“UserName” değeri Identity ile giriş yapmış kullanıcıların bilgisi.
“GroupId” sonraki versiyonda eklenecek olan özellik için altyapı görevi üstlenen
property.
Microsoft Azure kullanarak VGChat uygulamamızı yayınladık ve herkesin kullanımına açık hale
getirdik. (Erişim için: vgchat.azurewebsites.net)


7. Proje Arayüz-Modül Ekran Görüntüleri ve Açıklamaları
Projemizde arayüz taraımlarımızı Bootstrap kütüphanesi kullanarak yaptık. Bootswatch
ile bir tema seçtik ve temamızın renk paletini özelleştirdik. Ayrıca Resim 9.1 ’deki görüldüğü
gibi özgün bir logo tasarımı yaparak projemizi tamamlamış ve kullanıcılarımıza açık hale
getirmiş olduk.


Internet sitemize girdiğimiz anda kullanıcıları ilk olarak kayıt ve giriş ekranı
karşılamakta, böylelikle kaydolunabilir ya da daha önceden kayıt olunmuş bir hesap ile
giriş yapılabilir. Beni Hatırla özelliği ile her seferinde giriş yapmayı gerektirmeden
mesajlaşabilmeyi sağlar.

Kendimiz hariç tüm diğer kullanıcılarımızı “Ana Sayfa” ekranında listeledik, yeni
kullanıcılarımız dinamik olarak listelenmektedir.

Mesajlar ekranımızda ise mesajın içeriği, saati ve kim tarafından gönderildiği bilgileri
görüntülenmektedir. Kullanıcı dostu bir arayüz ve daha kolay mesajlaşma özelliklerini
sağlayabilmek için bazı JavaScript kodları da yazdık. Bu kodlar sayesinde enter tuşu ile mesaj
gönderme ve mesaj gönderildikten sonra inputumuzun silinmesi, scroll-bar özelliğinin otomatik
olarak aşağıda olması ve null mesaj gönderememe gibi geliştirmeler yaptık.

Sağ üstteki “Seçenekler” butonu kullanılarak mevcut hesaptan çıkış yapılır.

8. Sonuç ve Katkılar

VGChat sayesinde C# dilini ASP.NET kütüphanesi ile birlikte kullanarak web
uygulamaları geliştirmekte kullanmayı deneyimlemiş olduk. MVC(Model-View-Controller)
mimarisinin avantajlarını projemizde uyguladık.

Bununla birlikte SQL veri tabanı üzerinde CRUD işlemleri yürütmek, CodeFirst
yaklaşımıyla ve Entity Framework ile veri yönetimi yapmak, Identity framework ile oturum ve
yetkilendirme işlemlerini yönetmek, veri tabanı migrasyonu ve güncellenmesi konseptleriyle
back-end dünyasına ilk adımı da atmış olduk.

Html, css, bootstrap, JavaScript, JQuery, Adobe Photoshop teknolojileri kullanarak göze
hitap eden tasarımlar oluşturmaya çalıştık ve front-end dünyasına ilk adımı atmış olduk.

SignalR ile çalışma zamanında gerçek zamanlı proje geliştirmeyi ve web sockets
temellerini almış olduk.

Data annotation, razor runtime compilation, entity framework, identity framework
kütüphane/frameworklerini projemize nasıl entegre edeceğimizi ve NuGet paketlerini nasıl
yönetebileceğimizi öğrenmiş olduk.

Tag helpers, razor syntax gibi kod okumayı/yazmayı kolaylaştıracak yöntemlerden de bu
proje sayesinde haberdar olmuş olduk.

Microsoft Azure ile ilk defa dünyaya bir proje açtık ve bulut bilişim evrenine girmiş
olduk.

Git ve GitHub kullanarak eşli programlama yeteneklerimizi ve iş birliğimizi güçlendirdik.

Gerçek dünyada takip edilen proje geliştirme yöntemlerine ve ürünlerine yakın bir
deneyim elde etmek için çaba sarfettik. Hepimiz için kıymetli bir tecrübe, güzel bir hatıra,
portfolyomuza koyabileceğimiz bir proje ve GitHub repository’si elde etmiş olduk.
