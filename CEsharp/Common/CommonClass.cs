using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CEsharp.Common
{
    public static class CommonClass
    {
        public static Process? FindBaseAddress(string processName)
        {

            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {

                return null;
            }

            return processes[0];
        }
        public static bool ReplaceNop(Process process, int offset, int CodeLenth)
        {
            bool result = false;
            if (process is null)
            {
                return result;
            }
            try
            {
                byte[] nop = new byte[CodeLenth];
                nint baseAddress = process.MainModule?.BaseAddress ?? 0x00000;
                nint targetAddress = nint.Add(baseAddress, offset);
                nint processHandle = process.Handle;
                if (ReadProcessMemory(processHandle, targetAddress, nop, nop.Length, out int bytesRead))
                {
                    // 替换为NOP指令
                    for (int i = 0; i < CodeLenth; i++)
                    {
                        nop[i] = 0x90;
                    }
                    if (WriteProcessMemory(processHandle, targetAddress, nop, nop.Length, out int bytesWritten))
                    {
                        result = true;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(nint hProcess, nint lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(nint hProcess, nint lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
    }
}
