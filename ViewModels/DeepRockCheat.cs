namespace CEsharp.ViewModels;

using System;
using System.Diagnostics;

using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Windows.ApplicationModel.Background;

public partial class DeepRockCheat:ObservableObject
{

    public Process? process => FindBaseAddress();


    [ObservableProperty]
    public bool isEnable = false;

    [ObservableProperty]
    public bool weapenUmilit = false;

    [ObservableProperty]
    public string infoMsg = "请点击左侧按钮检测是否可以进行注入";
    public DeepRockCheat()
    {

    }


    [RelayCommand]
    public  async  void TestEable()
    {
        InfoMsg = "检测中";
        IsEnable = await Task.Run<bool>(() =>
        {
            var o = 0;
            while (o <1000000000)
            {
                o++;
            }
           return    process is not null;

        });
        if (IsEnable)
        {
            InfoMsg = "检测到 RockDeep";
        }
        else
        {
            InfoMsg = "未检测到RockDeep,请重试";
        }
        //IsEnable = true;

    }
    public void WeapenNoUsed()
    {

        if (IsEnable)
        {
            int offset = 0x166B60B; // 偏移量
            IntPtr baseAddress = process.MainModule?.BaseAddress ?? 0x00000;
            IntPtr targetAddress = IntPtr.Add(baseAddress, offset);
            IntPtr processHandle = process.Handle;
            byte[] buffer = new byte[6]; // 指令长度为 6 字节
            if (ReadProcessMemory(processHandle, targetAddress, buffer, buffer.Length, out int bytesRead))
            {
                Console.WriteLine("读取的原始指令：");
                foreach (byte b in buffer)
                {
                    Console.Write($"{b:X2} ");
                }
                Console.WriteLine();

                // 替换为NOP指令
                byte[] newInstructions = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }; // 6字节NOP指令
                if (WriteProcessMemory(processHandle, targetAddress, newInstructions, newInstructions.Length, out int bytesWritten))
                {
                    Console.WriteLine("指令替换成功！");
                    weapenUmilit = true;
                }
                else
                {
                    Console.WriteLine("指令替换失败。");
                    weapenUmilit=false;
                }
            }
            else
            {
                Console.WriteLine("读取内存失败。");
                weapenUmilit = false;
            }
          
        }
        return;
    }



    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    public Process? FindBaseAddress()
    {
        string processName = "FSD-Win64-Shipping"; // 替换为你的进程名称
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0)
        {
            Console.WriteLine($"未找到名称为 {processName} 的进程。");
            return null;
        }

        return processes[0];


    }
   
}