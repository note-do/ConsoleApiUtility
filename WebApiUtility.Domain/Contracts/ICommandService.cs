using System.Threading.Tasks;

namespace WebApiUtility.Domain.Contracts
{
    public interface ICommandService
    {
        /// <summary>
        /// Метод выбора комманды
        /// </summary>
        /// <param name="consoleString">Строка из интерфейса программы</param>
        void SelectCommand(string consoleString);

    }
}