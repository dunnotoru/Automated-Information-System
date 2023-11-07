using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTest
{
    public class ModuleImplementation : IModule
    {
        private readonly IShell _shell;
        private readonly TestViewModel _testViewModel;

        public ModuleImplementation(IShell shell, TestViewModel testViewmodel)
        {
            _shell = shell;
            _testViewModel = testViewmodel;
        }

        public void Initialize()
        {
            _shell.MenuItems.Add(new ShellMenuItem() { Caption = "First", ScreenViewModel = _testViewModel });
        }
    }
}
