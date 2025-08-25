// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Gameplay.Systems.NewTooltip.TooltipLibraryFiles
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using System;
    using Eco.Core.PropertyHandling;
    using Eco.Gameplay.GameActions;
    using Eco.Gameplay.Skills;
    using System.Linq;
    using Eco.Mods.TechTree;

    [TooltipLibrary] public static class TieredCaloriesTooltipLibrary
    {
        public static void Initialize() { }
		
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
				s.AppendLineLoc($"Tier: {tier}");
				
				var ratio = TieredCaloriesHelper.GetReductionRatio(item, user);
				
				if (ratio == 1)
				{
					s.AppendLineLoc($"No calories reduction.");
				}
				else
				{
					s.AppendLineLoc($"Calories after reduction: {tieredCalories.BaseCalories / ratio}");
				}
				
				return s.ToLocString().Trim();
			}
			
			return LocString.Empty;
        }
    }
}
