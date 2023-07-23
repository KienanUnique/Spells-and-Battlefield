public static class ScriptableObjectsMenuDirectories
{
    public const string RootDirectory = "Spells and Battlefield/";

    #region Spells

    public const string SpellSystemDirectory = RootDirectory + "Spell System/";

    public const string SpellAppliersDirectory = SpellSystemDirectory + "Spell Appliers/";
    public const string SpellObjectsProvidersDirectory = SpellSystemDirectory + "Spell Objects Providers/";
    public const string SpellMovementDirectory = SpellSystemDirectory + "Movement/";
    public const string SpellTargetSelectorDirectory = SpellSystemDirectory + "Target Selector/";
    public const string SpellTriggerDirectory = SpellSystemDirectory + "Trigger/";

    #endregion

    #region Mechanics

    public const string MechanicsDirectory = RootDirectory + "Mechanics/";

    #endregion

    #region Settings

    private const string SettingsDirectory = RootDirectory + "Settings/";

    public const string GeneralSettingsDirectory = SettingsDirectory + "General/";
    public const string ConcreteEnemiesSettingsDirectory = SettingsDirectory + "Concrete Enemies/";
    public const string ConcreteUISettingsDirectory = SettingsDirectory + "Concrete UI/";

    public const string PuzzleDirectory = SettingsDirectory + "Puzzles/";
    public const string PuzzleTriggersDirectory = PuzzleDirectory + "Triggers/";
    public const string IdentifiersDirectory = PuzzleDirectory + "Identifiers/";
    public const string PuzzleMechanismsDirectory = PuzzleDirectory + "Mechanisms/";

    #endregion

    #region Installers

    public const string InstallersDirectory = RootDirectory + "Installers/";

    #endregion

    #region PrefabProviders

    private const string PrefabProvidersDirectory = RootDirectory + "Prefab Providers/";

    public const string EnemiesPrefabProvidersDirectory = PrefabProvidersDirectory + "Enemies/";
    public const string PickableItemsProvidersDirectory = PrefabProvidersDirectory + "Pickable Items/";

    #endregion

    #region PickableItems

    public const string PickableItemsDirectory = RootDirectory + "Pickable Items/";

    #endregion
}