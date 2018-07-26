using System;
using WebApiConsoleClient.Contracts;
using WebApiUtility.Domain.Contracts;

namespace WebApiConsoleClient
{
#warning Спагетти-код. Лучше использовать MVC
    public class ApplicationStart: IApplication
    {
        private readonly ICommandService commandServive;

        public ApplicationStart()
        {

        }
        public ApplicationStart(ICommandService commandServive)
        {
            this.commandServive = commandServive;
        }
        public void Start()
        {
            while (true)
            {
                Console.Write("Введите команду : ");
                var str = Console.ReadLine();
                commandServive.SelectCommand(str);
            }
        }
    }
}
