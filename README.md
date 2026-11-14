# Weapon XP Multiplier

A BepInEx 5 mod for SULFUR that multiplies the XP weapons earn from kills. The player-facing description is in `Thunderstore/README.md`.

## How it works

The mod puts a Harmony prefix on:

```text
PerfectRandom.Sulfur.Core.Weapons.Weapon.AddExperience(float)
```

The prefix multiplies the `experienceIncrease` argument by `XpMultiplier`. Zero, negative and NaN amounts pass through untouched.

The only gameplay callers are in `Npc.DistributeExperience(int)`, which runs from `Npc.GiveExperience()` when an enemy the player damaged dies. It applies `Stat_BonusXP` (Library Card, Lock of the Magus) first, then splits the result evenly across the weapons that took part in the kill, or gives it all to the equipped weapon if none did. Each share goes through `AddExperience`, so the total is still multiplied by `XpMultiplier`.

Paths that deliberately bypass the patch:

- Loading a save writes the Experience attribute directly (`ItemStats.LoadAttributesFromData`), so stored XP is never multiplied again.
- `DevToolsManager.AddExperienceToCurrentWeapon` calls `InventoryItem.AddExperience` directly.

The game types are referenced directly (`PerfectRandom.Sulfur.Core`), so a rename in a game update shows up as a build error. At runtime, if the method is missing or patching fails, the plugin logs an error saying it is inactive.

Last checked against SULFUR 1.0.6 (build 26490055).

## Configuration

`BepInEx/config/kumo.sulfur.weapon_xp_multiplier.cfg`

- `General.EnableMod` (default `true`)
- `General.XpMultiplier` (default `2.0`, range 0 to 100, `1.0` = vanilla). BepInEx clamps the value on load and on every change.
- `Debug.LogXpChanges` (default `false`): logs every multiplied XP gain

The GUID `kumo.sulfur.weapon_xp_multiplier` is a released identifier: it names the config file and the Harmony ID. Do not change it.

## Build

Machine-specific paths (game install, Gale profile) live only in `Directory.Build.props`; each can be overridden on the command line, e.g. `-p:SulfurGameDir=...`.

```powershell
dotnet build WeaponXpMultiplier.csproj -c Release
```

Build, deploy to the configured Gale profile, and copy the DLL into the package folder (Release only):

```powershell
dotnet build WeaponXpMultiplier.csproj -c Release -p:DeployToSulfurProfile=true
```

## Packaging

`Thunderstore/` is flat: its content, minus `nexusmods.txt` and generated archives, is exactly the release zip.

- `manifest.json`, `README.md` (store page), `CHANGELOG.md`, `icon.png`
- `lang/*.json`: SULFUR Config localization for this mod's settings page (15 languages)
- `WeaponXpMultiplier.dll`: copied in by the Release deploy build, not committed
- `nexusmods.txt`: generated NexusMods description (README + CHANGELOG), committed, not packaged

The version appears in three places and must match: `Thunderstore/manifest.json`, the top entry of `Thunderstore/CHANGELOG.md`, and `Plugin.PluginVersion`.
