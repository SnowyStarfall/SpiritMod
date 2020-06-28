using SpiritMod.Items.Material;
using SpiritMod.Projectiles.Thrown;
using Terraria.ID;
using Terraria.ModLoader;
namespace SpiritMod.Items.Weapon.Thrown
{
	public class OmicronOrb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orb of Omicron");
			Tooltip.SetDefault("Shoots out a Cosmic Orb that explodes into sticking pins!");
		}


		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.rare = 11;
			item.maxStack = 1;
			item.crit = 15;
			item.damage = 99;
			item.knockBack = 5;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = item.useAnimation = 20;
			item.value = Terraria.Item.sellPrice(0, 15, 0, 0);
			item.melee = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.consumable = false;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<OmicronOrbProj>();
			item.shootSpeed = 11;
			item.UseSound = SoundID.Item1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AccursedRelic>(), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}