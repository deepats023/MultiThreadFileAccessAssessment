using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadFileAccess.Interfaces
{
    /// <summary>
    /// File writer interface can be enhanced further as we may want different behaviors when writing to file
    /// </summary>
    public interface IFileWriter
    {
        void WriteFirstLine(string line);
        void WriteLine(int threadId);
    }
}
