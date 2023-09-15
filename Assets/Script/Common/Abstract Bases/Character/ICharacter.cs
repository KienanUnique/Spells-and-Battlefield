using Interfaces;

namespace Common.Abstract_Bases.Character
{
    public interface ICharacter : ICharacterInformationProvider
    {
        public void DieInstantly();
    }
}