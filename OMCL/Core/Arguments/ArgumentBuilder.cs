using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCL.Core.Arguments
{
    /// <summary>
    /// Java启动参数的生成器
    /// </summary>
    public class ArgumentBuilder : IReadOnlyList<Argument>
    {
        private List<Argument> members = new List<Argument>();
        public ArgumentBuilder()
        {
            
        }
        /// <summary>
        /// 生成参数
        /// </summary>
        /// <returns>生成的参数</returns>
        public override string ToString()
        {
            List<string> arguments = new List<string>();
            Priority priority = Priority.First;
            while (priority < Priority.Tail)
            {
                foreach (var argument in this)
                {
                    if (!argument.IsEffective) continue;
                    if (argument.Priority == priority)
                    {
                        if (argument.WithSpaces)
                        {
                            arguments.Add(argument.ToString());
                        }
                        else
                        {
                            string arg = argument.ToString();
                            if(arg.Contains(' '))
                            {
                                arg = "\"" + arg + "\"";
                            }
                            arguments.Add(arg);
                        }
                    }
                }
                priority++;
            }
            return String.Join(" ", arguments.ToArray());
        }
        public Argument this[int index] => members[index];

        public int Count => members.Count;

        public IEnumerator<Argument> GetEnumerator() => members.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
