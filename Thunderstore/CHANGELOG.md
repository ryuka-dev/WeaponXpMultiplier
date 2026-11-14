# Changelog

## 1.0.3

- Added a Traditional Chinese (zh-TW) translation for the SULFUR Config settings page.
- The plugin now reports its real version. 1.0.2 still called itself 1.0.0 in the BepInEx log and in config managers.
- If a game update ever moves the code this mod patches, the log now says the mod is inactive.
- The settings page description no longer shows an internal method name.
- Readme fixes: manual install now includes the `lang` folder, and it explains that a comma in `XpMultiplier` is read as a thousands separator (`0,2` gives 2x).
- No gameplay changes. Checked on SULFUR 1.0.6.

## 1.0.2

- Moved the localization files into a `lang/` folder next to the plugin DLL (the layout preferred by SULFUR Native UI Lib). No gameplay changes.
- Verified against SULFUR v0.18.4: `Weapon.AddExperience(float)` is unchanged, so the multiplier behaves exactly as before.

## 1.0.1

- Added localization files for the SULFUR Config options page (14 languages). No gameplay changes.

## 1.0.0

- Initial release: configurable 0-100x weapon XP multiplier patched onto `Weapon.AddExperience(float)`.
