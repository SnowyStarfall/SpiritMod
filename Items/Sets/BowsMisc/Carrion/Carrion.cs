using Microsoft.Xna.Framework;
using SpiritMod.Projectiles.Arrow;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace SpiritMod.Items.Sets.BowsMisc.Carrion
{
	public class Carrion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Carrion");
			Tooltip.SetDefault("Wooden Arrows turn into Eyeballs!");
		}



		public override void SetDefaults()
		{
			item.damage = 14;
			item.noMelee = true;
			item.ranged = true;
			item.width = 50;
			item.height = 30;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shoot = ProjectileID.Shuriken;
			item.useAmmo = AmmoID.Arrow;
			item.knockBack = 4;
			item.useTurn = false;
			item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shootSpeed = 11.25f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly) {
				type = ModContent.ProjectileType<CarrionCrowArrow>();
			}
			for (int I = 0; I < 2; I++)
			{
				float angle = Main.rand.NextFloat(MathHelper.PiOver4, -MathHelper.Pi - MathHelper.PiOver4);
				Vector2 spawnPlace = Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 45f;
				if (Collision.CanHit(position, 0, 0, position + spawnPlace, 0, 0))
				{
					position += spawnPlace;
				}

				Vector2 velocity = Vector2.Normalize(Main.MouseWorld - position) * item.shootSpeed;
				int p = Projectile.NewProjectile(position.X, position.Y, velocity.X, velocity.Y, type, damage, knockBack, 0, 0.0f, 0.0f);
				for (float num2 = 0.0f; (double)num2 < 10; ++num2)
				{
					int dustIndex = Dust.NewDust(position, 2, 2, 54, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity = Vector2.Normalize(spawnPlace.RotatedBy(Main.rand.NextFloat(MathHelper.TwoPi))) * 1.6f;
				}
			}
			return false;

		}
	}
}
