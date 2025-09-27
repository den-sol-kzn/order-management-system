**Order Management System**

Тестовое задание для позиции .NET Middle Developer

📋 **Описание проекта**

Прототип системы управления заказами для интернет-магазина. Реализована базовая функциональность для работы с товарами, заказами и аналитикой продаж.

🚀 **Функциональность**

**Основные возможности**

* ✅ Создание и управление товарами (Product)
* ✅ Формирование заказов (Order) с позициями (OrderItem)
* ✅ Автоматический расчет суммы заказа
* ✅ Поиск и фильтрация заказов по дате
* ✅ Аналитика популярных товаров
* ✅ Валидация бизнес-правил
* ✅ Обработка исключений

**Ключевые классы**

* **Product** - товар с ценой и названием
* **Order** - заказ с коллекцией позиций и датой создания
* **OrderItem** - позиция в заказе (товар + количество)
* **OrderService** - сервис для управления заказами
* **OrderRepository** - инкапсулирует всю логику работы с хранилищем заказов
* **IOrderRepository** - абстракция для работы с хранилищем

🛠 **Технологии**

* **.NET 9.0**
* **C# 10+**
* **LINQ**
* **Object-Oriented Programming**
* **Dependency Injection**

📁 **Структура проекта**

order-management-system/  
- Models/  
  + Order.cs  
  + OrderItem.cs  
  + Product.cs  
- Services/  
  + OrderService.cs  
- Repositories/  
  + Interfaces/  
    * IOrderRepository.cs  
  + OrderRepository.cs  
- Program.cs  
- README.md

🏃 **Запуск проекта**

**Способ 1: Dotnet Fiddle**

Код доступен для запуска онлайн:  
[🔗 Запустить на dotnetfiddle.net](https://dotnetfiddle.net/aYefHV)

**Способ 2: Локальный запуск**

1. Клонируйте репозиторий:
   ```bash
   git clone https://github.com/den-sol-kzn/order-management-system.git
2. Перейти в директорию проекта:
   ```bash
   cd order-management-system
3. Запустить проект:
   ```bash
   dotnet run

💻 **Пример использования**

  ```bash
  // Создание сервиса
  var repository = new InMemoryOrderRepository();
  var orderService = new OrderService(repository);

  // Создание товаров
  var laptop = new Product(1, "Laptop", 1000m);
  var mouse = new Product(2, "Mouse", 25m);

  // Создание заказа
  var order = new Order(1);
  order.AddItem(laptop, 1);
  order.AddItem(mouse, 2);

  // Размещение заказа
  orderService.PlaceOrder(order);

  // Получение аналитики
  var topProducts = orderService.GetTopProducts(3);
  var todayOrders = orderService.GetOrdersInRange(DateTime.Today, DateTime.Now);
  ```

🧪 **Демонстрация работы**

Проект включает демонстрационный сценарий в методе Main(), который показывает:

1. Создание товаров и заказов
2. Добавление/удаление позиций
3. Расчет общей суммы
4. Поиск заказов за период
5. Анализ популярных товаров
6. Обработку ошибочных сценариев

📊 **Ключевые особенности реализации**

**Архитектура**

* **Чистая архитектура** с разделением ответственности
* **Dependency Injection** через интерфейсы
* **In-memory хранилище** для простоты демонстрации

**Бизнес-логика**

* **Валидация** входных параметров
* **Обработка исключений** (ArgumentException, InvalidOperationException)
* **LINQ запросы** для сложных операций

👨‍💻 **Автор**

Соловьёв Денис  
ya@den-sol-kzn.ru

---

**Тестовое задание выполнено для ООО "ЭТП"**

---
