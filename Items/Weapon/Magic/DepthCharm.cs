using Microsoft.Xna.Framework;
using SpiritMod.Items.Material;
using SpiritMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Magic
{
	public class DepthCharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Depth Charm");
			Tooltip.SetDefault("Shoots out bouncing seawater globs that may explode into seawater upon hitting enemies");
		}



		public override void SetDefaults()
		{
			item.damage = 47;
			item.magic = true;
			item.mana = 12;
			item.width = 40;
			item.height = 40;
			item.useTime = 34;
			item.useAnimation = 34;
			item.useStyle = ItemUseStyleID.HoldingOut;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 0f;
			item.useTurn = false;
			item.value = Terraria.Item.sellPrice(0, 1, 50, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<DepthBall>();
			item.shootSpeed = 8f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<DepthShard>(), 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
			Terraria.Projectile.NewProjectile(mouse.X, mouse.Y, 0f, 0f, type, damage, knockBack, player.whoAmI);
			return false;
		}
	}
}
