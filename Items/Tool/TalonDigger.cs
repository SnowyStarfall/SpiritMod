using SpiritMod.Items.Material;
using Terraria.ID;
using Terraria.ModLoader;
namespace SpiritMod.Items.Tool
{
	public class TalonDigger : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Talon Digger");
		}


		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
			item.pick = 75;
			item.damage = 19;
			item.knockBack = 3;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 18;
			item.useAnimation = 18;
			item.melee = true;
			item.useTurn = true;
			item.autoReuse = true;
			item.UseSound = SoundID.Item1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Talon>(), 14);
			recipe.AddIngredient(ModContent.ItemType<FossilFeather>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}