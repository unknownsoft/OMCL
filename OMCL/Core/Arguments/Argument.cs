using System.Text;

namespace OMCL.Core.Arguments
{
    /// <summary>
    /// 表示一个参数
    /// </summary>
    public abstract class Argument
    {
        /// <summary>
        /// 表示优先级
        /// 在<seealso cref="StringBuilder"/>生成参数时，决定这个参数被加入参数列表的位置
        /// </summary>
        public abstract Priority Priority { get; }
        /// <summary>
        /// 表示这个参数是否有效，如果无效，则<seealso cref="StringBuilder"/>不会将这个参数加入
        /// </summary>
        public abstract bool IsEffective { get; }
        /// <summary>
        /// 这个参数的字符串表示形式
        /// </summary>
        /// <returns>这个参数转换为字符串后的表示形式，前后无需补全空格</returns>
        public abstract override string ToString();
        /// <summary>
        /// 表示这个参数是否带空格。若为false，则<seealso cref="StringBuilder"/>在发现通过<see cref="ToString"/>生成的字符串有空格时，会自动补上引号，否则则不会。
        /// </summary>
        public abstract bool WithSpaces { get; }
    }
}
