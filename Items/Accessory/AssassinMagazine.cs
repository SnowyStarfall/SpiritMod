using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
	public class AssassinMagazine : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Assassin's Magazine");
			Tooltip.SetDefault("4% increased arrow and bullet damage when moving\nWhen standing still, nearby enemies are illuminated");
		}


		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.defense = 1;
			item.value = Item.buyPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetSpiritPlayer().assassinMag = true;
			if(player.velocity.X != 0) {
				player.arrowDamage += 0.04f;
				player.bulletDamage += 0.04f;
			} else if(player.velocity.Y != 0) {
				player.arrowDamage += 0.04f;
				player.bulletDamage += 0.04f;
			}

		}
	}
}
