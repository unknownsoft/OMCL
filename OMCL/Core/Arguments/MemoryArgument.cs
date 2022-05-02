namespace OMCL.Core.Arguments
{
    /// <summary>
    /// 一个参数，用于限制java虚拟机使用的的内存
    /// </summary>
    public class MemoryArgument : Argument
    {
        public MemoryArgument(int min,int max)
        {
            Minimun = min;
            Maximun = max;
        }
        public override Priority Priority => Priority.First;

        public override bool IsEffective => true;

        public override bool WithSpaces => true;
        /// <summary>
        /// 最小内存堆大小
        /// </summary>
        public int Minimun { get; set; } = 1;
        /// <summary>
        /// 最大内存堆大小
        /// </summary>
        public int Maximun { get; set; } = 1024;
        public override string ToString()
        {
            return string.Format("-Xms{0}m -Xmx{1}m", Minimun, Maximun);
        }
    }
}
