using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;

namespace ExpandedContent.Tweaks.Archetypes {
    //Locking base gods from Archetypes
    internal class PatchMantisZealotDeity {
                public static void MantisZealotDeityPatch() {
            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            var AbadarFeature = Resources.GetBlueprint<BlueprintFeature>("6122dacf418611540a3c91e67197ee4e");
            var AsmodeusFeature = Resources.GetBlueprint<BlueprintFeature>("a3a5ccc9c670e6f4ca4a686d23b89900");
            var CalistriaFeature = Resources.GetBlueprint<BlueprintFeature>("c7531715a3f046d4da129619be63f44c");
            var CaydenCaileanFeature = Resources.GetBlueprint<BlueprintFeature>("300e212868bca984687c92bcb66d381b");
            var DesnaFeature = Resources.GetBlueprint<BlueprintFeature>("2c0a3b9971327ba4d9d85354d16998c1");
            var ErastilFeature = Resources.GetBlueprint<BlueprintFeature>("afc775188deb7a44aa4cbde03512c671");
            var GorumFeature = Resources.GetBlueprint<BlueprintFeature>("8f49a5d8528a82c44b8c117a89f6b68c");
            var GozrehFeature = Resources.GetBlueprint<BlueprintFeature>("4af983eec2d821b40a3065eb5e8c3a72");
            var GyronnaFeature = Resources.GetBlueprint<BlueprintFeature>("8b535b6842e063d48a571a042c3c6e8f");
            var IomedaeFeature = Resources.GetBlueprint<BlueprintFeature>("88d5da04361b16746bf5b65795e0c38c");
            var IroriFeature = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234");
            var LamashtuFeature = Resources.GetBlueprint<BlueprintFeature>("f86bc8fbf13221f4f9041608a1fb8585");
            var NethysFeature = Resources.GetBlueprint<BlueprintFeature>("6262cfce7c31626458325ca0909de997");
            var NorgorberFeature = Resources.GetBlueprint<BlueprintFeature>("805b6bdc8c96f4749afc687a003f9628");
            var PharasmaFeature = Resources.GetBlueprint<BlueprintFeature>("458750bc214ab2e44abdeae404ab22e9");
            var RovagugFeature = Resources.GetBlueprint<BlueprintFeature>("04bc2b62273ab744092d992ed72bff41");
            var SarenraeFeature = Resources.GetBlueprint<BlueprintFeature>("c1c4f7f64842e7e48849e5e67be11a1b");
            var ShelynFeature = Resources.GetBlueprint<BlueprintFeature>("b382afa31e4287644b77a8b30ed4aa0b");
            var ToragFeature = Resources.GetBlueprint<BlueprintFeature>("d2d5c5a58885a6b489727467e13c3337");
            var UrgathoaFeature = Resources.GetBlueprint<BlueprintFeature>("812f6c07148088e41a9ac94b56ac2fc8");
            var ZonKuthonFeature = Resources.GetBlueprint<BlueprintFeature>("f7eed400baa66a744ad361d4df0e6f1b");
            var GodClawFeature = Resources.GetBlueprint<BlueprintFeature>("583a26e88031d0a4a94c8180105692a5");
            var AtheismFeature = Resources.GetBlueprint<BlueprintFeature>("92c0d2da0a836ce418a267093c09ca54");
            var NewAchaekekFeature = Resources.GetBlueprint<BlueprintFeature>("a3189d5b7c4d4d91beaa8bfffac3e38e");
            var NewApsuFeature = Resources.GetBlueprint<BlueprintFeature>("772e2673945e4583a804ae01f67efea0");
            var NewDahakFeature = Resources.GetBlueprint<BlueprintFeature>("8f7118d68f6e44dea94dddb51f38cbdd");

            #region Mantis Zealot
            AbadarFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            AsmodeusFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            IroriFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            NorgorberFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            UrgathoaFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            ZonKuthonFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            GodClawFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            #endregion
            #region Silver Champion
            AbadarFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            IomedaeFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            IroriFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            ToragFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            ShelynFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            SarenraeFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            ErastilFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            #endregion
            #region ClawOfTheFalseWyrmArchetype
            AbadarFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            AsmodeusFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            CalistriaFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            GorumFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            GozrehFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            IroriFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            LamashtuFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            NethysFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            PharasmaFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            RovagugFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            UrgathoaFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            ZonKuthonFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            GodClawFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            AtheismFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            GyronnaFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            ToragFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            ShelynFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            SarenraeFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            IomedaeFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            ErastilFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            DesnaFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            CaydenCaileanFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            NewAchaekekFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            NorgorberFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            NewApsuFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            #endregion
            #region Sworn of the Eldest
            AbadarFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            AsmodeusFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            CalistriaFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            CaydenCaileanFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            DesnaFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            ErastilFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            GorumFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            GozrehFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            GyronnaFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            IomedaeFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            IroriFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            LamashtuFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            NethysFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            NorgorberFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            PharasmaFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            RovagugFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            SarenraeFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            ShelynFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            ToragFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            UrgathoaFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            ZonKuthonFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            GodClawFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            NewAchaekekFeature.SetDisallowedArchetype(InquisitorClass, SwornOfTheEldestArchetype);
            #endregion
            #region Soldier of Gaia
            CalistriaFeature.DisallowSoldierOfGaia();
            CaydenCaileanFeature.DisallowSoldierOfGaia();
            DesnaFeature.DisallowSoldierOfGaia();
            GorumFeature.DisallowSoldierOfGaia();
            IomedaeFeature.DisallowSoldierOfGaia();
            IroriFeature.DisallowSoldierOfGaia();
            LamashtuFeature.DisallowSoldierOfGaia();
            NethysFeature.DisallowSoldierOfGaia();
            NorgorberFeature.DisallowSoldierOfGaia();
            UrgathoaFeature.DisallowSoldierOfGaia();
            ZonKuthonFeature.DisallowSoldierOfGaia();
            GodClawFeature.DisallowSoldierOfGaia();
            NewAchaekekFeature.DisallowSoldierOfGaia();
            NewApsuFeature.DisallowSoldierOfGaia();
            NewDahakFeature.DisallowSoldierOfGaia();
            #endregion
        }
    }
}