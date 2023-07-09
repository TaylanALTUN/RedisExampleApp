# RedisExampleApp .NET 7 > RedisExampleApp API Project

-----------------------------------------------------------------------------------------------

<code> docker run -d -p 6379:6379 --name redis-container redis; </code>


- -d  > Run container in background and print container ID    
     (detach : container ayağa kalkınca ona bağlanma, içindeki logları alma )
- -p 6379:6379 > bendeki portu : container içindeki portu
- --name  redis-container > containera verdiğim isim
- redis > imajın hub.dockercomdaki ismi, tag vermediğim için sonuncuyu çekecek

Docker komutları :  **[Docker Docs](https://docs.docker.com/engine/reference/commandline/run/)**

The docker run command creates running containers from images and can run commands inside them. When using the docker run command, a container can run a default action (if it has one), a user specified action, or a shell to be used interactively.


**[Another Redis Desktop Manager  - Download](https://github.com/qishibo/AnotherRedisDesktopManager/releases)**


------------------------------------------------------------------

> Cache projesi Class Library.

>> Class Library projeleri standard oluşturulmalıymış. Standard 2.1 oluşturduk, .net 7 vs oluştursaydık sadece core projelerinde kullanılabilirmiş,  standard olunca xamarin , core, windows her yerde kullanılabilirmiş.

------------------------------------------------------------------

Diyelim ki productlar cache te varsa ordan yoksa dbden gelsn denildi.

- Controller değiştirmemeliyim : SOLID.O-C prensibine ters
- Repository değiştirmemeliyim : SOLID.S prensibine ters


O zmaan imdada Decorator Design Pattern koşuyor : ProductController ProductRepository'i biliyor dbden çalışan o zaman ProductRepositoryWithCache diye yeni bir class implement ederim!   > Önemli : Controller hangi Interface'i biliyorsa!

Normalde : Controller > Service > Repository : yine yeni repo yazardık çnk data çeken yer orası. Redis nosql ve data çekiyor cache olayı o

------------------------------------------------------------------

Olay bu : Design patterns hangi sorunlara çare oluyor ?

- Decorator Design Pattern  : Uygulamanın kodlarını değiştirmeden yeni davranışlar eklemeye imkan verir, cache , log vs eklemek için güzel
- Strategy Design Pattern : runtimeda algoritma değişikliği
- Adapter Design Pattern : bir projeye, varolan yapıyı bozmadan  yeni 3rd party lib nasıl eklerim
- Bridge Design Pattern : tüm dp (23 tane design pattern) mantığını anlamak için : Has-A Is-A
