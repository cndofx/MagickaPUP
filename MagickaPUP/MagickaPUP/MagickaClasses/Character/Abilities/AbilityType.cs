using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character.Abilities
{
    // NOTE : This enum does NOT exist within Magicka's original code. This is just a way to simplify things for mpup's implementation.
    // The Ability class is an abstract class that corresponds to Utility Actions for an Utility System. Each one has an "usefulness" value (the utility value),
    // an action to execute, and a "fuzzy expression" (as they call it in Magicka's code) which corresponds to a 2D curve for an Utility System action.
    // Each Utility Action ("Ability") is implemented with its own class that inherits from the base Ability class.
    // Magicka uses RTTI when loading the data from XNB files to determine what constructor to use to build an object of the correct type that corresponds to the input
    // string. In our case, we don't really need to do any of that, since each specific Ability type does NOT implement any specific reading logic, so we can just
    // implement an AbilityType enum and use it to validate and parse the input strings.
    // Note that adding new custom abilities would require modifying the code of the base game.
    // The name of each entry in this enum corresponds to the name of one of the classes that inherit from the abstract base Ability class in Magicka.
    
    // Again, in case it is not fully clear, the purpose of this enum is to literally just validate that the input string is a real ability
    // that exist within Magicka's code. This could very well just be completely ignored and we could perform no validation in case we want to be compatible
    // with future versions / custom engine versions without needing any code changes in the packer/unpacker mpup tool.
    
    public enum AbilityType
    {
        Block,
        CastSpell,
        ConfuseGrip,
        DamageGrip,
        Dash,
        ElementalSteal,
        GripCharacterFromBehind,
        Jump,
        Melee,
        // Bash, // This one inherits from Melee iirc but it is not implemented so we just skip it for now
        PickUpCharacter,
        Ranged,
        RemoveStatus,
        SpecialAbilityAbility, // WTF even is this name LOL
        ThrowGrip,
        ZombieGrip
    }
}
