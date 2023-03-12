using System.Collections.Generic;

public interface ISpellMechanicEffect
{
    public void ApplyEffectToTargets(List<ICharacter> targets);
}