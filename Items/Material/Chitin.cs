using Terraria.ModLoader;

namespace SpiritMod.Items.Material
{
	public class Chitin : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chitin");
			Tooltip.SetDefault("'Gorgeous, iridescent beetle scales'");
		}


		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 18;
			item.value = 20;
			item.rare = 1;

			item.maxStack = 999;
		}
	}
}
