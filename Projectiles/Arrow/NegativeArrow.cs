using Terraria.ModLoader;
using SpiritMod.Utilities;
using Microsoft.Xna.Framework;

namespace SpiritMod.Projectiles.Arrow
{
	public class NegativeArrow : PositiveArrow, ITrailProjectile
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Negative Arrow");

		new public void DoTrailCreation(TrailManager tM) => tM.CreateTrail(projectile, new StandardColorTrail(new Color(255, 113, 36)), new RoundCap(), new ZigZagTrailPosition(3f), 8f, 250f);

		public override void SetDefaults()
		{
			base.SetDefaults();
			oppositearrow = ModContent.ProjectileType<PositiveArrow>();
		}
	}
}