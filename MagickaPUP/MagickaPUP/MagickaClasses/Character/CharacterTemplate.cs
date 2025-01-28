using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Audio;
using MagickaPUP.MagickaClasses.Character.Attachments;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Character.Animation;
using MagickaPUP.MagickaClasses.Character.Events;
using MagickaPUP.MagickaClasses.Character.Abilities;
using MagickaPUP.MagickaClasses.Character.Buffs;
using MagickaPUP.MagickaClasses.Character.Aura;

namespace MagickaPUP.MagickaClasses.Character
{
    // TODO : Change the way we handle values that go out of range for the sounds, lights and gibs... maybe just make this throw an exception so that we can bail out while writing a malformed file? like, if we input an invalid JSON file we should notify the error somehow, so yeah...

    public class CharacterTemplate : XnaObject
    {
        #region Constants

        // NOTE : This is the max number of animation sets that a CharacterTemplate can hold.
        private static readonly int TOTAL_ANIMATION_SETS = 27;

        #endregion

        #region Variables

        // ID Strings
        public string characterID { get; set; }
        public string characterDisplayID { get; set; }

        // Enum data
        public Factions faction { get; set; }
        public BloodType bloodType { get; set; }

        // Flags
        public bool isEthereal { get; set; }
        public bool looksEthereal { get; set; }
        public bool isFearless { get; set; }
        public bool isUncharmable { get; set; }
        public bool isNonSlippery { get; set; }
        public bool hasFairy { get; set; }
        public bool canSeeInvisible { get; set; }

        // Sounds
        public int numAttachedSounds { get; set; }
        public SoundHolder[] attachedSounds { get; set; }

        // Gibs
        public int numGibs { get; set; }
        public GibReference[] gibs { get; set; }

        // Lights
        public int numLights { get; set; }
        public LightHolder[] lights { get; set; }

        // Character Health Data
        public float maxHitPoints { get; set; }
        public int numHealthBars { get; set; }

        // Character NPC Data Part 1
        public bool isUndying { get; set; }
        public float undieTime { get; set; }
        public float undieHitPoints { get; set; }
        public int hitTolerance { get; set; }
        public float knockdownTolerance { get; set; }
        public int scoreValue { get; set; }
        public int experienceValue { get; set; }
        public bool rewardOnKill { get; set; }
        public bool rewardOnOverkill { get; set; }
        public int regeneration { get; set; }
        public float maxPanic { get; set; }
        public float zapModifier { get; set; }
        public float length { get; set; }
        public float radius { get; set; }
        public float mass { get; set; }
        public float speed { get; set; }
        public float turnSpeed { get; set; }
        public float bleedRate { get; set; }
        public float stunTime { get; set; }
        public Banks summonElementBank { get; set; }
        public string summonElementCueString { get; set; }

        // Resistances
        public int numResistances { get; set; }
        public Resistance[] resistances { get; set; }

        // Model
        public int numModelProperties { get; set; }
        public ModelProperties[] modelProperties { get; set; }
        public string skinnedModel { get; set; } /* ER */

        // Attached Effects
        // first : bone name, second : effect name
        public int numAttachedEffects { get; set; }
        public EffectHolder[] attachedEffects;

        // Animation Data
        public AnimationList[] animationClips { get; set; } // This "list" should ALWAYS contain 27 "lists" of 231 elements each.

        // Equipment Data
        public int numEquipementAttachments { get; set; }
        public EquipmentAttachment[] equipmentAttachments {get;set;}

        // Event Data
        public ConditionCollection conditions { get; set; }

        // Character NPC Data Part 2
        // NOTE : These look like weights and such for the utility system, maybe?
        // TODO : Figure out what system they interact with and how it is implemented. It's not really necessary for the reader / writer impl, but it would be cool to know if this uses an US under the hood.
        public float alertRadius { get; set; }
        public float groupChase { get; set; }
        public float groupSeparation { get; set; }
        public float groupCohesion { get; set; }
        public float groupAlignment { get; set; }
        public float groupWander { get; set; }
        public float friendlyAvoidance { get; set; }
        public float enemyAvoidance { get; set; }
        public float sightAvoidance { get; set; }
        public float dangerAvoidance { get; set; }
        public float angerWeight { get; set; }
        public float distanceWeight { get; set; }
        public float healthWeight { get; set; }
        public bool flocking { get; set; }
        public float breakFreeStrength { get; set; }

        // Abilities
        public int numAbilities { get; set; }
        public Ability[] abilities {get;set;}

