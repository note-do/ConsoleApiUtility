using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiConsoleClient.Contracts
{
    public interface IApplication
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        void Start();
    }
}
