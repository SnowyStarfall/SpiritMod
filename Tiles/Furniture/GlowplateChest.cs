using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using SpiritMod.Items.Placeable.Tiles;

namespace SpiritMod.Tiles.Furniture
{
	public class GlowplateChest : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1200;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileValue[Type] = 500;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.HookCheck = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[] { 127 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Glowplate Chest");
			AddMapEntry(new Color(70, 130, 180), name, MapChestName);
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.Containers };
			chest = "Glowplate Chest";
		}

		public string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;
			Tile tile = Main.tile[i, j];
			if (tile.frameX % 36 != 0) {
				left--;
			}
			if (tile.frameY != 0) {
				top--;
			}
			int chest = Chest.FindChest(left, top);
			if (Main.chest[chest].name == "") {
				return name;
			}
			else {
				return name + ": " + Main.chest[chest].name;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX % 36 != 0) {
				left--;
			}
			if (tile.frameY != 0) {
				top--;
			}
			return Chest.CanDestroyChest(left, top);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<GlowplateChestItem>());
		}
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 54 ? 36 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/Furniture/GlowplateChest_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), new Color(150, 150, 150, 100), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Tile t = Main.tile[i, j];
        }

		public override bool NewRightClick(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
			Tile tile = Main.tile[i, j];
			Main.mouseRightRelease = false;
			int left = i;
			int top = j;
			if (tile.frameX % 36 != 0) {
				left--;
			}
			if (tile.frameY != 0) {
				top--;
			}
			if (player.sign >= 0) {
				Main.PlaySound(SoundID.MenuClose, -1, -1, 1);
				player.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
			}
			if (Main.editChest) {
				Main.PlaySound(SoundID.MenuTick, -1, -1, 1);
				Main.editChest = false;
				Main.npcChatText = "";
			}
			if (player.editedChestName) {
				NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, null, player.chest, 1f, 0f, 0f, 0, 0, 0);
				player.editedChestName = false;
			}
			if (Main.netMode == NetmodeID.Server) {
				if (left == player.chestX && top == player.chestY && player.chest >= 0) {
					player.chest = -1;
					Recipe.FindRecipes();
					Main.PlaySound(SoundID.MenuClose, -1, -1, 1);
				}
				else {
					NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, (float)top, 0f, 0f, 0, 0, 0);
					Main.stackSplit = 600;
				}
			}
			else {
				int chest = Chest.FindChest(left, top);
				if (chest >= 0) {
					Main.stackSplit = 600;
					if (chest == player.chest) {
						player.chest = -1;
						Main.PlaySound(SoundID.MenuClose, -1, -1, 1);
					}
					else {
						player.chest = chest;
						Main.playerInventory = true;
						Main.recBigList = false;
						player.chestX = left;
						player.chestY = top;
						Main.PlaySound(player.chest < 0 ? 10 : 12, -1, -1, 1);
					}
					Recipe.FindRecipes();
				}
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX % 36 != 0) {
				left--;
			}
			if (tile.frameY != 0) {
				top--;
			}
			int chest = Chest.FindChest(left, top);
			player.showItemIcon2 = -1;
			if (chest < 0) {
				player.showItemIconText = Lang.chestType[0].Value;
			}
			else {
				player.showItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Spirit Chest";
				if (player.showItemIconText == "Spirit Chest") {
					player.showItemIcon2 = ModContent.ItemType<GlowplateChestItem>();
					player.showItemIconText = "";
				}
			}
			player.noThrow = 2;
			player.showItemIcon = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			MouseOver(i, j);
			Player player = Main.player[Main.myPlayer];
			if (player.showItemIconText == "") {
				player.showItemIcon = false;
				player.showItemIcon2 = 0;
			}
		}
	}
	public class GlowplateChestItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowplate Chest");
		}

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 28;
			item.value = item.value = Terraria.Item.buyPrice(0, 0, 5, 0);
			item.rare = 1;

			item.maxStack = 99;

			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 10;
			item.useAnimation = 15;

			item.useTurn = true;
			item.autoReuse = true;
			item.consumable = true;

			item.createTile = ModContent.TileType<GlowplateChest>();
		}

  		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TechBlockItem>(), 8);
			recipe.AddIngredient(ItemID.IronBar, 2);
			recipe.anyIronBar = true;
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}