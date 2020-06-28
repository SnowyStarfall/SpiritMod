using Microsoft.Xna.Framework;
using SpiritMod.Items.Material;
using SpiritMod.Projectiles;
using SpiritMod.Projectiles.DonatorItems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.DonatorItems
{
	public class RealityDefier : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mnemonic's Misfire");
			Tooltip.SetDefault("'You thought this gun shot BULLETS? Hah!'\nLeft click to shoot out an exploding blue fist\nRight-click to throw the gun itself");
		}


		private Vector2 newVect;
		public override void SetDefaults()
		{
			item.damage = 59;
			item.melee = true;
			item.width = 65;
			item.height = 21;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 10780;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<BlueFist>();
			item.shootSpeed = 12f;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2) {
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.noUseGraphic = true;
				item.UseSound = SoundID.Item1;
				item.shootSpeed = 8f;
			} else {
				item.noUseGraphic = false;
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.UseSound = SoundID.Item92;
				item.shootSpeed = 12f;
			}
			return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if(player.altFunctionUse == 2) {

				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<GunProj>(), (int)(damage * 2.5f), knockBack, player.whoAmI);
				return false;
			} else {
				int proj2 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);

				return false;
			}
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Shotgun, 1);
			recipe.AddIngredient(ModContent.ItemType<SoulShred>(), 5);
			recipe.AddIngredient(ModContent.ItemType<SpiritBar>(), 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}