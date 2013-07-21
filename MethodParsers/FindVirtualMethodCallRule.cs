using System;
using System.Reflection;
using System.Text.RegularExpressions;
using ClrTest.Reflection;
using Depender.Tests;

namespace Depender.Rules
{
    public class FindVirtualMethodCallRule : MethodParserRule
    {
        protected override void DoChecks(MethodBase mehodBeingChecked, MethodBodyInfo methodBody, Dependency parent)
        {
            if (mehodBeingChecked == null) throw new ArgumentNullException("mehodBeingChecked");
            foreach (ILInstruction instruction in methodBody.Instructions)
            {
                if (!(instruction is InlineMethodInstruction)) continue;
                InlineMethodInstruction line = instruction as InlineMethodInstruction;
                if (line.Method.IsVirtual)
                {
                    parent.Add(new VirtualMethodCallDependency(line.Method.ReflectedType.Name, line.Method.Name));

                }
            }
        }
    }
}