# Weapon XP Multiplier

Weapon XP Multiplier is a BepInEx mod for SULFUR that multiplies the experience weapons gain from kills.

## Features

- Configurable weapon XP multiplier (`XpMultiplier`, default `2.0`, `1.0` = vanilla).
- Can be toggled without uninstalling (`EnableMod`).
- Optional debug logging of every XP change (`LogXpChanges`).

## Compatibility

- Compatible with SULFUR v0.17.4 through v0.18.4: `Weapon.AddExperience(float)` is unchanged across these versions (checked against decompiled v0.18.4 assemblies).
- The mod locates `Weapon.AddExperience(float)` by reflection at startup and logs an error if a game update ever removes or renames it, so failures are visible instead of silent.

## Configuration

The config file is created on first launch at `BepInEx/config/kumo.sulfur.weapon_xp_multiplier.cfg`.

| Setting | Section | Default | Description |
| --- | --- | --- | --- |
| `EnableMod` | General | `true` | Enable the multiplier. |
| `XpMultiplier` | General | `2.0` | Weapon XP multiplier. `1.0` = vanilla. |
| `LogXpChanges` | Debug | `false` | Log every weapon XP change. |

## Build

Requires the game assemblies referenced in `WeaponXpMultiplier.csproj` (BepInEx core DLLs and Unity DLLs from a local SULFUR install).

```powershell
dotnet build WeaponXpMultiplier.csproj -c Release
```

The plugin DLL is written to `bin/Release/WeaponXpMultiplier.dll`.
