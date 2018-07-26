using Autofac.Extras.NLog;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using WebApiUtility.Domain.Contracts;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiConsoleClient.LaguageSupport
{
#warning GodClass. Лучше разделить UI, обработку ошибок и модель
    public class CommandService : ICommandService
    {
        private readonly ISearch search;
        private readonly IUpdate update;
        private readonly ILogger logger;

        public CommandService(ISearch search, IUpdate update, ILogger logger)
        {
            this.search = search;
            this.update = update;
            this.logger = logger;
        }

        public void SelectCommand(string consoleString)
        {
            if (String.IsNullOrWhiteSpace(consoleString))
            {
                Console.WriteLine("Команда не введена");
            }

            if (Regex.IsMatch(consoleString, @"(?:найди объекты класса)\s(.*)$"))
            {
                try
                {
                    var className = Regex.Match(consoleString, @"(?:класса)\s(.*)").Groups[1].Value.ToLower();
                    var classId = Dictionaries.systemObjects[className].ToLower();

                    var result = search.SearchObjectAsync(classId, FilterTypes.Class).Result;
                    foreach (var item in result)
                    {
                        Console.WriteLine($"{item.ObjectId} {item.ObjectName}");
                    }
                }
                catch (ArgumentNullException argumentNull)
                {
                    logger.Fatal("Произошло исключение ArgumentNullException", argumentNull);
                    Console.WriteLine(argumentNull.Message);
                }
                catch (ArgumentException argument)
                {
                    logger.Fatal("Произошло исключение ArgumentException", argument);
                    Console.WriteLine(argument.Message);
                }
                catch (Exception exception)
                {
                    logger.Fatal("Произошло исключение", exception);
                    Console.WriteLine(exception.Message);
                }

            }
            else if (Regex.IsMatch(consoleString, @"(?:найди объекты с атрибутом)\s(.*)$"))
            {
                try
                {
                    var value = Regex.Match(consoleString, @"(?:атрибутом)\s(.*)").Groups[1].Value.ToLower();
                    var attributeId = Dictionaries.attributes[value].ToLower();
                    var result = search.SearchObjectAsync(attributeId, SearchConditionType.Attribute, SearchOperatorType.Exists).Result;
                    foreach (var item in result)
                    {
                        var className = Dictionaries.systemObjects.Where(x => x.Value.ToLower() == item.EntityId.ToLower()).Select(y => y.Key).FirstOrDefault();
                        Console.WriteLine($"{item.ObjectId} {className}");
                        foreach (var attrib in item.ObjectAttributes)
                        {
                            Console.WriteLine($"{attrib.Id} {attrib.AttributeType}");
                        }
                    }
                }
                catch (ArgumentNullException argumentNull)
                {
                    logger.Fatal("Произошло исключение ArgumentNullException", argumentNull);
                    Console.WriteLine(argumentNull.Message);
                }
                catch (ArgumentException argument)
                {
                    logger.Fatal("Произошло исключение ArgumentException", argument);
                    Console.WriteLine(argument.Message);
                }
                catch (Exception exception)
                {
                    logger.Fatal("Произошло исключение", exception);
                    Console.WriteLine(exception.Message);
                }
            }
            else if (Regex.IsMatch(consoleString, @"(?:найдем объекты класса)\s(.*)\s(присвоим атрибуту)\s(.*)\s(значение)\s(.*)$"))
            {
                try
                {
                    var match = Regex.Match(consoleString, @"(?:найдем объекты класса)\s(.*)\s(присвоим атрибуту)\s(.*)\s(значение)\s(.*)$");
                    var objectId = Dictionaries.systemObjects[match.Groups[1].Value].ToLower();
                    var attributeId = Dictionaries.attributes[match.Groups[3].Value].ToLower();
                    var value = match.Groups[5].Value.ToLower();
                    var result = update.UpdateAttributeAsync(objectId, attributeId, value, FilterTypes.Class).Result;

                    Console.WriteLine($"Операция выполнена успешно. Изменеия произведены в {result.Count} объектах");

                }
                catch (ArgumentNullException argumentNull)
                {
                    logger.Fatal("Произошло исключение ArgumentNullException", argumentNull);
                    Console.WriteLine(argumentNull.Message);
                }
                catch (ArgumentException argument)
                {
                    logger.Fatal("Произошло исключение ArgumentException", argument);
                    Console.WriteLine(argument.Message);
                }
                catch (Exception exception)
                {
                    logger.Fatal("Произошло исключение", exception);
                    Console.WriteLine(exception.Message);
                }

            }
            else if (Regex.IsMatch(consoleString, @"(?:найдем объекты класса)\s(.*)\s(присвоим атрибуту)\s(.*)\s(объект)\s(.*)$"))
            {
                try
                {
                    var match = Regex.Match(consoleString, @"(?:найдем объекты класса)\s(.*)\s(присвоим атрибуту)\s(.*)\s(объект)\s(.*)$");
                    var classId = Dictionaries.systemObjects[match.Groups[1].Value.ToLower()].ToLower();
                    var attributeId = Dictionaries.attributes[match.Groups[3].Value.ToLower()].ToLower();
                    var value = match.Groups[5].Value.ToLower();

                    var result = update.UpdateAttributeAsync(classId, attributeId, value, FilterTypes.Class).Result;

                    Console.WriteLine($"Операция выполнена успешно. Изменеия произведены в {result.Count} объектах");

                }
                catch (ArgumentNullException argumentNull)
                {
                    logger.Fatal("Произошло исключение ArgumentNullException", argumentNull);
                    Console.WriteLine(argumentNull.Message);
                }
                catch (ArgumentException argument)
                {
                    logger.Fatal("Произошло исключение ArgumentException", argument);
                    Console.WriteLine(argument.Message);
                }
                catch (Exception exception)
                {
                    logger.Fatal("Произошло исключение", exception);
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine("Плохая команда, сверьте синтаксис");
            }
        }
    }
}
