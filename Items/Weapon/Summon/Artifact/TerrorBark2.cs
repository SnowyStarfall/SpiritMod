using Microsoft.Xna.Framework;
using SpiritMod.Buffs.Artifact;
using SpiritMod.Projectiles.Summon.Artifact;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace SpiritMod.Items.Weapon.Summon.Artifact
{
	public class TerrorBark2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terror Bark");
			Tooltip.SetDefault("Takes up two minion slots\nSummons a Terror Fiend to shoot Wither bolts at foes\nWither bolts may inflict 'Blood Corruption'\nTerror Fiends may also shoot out more powerful, homing Wither Bolts\nOther summons recieve a buff to their damage and knockback, signified by a Nightmare Eye that appears above the player");
		}


		public override void SetDefaults()
		{
			item.width = 56;
			item.height = 66;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 4;
			item.mana = 11;
			item.damage = 27;
			item.knockBack = 3;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 30;
			item.useAnimation = 30;
			item.summon = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<Terror2Summon>();
			item.buffType = ModContent.BuffType<Terror2SummonBuff>();
			item.buffTime = 3600;
			item.UseSound = SoundID.Item60;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(mod, "ItemName", "Artifact Weapon");
			line.overrideColor = new Color(100, 0, 230);
			tooltips.Add(line);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<NightmareEye>(), 0, 0, player.whoAmI, 0f, 0f);
			return true;
		}

		/*  public override void AddRecipes()
          {
              ModRecipe recipe = new ModRecipe(mod);
              recipe.AddIngredient(ModContent.ItemType<TerrorBark1>(), 1);
              recipe.AddIngredient(ModContent.ItemType<NecroticSkull>(), 1);
              recipe.AddIngredient(ModContent.ItemType<TideStone>(), 1);
              recipe.AddIngredient(ModContent.ItemType<StellarTech>(), 1);
              recipe.AddIngredient(ModContent.ItemType<PrimordialMagic>(), 100);
              recipe.AddTile(ModContent.TileType<CreationAltarTile>());
              recipe.SetResult(this, 1);
              recipe.AddRecipe();

          }*/
	}
}