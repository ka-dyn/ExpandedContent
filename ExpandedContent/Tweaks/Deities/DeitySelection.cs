using BlueprintCore.Blueprints;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Deities {
    internal class DeitySelectionFeature {

        public static void PatchDeitySelection() {

            //Demon Lords
            var ZuraFeature = Resources.GetModBlueprint<BlueprintFeature>("ZuraFeature");
            var DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("ddf913858bdf43b4da3b731e082fbcc0");
            var BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
            var AreshkegalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
            var KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");


            //Empyreal Lords
            var ArsheaFeature = Resources.GetModBlueprint<BlueprintFeature>("ArsheaFeature");
            var RagathielFeature = Resources.GetModBlueprint<BlueprintFeature>("RagathielFeature");
            var PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");



            //Gods and Goddesses
            var MilaniFeature = Resources.GetModBlueprint<BlueprintFeature>("MilaniFeature");
            var ErastilFeature = Resources.GetBlueprint<BlueprintFeature>("afc775188deb7a44aa4cbde03512c671");
            var IroriFeature = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234");
            var IomedaeFeature = Resources.GetBlueprint<BlueprintFeature>("88d5da04361b16746bf5b65795e0c38c");
            var ToragFeature = Resources.GetBlueprint<BlueprintFeature>("d2d5c5a58885a6b489727467e13c3337");
            var ShelynFeature = Resources.GetBlueprint<BlueprintFeature>("b382afa31e4287644b77a8b30ed4aa0b");
            var SarenraeFeature = Resources.GetBlueprint<BlueprintFeature>("c1c4f7f64842e7e48849e5e67be11a1b");
            var AbadarFeature = Resources.GetBlueprint<BlueprintFeature>("6122dacf418611540a3c91e67197ee4e");
            var GorumFeature = Resources.GetBlueprint<BlueprintFeature>("8f49a5d8528a82c44b8c117a89f6b68c");
            var GozrehFeature = Resources.GetBlueprint<BlueprintFeature>("4af983eec2d821b40a3065eb5e8c3a72");
            var GyronnaFeature = Resources.GetBlueprint<BlueprintFeature>("8b535b6842e063d48a571a042c3c6e8f");
            var LamashtuFeature = Resources.GetBlueprint<BlueprintFeature>("f86bc8fbf13221f4f9041608a1fb8585");
            var LichDeityFeature = Resources.GetBlueprint<BlueprintFeature>("b4153b422d02d4f48b3f8f0ceb6a10eb");
            var AsmodeusFeature = Resources.GetBlueprint<BlueprintFeature>("a3a5ccc9c670e6f4ca4a686d23b89900");
            var NethysFeature = Resources.GetBlueprint<BlueprintFeature>("6262cfce7c31626458325ca0909de997");
            var NorgorberFeature = Resources.GetBlueprint<BlueprintFeature>("805b6bdc8c96f4749afc687a003f9628");
            var PharasmaFeature = Resources.GetBlueprint<BlueprintFeature>("458750bc214ab2e44abdeae404ab22e9");
            var RovagugFeature = Resources.GetBlueprint<BlueprintFeature>("04bc2b62273ab744092d992ed72bff41");
            var UrgathoaFeature = Resources.GetBlueprint<BlueprintFeature>("812f6c07148088e41a9ac94b56ac2fc8");
            var ZonKuthonFeature = Resources.GetBlueprint<BlueprintFeature>("f7eed400baa66a744ad361d4df0e6f1b");
            var CalistriaFeature = Resources.GetBlueprint<BlueprintFeature>("c7531715a3f046d4da129619be63f44c");
            var CaydenCaileanFeature = Resources.GetBlueprint<BlueprintFeature>("300e212868bca984687c92bcb66d381b");
            var DesnaFeature = Resources.GetBlueprint<BlueprintFeature>("2c0a3b9971327ba4d9d85354d16998c1");


            //Philosophies
            var GreenFaithFeature = Resources.GetBlueprint<BlueprintFeature>("99a7a8f13c1300c42878558fa9471e2f");
            var AtheismFeature = Resources.GetBlueprint<BlueprintFeature>("92c0d2da0a836ce418a267093c09ca54");

            //Pantheons
            var GodclawFeature = Resources.GetBlueprint<BlueprintFeature>("583a26e88031d0a4a94c8180105692a5");

            //Dragon Gods/Aspects
            var ApsuFeature = Resources.GetModBlueprint<BlueprintFeature>("ApsuFeature");


            //Archdevils
            var MephistophelesFeature = Resources.GetModBlueprint<BlueprintFeature>("MephistophelesFeature");
            var DispaterFeature = Resources.GetModBlueprint<BlueprintFeature>("DispaterFeature");


            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");


            var DemonLordsIcon = AssetLoader.LoadInternal("Deities", "Icon_DemonLords.jpg");
            var DemonLordSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DemonLordSelection", bp => {
                bp.SetName("Demon Lords");
                bp.SetDescription("A demon lord is a very powerful and unique demon. They are, by definition, rulers of at least " +
                    "one layer of the Abyss, and have hordes of nascent demon lords and lesser demons in their service. Being creatures " +
                    "of chaos, however, not all demons are servants to a demon lord. As the Abyss is nigh infinite, so too are " +
                    "the number of demon lords.");
                bp.m_Icon = DemonLordsIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                ZuraFeature.ToReference<BlueprintFeatureReference>(),
                DeskariFeature.ToReference<BlueprintFeatureReference>(),
                KabririFeature.ToReference<BlueprintFeatureReference>(),
                AreshkegalFeature.ToReference<BlueprintFeatureReference>(),
                BaphometFeature.ToReference<BlueprintFeatureReference>() };

                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var ArchdevilIcon = AssetLoader.LoadInternal("Deities", "Icon_Archdevils.jpg");
            var ArchdevilSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ArchdevilSelection", bp => {
                bp.SetName("Archdevils");
                bp.SetDescription("Archdevils are the nine major devils who rule the nine layers of Hell. They are demigods " +
                    "who posses god-like powers (such as the ability to grant spells to their worshipers), but are less " +
                    "powerful than the master of Hell, Asmodeus. The others pay strict obeisance to him, even while they " +
                    "tirelessly scheme against him and each other. Each archdevil represents a different form of punishment " +
                    "for sinful mortals.");
                bp.m_Icon = ArchdevilIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                DispaterFeature.ToReference<BlueprintFeatureReference>(),
                MephistophelesFeature.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var EmpyrealLordsIcon = AssetLoader.LoadInternal("Deities", "Icon_EmpyrealLords.jpg");
            var EmpyrealLordSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("EmpyrealLordSelection", bp => {
                bp.SetName("Empyreal Lords");
                bp.SetDescription("Empyreal Lords, or the Lords of the Empyrean, are unique outsiders who have transcended their original " +
                    "forms and have acquired a small spark of divinity, becoming demigods. They guide mortals on the various paths to " +
                    "righteousness, and the goddess Sarenrae herself is said to have risen from their ranks. Angels, archons, azatas, and " +
                    "powerful agathions known as agathion leaders have all entered the ranks of empyreal lords.");
                bp.m_Icon = EmpyrealLordsIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                ArsheaFeature.ToReference<BlueprintFeatureReference>(),
                RagathielFeature.ToReference<BlueprintFeatureReference>(),
                PuluraFeature.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var DeitiesSelectionIcon = AssetLoader.LoadInternal("Deities", "Icon_DeitiesSelection.jpg");
            var DeitiesSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DeitiesSelection", bp => {
                bp.SetName("Gods and Goddesses");
                bp.SetDescription("A deity, also known as a god or goddess, is a being or force of incredible power capable " +
                    "of granting its power to mortal beings through divine magic. These are the only beings that can claim full Divinity. " +
                    "A deity is strongly associated with a specific alignment, " +
                    "several domains, and a plane (typically an Outer Sphere plane).");
                bp.m_Icon = DeitiesSelectionIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                AbadarFeature.ToReference<BlueprintFeatureReference>(),
                IomedaeFeature.ToReference<BlueprintFeatureReference>(),
                AsmodeusFeature.ToReference<BlueprintFeatureReference>(),
                GorumFeature.ToReference<BlueprintFeatureReference>(),
                GozrehFeature.ToReference<BlueprintFeatureReference>(),
                IroriFeature.ToReference<BlueprintFeatureReference>(),
                ToragFeature.ToReference<BlueprintFeatureReference>(),
                ShelynFeature.ToReference<BlueprintFeatureReference>(),
                SarenraeFeature.ToReference<BlueprintFeatureReference>(),
                DesnaFeature.ToReference<BlueprintFeatureReference>(),
                UrgathoaFeature.ToReference<BlueprintFeatureReference>(),
                ZonKuthonFeature.ToReference<BlueprintFeatureReference>(),
                CalistriaFeature.ToReference<BlueprintFeatureReference>(),
                CaydenCaileanFeature.ToReference<BlueprintFeatureReference>(),
                GyronnaFeature.ToReference<BlueprintFeatureReference>(),
                PharasmaFeature.ToReference<BlueprintFeatureReference>(),
                NorgorberFeature.ToReference<BlueprintFeatureReference>(),
                LamashtuFeature.ToReference<BlueprintFeatureReference>(),
                RovagugFeature.ToReference<BlueprintFeatureReference>(),
                NethysFeature.ToReference<BlueprintFeatureReference>(),
                ErastilFeature.ToReference<BlueprintFeatureReference>(),
                LichDeityFeature.ToReference<BlueprintFeatureReference>(),
                MilaniFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var DragonDeityIcon = AssetLoader.LoadInternal("Deities", "Icon_DragonDeities.jpg");
            var DraconicDeitySelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DraconicDeitySelection", bp => {
                bp.SetName("Draconic Deities");
                bp.SetDescription("In accordance with Draconic Lore, at the dawn of time there flowed two waters, fresh and salt, which became Apsu and Tiamat, " +
                    "parents of the first gods. However, their eldest son Dahak came to Hell to rampage, then killed his siblings, " +
                    "whose shattered remains fell into the Material Plane and became the first metallic dragons.");
                bp.m_Icon = DragonDeityIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] { ApsuFeature.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var PhilosophiesIcon = AssetLoader.LoadInternal("Deities", "Icon_Philosophies.jpg");
            var PhilosophiesSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("PhilosophiesSelection", bp => {
                bp.SetName("Philosophies");
                bp.SetDescription("Philosophies of Golarion differ from religions in that they are teachings and ways of " +
                    "thinking that are propounded by a mortal founder who, usually, does not become a deity. By assenting to a " +
                    "particular philosophy, one is generally not limited in choosing any particular occupation; however, some " +
                    "combinations of philosophy and religion present inherent conflicts. The study of various fundamental " +
                    "underpinnings of many philosophies is known as metaphysics.");
                bp.m_Icon = PhilosophiesIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    GreenFaithFeature.ToReference<BlueprintFeatureReference>(),
                    AtheismFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var PantheonIcon = AssetLoader.LoadInternal("Deities", "Icon_Pantheon.jpg");
            var PantheonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("PantheonSelection", bp => {
                bp.SetName("Pantheons");
                bp.SetDescription("A pantheon is a group of related gods worshipped either individually or together. " +
                    "Most pantheons are associated with a specific ancestry or geopolitical region, but rarely, a pantheon " +
                    "consists of deities with overlapping areas of concern. Followers work to advance the shared interests of their " +
                    "pantheon, directing prayers to whichever god presides over their current activity or circumstance.");
                bp.m_Icon = PantheonIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    GodclawFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });









            var DeitySelectionIcon = AssetLoader.LoadInternal("Deities", "Icon_DeitySelection.jpg");
            DeitySelection.m_Icon = DeitySelectionIcon;
            DeitySelection.m_AllFeatures = new BlueprintFeatureReference[] {
                DemonLordSelection.ToReference<BlueprintFeatureReference>(),
                ArchdevilSelection.ToReference<BlueprintFeatureReference>(),
                EmpyrealLordSelection.ToReference<BlueprintFeatureReference>(),
                DeitiesSelection.ToReference<BlueprintFeatureReference>(),
                DraconicDeitySelection.ToReference<BlueprintFeatureReference>(),
                PhilosophiesSelection.ToReference<BlueprintFeatureReference>(),
                PantheonSelection.ToReference<BlueprintFeatureReference>()};
            DeitySelection.Groups = new FeatureGroup[] { FeatureGroup.Deities };
            DeitySelection.Group = FeatureGroup.Deities;

            PaladinClass.RemoveComponents<PrerequisiteFeaturesFromList>();
            PaladinClass.AddComponent<PrerequisiteFeaturesFromList>(c => {
                c.m_Features = new BlueprintFeatureReference[] {
                    IomedaeFeature.ToReference<BlueprintFeatureReference>(),
                    MilaniFeature.ToReference<BlueprintFeatureReference>(),
                    IroriFeature.ToReference<BlueprintFeatureReference>(),
                    ToragFeature.ToReference<BlueprintFeatureReference>(),
                    ShelynFeature.ToReference<BlueprintFeatureReference>(),
                    SarenraeFeature.ToReference<BlueprintFeatureReference>(),
                    ErastilFeature.ToReference<BlueprintFeatureReference>(),
                    ArsheaFeature.ToReference<BlueprintFeatureReference>(),
                    RagathielFeature.ToReference<BlueprintFeatureReference>(),
                    ApsuFeature.ToReference<BlueprintFeatureReference>() };
            });
            var Seelah = Resources.GetBlueprint<BlueprintUnit>("54be53f0b35bf3c4592a97ae335fe765");
            Seelah.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { IomedaeFeature.ToReference<BlueprintUnitFactReference>() };
            });
        }
    }
}








