# Weapon XP Multiplier

A BepInEx mod for **SULFUR** that lets you configure weapon level-up XP from 0x to 100x.

## What This Mod Does

This mod adds a configurable multiplier for weapon XP.

Default value:

```text
2.0x
```

Examples:

```text
XpMultiplier = 0     -> no weapon XP
XpMultiplier = 0.01  -> 1% weapon XP
XpMultiplier = 0.2   -> 20% weapon XP
XpMultiplier = 1     -> vanilla weapon XP
XpMultiplier = 2     -> double weapon XP
XpMultiplier = 10.2  -> 10.2x weapon XP
XpMultiplier = 100   -> 100x weapon XP
```

## What It Affects

This mod patches weapon XP gain through:

```text
PerfectRandom.Sulfur.Core.Weapons.Weapon.AddExperience(float)
```

It affects non-melee, non-throwable weapons that normally gain weapon XP.

## What It Does NOT Affect

This mod does not change:

```text
Player XP
Money
Loot
Enemy stats
Weapon damage
Weapon fire rate
Weapon durability
Melee weapon XP
Throwable weapon XP
```

## Configuration

After launching the game once with the mod installed, a config file will be generated at:

```text
BepInEx/config/kumo.sulfur.weapon_xp_multiplier.cfg
```

Default config:

```ini
[General]

EnableMod = true
XpMultiplier = 2

[Debug]

LogXpChanges = false
```

### EnableMod

```ini
EnableMod = true
```

Set to `false` to disable the mod without uninstalling it.

### XpMultiplier

```ini
XpMultiplier = 2
```

Weapon XP multiplier.

Allowed range:

```text
0 to 100
```

Decimal values are supported.

Use a period for decimals:

```ini
XpMultiplier = 0.2
XpMultiplier = 10.2
```

Do not use a comma:

```ini
XpMultiplier = 0,2
```

### LogXpChanges

```ini
LogXpChanges = false
```

Set this to `true` only for testing.

For normal gameplay, keep it set to `false`.

## Installation

### Thunderstore / r2modman

Install the mod through Thunderstore Mod Manager or r2modman.

Make sure the required BepInExPack dependency is installed.

### Manual Installation

1. Install BepInExPack for SULFUR.
2. Start the game once so BepInEx creates its folders.
3. Place `WeaponXpMultiplier.dll` into:

```text
BepInEx/plugins/
```

4. Start the game.
5. Edit the generated config file if you want a different multiplier.

## Compatibility

This mod should be compatible with most mods.

If another mod also changes weapon XP through `Weapon.AddExperience`, the final XP amount may depend on patch order.

Do not install this together with a fixed `Weapon Double XP` mod unless you intentionally want the multipliers to stack.

## Uninstallation

Remove:

```text
BepInEx/plugins/WeaponXpMultiplier.dll
```

Optionally remove:

```text
BepInEx/config/kumo.sulfur.weapon_xp_multiplier.cfg
```

## Changelog

### 1.0.0

- Initial release.
- Added configurable weapon XP multiplier.
- Supports 0x to 100x.
- Supports decimal multipliers.

## SULFUR Config Localization Support

This mod includes localization files for SULFUR Config.

This localization support is only for the in-game configuration page provided by SULFUR Config. It localizes the mod name, config sections, setting names, and setting descriptions shown in the config UI.

It does not change the game’s own text, item names, dialogue, or gameplay content.

Supported SULFUR Config languages:

- English
- Simplified Chinese
- Swedish
- French
- Italian
- German
- Spanish
- Portuguese
- Russian
- Polish
- Japanese
- Korean
- Turkish
- Arabic