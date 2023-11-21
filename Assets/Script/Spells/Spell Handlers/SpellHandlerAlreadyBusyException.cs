using System;

namespace Spells.Spell_Handlers
{
    public class SpellHandlerAlreadyBusyException : InvalidOperationException
    {
        public SpellHandlerAlreadyBusyException() : base("Spell handler is already busy")
        {
        }
    }
}