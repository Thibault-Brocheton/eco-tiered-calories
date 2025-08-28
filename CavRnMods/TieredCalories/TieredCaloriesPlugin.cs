namespace CavRn.TieredCalories
{
    using Eco.Core.Plugins.Interfaces;
    using Eco.Core.Plugins;
    using Eco.Core.Utils;
    using Eco.Shared.Utils;

	public class TieredCaloriesMod: IModInit
	{
		public static ModRegistration Register() => new()
		{
			ModName = "TieredCalories",
			ModDescription = "Adds a tier-based food system where higher-level players gain fewer calories from lower-tier meals, encouraging progression and balanced gameplay.",
			ModDisplayName = "Tiered Calories"
		};
	}

	public class TieredCaloriesConfig: Singleton<TieredCaloriesConfig>
	{
		public bool Enabled { get; set; } = true;
        public int[] MaxSkillsWithoutReduction { get; set; } = new int[] {1,2,4,6,100};
        public int[] CaloriesDivisionPerAdditionalSkill { get; set; } = new int[] {2,3,4,7,10};
        public bool CountStarsSpentInsteadOfSkills { get; set; } = false;
    }

	public class TieredCaloriesPlugin: Singleton<TieredCaloriesPlugin>, IModKitPlugin, IConfigurablePlugin
	{
		public static ThreadSafeAction OnSettingsChanged = new();
		public IPluginConfig PluginConfig => this.config;
		private readonly PluginConfig<TieredCaloriesConfig> config;
		public TieredCaloriesConfig Config => this.config.Config;
		public ThreadSafeAction<object, string> ParamChanged { get; set; } = new();

		public TieredCaloriesPlugin()
		{
			this.config = new PluginConfig<TieredCaloriesConfig>("TieredCalories");
			this.SaveConfig();
		}

		public string GetStatus()
		{
			return "OK";
		}

		public string GetCategory()
		{
			return "Mods";
		}

		public object GetEditObject() => this.config.Config;
		public void OnEditObjectChanged(object o, string param) { this.SaveConfig(); }
	}
}
