﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace ChampionMod.Tiles.Crops
{
    public class TomatoCrop : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);

            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 20 }; // Last one is 20 so it extends into the grass

            TileObjectData.newTile.Origin = new Point16(1, 2);

            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);

            TileObjectData.newTile.AnchorValidTiles = new[]
            {
                2, // Grass
                109 // Hallowed Grass
            };

            TileObjectData.addTile(Type);
        }

        public override bool CanPlace(int i, int j)
        {
            // So you can't hold down the tomato seeds to constantly replace it
            // Since the tomato crop is 3 blocks wide you also need to make sure it has enough space
            return Main.tile[i, j].type != Type && Main.tile[i + 1, j].type != Type && Main.tile[i - 1, j].type != Type;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int stage = frameX / 54;
            if (stage == 0)
            {
                for (int x = 0; x < 10; x++)
                {
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 2) * 16), 16, 16, 3);
                }
            }
            else if (stage == 1)
            {
                for (int x = 0; x < 10; x++)
                {
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 2) * 16), 16, 16, 3);
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 1) * 16), 16, 16, 3);
                }
            }
            else if (stage == 2)
            {
                for (int x = 0; x < 15; x++)
                {
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 2) * 16), 16, 16, 3);
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 1) * 16), 16, 16, 3);
                }
            }
            else if (stage == 3)
            {
                for (int x = 0; x < 15; x++)
                {
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 2) * 16), 16, 16, 3);
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 1) * 16), 16, 16, 3);
                }
            }
            else if (stage == 4)
            {
                Item.NewItem(i * 16, j * 16, 0, 0, mod.ItemType("TomatoSeeds"), 2 + Main.rand.Next(3));
                Item.NewItem(i * 16, j * 16, 0, 0, mod.ItemType("Tomato"), 2 + Main.rand.Next(2));

                for (int x = 0; x < 15; x++)
                {
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 2) * 16), 16, 16, 3);
                    Dust.NewDust(new Vector2((i + 1) * 16, (j + 1) * 16), 16, 16, 3);
                }
            }
        }

        public override void RandomUpdate(int i, int j)
        {
            // So it is not executing all this code for no reason when it's at the max stage
            // So it slows down the growth
            // So the crop doesn't grow when it is not on the surface
            if (Main.tile[i, j].frameX < 216 && Main.rand.Next(65) == 0 && j < Main.worldSurface)
            {
                // Gets the top left tile coords
                int topLeftX = i - Main.tile[i, j].frameX / 18 % 3;
                int topLeftY = j - Main.tile[i, j].frameY / 18 % 3;

                for (int x = 0; x <= 2; x++)
                {
                    for (int y = 0; y <= 2; y++)
                    {
                        if (Main.tile[topLeftX + x, topLeftY + y].frameX < 216)
                        {
                            Main.tile[topLeftX + x, topLeftY + y].frameX += 54;
                            NetMessage.SendTileSquare(-1, topLeftX + x, topLeftY + y, 1); // For multiplayer syncing
                        }
                    }
                }
            }
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }

        public override bool KillSound(int i, int j)
        {
            Main.PlaySound(6, new Vector2(i * 16, j * 16));

            return false;
        }
    }
}