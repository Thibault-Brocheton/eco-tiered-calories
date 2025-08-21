namespace CavRn.TieredCalories
{
	using Eco.Core.Plugins;
	using Eco.Core.Plugins.Interfaces;
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
		public int LevelOffset { get; set; } = 0;
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
