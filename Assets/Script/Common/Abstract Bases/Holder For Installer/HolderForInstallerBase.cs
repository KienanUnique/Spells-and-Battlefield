namespace Common.Abstract_Bases.Holder_For_Installer
{
    public abstract class HolderForInstallerBase<T> : IHolderForInstallerSetter<T>, IHolderForInstallerGetter<T>
    {
        public T ItemToHold { get; private set; }

        public void SetItem(T itemToHold)
        {
            ItemToHold = itemToHold;
        }
    }
}