using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpiritMod.Buffs.Summon;
using SpiritMod.Projectiles.BaseProj;
using SpiritMod.Utilities;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Summon.PigronStaff
{
	[AutoloadMinionBuff("Pigron Minion", "Bacon!")]
	public class PigronMinion : BaseMinion
	{
		public PigronMinion() : base(800, 1800, new Vector2(30, 30)) { }
		public override void AbstractSetStaticDefaults()
		{
			DisplayName.SetDefault("Pigron");
			Main.projFrames[projectile.type] = 14;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override bool DoAutoFrameUpdate(ref int framespersecond)
		{
			framespersecond = 14;
			return true;
		}

		public override bool PreAI()
		{
			projectile.direction = projectile.spriteDirection = Math.Sign(-projectile.velocity.X);
			projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction > 0 ? MathHelper.Pi : 0);
			if (Main.rand.Next(600) == 0 && Main.netMode != NetmodeID.Server)
				Main.PlaySound(SoundID.Zombie, (int)projectile.Center.X, (int)projectile.Center.Y, Main.rand.Next(39, 41), 0.33f, 0.5f);

			return true;
		}

		public override void IdleMovement(Player player)
		{
			if(projectile.Distance(player.Center) > 70)
				projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(player.Center) * MathHelper.Clamp(projectile.Distance(player.Center) / 50, 8, 14), 0.04f / (IndexOfType/3f + 1));

			if (projectile.Distance(player.Center) > 1800)
			{
				projectile.Center = player.Center;
				projectile.netUpdate = true;
			}

			projectile.ai[0] = 0;
			projectile.ai[1] = 0;
			projectile.alpha = Math.Max(projectile.alpha - 8, 0);
		}

		public override void TargettingBehavior(Player player, NPC target)
		{
			projectile.ai[0] = 1;
			if(Main.rand.Next(9) == 0 && projectile.velocity.Length() > 7)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 255, projectile.velocity.X / 3, projectile.velocity.Y / 3, 100, default, Main.rand.NextFloat(0.7f, 1.2f));
				dust.fadeIn = 0.8f;
				dust.noGravity = true;
			}

			switch (projectile.ai[1])
			{
				case 0:
				case 2:
				case 4:
					projectile.alpha += Math.Max(16 - IndexOfType / 2, 12);
					projectile.velocity *= 0.97f;
					if (projectile.alpha >= 255)
					{
						projectile.ai[1]++;
						projectile.alpha = 255;
						projectile.Center = target.Center + target.DirectionTo(player.Center).RotatedByRandom(MathHelper.PiOver4) * Main.rand.NextFloat(350, 400);
						projectile.velocity = projectile.DirectionTo(target.Center) * Main.rand.NextFloat(11, 16);
						if (Main.netMode != NetmodeID.Server)
							Main.PlaySound(SoundID.DD2_WyvernDiveDown.WithPitchVariance(0.3f).WithVolume(0.5f), projectile.Center);
						projectile.netUpdate = true;
					}
					break;
				case 1:
				case 3:
				case 5:
					projectile.velocity *= 1.03f;
					projectile.alpha -= Math.Max(16 - IndexOfType / 2, 12);
					if (projectile.alpha <= 0)
					{
						projectile.alpha = 0;
						projectile.netUpdate = true;
						projectile.ai[1]++;
					}
					break;
				case 6:
					if (++projectile.localAI[0] == 10)
					{
						projectile.localAI[1] = (Main.rand.NextBool()? 1 : -1);
						if (Main.netMode != NetmodeID.Server)
							Main.PlaySound(SoundID.DD2_WyvernDiveDown.WithPitchVariance(0.3f).WithVolume(0.5f), projectile.Center);

						projectile.netUpdate = true;
					}

					if(projectile.localAI[0] > 10)
					{
						projectile.velocity = projectile.velocity.RotatedBy(projectile.localAI[1] * MathHelper.TwoPi / 20);
						if(projectile.localAI[0] % 7 == 0){
							Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<HallowBubble>(), projectile.damage / 3, projectile.knockBack, projectile.owner, target.whoAmI);
							projectile.netUpdate = true;
						}
					}

					if (projectile.localAI[0] >= 40)
						projectile.ai[1]++;
					break;
				default: 
					projectile.ai[1] = 0;
					projectile.localAI[0] = 0;
					break;
			}
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(projectile.localAI[0]);
			writer.Write(projectile.localAI[1]);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			projectile.localAI[0] = reader.ReadSingle();
			projectile.localAI[1] = reader.ReadSingle();
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if(projectile.ai[0] == 1)
				projectile.QuickDrawTrail(spriteBatch);
			projectile.QuickDraw(spriteBatch);
			return false;
		}
	}

	internal class HallowBubble : ModProjectile, IDrawAdditive
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallow Bubble");
			ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.Size = new Vector2(20, 20);
			projectile.scale = Main.rand.NextFloat(0.5f, 1f);
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 60;
			projectile.rotation = Main.rand.NextFloat(MathHelper.Pi);
			projectile.hide = true;
		}

		public override void AI()
		{
			NPC target = Main.npc[(int)projectile.ai[0]];
			if (!target.active || !target.CanBeChasedBy(this))
			{
				projectile.Kill();
				return;
			}
			projectile.rotation += (projectile.velocity.X < 0) ? -0.15f : 0.15f;
			projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(target.Center) * 16, 0.05f);
		}

		public override void Kill(int timeLeft)
		{
			if (Main.netMode != NetmodeID.Server)
				Main.PlaySound(SoundID.Item54.WithPitchVariance(0.3f), projectile.Center);

			for(int i = 0; i < 12; i++)
			{
				Dust dust = Dust.NewDustPerfect(projectile.Center, 255, Main.rand.NextVector2Circular(5, 5), 50, default, (projectile.scale / 3) * Main.rand.NextFloat(0.7f, 1.3f));
				dust.noGravity = true;
				dust.fadeIn = 0.4f;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => damage += Math.Min(target.defense / 2, 10);

		public void AdditiveCall(SpriteBatch spriteBatch)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];
			Color drawColor = Color.White;
			float glowscale = (float)(Math.Sin(Main.GlobalTime * 4) / 5 + 1);
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, drawColor, projectile.rotation, tex.Size() / 2, projectile.scale, SpriteEffects.None, 0);
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, drawColor * 0.75f, projectile.rotation, tex.Size() / 2, projectile.scale * glowscale, SpriteEffects.None, 0);
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, drawColor * 0.75f, projectile.rotation, tex.Size() / 2, projectile.scale * (1/glowscale), SpriteEffects.None, 0);

			Texture2D bloom = mod.GetTexture("Effects/Masks/CircleGradient");
			spriteBatch.Draw(bloom, projectile.Center - Main.screenPosition, null, Color.Pink * 0.75f, projectile.rotation, bloom.Size() / 2, projectile.scale/3.5f, SpriteEffects.None, 0);
		}
	}
}