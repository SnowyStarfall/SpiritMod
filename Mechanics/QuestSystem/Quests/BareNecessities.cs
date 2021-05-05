﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;

namespace SpiritMod.Mechanics.QuestSystem.Quests
{
    public class BareNecessities : Quest
    {
        public override string QuestName => "Bare Necessities";
		public override string QuestClient => "The Adventurer";
		public override string QuestDescription => "You know, I'm real appreciative of the home you've given me after savin' me, and I don't want to sound rude. Buuuut, I think we can really spice this place up, lad. And it all starts with craftin' a Naturalist's Workshop for some pretty buildin' materials.";
		public override int Difficulty => 1;
        public override QuestType QuestType => QuestType.Designer;

		public override (int, int)[] QuestRewards => _rewards;
		private (int, int)[] _rewards = new[]
		{
			(ModContent.ItemType<Tiles.Block.Ambient.RuinstoneItem>(), 50),
			(ModContent.ItemType<Tiles.Block.Ambient.FracturedStoneItem>(), 50),
			(ModContent.ItemType<Tiles.Block.Ambient.CragstoneItem>(), 50),
			(Terraria.ID.ItemID.SilverCoin, 25)
		};
 		public override void OnQuestComplete()
		{
            bool showUnlocks = true;
            QuestManager.UnlockQuest<AncestralWorship>(showUnlocks);
			QuestManager.UnlockQuest<StylishSetup>(showUnlocks);

            base.OnQuestComplete();
        }
		public BareNecessities()
        {
           _questSections.Add(new RetrievalTask(ModContent.ItemType<Items.Placeable.Furniture.ForagerTableItem>(), 1, "Craft"));
        }
    }
}