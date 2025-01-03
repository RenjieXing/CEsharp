using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEsharp.Model.DeepRock
{
    public class DeepRockAddressRecord
    {
        /// <summary>
        /// 相对偏移地址
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 执行正确的消息
        /// </summary>
        public string rMsg => Name + "开启成功,快进游戏试试吧";
        /// <summary>
        /// 执行错误的消息
        /// </summary>
        public string eMsg => Name + "开启失败,请重试";
        /// <summary>
        /// 指令长度
        /// </summary>
        public int Codelenth { get; set; }
    }
}
