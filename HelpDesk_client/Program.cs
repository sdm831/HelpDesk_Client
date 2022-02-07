using System;
using System.ServiceModel;

namespace HelpDesk_client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите Имя сервера или IP адрес: ");
            var serverAddress = Console.ReadLine();
            Uri tcpUri = new Uri($"http://{serverAddress}:8000/MyService");

            EndpointAddress address = new EndpointAddress(tcpUri);
            BasicHttpBinding binding = new BasicHttpBinding();

            ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(binding, address);

            IMyService service = factory.CreateChannel();

            for (; ; )
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить абонента.");
                Console.WriteLine("2. Показать абонентов.");
                Console.WriteLine("3. Удалить абонента.");
                Console.WriteLine("4. Добавить запрос.");
                Console.WriteLine("5. Показать запросы.");
                Console.WriteLine("6. Удалить запрос.");
                Console.WriteLine();

                var answer = Console.ReadLine();

                if (answer == "1")
                {
                    Console.Write("Введите ФИО абонента: ");
                    var fio = Console.ReadLine();

                    Console.Write("Введите телефон абонента: ");
                    var phone = Console.ReadLine();

                    if (service.AddSubscriber(fio, phone))
                    {
                        Console.WriteLine("Абонент успешно добавлен\n");
                    }
                    else { Console.WriteLine("Ошибка\n"); }

                    continue;
                }

                if (answer == "2")
                {
                    Console.Write("Введите имя или телефон: ");
                    var data = Console.ReadLine();

                    var subscribers = service.TryGetSubscribers(data);

                    Console.WriteLine("Список абонентов:");
                    foreach (var subscriber in subscribers)
                    {
                        Console.WriteLine(subscriber);
                    }
                    Console.WriteLine();

                    continue;
                }

                if (answer == "3")
                {
                    Console.WriteLine("Введите точное ФИО абонента");
                    var fio = Console.ReadLine();

                    if (service.DeleteSubscriber(fio))
                    {
                        Console.WriteLine($"Абонент {fio} удален из БД.\n");
                    }
                    else
                    {
                        Console.WriteLine($"Абонент {fio} не найден.\n");
                    }

                    continue;
                }

                if (answer == "4")
                {
                    Console.Write("Введите тип обращения (1. Phone, 2. Email, 3. SMS): ");
                    var type = Console.ReadLine();
                    
                    Console.Write("Введите ФИО абонента: ");
                    var fio = Console.ReadLine();

                    Console.Write("Введите источник обращения (Телефон или Email): ");
                    var source = Console.ReadLine();

                    Console.Write("Введите текст обращения: ");
                    var text = Console.ReadLine();

                    if (service.AddRequest(type, text, fio, source))
                    {
                        Console.WriteLine("Обращение успешно зарегистрировано\n");
                    }
                    else { Console.WriteLine("Ошибка\n"); }

                    continue;
                }

                if (answer == "5")
                {
                    Console.Write("Для поиска обращения введите ФИО или телефон абонента: ");
                    var input = Console.ReadLine();

                    var requests = service.TryGetRequests(input);
                    
                    foreach(var request in requests)
                    {
                        Console.WriteLine(request);
                    }
                    Console.WriteLine();

                    continue;
                }

                if (answer == "6")
                {
                    Console.WriteLine("Введите Id обращения");
                    var id = Console.ReadLine();

                    if (service.DeleteRequest(id))
                    {
                        Console.WriteLine($"Обращение {id} удалено из БД.\n");
                    }
                    else
                    {
                        Console.WriteLine($"Обращение {id} не найдено.\n");
                    }

                    continue;
                }
            }
        }
    }
}