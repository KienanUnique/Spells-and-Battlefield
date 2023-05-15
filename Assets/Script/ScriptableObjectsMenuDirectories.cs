public static class ScriptableObjectsMenuDirectories
{
    private const string RootDirectory = "Spells and Battlefield/";

    #region Spells

    public const string SpellSystemDirectory = RootDirectory + "Spell System/";

    public const string SpellAppliersDirectory = SpellSystemDirectory + "Spell Appliers/";
    public const string SpellObjectsProvidersDirectory = SpellSystemDirectory + "Spell Objects Providers/";
    public const string SpellMechanicDirectory = SpellSystemDirectory + "Mechanic/";
    public const string SpellMovementDirectory = SpellSystemDirectory + "Movement/";
    public const string SpellTargetSelectorDirectory = SpellSystemDirectory + "Target Selector/";
    public const string SpellTriggerDirectory = SpellSystemDirectory + "Trigger/";

    #endregion


    #region Settings

    public const string SettingsDirectory = RootDirectory + "Settings/";

    public const string GeneralSettingsDirectory = SettingsDirectory + "General/";
    public const string ConcreteEnemiesSettingsDirectory = SettingsDirectory + "Concrete Enemies/";

    #endregion


    #region Installers

    public const string InstallersDirectory = RootDirectory + "Installers/";

    #endregion
}