# Eco Tiered Calories Mod

This mod has been developed for Eco version v11.1.13  
This mod introduces a **tier-based food system** for [Eco](https://www.strangeloopgames.com/eco/).  
Each food item is assigned a **Tier**. If a player's overall skill level is higher than the food's tier, the **calories provided are reduced proportionally**, encouraging players to consume higher-tier meals as they progress.

## Installation

Download files and copy AutoGen and CavRnMods Folder inside your Mods/UserCode folder in your eco server.  
! If other mods you installed already override some foods, you will have to handle conflicts.  

## ⚙️ Configuration

The mod automatically creates a configuration file:  
This file can be edited to adjust the mod's behavior.  

### Available Parameters

- **`Enabled`** *(bool, default = true)*  
  Enables or disables the tier-based calorie system.  
  - `true` → the system is active (calorie reduction based on the player's level).  
  - `false` → the system is disabled (foods behave like in vanilla Eco).  

- **`LevelOffset`** *(int, default = 0)*  
  Adds or remove an **artificial offset** to the player’s level when calculating calorie reduction.  
  - Example: if `LevelOffset = 2`, a level 3 player will be treated as level 5 for the calculation.  
  - Useful to make the mod **more restrictive**, forcing players to aim for higher-tier foods.
