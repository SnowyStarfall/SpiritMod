
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpiritMod.Mechanics.Interfaces.Hosts;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SpiritMod.Mechanics.Boids
{
    internal class Flock : ComponentManager<Fish>
    {
        public Texture2D FlockTexture { get; set; }

        public float FlockScale;

		public float MaxFish;

		private const int SimulationDistance = 2500;

        public Flock(Texture2D tex, float Scale = 1, float MaxFlockSize = 50)
        {
			if (tex != null) FlockTexture = tex;
			else FlockTexture = Main.magicPixel;

            FlockScale = Scale;
			MaxFish = MaxFlockSize;
		}

        internal void Populate(Vector2 position, int amount, float spread)
        {
            for(int i = 0; i<amount; i++)
            {
                if (Objects.Count < MaxFish)
                {
                    Fish fish = new Fish(this)
                    {
                        position = position + new Vector2(Main.rand.NextFloat(-spread, spread), Main.rand.NextFloat(-spread, spread)),
                        velocity = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1))
                    };
                    Objects.Add(fish);
                }
            }
        }

        protected override void OnUpdate()
        {
            foreach(Fish fish in Objects.ToArray())
            {
                if (fish != null)
                {
                    fish.AdjFish.Clear();
                    foreach (Fish adjfish in Objects)
                    {
                        if (!fish.Equals(adjfish))
                        {
                            if (Vector2.DistanceSquared(fish.position, adjfish.position) < Fish.Vision * Fish.Vision)
                            {
                                fish.AdjFish.Add(adjfish);
                            }
                        }
                    }
                    if (Vector2.DistanceSquared(fish.position, Main.LocalPlayer.Center) > SimulationDistance * SimulationDistance)
                        Objects.Remove(fish);
                }
            }
        }
    }
}