        // Movement Properties
        public int numMovementData { get; set; }
        public MovementData[] movementData { get; set; }

        // Buff Data
        public int numBuffs { get; set; }
        public BuffStorage[] Buffs { get; set; } // Buff as in buffing a character's stats, not buffer...

        // Aura Data
        public int numAuras { get; set; }
        public AuraStorage[] Auras { get; set; }

        #endregion

        #region Constructor

        public CharacterTemplate()
        {
            // ID Strings Initialization
            this.characterID = default;
            this.characterDisplayID = default;
            
            // Enum data initialization
            this.faction = default;
            this.bloodType = default;

            // Flags initialization
            this.isEthereal = false;
            this.looksEthereal = false;
            this.isFearless = false;
            this.isUncharmable = false;
            this.isNonSlippery = false;
            this.hasFairy = false;
            this.canSeeInvisible = false;

            // Sounds initialization
            this.numAttachedSounds = 0;
            this.attachedSounds = new SoundHolder[4]; // we hard code 4 because this is all that Magicka ever uses.

            // Gibs initialization
            this.numGibs = 0;
            this.gibs = new GibReference[0];

            // Light initialization
            this.numLights = 0;
            this.lights = new LightHolder[0]; // a character in Magicka can only hold up to 4 light holders.

            // Health initialization
            this.maxHitPoints = 100.0f; // Maybe this value is not a good one for Magicka, but this is the default I'm going with for now lol...
            this.numHealthBars = 1; // We should have at least 1 health bar, but even if we set it to 0 it does not matter, the reader code clamps it.

            // Character Data initialization
            this.isUndying = false;
            this.undieTime = 0.0f;
            this.undieHitPoints = 100.0f;
            this.hitTolerance = 0;
            this.knockdownTolerance = 0.0f;
            this.scoreValue = 100;
            this.experienceValue = 100;
            this.rewardOnKill = true;
            this.rewardOnOverkill = true;
            this.regeneration = 10;
            this.maxPanic = 1.0f;
            this.zapModifier = 0.0f;
            this.length = 1.0f;
            this.radius = 1.0f;
            this.mass = 1.0f;
            this.speed = 100.0f;
            this.turnSpeed = 10.0f;
            this.bleedRate = 10.0f;
            this.stunTime = 10.0f;
            this.summonElementBank = default;
            this.summonElementCueString = default;

            // Resistances
            this.numResistances = 0;
            this.resistances = new Resistance[11]; // We hard code this to 11, just like Magicka does. Because there are only 11 elements that we should be capable of adding resistances for, with indices 0 to 10 (read the notes within Elements.cs for further context and information)

            // Model
            this.numModelProperties = 0;
            this.modelProperties = new ModelProperties[0];
            this.skinnedModel = default;

            // Effects
            this.numAttachedEffects = 0;
            this.attachedEffects = new EffectHolder[0];

            // Animations
            this.animationClips = new AnimationList[TOTAL_ANIMATION_SETS];

            // Equipment Attachments
            this.numEquipementAttachments = 0;
            this.equipmentAttachments = new EquipmentAttachment[8]; // We hard code this to 8, just like Magicka does. We can have 8 attachments per character at most.

            // Event Data
            this.conditions = new ConditionCollection();

            // Character Data Initialization Part 2
            this.alertRadius = default;
            this.groupChase = default;
            this.groupSeparation = default;
            this.groupCohesion = default;
            this.groupAlignment = default;
            this.groupWander = default;
            this.friendlyAvoidance = default;
            this.enemyAvoidance = default;
            this.sightAvoidance = default;
            this.dangerAvoidance = default;
            this.angerWeight = default;
            this.distanceWeight = default;
            this.healthWeight = default;
            this.flocking = default;
            this.breakFreeStrength = default;

            // Abilities
            this.numAbilities = 0;
            this.abilities = new Ability[0];

            // Movement Data
            this.numMovementData = 0;
            this.movementData = new MovementData[0];

            // Buff Data
            this.numBuffs = 0;
            this.Buffs = new BuffStorage[0];

            // Aura Data
            this.numAuras = 0;
            this.Auras = new AuraStorage[0];
        }

        #endregion

        #region PublicMethods
        #endregion

        #region PrivateMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading CharacterTemplate...");

            // Internally, Magicka computes a hash for both of these ID strings after reading them, but we don't need it here.
            this.characterID = reader.ReadString().ToLowerInvariant();
            this.characterDisplayID = reader.ReadString().ToLowerInvariant();

