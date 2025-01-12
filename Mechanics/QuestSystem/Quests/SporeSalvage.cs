﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;

namespace SpiritMod.Mechanics.QuestSystem.Quests
{
    public class SporeSalvage : Quest
    {
        public override string QuestName => "Sanctuary: Spore Salvage";
		public override string QuestClient => "The Dryad";
		public override string QuestDescription => "The glowing mushroom fields are a hotspot for biodiverse flora and fauna. It is no surprise that a new, sentient mushroom has been spotted; however, this fungus is not dangerous at all. All it seems to do is sway gently from side to side. We must ensure its survival, and not just because it is cute!";
		public override int Difficulty => 2;
		public override string QuestCategory => "Forager";

		public override (int, int)[] QuestRewards => _rewards;
		private (int, int)[] _rewards = new[]
		{
			(ModContent.ItemType<Tiles.Furniture.Critters.VibeshroomJarItem>(), 1),
			(ModContent.ItemType<Items.Material.GlowRoot>(), 3),
			(Terraria.ID.ItemID.GlowingMushroom, 16),
			(Terraria.ID.ItemID.GoldCoin, 3)
		};
        public override void OnActivate()
		{
			QuestGlobalNPC.OnEditSpawnPool += QuestGlobalNPC_OnEditSpawnPool;
			base.OnActivate();
		}

		public override void OnDeactivate()
		{
			QuestGlobalNPC.OnEditSpawnPool -= QuestGlobalNPC_OnEditSpawnPool;
			base.OnDeactivate();
		}
		private void QuestGlobalNPC_OnEditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			if (pool[ModContent.NPCType<NPCs.Critters.Vibeshroom>()] > 0f && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Critters.Vibeshroom>()))
			{
				pool[ModContent.NPCType<NPCs.Critters.Vibeshroom>()] = 1.15f;
			}
		}
		private SporeSalvage()
        {
            _tasks.AddTask(new RetrievalTask(ModContent.ItemType<Items.Consumable.VibeshroomItem>(), 1, "Capture"));
        }
    }
}