using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory.DarkfeatherVisage
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkfeatherVisage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkfeather Visage");
            Tooltip.SetDefault("Generates exploding darkfeather bolts around the player\nIncreases magic damage by 8%\nCan be worn in the accessory or helmet slot");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 49;
            item.value = Item.sellPrice(0, 1, 6, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 1;
            item.accessory = true;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair) => drawHair = false;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += .08f;
            player.GetSpiritPlayer().darkfeatherVisage = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .08f;
            player.GetSpiritPlayer().darkfeatherVisage = true;
        }
    }
}
