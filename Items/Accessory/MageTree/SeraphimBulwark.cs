
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using SpiritMod.Items.Accessory.Leather;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory.MageTree
{
    [AutoloadEquip(EquipType.Shield)]
    public class SeraphimBulwark : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seraphim Bulwark");
			Tooltip.SetDefault("Increases maximum mana by 40\nAbsorbs 10% of the damage dealt by enemies\nThis damage is converted into a loss of mana instead");
		}


		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 2;
            item.defense = 4;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetSpiritPlayer().manaShield = true;
			player.statManaMax2 += 40;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ArcaneNecklace>());
            recipe.AddIngredient(ModContent.ItemType<ManaShield>());
            recipe.AddIngredient(ModContent.ItemType<Items.Sets.SpiritSet.SoulShred>(), 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
