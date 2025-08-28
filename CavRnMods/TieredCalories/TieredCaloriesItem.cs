namespace Eco.Mods.TechTree
{
    using CavRn.TieredCalories;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Logging;
    using Eco.Shared.Networking;
    using Eco.Shared.Utils;
    using System.Linq;
    using System;

    public interface ITieredCalories
    {
        float BaseCalories { get; set; }
    }

    public static class TieredCaloriesHelper
    {
        public static int GetReductionRatio(FoodItem item, User user)
        {
			if (!TieredCaloriesPlugin.Obj.Config.Enabled) {
				return 1;
			}

            var tier = (int?)ItemAttribute.Get<TierAttribute>(item.Type)?.Tier;
            if (tier == null)
            {
                Log.WriteErrorLineLoc($"{item.Name} has no Tier attribute");
                return 1;
            }

            var userValue = TieredCaloriesPlugin.Obj.Config.CountStarsSpentInsteadOfSkills ? user.Skillset.Skills.Sum(s => s.StarsSpent) : user.Skillset.Skills.Count(s => s.StarsSpent > 0);
            var tierValue = TieredCaloriesPlugin.Obj.Config.MaxSkillsWithoutReduction.GetAtIndexOrLast(tier.Value);

            // No reduction
            if (userValue <= tierValue)
            {
                return 1;
            }

            return TieredCaloriesPlugin.Obj.Config.CaloriesDivisionPerAdditionalSkill.GetAtIndexOrLast(userValue - tierValue - 1);
        }

        public static string Consume<T>(T item, Player player, Func<string> baseConsume)
            where T : FoodItem, ITieredCalories
        {
            var save = item.BaseCalories;
            var ratio = GetReductionRatio(item, player.User);
            try
            {
                item.BaseCalories = (float)Math.Truncate(item.BaseCalories / ratio);
                return baseConsume();
            }
            finally
            {
                if (ratio > 1)
                {
                    player.InfoBoxLocStr($"{item.DisplayName} provided only {Math.Truncate(100f / ratio)}% of its calories: {item.BaseCalories}");
                    item.BaseCalories = save;
                }
            }
        }

        public static string OnUsed<T>(T item, Player player, ItemStack stack, Func<string> baseOnUsed)
            where T : FoodItem, ITieredCalories
        {
            var save = item.BaseCalories;
            var ratio = GetReductionRatio(item, player.User);
            try
            {
                item.BaseCalories = (float)Math.Truncate(item.BaseCalories / ratio);
                return baseOnUsed();
            }
            finally
            {
                if (ratio > 1)
                {
                    player.InfoBoxLocStr($"{item.DisplayName} provided only {Math.Truncate(100f / ratio)}% of its calories: {item.BaseCalories}");
                    item.BaseCalories = save;
                }
            }
        }
    }

    public abstract class TieredCaloriesFoodItem : FoodItem, ITieredCalories
    {
        public virtual float BaseCalories { get; set; }
        public override float Calories => this.BaseCalories;

        [RPC]
        public override string Consume(Player player) =>
            TieredCaloriesHelper.Consume(this, player, () => base.Consume(player));

        public override string OnUsed(Player player, ItemStack itemStack) =>
            TieredCaloriesHelper.OnUsed(this, player, itemStack, () => base.OnUsed(player, itemStack));
    }

    public abstract class TieredCaloriesSeedItem : SeedItem, ITieredCalories
    {
        public virtual float BaseCalories { get; set; }
        public override float Calories => this.BaseCalories;

        [RPC]
        public override string Consume(Player player) =>
            TieredCaloriesHelper.Consume(this, player, () => base.Consume(player));

        public override string OnUsed(Player player, ItemStack itemStack) =>
            TieredCaloriesHelper.OnUsed(this, player, itemStack, () => base.OnUsed(player, itemStack));
    }
}
