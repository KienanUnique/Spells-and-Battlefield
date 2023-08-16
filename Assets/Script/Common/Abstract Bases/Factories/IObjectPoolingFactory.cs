using Common.Abstract_Bases.Disableable;

namespace Common.Abstract_Bases.Factories
{
    public interface IObjectPoolingFactory: IDisableable
    {
        public void FillPool();
    }
}