
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpiritMod.Mechanics.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SpiritMod.Mechanics.Boids
{
	public class BoidHost
	{
		internal List<Flock> Flocks = new List<Flock>();
		private const int SPAWNRATE = 40;
		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Flock fishflock in Flocks)
			{
				fishflock.Draw(spriteBatch);
			}
		}

		public void Update()
		{
			foreach (Flock fishflock in Flocks)
			{
				fishflock.Update();
			}

			//Test
			if (Main.GameUpdateCount % SPAWNRATE == 0 && Main.LocalPlayer.ZoneBeach)
			{
				int flock = Main.rand.Next(0, Flocks.Count);
				int fluff = 1000;

				Vector2 rand = new Vector2(
					Main.rand.Next(-Main.screenWidth/2 - fluff, Main.screenWidth / 2 + fluff),
					Main.rand.Next(-Main.screenHeight / 2 - fluff, Main.screenHeight / 2 + fluff));

				if (!new Rectangle(0, 0, Main.screenWidth, Main.screenHeight).Contains(rand.ToPoint()))
				{
					Vector2 position = Main.LocalPlayer.Center + rand;
					Point tP = position.ToTileCoordinates();
					if (WorldGen.InWorld(tP.X, tP.Y, 10))
					{
						Tile tile = Framing.GetTileSafely(tP.X, tP.Y);
						if (tile.liquid > 100)
							Flocks[flock].Populate(position, Main.rand.Next(20, 30), 50f);
					}
				}
			}
		}

		public void LoadContent()
		{
			for(int i = 0; i<7; i++) Flocks.Add(new Flock(ModContent.GetTexture($"SpiritMod/Textures/AmbientFish/fish_{i}"), 1f));

			On.Terraria.Main.DrawWoF += Main_DrawWoF;
		}

		//TODO: Move to update hook soon
		private void Main_DrawWoF(On.Terraria.Main.orig_DrawWoF orig, Main self)
		{
			Update();
			Draw(Main.spriteBatch);
			orig(self);
		}

		public void UnloadContent()
		{
			Flocks.Clear(); 
			On.Terraria.Main.DrawWoF -= Main_DrawWoF;
		}
	}
}
