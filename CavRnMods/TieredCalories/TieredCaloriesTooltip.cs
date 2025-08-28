namespace CavRn.TieredCalories
{
    using Eco.Core.PropertyHandling;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.NewTooltip.TooltipLibraryFiles;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Mods.TechTree;
    using Eco.Shared.IoC;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using System;

    [TooltipLibrary] public static class TieredCaloriesTooltipLibrary
    {
        public static void Initialize()
        {
            Skillset.UserSkillLevelChangedEvent.Add((user, _) => ServiceHolder<ITooltipSubscriptions>.Obj.MarkTooltipPartDirty(nameof(TooltipTierAndReduction), type: typeof(FoodItem), user: user, includeDerivedTypes: true, markDirtyForAllUsers: false));
        }

        [TooltipAffectedBy(typeof(UserXP), nameof(UserXP.StarsAvailable))]
        [NewTooltip(CacheAs.User | CacheAs.SubType, 109, overrideType: typeof(FoodItem))]
        public static LocString TooltipTierAndReduction(this FoodItem item, User user)
        {
            if (item is ITieredCalories tieredCalories)
            {
                var tier = (int?)ItemAttribute.Get<TierAttribute>(item.Type)?.Tier;

                if (tier == null)
                {
                    return LocString.Empty;
                }

                var s = new LocStringBuilder();
                s.AppendLineLoc($"{TextLoc.ColorLocStr(Color.White, TextLoc.BoldLocStr("Tiered Calories Mod"))}");
                var ratio = TieredCaloriesHelper.GetReductionRatio(item, user);
                var color = ratio == 1 ? Color.Green : Color.Orange;

                s.AppendLineLoc($"{TextLoc.ColorLocStr(color, $"Tier: {tier.Value}")}");
                s.AppendLineLoc($"{TextLoc.ColorLocStr(color, $"{Math.Truncate(tieredCalories.BaseCalories / ratio)} Calories ({Math.Truncate(100f / ratio)}%)")}");

                return s.ToLocString().Trim();
            }

            return LocString.Empty;
        }
    }
}