            // Read enum data
            this.faction = (Factions)reader.ReadInt32();
            this.bloodType = (BloodType)reader.ReadInt32();
            
            // Read flags
            this.isEthereal = reader.ReadBoolean();
            this.looksEthereal = reader.ReadBoolean();
            this.isFearless = reader.ReadBoolean();
            this.isUncharmable = reader.ReadBoolean();
            this.isNonSlippery = reader.ReadBoolean();
            this.hasFairy = reader.ReadBoolean();
            this.canSeeInvisible = reader.ReadBoolean();

            // Read character sounds
            this.numAttachedSounds = reader.ReadInt32(); // NOTE : To prevent having to store this value as a variable within this class and just working with the array input data from JSON, we could just initialize the attached sounds array to this input length here, and set the length to min(4, reader.readi32()), maybe do this in the future when we clean up all of the manually hard coded counts in the other JSON files for the level data and stuff?
            for (int i = 0; i < this.numAttachedSounds; ++i)
            {
                #region Comment
                // Can't read more than 4 sounds, since that is the max amount of sounds reserved by magicka for each CharacterTemplate, so we break out.
                // Note that imo this should potentially be an exception on mpup, but we'll do the thing that magicka does and just let it slide... for now.
                // Also, note that magicka's code just keeps looping over the non valid values out of bounds rather than breaking, so malformed files will not read
                // the bytes, which will break things and just crash on load, and also wastes runtime in case of a miswritten byte making this loop iterate for more than it should, when we can just break out and call it a day...
                // In the future, when the engine rewrite comes around, we could actually modify this to support more than 4 sounds per character template...
                #endregion
                if (i >= 4)
                    break;
                this.attachedSounds[i] = SoundHolder.Read(reader, logger);
            }

            // Read character gibs (GORE!!!!!!! YEAH, BABY!!!! BLOOD!)
            this.numGibs = reader.ReadInt32();
            this.gibs = new GibReference[numGibs];
            for(int i = 0; i < this.numGibs; ++i)
            {
                this.gibs[i] = GibReference.Read(reader, logger);
            }

            // Read character lights
            this.numLights = reader.ReadInt32();
            this.lights = new LightHolder[this.numLights];
            for (int i = 0; i < this.numLights; ++i)
            {
                // Once again, there's a limit of 4 for these. In this case tho, Magicka does throw an exception if the found character has more than 4 point light holders!
                if (i >= 4)
                    break;
                this.lights[i] = LightHolder.Read(reader, logger);
            }

            // Read character health data (HP and num health bars)
            // NOTE : The number of health bars should be at least 1 (duh). If the input is N <= 0, we set it to 1, just as Magicka does internally.
            this.maxHitPoints = reader.ReadSingle();
            this.numHealthBars = reader.ReadInt32();
            if (this.numHealthBars <= 0)
                this.numHealthBars = 1;

            // Read character data
            this.isUndying = reader.ReadBoolean();
            this.undieTime = reader.ReadSingle();
            this.undieHitPoints = reader.ReadSingle();
            this.hitTolerance = reader.ReadInt32();
            this.knockdownTolerance = reader.ReadSingle();
            this.scoreValue = reader.ReadInt32();
            this.experienceValue = reader.ReadInt32();
            this.rewardOnKill = reader.ReadBoolean();
            this.rewardOnOverkill = reader.ReadBoolean();
            this.regeneration = reader.ReadInt32();
            this.maxPanic = reader.ReadSingle();
            this.zapModifier = reader.ReadSingle();
            this.length = reader.ReadSingle(); /* Math.Max(reader.ReadSingle(), 0.01f); */ // NOTE : this is limited with Math.Max(readValue, 0.01f) within Magicka's code, but we don't care about that here tbh
            this.radius = reader.ReadSingle();
            this.mass = reader.ReadSingle();
            this.speed = reader.ReadSingle();
            this.turnSpeed = reader.ReadSingle();
            this.bleedRate = reader.ReadSingle();
            this.stunTime = reader.ReadSingle();
            this.summonElementBank = (Banks)reader.ReadInt32();
            this.summonElementCueString = reader.ReadString();

