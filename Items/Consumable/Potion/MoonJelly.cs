
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpiritMod.Buffs.Potion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Consumable.Potion
{
	public class MoonJelly : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Jelly");
		}


		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.rare = ItemRarityID.Green;
			item.maxStack = 30;

			item.useStyle = ItemUseStyleID.EatingUsing;
			item.useTime = item.useAnimation = 20;

			item.consumable = true;
			item.autoReuse = false;
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.potion = true;
			item.healLife = 120;
			item.UseSound = SoundID.Item3;
		}
		public override Color? GetAlpha(Color lightColor) => Color.White;
		public override bool CanUseItem(Player player)
		{
			if (player.FindBuffIndex(BuffID.PotionSickness) >= 0) {
				return false;
			}
			return true;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) //pulsating glow effect in world
		{
			spriteBatch.Draw(Main.itemTexture[item.type], 
				item.Center - Main.screenPosition,
				null, 
				Color.Lerp(Color.White, Color.Transparent, 0.75f), 
				rotation, 
				item.Size / 2, 
				MathHelper.Lerp(1f, 1.3f, (float)Math.Sin(Main.GlobalTime * 3) / 2 + 0.5f), 
				SpriteEffects.None, 
				0);
		}
		public override bool UseItem(Player player)
		{
			item.healLife = 0; //set item's heal life to 0 when actually used, so it doesnt heal player
			if (!player.pStone)
				player.AddBuff(BuffID.PotionSickness, 3600);
			else
				player.AddBuff(BuffID.PotionSickness, 2700);

			player.AddBuff(ModContent.BuffType<MoonBlessing>(), 600);
			return true;
		}

		public override void UpdateInventory(Player player) => item.healLife = 120; //update the heal life back to 120 for tooltip and quick heal purposes

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach(TooltipLine line in tooltips.Where(x => x.mod == "Terraria" && x.Name == "HealLife")) {
				line.text = "Restores 120 life over 10 seconds\nCauses Potion Sickness";
			}
		}
	}
}
