using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CEsharp.Model.DeepRock.View
{
    public  partial class NodeInfo: ObservableObject
    {


        public int Index { get; set; }  
        /// <summary>
        /// 是否可见和可执行
        /// </summary>
        [ObservableProperty]
        public bool isVisable;
        /// <summary>
        /// 功能名称
        /// </summary>
        [ObservableProperty]
        public string funcName;
        /// <summary>
        /// 提示字段
        /// </summary>
        [ObservableProperty]
        public string info;
        /// <summary>
        /// 是否正在运行
        /// </summary>
        [ObservableProperty]
        public bool isRunning = false;
    }
}
