# CoffeeVendingProject - Kahve Otomat Sistemi
![coffeeVendingPage](https://github.com/Enesphlvn/CoffeeVendingBackend/assets/98655108/58621315-2eb3-42d9-b261-c29a122d98d6)

## :pushpin:Getting Started  
Kahve otomatları ve kahve satışı yapan iş yerleri için tasarlanmış bu projede CRUD işlemlerini içeren bir yapı bulunmaktadır.

- Orm aracı olarak `Entity Framework`, veri tabanı olarak `PostgreSQL` kullanılmıştır.
- Projeyi başlatmadan önce `appsettings.json` dosyasındaki `ConnectionString` bilgilerinin doğruluğunu kontrol etmek ve uygun şekilde düzenlemek önerilir.
- Proje ilk çalıştırıldığında otomatik migration işlemi gerçekleştirilecek ve ilgili tablolar otomatik olarak oluşturularak verilerle doldurulacaktır.  

## :computer: Kullanılan Teknolojiler  
- Newtonsoft.Json
- EntityFrameworkCore
- Autofac
- Automapper
- FluentValidation
- PostgreSQL
- Jwt

## :rocket: API Kullanımı  
***Proje ihtiyaçlarına uygun olarak endpointler ve açıklamaları aşağıda bulunmaktadır:***

## Ürün Listeleme
- **HTTP Method:** `GET`
- **Endpoint:** `/api/products/getall`
- **Açıklama:** Bu API, sistemdeki tüm ürünleri getirir ve yalnızca `IsStatus` özelliği true olanları içerir. Herhangi bir parametresi yoktur.

## İçerik Id'ye Göre Ürün Listeleme
- **HTTP Method:** `GET`
- **Endpoint:** `/api/products/getproductsbygeneralcontentid`
- **Açıklama:** Bu API, belirli bir içerik Id'sine sahip olan ürünleri getirir.

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `generalContentId` | `int` | İşlem yapılacak içerik ID'si |

## Sipariş Oluşturma
- **HTTP Method:** `POST`
- **Endpoint:** `/api/orders/add`
- **Açıklama:** Bu API, yeni bir sipariş oluşturmak için kullanılır ve `token` zorunludur.

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `userId` | `int` | İşlemi yapan kullanıcının ID'si |
| `productId` | `int` | İşlem yapılan ürünün ID'si |
| `amountPaid` | `int` | Ürün için ödenecek tutar |

## Yeni Kullanıcı Oluşturma
- **HTTP Method:** `POST`
- **Endpoint:** `/api/auth/register`
- **Açıklama:** Bu API, yeni bir kullanıcı hesabı oluşturmak için kullanılır.

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `firstName` | `string` | Yeni kullanıcı adı |
| `lastName` | `string` | Yeni kullanıcı soyadı |
| `email` | `string` | Yeni kullanıcı mail adresi |
| `password` | `string` | Yeni kullanıcı şifresi |

## Kullanıcı Girişi
- **HTTP Method:** `POST`
- **Endpoint:** `/api/auth/login`
- **Açıklama:** Bu API, kullanıcılara sisteme giriş yapma imkanı tanır. Eğer kullanıcının `email` ve `password` alanları doğru ise, API tarafından token içeren bir response model döner.

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `email` | `string` | Kullanıcı mail adresi |
| `password` | `string` | Kullanıcı şifresi |

## İstatistik Listeleme
- **HTTP Method:** `GET`
- **Endpoint:** `/api/statistics/getall`
- **Açıklama:** Bu API, sistemde bulunan istatistik verilerini getirir. Yalnızca `admin` yetkisine sahip kullanıcılar tarafından kullanılabilir, bu nedenle `token` zorunludur.

## :camera: ScreenShots  
***Projenin çalışma anından görüntüler:***  
<br>![login](https://github.com/Enesphlvn/CoffeeVendingBackend/assets/98655108/3e2a08f0-766b-46d4-8575-ac2111cb4ed2)
<br>![homePage](https://github.com/Enesphlvn/CoffeeVendingBackend/assets/98655108/f7073840-041a-4147-af64-f9d2ef231f14)
<br>![generalContent](https://github.com/Enesphlvn/CoffeeVendingBackend/assets/98655108/412360d2-053f-4378-beb3-58f6083db950)
<br>![pay](https://github.com/Enesphlvn/CoffeeVendingBackend/assets/98655108/4ef23aba-5d45-4cbb-98bd-288ae097c251)
<br>![adminPanel](https://github.com/Enesphlvn/CoffeeVendingBackend/assets/98655108/174b4f0e-585b-4c4c-bab0-f98f6bdce745)
<br>![statistics](https://github.com/Enesphlvn/CoffeeVendingBackend/assets/98655108/95bc236d-4443-4c49-9091-8a01c7fdd495)

## :pencil2:Authors  
***Enes Pehlivan*** - [EnesPehlivan](https://github.com/Enesphlvn)
