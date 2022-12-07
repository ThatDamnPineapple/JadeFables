﻿//TODO
//Bestiary
//Banners
//Balance
//Gores
//Better sound effects

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using System.Collections.Generic;

using Terraria.DataStructures;
using Terraria.GameContent;

using Terraria.Audio;

using System;
using System.Linq;
using static Terraria.ModLoader.ModContent;
using JadeFables.Core;
using JadeFables.Helpers;
using static System.Formats.Asn1.AsnWriter;
using JadeFables.Biomes.JadeLake;

namespace JadeFables.NPCs.Koi
{
    internal class MediumKoi : ModNPC
    {

        private Player target => Main.player[NPC.target];

        private int yFrame = 0;
        private int frameCounter = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Koi");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 40;
            NPC.damage = 0;
            NPC.defense = 5;
            NPC.lifeMax = 50;
            NPC.value = 10f;
            NPC.knockBackResist = 2.6f;
            NPC.HitSound = SoundID.NPCHit23;
            NPC.DeathSound = SoundID.NPCDeath26;
            NPC.noGravity = true;
            NPC.aiStyle = 16;
            AIType = NPCID.Goldfish;
        }

        public override void AI()
        {
            NPC.spriteDirection = Math.Sign(-NPC.velocity.X);
            frameCounter++;

            if (Main.rand.NextBool(900))
                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + (Vector2.UnitX * NPC.direction * 20), -Vector2.UnitY.RotatedByRandom(0.5f) * Main.rand.NextFloat(0.75f, 2f), ModContent.ProjectileType<AirBubble>(), 100, 0).scale = Main.rand.NextFloat(0.4f,0.6f);

            int threshhold = 4;

            if (frameCounter >= threshhold)
            {
                frameCounter = 0;
                yFrame++;
                if (yFrame >= Main.npcFrameCount[NPC.type])
                {
                    yFrame = 0;
                }
            }
            //NPC.TargetClosest(true);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D mainTex = ModContent.Request<Texture2D>(Texture).Value;

            int frameWidth = mainTex.Width;
            int frameHeight = mainTex.Height / Main.npcFrameCount[NPC.type];
            Rectangle frameBox = new Rectangle(0, yFrame * frameHeight, frameWidth, frameHeight);

            SpriteEffects effects = SpriteEffects.None;
            Vector2 origin = new Vector2(frameWidth, frameHeight) / 2;

            if (NPC.spriteDirection != 1)
            {
                effects = SpriteEffects.FlipHorizontally;
                origin.X = frameWidth - origin.X;
            }
            Vector2 slopeOffset = new Vector2(0, NPC.gfxOffY);
            Main.spriteBatch.Draw(mainTex, slopeOffset + NPC.Center - screenPos, frameBox, drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Water && spawnInfo.Player.InModBiome(ModContent.GetInstance<JadeLakeBiome>()) ? 50f : 0f;
    }
}