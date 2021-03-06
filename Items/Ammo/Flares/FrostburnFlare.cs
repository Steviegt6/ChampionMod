using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ChampionMod.Items.Ammo.Flares
{
	public class FrostburnFlare : ModItem
	{
		public override void SetDefaults()
        {
			item.damage = 2;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1.5f;
			item.value = 10;
			item.rare = 1;
            item.shoot = mod.ProjectileType("FrostburnFlareProjectile");
			item.shootSpeed = 6f;
			item.ammo = AmmoID.Flare;
		}

        public override void AddRecipes()
         {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("ChampionMod:Flares", 10);
			recipe.AddIngredient(ItemID.IceTorch, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 10);
			recipe.AddRecipe();
        }
    }
}
