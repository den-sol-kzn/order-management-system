using order_management_system.Models;
using order_management_system.Services;

namespace order_management_system;

public class Program
{
    public static async Task Main()
    {
        try
        {
            // 1. Инициализация сервиса и товаров
            var orderService = new OrderService();
            var monoBlock = new Product(1, "Моноблок", 27800m);
            var mouse = new Product(2, "Компьютерная мышь", 1450m);
            var keyboard = new Product(3, "Клавиатура", 2325m);

            // 2. Создание и заполнение заказа
            var order = new Order(1);
            order.AddItem(monoBlock, 1);
            order.AddItem(mouse, 2);
            order.AddItem(keyboard, 1);
            order.AddItem(mouse, 1);

            Console.WriteLine($"Общая сумма заказа: {order.GetTotalAmount():N2} руб.");

            // 3. Размещение заказа
            orderService.PlaceOrder(order);
            Console.WriteLine("Заказ успешно размещен.");

            // 4. Создаем и размещаем второй заказ
            var order2 = new Order();
            order2.AddItem(monoBlock, 2);
            orderService.PlaceOrder(order2);

            // 5. Обновляем количество товара
            order2.UpdateItemQuantity(monoBlock.Id, 4);

            // 6. Получаем заказы за сегодня
            var today = DateTime.Today;
            var recentOrders = orderService.GetOrdersInRange(today, today.AddDays(1));
            Console.WriteLine($"\nРазмещено заказов сегодня: {recentOrders.Count}");

            // 7. Получаем топ-2 товара
            var topProducts = orderService.GetTopProducts(2);
            Console.WriteLine("\nТОП 2 среди товаров:");

            foreach (var (nameProduct, totalQuantity) in topProducts)
            {
                Console.WriteLine($"- {nameProduct} ({totalQuantity} шт.)");
            }

            // 8. Демонстрация ошибки - попытка разместить пустой заказ
            var emptyOrder = new Order(3, today);
            orderService.PlaceOrder(emptyOrder);

            Console.WriteLine("\nНажмите Enter для выхода...");
            await Task.Run(Console.ReadLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Environment.Exit(1);
        }
    }
}