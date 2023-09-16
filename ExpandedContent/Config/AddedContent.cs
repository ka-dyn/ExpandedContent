namespace ExpandedContent.Config {
    public class AddedContent : IUpdatableSettings {
        public bool NewSettingsOffByDefault = false;
        public SettingGroup RacialArchetypes = new SettingGroup();
        public SettingGroup RetiredFeatures = new SettingGroup();        
        public SettingGroup Classes = new SettingGroup();
        public SettingGroup Backgrounds = new SettingGroup();
        public SettingGroup Archetypes = new SettingGroup();
        public SettingGroup Domains = new SettingGroup();
        public SettingGroup Deities = new SettingGroup();
        public SettingGroup Miscellaneous = new SettingGroup();
        public SettingGroup Feats = new SettingGroup();

        public void Init() {
        }

        public void OverrideSettings(IUpdatableSettings userSettings) {
            var loadedSettings = userSettings as AddedContent;
            NewSettingsOffByDefault = loadedSettings.NewSettingsOffByDefault;
            RacialArchetypes.LoadSettingGroup(loadedSettings.RacialArchetypes, NewSettingsOffByDefault);
            RetiredFeatures.LoadSettingGroup(loadedSettings.RetiredFeatures, NewSettingsOffByDefault);
            Classes.LoadSettingGroup(loadedSettings.Classes, NewSettingsOffByDefault);
            Backgrounds.LoadSettingGroup(loadedSettings.Backgrounds, NewSettingsOffByDefault);
            Archetypes.LoadSettingGroup(loadedSettings.Archetypes, NewSettingsOffByDefault);
            Domains.LoadSettingGroup(loadedSettings.Domains, NewSettingsOffByDefault);
            Deities.LoadSettingGroup(loadedSettings.Deities, NewSettingsOffByDefault);
            Miscellaneous.LoadSettingGroup(loadedSettings.Miscellaneous, NewSettingsOffByDefault);
            Feats.LoadSettingGroup(loadedSettings.Feats, NewSettingsOffByDefault);
        }
    }
}