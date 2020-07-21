using SpiritMod.Items.Material;
using SpiritMod.Projectiles.Flail;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace SpiritMod.Items.Weapon.Flail
{
	public class ClatterMace : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Clattering Mace");
			Tooltip.SetDefault("Has a chance to lower enemy defense on hit");
		}


		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 10;
			item.value = Item.sellPrice(0, 1, 43, 0);
			item.rare = ItemRarityID.Green;
			item.damage = 19;
			item.knockBack = 8;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = item.useAnimation = 35;
			item.scale = 1.1F;
			item.melee = true;
			item.noMelee = true;
			item.channel = true;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<ClatterMaceProj>();
			item.shootSpeed = 12.5F;
			item.UseSound = SoundID.Item1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Carapace>(), 14);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}