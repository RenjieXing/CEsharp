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
    public string infoMsg = "������ఴť����Ƿ���Խ���ע��";
    public DeepRockCheat()
    {

    }


    [RelayCommand]
    public  async  void TestEable()
    {
        InfoMsg = "�����";
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
            InfoMsg = "��⵽ RockDeep";
        }
        else
        {
            InfoMsg = "δ��⵽RockDeep,������";
        }
        //IsEnable = true;

    }
    public void WeapenNoUsed()
    {

        if (IsEnable)
        {
            int offset = 0x166B60B; // ƫ����
            IntPtr baseAddress = process.MainModule?.BaseAddress ?? 0x00000;
            IntPtr targetAddress = IntPtr.Add(baseAddress, offset);
            IntPtr processHandle = process.Handle;
            byte[] buffer = new byte[6]; // ָ���Ϊ 6 �ֽ�
            if (ReadProcessMemory(processHandle, targetAddress, buffer, buffer.Length, out int bytesRead))
            {
                Console.WriteLine("��ȡ��ԭʼָ�");
                foreach (byte b in buffer)
                {
                    Console.Write($"{b:X2} ");
                }
                Console.WriteLine();

                // �滻ΪNOPָ��
                byte[] newInstructions = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 }; // 6�ֽ�NOPָ��
                if (WriteProcessMemory(processHandle, targetAddress, newInstructions, newInstructions.Length, out int bytesWritten))
                {
                    Console.WriteLine("ָ���滻�ɹ���");
                    weapenUmilit = true;
                }
                else
                {
                    Console.WriteLine("ָ���滻ʧ�ܡ�");
                    weapenUmilit=false;
                }
            }
            else
            {
                Console.WriteLine("��ȡ�ڴ�ʧ�ܡ�");
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
        string processName = "FSD-Win64-Shipping"; // �滻Ϊ��Ľ�������
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0)
        {
            Console.WriteLine($"δ�ҵ�����Ϊ {processName} �Ľ��̡�");
            return null;
        }

        return processes[0];


    }
   
}