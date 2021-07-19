using Prism.Mvvm;
using Prism.Navigation;

namespace SqlProject.CoreLibrary.Mvvm
{
    public class DestructibleBase : BindableBase, IDestructible
    {
        public virtual void Destroy()
        {

        }
    }
}
