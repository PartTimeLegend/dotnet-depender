using System;
using System.Reflection;
using ClrTest.Reflection;

namespace Depender.Rules
{
    public class FindStaticMethodCallRule : MethodParserRule
    {
        protected override void DoChecks(MethodBase mehodBeingChecked, MethodBodyInfo methodBody, Dependency parent)
        {
            if (mehodBeingChecked == null) throw new ArgumentNullException("mehodBeingChecked");
            foreach (ILInstruction instruction in methodBody.Instructions)
            {
                var methodInstruction = instruction as InlineMethodInstruction;
                if (methodInstruction == null) continue;
                InlineMethodInstruction line = methodInstruction;
                if (!line.Method.IsStatic) continue;
                var message =
                    string.Format("Static method call {0} on {1}", line.Method.Name,
                                  line.Method.ReflectedType.Name);

                parent.Add(new ProblemDependency(message));
            }
        }
    }
}