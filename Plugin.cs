using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using PerfectRandom.Sulfur.Core.Weapons;
using System;
using System.Reflection;

namespace WeaponXpMultiplier
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class Plugin : BaseUnityPlugin
    {
        // Released GUID: it names the player's config file and the Harmony ID. Never rename it.
        public const string PluginGuid = "kumo.sulfur.weapon_xp_multiplier";
        public const string PluginName = "Weapon XP Multiplier";
        public const string PluginVersion = "1.0.3";

        internal static ManualLogSource Log;

        internal static ConfigEntry<bool> EnableMod;
        internal static ConfigEntry<float> XpMultiplier;
        internal static ConfigEntry<bool> LogXpChanges;

        private Harmony harmony;

        private void Awake()
        {
            Log = Logger;

            EnableMod = Config.Bind(
                "General",
                "EnableMod",
                true,
                "Enable the weapon XP multiplier."
            );

            // AcceptableValueRange clamps on load and on every set (NaN becomes 0),
            // so the prefix can use the value as is.
            XpMultiplier = Config.Bind(
                "General",
                "XpMultiplier",
                2.0f,
                new ConfigDescription(
                    "Weapon XP multiplier. 1.0 = vanilla, 2.0 = double XP, 10.0 = ten times XP.",
                    new AcceptableValueRange<float>(0f, 100f)
                )
            );

            LogXpChanges = Config.Bind(
                "Debug",
                "LogXpChanges",
                false,
                "Log weapon XP changes to the BepInEx log. Keep this off for normal gameplay."
            );

            MethodInfo target = AccessTools.Method(
                typeof(Weapon),
                nameof(Weapon.AddExperience),
                new[] { typeof(float) }
            );

            if (target == null)
            {
                Logger.LogError(
                    "Weapon.AddExperience(float) was not found. The game has probably changed; the mod is inactive."
                );
                return;
            }

            harmony = new Harmony(PluginGuid);

            try
            {
                harmony.Patch(target, prefix: new HarmonyMethod(typeof(Plugin), nameof(AddExperiencePrefix)));
            }
            catch (Exception e)
            {
                Logger.LogError("Patching Weapon.AddExperience(float) failed; the mod is inactive. " + e);
                return;
            }

            Logger.LogInfo("Patched Weapon.AddExperience(float).");
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();
        }

        private static void AddExperiencePrefix(Weapon __instance, ref float experienceIncrease)
        {
            if (!EnableMod.Value)
                return;

            // Also rejects NaN.
            if (!(experienceIncrease > 0f))
                return;

            float multiplier = XpMultiplier.Value;

            if (Math.Abs(multiplier - 1f) < 0.0001f)
                return;

            float originalXp = experienceIncrease;
            experienceIncrease *= multiplier;

            if (LogXpChanges.Value)
            {
                string weaponName = __instance != null ? __instance.ToString() : "Unknown Weapon";
                Log.LogInfo("Weapon XP changed for " + weaponName + ": " + originalXp + " -> " + experienceIncrease);
            }
        }
    }
}
