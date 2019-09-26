using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine
{
	// Token: 0x0200000D RID: 13
	public class Board
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003DBC File Offset: 0x00001FBC
		public static void Load(SpriteFont FFont1)
		{
			Board.Font1 = FFont1;
			Board.StillImageSampler = new SamplerState
			{
				Filter = TextureFilter.Anisotropic,
				AddressU = TextureAddressMode.Wrap,
				AddressV = TextureAddressMode.Wrap,
				AddressW = TextureAddressMode.Wrap,
				MaxMipLevel = 64,
				MipMapLevelOfDetailBias = (float)Main.MapVariablePipeline.LODBias,
				ComparisonFunction = CompareFunction.Less
			};
			Board.MovingImageSampler = new SamplerState
			{
				Filter = TextureFilter.Anisotropic,
				AddressU = TextureAddressMode.Wrap,
				AddressV = TextureAddressMode.Wrap,
				AddressW = TextureAddressMode.Wrap,
				MaxMipLevel = 64,
				MipMapLevelOfDetailBias = (float)Math.Max(Main.MapVariablePipeline.LODBias, 0.0),
				ComparisonFunction = CompareFunction.Less
			};
			Board.DepthBuff = new DepthStencilState
			{
				DepthBufferEnable = true,
				DepthBufferFunction = CompareFunction.Less,
				CounterClockwiseStencilDepthBufferFail = StencilOperation.Keep,
				CounterClockwiseStencilFail = StencilOperation.Keep,
				CounterClockwiseStencilFunction = CompareFunction.Less,
				CounterClockwiseStencilPass = StencilOperation.Keep,
				StencilEnable = true,
				StencilDepthBufferFail = StencilOperation.Keep
			};
			Board.Rasterizer = new RasterizerState
			{
				MultiSampleAntiAlias = true,
				CullMode = CullMode.CullCounterClockwiseFace,
				FillMode = FillMode.Solid
			};
			Board.Sampler = Board.StillImageSampler;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003EE8 File Offset: 0x000020E8
		public static void Draw()
		{
			Board.DebugText = "";
			Board.DebugText = string.Concat(new string[]
			{
				Board.DebugText,
				"Chunk = ",
				Main.Player1.CurrentChunk.Index.ToString(),
				"   Position = ",
				Main.Player1.Position.ToString(),
				"\r\nTime = ",
				Math.Round(Main.TimeOfTheDay / 60.0, 2).ToString(),
				"    FPS = ",
				Main.FPS.ToString(),
				" @ ",
				Main.RenderDistance.ToString(),
				"\r\nHealth ",
				Main.Player1.Health.ToString(),
				"   Money ",
				Main.Player1.Money.ToString(),
				"\r\nSun : ",
				Main.SunlightDirection.ToString()
			});
			Board.DebugText = Board.DebugText + "\r\n" + Board.SelectedToolAndBlock;
			Board.SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Board.Sampler, Board.DepthBuff, Board.Rasterizer, null, null);
			bool isDebugInfoVisible = Board.IsDebugInfoVisible;
			if (isDebugInfoVisible)
			{
				Board.SB.DrawString(Board.Font1, Board.DebugText, Board.DebugTextCords, Color.Red, 0f, Vector2.Zero, (float)Main.MapVariablePipeline.GraphicQuality, SpriteEffects.None, 0f);
			}
			Board.SB.DrawString(Board.Font1, "x", new Vector2((float)(Game1.Graphics.GraphicsDevice.Viewport.Width / 2), (float)(Game1.Graphics.GraphicsDevice.Viewport.Height / 2)), Color.Red);
			Board.SB.End();
		}

		// Token: 0x04000021 RID: 33
		public static SpriteBatch SB;

		// Token: 0x04000022 RID: 34
		public static bool IsDebugInfoVisible = true;

		// Token: 0x04000023 RID: 35
		public static SpriteFont Font1;

		// Token: 0x04000024 RID: 36
		public static string DebugText;

		// Token: 0x04000025 RID: 37
		public static Vector2 DebugTextCords = default;

		// Token: 0x04000026 RID: 38
		public static string SelectedToolAndBlock = "";

		// Token: 0x04000027 RID: 39
		public static SamplerState Sampler;

		// Token: 0x04000028 RID: 40
		public static SamplerState StillImageSampler;

		// Token: 0x04000029 RID: 41
		public static SamplerState MovingImageSampler;

		// Token: 0x0400002A RID: 42
		public static DepthStencilState DepthBuff;

		// Token: 0x0400002B RID: 43
		public static RasterizerState Rasterizer;
	}
}
