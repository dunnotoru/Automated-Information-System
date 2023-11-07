using System.Reflection;

namespace Contract
{
    public interface IShell
    {
        IList<ShellMenuItem> MenuItems { get; }
        IModule LoadModule(Assembly assembly);
    }
}
