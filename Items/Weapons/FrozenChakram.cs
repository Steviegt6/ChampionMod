using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ChampionMod.Items.Weapons
{
    public class FrozenChakram : ModItem
    {
		public override void SetDefaults() {
            item.damage = 19;            
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.scale = 0.8f; // Changes sprite size
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true; // So you don't see the item swing (and you just see the projectile)
            item.useStyle = 1;
            item.knockBack = 8;
            item.value = Item.sellPrice(silver : 2);
            item.rare = 1;
            item.shootSpeed = 11f;
            item.shoot = mod.ProjectileType ("FrozenChakramProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
        public override bool CanUseItem(Player player)     
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.BorealWood, 20);
				recipe.AddIngredient(ItemID.Silk, 5);
				recipe.AddIngredient(ItemID.SilverBar, 6);
				recipe.AddIngredient(ItemID.IceTorch, 1);
                recipe.AddIngredient(ItemID.LastPrism, 999);
				recipe.AddTile(TileID.Anvils); 
                recipe.SetResult(this);
                recipe.AddRecipe();
        }
    }
}