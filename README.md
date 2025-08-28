# 🍅​ Eco Tiered Calories Mod

⚠️ This mod needs balancing ! ⚠️ To be tested in a full cycle ⚠️

This mod has been developed for Eco version v11.1.13  
This mod introduces a **tier-based food system** for [Eco](https://www.strangeloopgames.com/eco/).  
Each food item is assigned a **Tier**. If a player's overall skill level is higher than the food's tier, the **calories provided are reduced proportionally**, encouraging players to consume higher-tier meals as they progress.

## 🧮 Calculation Method

1. **Player Level Determination**  
   - ✅ Only skills where the player has **spent at least one star** are counted.  
   - ❌ Skills learned only via scrolls and the default *Survivalist* skill are ignored.  

2. **Food Tier Classification**  
   Each food item belongs to a tier:  
   - 🌽 **Raw food** → Tier 0  
   - 🍖 **Charred food** → Tier 1  
   - 🥗 **Campfire cooking** → Tier 2  
   - 🍱 **Cooking & Baking** → Tier 3  
   - 🍔 **Advanced Cooking & Advanced Baking** → Tier 4  

3. **Calorie Reduction Rule**  
   If the player’s skill count is **strictly greater** than the food’s tier, calories are reduced.  
   The larger the gap between the player’s level and the food’s tier, the stronger the reduction.  

## 💻 Installation

Download files and copy AutoGen and CavRnMods Folder inside your Mods/UserCode folder in your eco server.  
⚠️​ If other mods you installed already override some foods, you will have to handle conflicts.  

## ⚙️ Configuration

The mod automatically creates a configuration file, in Configs/TieredCalories.eco   
This file can be edited to adjust the mod's behavior.  

### Available Parameters

- **`Enabled`** *(bool, default = true)*  
  Enables or disables the tier-based calorie system.  
  - `true` → the system is active (calorie reduction based on the player's level).  
  - `false` → the system is disabled (foods behave like in vanilla Eco).  

- **`MaxSkillsWithoutReduction`** *(int[], default = [1, 2, 4, 6, 100])*  
  Per-food-tier skill threshold with no penalty.  
  Arrays are indexed by the Eco Tier value; if T is out of bounds, the last element is used.  

- **`DivisionPerAdditionalSkill`** *(int[], default = [2, 3, 4, 7, 10])*
  Divisor applied based on how many skills are over the threshold.  
  over=1 → use index 0, over=2 → index 1, etc.; if out of bounds, use the last value.  
  Tip: keep values ≥2, increasing, and cap your desired maximum (e.g. 10).  
  
- **`CountStarsSpentInsteadOfSkills`** *(bool, default = false)*
  If true, use total stars spent instead of the count of skilled professions.  
  To be used with [Skills Requirements](https://github.com/Thibault-Brocheton/eco-skills-requirements) that allows to increase the star cost of skills.
