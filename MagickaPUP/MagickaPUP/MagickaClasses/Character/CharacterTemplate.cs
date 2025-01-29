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
using System.Text.Json.Serialization;
using MagickaPUP.Utility.Exceptions;
using System.Runtime.Remoting.Messaging;

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
        public string CharacterID { get; set; }
        public string CharacterDisplayID { get; set; }

        // Enum data
        [JsonConverter(typeof(JsonStringEnumConverter<Factions>))] public Factions Faction { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter<BloodType>))] public BloodType BloodType { get; set; }

        // Flags
        public bool IsEthereal { get; set; }
        public bool LooksEthereal { get; set; }
        public bool IsFearless { get; set; }
        public bool IsUncharmable { get; set; }
        public bool IsNonSlippery { get; set; }
        public bool HasFairy { get; set; }
        public bool CanSeeInvisible { get; set; }

        // Sounds
        public int NumAttachedSounds { get; set; }
        public SoundHolder[] AttachedSounds { get; set; }

        // Gibs
        public int NumGibs { get; set; }
        public GibReference[] Gibs { get; set; }

        // Lights
        public int NumLights { get; set; }
        public LightHolder[] Lights { get; set; }

        // Character Health Data
        public float MaxHitPoints { get; set; }
        public int NumHealthBars { get; set; }

        // Character NPC Data Part 1
        public bool IsUndying { get; set; }
        public float UndieTime { get; set; }
        public float UndieHitPoints { get; set; }
        public int HitTolerance { get; set; }
        public float KnockdownTolerance { get; set; }
        public int ScoreValue { get; set; }
        public int ExperienceValue { get; set; }
        public bool RewardOnKill { get; set; }
        public bool RewardOnOverkill { get; set; }
        public int Regeneration { get; set; }
        public float MaxPanic { get; set; }
        public float ZapModifier { get; set; }
        public float Length { get; set; }
        public float Radius { get; set; }
        public float Mass { get; set; }
        public float Speed { get; set; }
        public float TurnSpeed { get; set; }
        public float BleedRate { get; set; }
        public float StunTime { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter<Banks>))] public Banks SummonElementBank { get; set; }
        public string SummonElementCueString { get; set; }

        // Resistances
        public int NumResistances { get; set; }
        public Resistance[] Resistances { get; set; }

        // Model
        public int NumModelProperties { get; set; }
        public ModelProperties[] ModelProperties { get; set; }
        public string SkinnedModel { get; set; } /* ER */

        // Attached Effects
        // first : bone name, second : effect name
        public int NumAttachedEffects { get; set; }
        public EffectHolder[] AttachedEffects;

        // Animation Data
        public AnimationList[] AnimationClips { get; set; } // This "list" should ALWAYS contain 27 "lists" of 231 elements each.

        // Equipment Data
        public int NumEquipementAttachments { get; set; }
        public EquipmentAttachment[] EquipmentAttachments {get;set;}

        // Event Data
        public ConditionCollection Conditions { get; set; }

        // Character NPC Data Part 2
        // NOTE : These look like weights and such for the utility system, maybe?
        // TODO : Figure out what system they interact with and how it is implemented. It's not really necessary for the reader / writer impl, but it would be cool to know if this uses an US under the hood.
        public float AlertRadius { get; set; }
        public float GroupChase { get; set; }
        public float GroupSeparation { get; set; }
        public float GroupCohesion { get; set; }
        public float GroupAlignment { get; set; }
        public float GroupWander { get; set; }
        public float FriendlyAvoidance { get; set; }
        public float EnemyAvoidance { get; set; }
        public float SightAvoidance { get; set; }
        public float DangerAvoidance { get; set; }
        public float AngerWeight { get; set; }
        public float DistanceWeight { get; set; }
        public float HealthWeight { get; set; }
        public bool Flocking { get; set; }
        public float BreakFreeStrength { get; set; }

        // Abilities
        public int NumAbilities { get; set; }
        public Ability[] Abilities {get;set;}

        // Movement Properties
        public int NumMovementData { get; set; }
        public MovementData[] MovementData { get; set; }

        // Buff Data
        public int NumBuffs { get; set; }
        public BuffStorage[] Buffs { get; set; } // Buff as in buffing a character's stats, not buffer...

        // Aura Data
        public int NumAuras { get; set; }
        public AuraStorage[] Auras { get; set; }

        #endregion

        #region Constructor

        public CharacterTemplate()
        {
            // ID Strings Initialization
            this.CharacterID = default;
            this.CharacterDisplayID = default;
            
            // Enum data initialization
            this.Faction = default;
            this.BloodType = default;

            // Flags initialization
            this.IsEthereal = false;
            this.LooksEthereal = false;
            this.IsFearless = false;
            this.IsUncharmable = false;
            this.IsNonSlippery = false;
            this.HasFairy = false;
            this.CanSeeInvisible = false;

            // Sounds initialization
            this.NumAttachedSounds = 0;
            this.AttachedSounds = new SoundHolder[4]; // we hard code 4 because this is all that Magicka ever uses.

            // Gibs initialization
            this.NumGibs = 0;
            this.Gibs = new GibReference[0];

            // Light initialization
            this.NumLights = 0;
            this.Lights = new LightHolder[0]; // a character in Magicka can only hold up to 4 light holders.

            // Health initialization
            this.MaxHitPoints = 100.0f; // Maybe this value is not a good one for Magicka, but this is the default I'm going with for now lol...
            this.NumHealthBars = 1; // We should have at least 1 health bar, but even if we set it to 0 it does not matter, the reader code clamps it.

            // Character Data initialization
            this.IsUndying = false;
            this.UndieTime = 0.0f;
            this.UndieHitPoints = 100.0f;
            this.HitTolerance = 0;
            this.KnockdownTolerance = 0.0f;
            this.ScoreValue = 100;
            this.ExperienceValue = 100;
            this.RewardOnKill = true;
            this.RewardOnOverkill = true;
            this.Regeneration = 10;
            this.MaxPanic = 1.0f;
            this.ZapModifier = 0.0f;
            this.Length = 1.0f;
            this.Radius = 1.0f;
            this.Mass = 1.0f;
            this.Speed = 100.0f;
            this.TurnSpeed = 10.0f;
            this.BleedRate = 10.0f;
            this.StunTime = 10.0f;
            this.SummonElementBank = default;
            this.SummonElementCueString = default;

            // Resistances
            this.NumResistances = 0;
            this.Resistances = new Resistance[0]; // NOTE : We used to hard code this to 11, just like Magicka does, because there are only 11 elements that we should be capable of adding resistances for, with indices 0 to 10 (read the notes within Elements.cs for further context and information), but tbh Magicka does not really impose a hard limit of how many resistances can be stored within the XNB file, so we should just uncap this.

            // Model
            this.NumModelProperties = 0;
            this.ModelProperties = new ModelProperties[0];
            this.SkinnedModel = default;

            // Effects
            this.NumAttachedEffects = 0;
            this.AttachedEffects = new EffectHolder[0];

            // Animations
            this.AnimationClips = new AnimationList[TOTAL_ANIMATION_SETS];

            // Equipment Attachments
            this.NumEquipementAttachments = 0;
            this.EquipmentAttachments = new EquipmentAttachment[8]; // We hard code this to 8, just like Magicka does. We can have 8 attachments per character at most.

            // Event Data
            this.Conditions = new ConditionCollection();

            // Character Data Initialization Part 2
            this.AlertRadius = default;
            this.GroupChase = default;
            this.GroupSeparation = default;
            this.GroupCohesion = default;
            this.GroupAlignment = default;
            this.GroupWander = default;
            this.FriendlyAvoidance = default;
            this.EnemyAvoidance = default;
            this.SightAvoidance = default;
            this.DangerAvoidance = default;
            this.AngerWeight = default;
            this.DistanceWeight = default;
            this.HealthWeight = default;
            this.Flocking = default;
            this.BreakFreeStrength = default;

            // Abilities
            this.NumAbilities = 0;
            this.Abilities = new Ability[0];

            // Movement Data
            this.NumMovementData = 0;
            this.MovementData = new MovementData[0];

            // Buff Data
            this.NumBuffs = 0;
            this.Buffs = new BuffStorage[0];

            // Aura Data
            this.NumAuras = 0;
            this.Auras = new AuraStorage[0];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading CharacterTemplate...");

            // Internally, Magicka computes a hash for both of these ID strings after reading them, but we don't need it here.
            // Magicka also passes the strings to lower invariant, but we don't need to do that. We can work with whatever we want considering
            // how internally the game just "normalizes" the strings by passing them all to lowercase invariant to be able to compute consistent hashes.
            // We also don't do it because we don't really want to mutate the data. The idea is that if we decompile an XNB file and then recompile the JSON, we should
            // get the exact same file back (or maybe slightly different because now it has extra readers attached lol...)
            this.CharacterID = reader.ReadString();
            this.CharacterDisplayID = reader.ReadString();

            logger?.Log(2, $" - CharacterID        : {this.CharacterID}");
            logger?.Log(2, $" - CharacterDisplayID : {this.CharacterID}");

            // Read enum data
            this.Faction = (Factions)reader.ReadInt32();
            this.BloodType = (BloodType)reader.ReadInt32();

            logger?.Log(2, $" - Faction   : {this.Faction}");
            logger?.Log(2, $" - BloodType : {this.BloodType}");

            // Read flags
            this.IsEthereal = reader.ReadBoolean();
            this.LooksEthereal = reader.ReadBoolean();
            this.IsFearless = reader.ReadBoolean();
            this.IsUncharmable = reader.ReadBoolean();
            this.IsNonSlippery = reader.ReadBoolean();
            this.HasFairy = reader.ReadBoolean();
            this.CanSeeInvisible = reader.ReadBoolean();

            logger?.Log(2, $" - IsEthereal      : {this.IsEthereal}");
            logger?.Log(2, $" - LooskEthereal   : {this.LooksEthereal}");
            logger?.Log(2, $" - IsFearless      : {this.IsFearless}");
            logger?.Log(2, $" - IsUncharmable   : {this.IsUncharmable}");
            logger?.Log(2, $" - IsNonSlippery   : {this.IsNonSlippery}");
            logger?.Log(2, $" - HasFairy        : {this.HasFairy}");
            logger?.Log(2, $" - CanSeeInvisible : {this.CanSeeInvisible}");

            // Read character sounds
            this.NumAttachedSounds = reader.ReadInt32(); // NOTE : To prevent having to store this value as a variable within this class and just working with the array input data from JSON, we could just initialize the attached sounds array to this input length here, and set the length to min(4, reader.readi32()), maybe do this in the future when we clean up all of the manually hard coded counts in the other JSON files for the level data and stuff?
            logger?.Log(2, $" - NumAttachedSounds : {this.NumAttachedSounds}");
            if (this.NumAttachedSounds > 4)
                throw new MagickaLoadException(GetExceptionNumAttachedSounds());
            
            for (int i = 0; i < this.NumAttachedSounds; ++i)
            {
                #region Comment
                // Can't read more than 4 sounds, since that is the max amount of sounds reserved by magicka for each CharacterTemplate, so we break out.
                // Note that imo this should potentially be an exception on mpup, but we'll do the thing that magicka does and just let it slide... for now.
                // Also, note that magicka's code just keeps looping over the non valid values out of bounds rather than breaking, so malformed files will not read
                // the bytes, which will break things and just crash on load, and also wastes runtime in case of a miswritten byte making this loop iterate for more than it should, when we can just break out and call it a day...
                // In the future, when the engine rewrite comes around, we could actually modify this to support more than 4 sounds per character template...
                #endregion
                // if (i >= 4)
                //     break;
                this.AttachedSounds[i] = SoundHolder.Read(reader, logger);
            }

            // Read character gibs (GORE!!!!!!! YEAH, BABY!!!! BLOOD!)
            this.NumGibs = reader.ReadInt32();
            logger?.Log(2, $" - NumGibs : {this.NumGibs}");
            this.Gibs = new GibReference[this.NumGibs];
            for(int i = 0; i < this.NumGibs; ++i)
            {
                this.Gibs[i] = GibReference.Read(reader, logger);
            }

            // Read character lights
            this.NumLights = reader.ReadInt32();
            logger?.Log(2, $" - NumLights : {this.NumLights}");
            if (this.NumLights > 4)
                throw new MagickaLoadException(GetExceptionNumLights());
            this.Lights = new LightHolder[this.NumLights];
            for (int i = 0; i < this.NumLights; ++i)
            {
                // Once again, there's a limit of 4 for these. In this case tho, Magicka does throw an exception if the found character has more than 4 point light holders!
                // if (i >= 4)
                //     break;
                this.Lights[i] = LightHolder.Read(reader, logger);
            }

            // Read character health data (HP and num health bars)
            // NOTE : The number of health bars should be at least 1 (duh). If the input is N <= 0, we set it to 1, just as Magicka does internally.
            this.MaxHitPoints = reader.ReadSingle();
            this.NumHealthBars = reader.ReadInt32();
            if (this.NumHealthBars <= 0)
                this.NumHealthBars = 1;

            // Read character data
            this.IsUndying = reader.ReadBoolean();
            this.UndieTime = reader.ReadSingle();
            this.UndieHitPoints = reader.ReadSingle();
            this.HitTolerance = reader.ReadInt32();
            this.KnockdownTolerance = reader.ReadSingle();
            this.ScoreValue = reader.ReadInt32();
            this.ExperienceValue = reader.ReadInt32();
            this.RewardOnKill = reader.ReadBoolean();
            this.RewardOnOverkill = reader.ReadBoolean();
            this.Regeneration = reader.ReadInt32();
            this.MaxPanic = reader.ReadSingle();
            this.ZapModifier = reader.ReadSingle();
            this.Length = reader.ReadSingle(); /* Math.Max(reader.ReadSingle(), 0.01f); */ // NOTE : this is limited with Math.Max(readValue, 0.01f) within Magicka's code, but we don't care about that here tbh
            this.Radius = reader.ReadSingle();
            this.Mass = reader.ReadSingle();
            this.Speed = reader.ReadSingle();
            this.TurnSpeed = reader.ReadSingle();
            this.BleedRate = reader.ReadSingle();
            this.StunTime = reader.ReadSingle();
            this.SummonElementBank = (Banks)reader.ReadInt32();
            this.SummonElementCueString = reader.ReadString();

            // Read resistances (elemental resistances)
            this.NumResistances = reader.ReadInt32();
            this.Resistances = new Resistance[this.NumResistances];
            for (int i = 0; i < this.NumResistances; ++i)
            {
                this.Resistances[i] = new Resistance(reader, logger);
            }

            // Read Model
            this.NumModelProperties = reader.ReadInt32();
            this.ModelProperties = new ModelProperties[this.NumModelProperties];
            for (int i = 0; i < this.NumModelProperties; ++i)
            {
                this.ModelProperties[i] = Character.ModelProperties.Read(reader, logger);
            }
            this.SkinnedModel = reader.ReadString(); /* ER */

            // Read Attached Effects / Particles
            // NOTE : This is kinda similar to the mesh settings stuff for the level data, it comes in pairs where we assign a particle to a specific bone name. The bone acts as a socket for the particles.
            this.NumAttachedEffects = reader.ReadInt32();
            this.AttachedEffects = new EffectHolder[this.NumAttachedEffects];
            for (int i = 0; i < this.NumAttachedEffects; ++i)
            {
                this.AttachedEffects[i] = EffectHolder.Read(reader, logger);
            }

            // Read animation data
            // NOTE : A character has a limit of 27 lists of animation clips, and each list of animation clips has a limit of 231 individual animation clips.
            // In short, we have a 2D array AnimationClipAction[27][], and 1D array within that 2D array is an AnimationClipAction[231].
            // Not sure as of now what the implications of this are, or why the Magicka devs coded it like that, but here we are...
            // All I know is that there's a limit of 27 animation lists, and each list can hold at most 231 animations, which is the number of total animations
            // that exist in the "base" game (Magicka + DLCs, this value basically changes depending on what DLCs a given version has access to...)
            for (int i = 0; i < this.AnimationClips.Length; ++i)
            {
                // TODO : Finish making sure that the implementation of the AnimationList reading process is correct...
                // Remember that part of it comes from the loop within this class, since that is what the anim list class is abstracting away.
                this.AnimationClips[i] = new AnimationList(reader, logger);
            }

            // Read Equipment Attachments
            this.NumEquipementAttachments = reader.ReadInt32();
            for (int i = 0; i < this.NumEquipementAttachments; ++i)
            {
                this.EquipmentAttachments[i] = EquipmentAttachment.Read(reader, logger);
            }

            // Read Events into Condition Collection
            this.Conditions = ConditionCollection.Read(reader, logger);

            // Read Character Data part 2
            this.AlertRadius = reader.ReadSingle();
            this.GroupChase = reader.ReadSingle();
            this.GroupSeparation = reader.ReadSingle();
            this.GroupCohesion = reader.ReadSingle();
            this.GroupAlignment = reader.ReadSingle();
            this.GroupWander = reader.ReadSingle();
            this.FriendlyAvoidance = reader.ReadSingle();
            this.EnemyAvoidance = reader.ReadSingle();
            this.SightAvoidance = reader.ReadSingle();
            this.DangerAvoidance = reader.ReadSingle();
            this.AngerWeight = reader.ReadSingle();
            this.DistanceWeight = reader.ReadSingle();
            this.HealthWeight = reader.ReadSingle();
            this.Flocking = reader.ReadBoolean();
            this.BreakFreeStrength = reader.ReadSingle();

            // Abilities
            this.NumAbilities = reader.ReadInt32();
            this.Abilities = new Ability[NumAbilities];
            for (int i = 0; i < this.NumAbilities; ++i)
            {
                this.Abilities[i] = Ability.Read(reader, logger);
            }

            // Movement Data
            this.NumMovementData = reader.ReadInt32();
            this.MovementData = new MovementData[this.NumMovementData];
            for (int i = 0; i < this.NumMovementData; ++i)
                MovementData[i] = new MovementData(reader, logger);

            // Buff Data
            this.NumBuffs = reader.ReadInt32();
            this.Buffs = new BuffStorage[this.NumBuffs];
            for(int i = 0; i < this.NumBuffs; ++i)
                this.Buffs[i] = new BuffStorage(reader, logger);

            // Aura Data
            this.NumAuras = reader.ReadInt32();
            this.Auras = new AuraStorage[this.NumAuras];
            for (int i = 0; i < this.NumAuras; ++i)
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

            // Character ID Strings
            writer.Write(this.CharacterID);
            writer.Write(this.CharacterDisplayID);

            // Faction and blood enums
            writer.Write((int)this.Faction);
            writer.Write((int)this.BloodType);

            // Character properties (1)
            writer.Write(this.IsEthereal);
            writer.Write(this.LooksEthereal);
            writer.Write(this.IsFearless);
            writer.Write(this.IsUncharmable);
            writer.Write(this.IsNonSlippery);
            writer.Write(this.HasFairy);
            writer.Write(this.CanSeeInvisible);

            // Sounds
            writer.Write(this.NumAttachedSounds);
            if (this.NumAttachedSounds > 4)
                throw new MagickaWriteException(GetExceptionNumAttachedSounds());
            for (int i = 0; i < this.NumAttachedSounds; ++i)
                this.AttachedSounds[i].WriteInstance(writer, logger);

            // Gibs
            writer.Write(this.NumGibs);
            for (int i = 0; i < this.NumGibs; ++i)
                this.Gibs[i].WriteInstance(writer, logger);

            // Lights
            writer.Write(this.NumLights);
            if (this.NumLights > 4)
                throw new MagickaWriteException(GetExceptionNumLights());
            for (int i = 0; i < this.NumLights; ++i)
                this.Lights[i].WriteInstance(writer, logger);

            // Health
            writer.Write(this.MaxHitPoints);
            writer.Write(this.NumHealthBars);

            // Character properties (2)
            writer.Write(this.IsUndying);
            writer.Write(this.UndieTime);
            writer.Write(this.UndieHitPoints);
            writer.Write(this.HitTolerance);
            writer.Write(this.KnockdownTolerance);
            writer.Write(this.ScoreValue);
            writer.Write(this.ExperienceValue);
            writer.Write(this.RewardOnKill);
            writer.Write(this.RewardOnOverkill);
            writer.Write(this.Regeneration);
            writer.Write(this.MaxPanic);
            writer.Write(this.ZapModifier);
            writer.Write(this.Length);
            writer.Write(this.Radius);
            writer.Write(this.Mass);
            writer.Write(this.Speed);
            writer.Write(this.TurnSpeed);
            writer.Write(this.BleedRate);
            writer.Write(this.StunTime);
            writer.Write((int)this.SummonElementBank);
            writer.Write(this.SummonElementCueString);

            // Resistances
            writer.Write(this.NumResistances);
            foreach (var resistance in this.Resistances)
                resistance.Write(writer, logger);

            throw new NotImplementedException("Write Character Template not implemented yet!");
        }

        public override string GetReaderName()
        {
            return "Magicka.ContentReaders.CharacterTemplateReader, Magicka, Version=1.0.0.0, Culture=neutral";
        }

        #endregion

        #region PrivateMethods

        private string GetExceptionNumAttachedSounds()
        {
            return $"CharacterTemplate cannot contain more than 4 attached sounds, but {this.NumAttachedSounds} were found!";
        }

        private string GetExceptionNumLights()
        {
            return $"CharacterTemplate cannot contain more than 4 attached lights, but {this.NumLights} were found!";
        }

        #endregion
    }
}
