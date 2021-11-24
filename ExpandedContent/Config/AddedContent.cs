
namespace ExpandedContent.Config {
    public class AddedContent : IUpdatableSettings {
        public bool NewSettingsOffByDefault = false;
        public SettingGroup RacialArchetypes = new SettingGroup();
        public SettingGroup Deities = new SettingGroup();
        public SettingGroup Archdevils = new SettingGroup();
        public SettingGroup DemonLords = new SettingGroup();
        public SettingGroup Classes = new SettingGroup();
        public SettingGroup Backgrounds = new SettingGroup();
        public SettingGroup Archetypes = new SettingGroup();



        public void Init() {
        }

        public void OverrideSettings(IUpdatableSettings userSettings) {
            var loadedSettings = userSettings as AddedContent;
            NewSettingsOffByDefault = loadedSettings.NewSettingsOffByDefault;
            RacialArchetypes.LoadSettingGroup(loadedSettings.RacialArchetypes, NewSettingsOffByDefault);
            Deities.LoadSettingGroup(loadedSettings.Deities, NewSettingsOffByDefault);
            Archdevils.LoadSettingGroup (loadedSettings.Archdevils, NewSettingsOffByDefault);
            DemonLords.LoadSettingGroup (loadedSettings.DemonLords, NewSettingsOffByDefault);
            Classes.LoadSettingGroup(loadedSettings.Classes, NewSettingsOffByDefault);
            Backgrounds.LoadSettingGroup(loadedSettings.Backgrounds, NewSettingsOffByDefault);
            Archetypes.LoadSettingGroup(loadedSettings.Archetypes, NewSettingsOffByDefault);

        }
    }
}