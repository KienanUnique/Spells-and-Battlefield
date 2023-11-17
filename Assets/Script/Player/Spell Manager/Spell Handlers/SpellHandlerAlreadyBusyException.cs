using System;

namespace Player.Spell_Manager.Spell_Handlers
{
    public class SpellHandlerAlreadyBusyException : InvalidOperationException
    {
        public SpellHandlerAlreadyBusyException() : base("Spell handler is already busy")
        {
        }
    }
}