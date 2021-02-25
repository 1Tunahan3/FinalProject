using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.CCS
{
  public  class FileLogger:ILogger
    {
        public void Log()
        {
            Console.WriteLine("dosyaya loglandı");
        }
    }
}
