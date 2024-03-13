public static class ScriptableObjectsMenuDirectories
{
    public const string RootDirectory = "Spells and Battlefield/";

    #region Mechanics

    public const string MechanicsDirectory = RootDirectory + "Mechanics/";

    #endregion

    #region Installers

    public const string InstallersDirectory = RootDirectory + "Installers/";

    #endregion

    #region PickableItems

    public const string PickableItemsDirectory = RootDirectory + "Pickable Items/";

    #endregion

    #region Scenes

    public const string ScenesDirectory = RootDirectory + "Scenes Data/";

    #endregion

    #region Comics

    public const string ComicsDirectory = RootDirectory + "Comics/";

    #endregion

    #region Dialogs

    public const string DialogsDirectory = RootDirectory + "Dialogs/";

    #endregion

    #region Spells

    public const string SpellSystemDirectory = RootDirectory + "Spell System/";

    public const string SpellAppliersDirectory = SpellSystemDirectory + "Spell Appliers/";
    public const string SpellMovementDirectory = SpellSystemDirectory + "Movement/";
    public const string SpellTargetSelectorDirectory = SpellSystemDirectory + "Target Selector/";
    public const string SpellTriggerDirectory = SpellSystemDirectory + "Trigger/";

    private const string SpellConcreteTypesDirectory = SpellSystemDirectory + "Concrete Types/";
    public const string InstantSpellDirectory = SpellConcreteTypesDirectory + "Instant/";
    public const string ContinuousSpellDirectory = SpellConcreteTypesDirectory + "Continuous/";

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
    public const string SystemsSettingsDirectory = SettingsDirectory + "Systems/";
    public const string ComicsSettingsDirectory = SettingsDirectory + "Comics/";

    public const string PuzzleDirectory = SettingsDirectory + "Puzzles/";
    public const string PuzzleTriggersDirectory = PuzzleDirectory + "Triggers/";
    public const string IdentifiersDirectory = PuzzleDirectory + "Identifiers/";
    public const string PuzzleMechanismsDirectory = PuzzleDirectory + "Mechanisms/";

    #endregion

    #region PrefabProviders

    public const string SystemsPrefabProvidersDirectory = RootDirectory + "System/";
    public const string UIPrefabProvidersDirectory = RootDirectory + "UI/";
    public const string PickableItemsProvidersDirectory = RootDirectory + "Pickable Items/";
    public const string PopupTextProvidersDirectory = RootDirectory + "Popup Texts/";

    #endregion
}