using System.Reflection;
using Depender.Rules;

namespace Depender
{
    class FindOverridableMethodsRule:IRule
    {
        public void Check(MethodInfo info, Dependency parent)
        {
            if (info.IsFinal || (!info.IsVirtual && !info.IsAbstract)) return;
            parent.Add(new Dependency(string.Format(" {0}() can be overriden", info.Name)));
        }

        public bool CanCheck(object obj)
        {
            var info = obj as MethodInfo;
            return info != null;
        }

        public void Check(object obj, Dependency parent)
        {
            if (obj == null) return;
            Check(obj as MethodInfo, parent);
        }
    }
}
