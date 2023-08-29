namespace Common.Abstract_Bases.Holder_For_Installer
{
    public interface IHolderForInstallerSetter<in T>
    {
        void SetItem(T itemToHold);
    }
}