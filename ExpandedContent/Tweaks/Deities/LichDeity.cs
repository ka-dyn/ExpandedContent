using HarmonyLib;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Deities {
    internal class PatchLichDeity {

        


        
        

                
        public static void AddLichDeity() {

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");


            var LichDeityMythicFeature = Resources.GetBlueprint<BlueprintFeature>("d633cf9ebcdc8ed4e8f2546c3e08742e");
            var LichDeityFeature = Resources.GetBlueprint<BlueprintFeature>("b4153b422d02d4f48b3f8f0ceb6a10eb");
            LichDeityFeature.RemoveComponents<PrerequisiteNoFeature>();
            LichDeityFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
            });
            LichDeityFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
            });
            LichDeityFeature.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[] { LichDeityMythicFeature.ToReference<BlueprintUnitFactReference>() };
                    });
            LichDeityFeature.SetDescription("Leader of a small cult of undead, created by their own hands, becoming famous as the " +
                "indomitable commander of the Fifth Mendevian Crusade against the Worldwound. Determined to destroy the forces of the Abyss at any cost, " +
                "they ventured on the path of the Lord of Death. For millennia, powerful necromancers have dreamed of a power that could rival the divine. " +
                "They have created cults in their own names, and attracted many followers. Thanks to their legendary powers, they has managed to achieve much greater " +
                "success than the others: acquireing a talent to bestow divine spells upon their followers. From that moment, the undead created by them " +
                "needed no other deities to cast spells.\nDomains: Death, Evil, Strength, War. \nFavoured Weapons: Scythe, Sickle.");

                    


                
        }
            
    }
        
}
    
