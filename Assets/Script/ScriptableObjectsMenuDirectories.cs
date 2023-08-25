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

    #region Enemies

    public const string EnemiesDirectory = RootDirectory + "Enemies/";
    public const string EnemyMovementProvidersDirectory = EnemiesDirectory + "Movement Providers/";
    public const string EnemyCharacterProvidersDirectory = EnemiesDirectory + "Character Providers/";

    public const string StatesDataDirectory = EnemiesDirectory + "States Data/";
    public const string EnemyUseSpellStateSpellsSelectors = StatesDataDirectory + "Spells Selectors/";

    #endregion

    #region Settings

    private const string SettingsDirectory = RootDirectory + "Settings/";

    public const string GeneralSettingsDirectory = SettingsDirectory + "General/";
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

    public const string PickableItemsProvidersDirectory = RootDirectory + "Pickable Items/";
    public const string PopupTextProvidersDirectory = RootDirectory + "Popup Texts/";

    #endregion

    #region PickableItems

    public const string PickableItemsDirectory = RootDirectory + "Pickable Items/";

    #endregion
    
    #region Scenes

    public const string ScenesDirectory = RootDirectory + "Scenes Data/";

    #endregion
}