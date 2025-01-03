using Microsoft.Maui.Controls.PlatformConfiguration;

namespace CEsharp.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

using System.Runtime.InteropServices;
using CEsharp.Common;
using CEsharp.Model.DeepRock;
using CEsharp.Model.DeepRock.View;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class DeepRockCheat : ObservableObject
{
    string processName = "FSD-Win64-Shipping";


    Dictionary<int, DeepRockAddressRecord> codeaddressInfo = new()
    {
        [1] = new DeepRockAddressRecord()
        {
            Codelenth = 6,
            Name = "枪械子弹无限",
            offset = 0x166B60B,
        },
        [2] = new DeepRockAddressRecord()
        {
            Codelenth = 1,
            Name = "无限手雷",
            offset = 0x16A00DE,
        },
        [3] = new DeepRockAddressRecord()
        {
            Codelenth = 1,
            Name = "无限荧光棒",
            offset = 0x13BD5BD,
        },
        [4] = new DeepRockAddressRecord()
        {
            Codelenth = 8,
            Name = "超载失效",
            offset = 0x16C8DDE,
        },

    };

    [ObservableProperty]
    public bool isEnable = false;

    [ObservableProperty]
    public string infoMsg = "请点击左侧按钮检测是否可以进行注入";

    public ObservableCollection<NodeInfo> FuncList { get; set; }

    public DeepRockCheat()
    {
        FuncList = new ObservableCollection<NodeInfo>(codeaddressInfo
            .Select(it => new NodeInfo
            {
                Index = it.Key,
                Info = it.Value.Name,
                IsVisable = false,
                IsRunning = false
            }));
    }

    public void TestEable()
    {
        InfoMsg = "检测中";
        IsEnable = CommonClass.FindBaseAddress(processName) is not null;
        if (IsEnable)
        {
            InfoMsg = "检测到 RockDeep";
        }
        else
        {
            InfoMsg = "未检测到RockDeep,请重试";
        }
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="deepRockAddressRecord"></param>
    public bool Excute(NodeInfo deepRockAddressRecord)
    {
        
        if (IsEnable)
        {
            if (CommonClass.ReplaceNop(CommonClass.FindBaseAddress(processName), codeaddressInfo[deepRockAddressRecord.Index].offset, codeaddressInfo[deepRockAddressRecord.Index].Codelenth))
            {
                deepRockAddressRecord.IsRunning= true;
                deepRockAddressRecord.Info = codeaddressInfo[deepRockAddressRecord.Index].rMsg;
                return true;
            }
            else {
                deepRockAddressRecord.IsRunning = false;
                deepRockAddressRecord.Info = codeaddressInfo[deepRockAddressRecord.Index].eMsg;
            } ;
        }
        return false;
    }







}