            // Read resistances (elemental resistances)
            this.numResistances = reader.ReadInt32();
            for (int i = 0; i < this.numResistances; ++i)
            {
                Elements elements = (Elements)reader.ReadInt32();
                int elementIdx = MagickaDefines.ElementIndex(elements);
                this.resistances[elementIdx].elements = elements;
                this.resistances[elementIdx].multiplier = reader.ReadSingle();
                this.resistances[elementIdx].modifier = reader.ReadSingle();
                this.resistances[elementIdx].statusResistance = reader.ReadBoolean();
            }

            // Read Model
            this.numModelProperties = reader.ReadInt32();
            this.modelProperties = new ModelProperties[this.numModelProperties];
            for (int i = 0; i < this.numModelProperties; ++i)
            {
                this.modelProperties[i] = ModelProperties.Read(reader, logger);
            }
            this.skinnedModel = reader.ReadString(); /* ER */

            // Read Attached Effects / Particles
            // NOTE : This is kinda similar to the mesh settings stuff for the level data, it comes in pairs where we assign a particle to a specific bone name. The bone acts as a socket for the particles.
            this.numAttachedEffects = reader.ReadInt32();
            this.attachedEffects = new EffectHolder[this.numAttachedEffects];
            for (int i = 0; i < this.numAttachedEffects; ++i)
            {
                this.attachedEffects[i] = EffectHolder.Read(reader, logger);
            }

            // Read animation data
            // NOTE : A character has a limit of 27 lists of animation clips, and each list of animation clips has a limit of 231 individual animation clips.
            // In short, we have a 2D array AnimationClipAction[27][], and 1D array within that 2D array is an AnimationClipAction[231].
            // Not sure as of now what the implications of this are, or why the Magicka devs coded it like that, but here we are...
            // All I know is that there's a limit of 27 animation lists, and each list can hold at most 231 animations, which is the number of total animations
            // that exist in the "base" game (Magicka + DLCs, this value basically changes depending on what DLCs a given version has access to...)
            for (int i = 0; i < this.animationClips.Length; ++i)
            {
                // TODO : Finish making sure that the implementation of the AnimationList reading process is correct...
                // Remember that part of it comes from the loop within this class, since that is what the anim list class is abstracting away.
                this.animationClips[i] = AnimationList.Read(reader, logger);
            }

            // Read Equipment Attachments
            this.numEquipementAttachments = reader.ReadInt32();
            for (int i = 0; i < this.numEquipementAttachments; ++i)
            {
                this.equipmentAttachments[i] = EquipmentAttachment.Read(reader, logger);
            }

            // Read Events into Condition Collection
            this.conditions = ConditionCollection.Read(reader, logger);

            // Read Character Data part 2
            this.alertRadius = reader.ReadSingle();
            this.groupChase = reader.ReadSingle();
            this.groupSeparation = reader.ReadSingle();
            this.groupCohesion = reader.ReadSingle();
            this.groupAlignment = reader.ReadSingle();
            this.groupWander = reader.ReadSingle();
            this.friendlyAvoidance = reader.ReadSingle();
            this.enemyAvoidance = reader.ReadSingle();
            this.sightAvoidance = reader.ReadSingle();
            this.dangerAvoidance = reader.ReadSingle();
            this.angerWeight = reader.ReadSingle();
            this.distanceWeight = reader.ReadSingle();
            this.healthWeight = reader.ReadSingle();
            this.flocking = reader.ReadBoolean();
            this.breakFreeStrength = reader.ReadSingle();

            // Abilities
            this.numAbilities = reader.ReadInt32();
            this.abilities = new Ability[numAbilities];
            for (int i = 0; i < this.numAbilities; ++i)
            {
                this.abilities[i] = Ability.Read(reader, logger);
            }

            // Movement Data
            this.numMovementData = reader.ReadInt32();
            this.movementData = new MovementData[this.numMovementData];
            for (int i = 0; i < this.numMovementData; ++i)
                movementData[i] = new MovementData(reader, logger);

            // Buff Data
            this.numBuffs = reader.ReadInt32();
            this.Buffs = new BuffStorage[this.numBuffs];
            for(int i = 0; i < this.numBuffs; ++i)
                this.Buffs[i] = new BuffStorage(reader, logger);

            // Aura Data
            this.numAuras = reader.ReadInt32();
            this.Auras = new AuraStorage[this.numAuras];
            for (int i = 0; i < this.numAuras; ++i)
                this.Auras[i] = new AuraStorage(reader, logger);
        }

        public static CharacterTemplate Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new CharacterTemplate();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing CharacterTemplate...");

            throw new NotImplementedException("Write Character Template not implemented yet!");
        }

        #endregion
    }
}
