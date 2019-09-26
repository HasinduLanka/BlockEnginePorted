using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary> 
    // Token: 0x0200001E RID: 30
    public class Game1 : Game
    {
        // Token: 0x17000036 RID: 54
        // (get) Token: 0x06000104 RID: 260 RVA: 0x0000B16F File Offset: 0x0000936F
        // (set) Token: 0x06000105 RID: 261 RVA: 0x0000B178 File Offset: 0x00009378
        public static GraphicsDeviceManager Graphics { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

        // Token: 0x06000106 RID: 262 RVA: 0x0000B180 File Offset: 0x00009380
        public Game1()
        {
            this.InitialHumanCount = 0;
            this.HRAngle = 0f;
            this.MouseSwitchLast = 0.0;
            this.DevUtilsTime = 0L;
            this.BF = new BoundingFrustum(Main.viewMatrix * Main.projectionMatrix);
            this.BS = new BoundingSphere(Vector3.Zero, 800f);
            this.LastviewMatrix = Matrix.Identity;
            this.FCP = default;
            this.IVBs = new DynamicVertexBuffer[201];
            this.MAs = new Vector4[201][];
            this.ArrayUsage = new bool[201];
            this.ChunkArray = new Chunk[1501];
            this.nChunkArray = 0;
            this.CountForBT = new int[11];
            this.nForBT = new int[11];
            this.ArraysForBT = new Vector4[11][];
            this.IVBsForBT = new DynamicVertexBuffer[11];
            Game1.Graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
        }

        // Token: 0x06000107 RID: 263 RVA: 0x0000B29C File Offset: 0x0000949C
        protected override void Initialize()
        {
            Main.TimeOfTheDay = 540.0;
            Main.BackColor = Color.LightBlue;
            base.IsMouseVisible = true;
            base.Initialize();
        }

        // Token: 0x06000108 RID: 264 RVA: 0x0000B2C8 File Offset: 0x000094C8
        public Matrix[] SetupEffectDefaults(Model myModel, Matrix projectionMatrix, Matrix viewMatrix)
        {
            Matrix[] absoluteTransforms = new Matrix[checked(myModel.Bones.Count - 1 + 1)];
            myModel.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

            foreach (ModelMesh mesh in myModel.Meshes)
            {

                foreach (Effect effect2 in mesh.Effects)
                {
                    BasicEffect effect = (BasicEffect)effect2;
                    effect.EnableDefaultLighting();
                    effect.Projection = projectionMatrix;
                    effect.View = viewMatrix;
                    effect.FogEnabled = true;
                    effect.FogColor = new Vector3(0.1f);
                    effect.FogStart = Main.RenderDistance - 300f;
                    effect.FogEnd = Main.RenderDistance + 300f;
                }

            }

            return absoluteTransforms;
        }

        // Token: 0x06000109 RID: 265 RVA: 0x0000B3E8 File Offset: 0x000095E8
        protected override void LoadContent()
        {
            checked
            {
                Game1.Graphics.PreferredBackBufferHeight = (int)Math.Round(unchecked((double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * Main.MapVariablePipeline.GraphicQuality));
                Game1.Graphics.PreferredBackBufferWidth = (int)Math.Round(unchecked((double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * Main.MapVariablePipeline.GraphicQuality));
                Game1.Graphics.GraphicsDevice.BlendState = BlendState.Opaque;
                Game1.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
                Game1.Graphics.ApplyChanges();
                Main.FOV = MathHelper.ToRadians(80f);
                Main.Viewport = new Viewport(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, 0f, 10000f);
                Main.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(Main.FOV, Main.Viewport.AspectRatio, 1f, Main.RenderDistance);
                Main.viewMatrix = Matrix.CreateLookAt(Main.cameraPosition, Vector3.Zero, Vector3.Up);
                int i = 0;
                do
                {
                    int count = (i + 1) * 256;
                    this.IVBs[i] = new DynamicVertexBuffer(base.GraphicsDevice, Game1.instanceVertexDeclaration, count, BufferUsage.WriteOnly);
                    this.MAs[i] = new Vector4[count - 1 + 1];
                    i++;
                }
                while (i <= 200);
                Game1.Graphics.PreferredBackBufferHeight = (int)Math.Round(unchecked((double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * Main.MapVariablePipeline.GraphicQuality));
                Game1.Graphics.PreferredBackBufferWidth = (int)Math.Round(unchecked((double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * Main.MapVariablePipeline.GraphicQuality));
                Game1.Graphics.ApplyChanges();
                Board.SB = new SpriteBatch(Game1.Graphics.GraphicsDevice);
                Board.Load(base.Content.Load<SpriteFont>("SF1"));
                base.GraphicsDevice.DepthStencilState = Board.DepthBuff;
                base.GraphicsDevice.RasterizerState = Board.Rasterizer;
                base.GraphicsDevice.SamplerStates[0] = Board.StillImageSampler;
                EntityTypes.WayPoint = new EntityType();
                EntityTypes.WayPoint.Load(null, 0);
                EntityTypes.Lst.Add(EntityTypes.WayPoint);
                EntityTypes.Player1 = new EntityType();
                EntityTypes.Player1.Load(base.Content.Load<Model>("Models\\Player1"), 1);
                EntityTypes.Player1.IsHuman = true;
                EntityTypes.Player1.SetMethods(typeof(Player));
                EntityTypes.Player1.IsPlayer = true;
                EntityTypes.Player1.Transforms = this.SetupEffectDefaults(EntityTypes.Player1.Model, Main.projectionMatrix, Main.viewMatrix).Last<Matrix>();
                EntityTypes.Player1.Width = 30f;
                EntityTypes.Lst.Add(EntityTypes.Player1);
                EntityTypes.Human1 = new EntityType();
                EntityTypes.Human1.Load(base.Content.Load<Model>("Models\\Man1"), 2);
                EntityTypes.Human1.IsHuman = true;
                EntityTypes.Human1.SetMethods(typeof(Human));
                EntityTypes.Human1.CreateBasicTargetingRules();
                EntityTypes.Human1.UpdateAI = (Action<Entity>)AIs.YoungMan;
                EntityTypes.Human1.Transforms = this.SetupEffectDefaults(EntityTypes.Human1.Model, Main.projectionMatrix, Main.viewMatrix).Last<Matrix>();
                EntityTypes.Human1.Width = 30f;
                EntityTypes.Lst.Add(EntityTypes.Human1);
                EntityTypes.Arrow1 = new EntityType();
                EntityTypes.Arrow1.Load(base.Content.Load<Model>("Models\\Shovel"), 3);
                EntityTypes.Arrow1.SetMethods(typeof(Arrow));
                EntityTypes.Arrow1.IsHuman = false;
                EntityTypes.Arrow1.Transforms = this.SetupEffectDefaults(EntityTypes.Arrow1.Model, Main.projectionMatrix, Main.viewMatrix).Last<Matrix>();
                EntityTypes.Lst.Add(EntityTypes.Arrow1);
                EntityTypes.Civilian = new EntityType();
                EntityTypes.Civilian.Load(base.Content.Load<Model>("Models\\Civilian"), 4);
                ((BasicEffect)EntityTypes.Civilian.Model.Meshes[7].Effects[0]).DiffuseColor = new Vector3(0f, 1f, 1f);
                ((BasicEffect)EntityTypes.Civilian.Model.Meshes[2].Effects[0]).DiffuseColor = new Vector3(0f, 1f, 1f);
                ((BasicEffect)EntityTypes.Civilian.Model.Meshes[0].Effects[3]).DiffuseColor = new Vector3(0f, 1f, 1f);
                ((BasicEffect)EntityTypes.Civilian.Model.Meshes[4].Effects[1]).DiffuseColor = new Vector3(0f, 1f, 1f);
                ((BasicEffect)EntityTypes.Civilian.Model.Meshes[3].Effects[1]).DiffuseColor = new Vector3(0f, 1f, 1f);
                EntityTypes.Civilian.IsHuman = true;
                EntityTypes.Civilian.SetMethods(typeof(Human));
                EntityTypes.Civilian.CreateBasicTargetingRules();
                EntityTypes.Civilian.UpdateAI = (Action<Entity>)AIs.Civilian;
                EntityTypes.Civilian.Transforms = this.SetupEffectDefaults(EntityTypes.Civilian.Model, Main.projectionMatrix, Main.viewMatrix).Last<Matrix>();
                EntityTypes.Civilian.Width = 30f;
                EntityTypes.Lst.Add(EntityTypes.Civilian);
                EntityTypes.Guard = new EntityType();
                EntityTypes.Guard.Load(base.Content.Load<Model>("Models\\Guard"), 5);
                EntityTypes.Guard.IsHuman = true;
                EntityTypes.Guard.SetMethods(typeof(Human));
                EntityTypes.Guard.UpdateAI = (Action<Entity>)AIs.Guard;
                EntityTypes.Guard.CreateBasicTargetingRules();
                EntityTypes.Guard.TargetSelectingRulesForDetection[EntityRelationMode.Unknowen] = AITargetMode.FollowAndKill;
                EntityTypes.Guard.DelTargetScan = (Action<Entity>)AIs.AttackWhenCloseBy;
                EntityTypes.Guard.EtFriendlies.Add(EntityTypes.Guard);
                EntityTypes.Guard.Transforms = this.SetupEffectDefaults(EntityTypes.Guard.Model, Main.projectionMatrix, Main.viewMatrix).Last<Matrix>();
                EntityTypes.Guard.Width = 30f;
                EntityTypes.Lst.Add(EntityTypes.Guard);
                EntityTypes.Murderer = new EntityType();
                EntityTypes.Murderer.Load(base.Content.Load<Model>("Models\\Murderer"), 6);
                ((BasicEffect)EntityTypes.Murderer.Model.Meshes[7].Effects[0]).DiffuseColor = new Vector3(1f, 0f, 0f);
                ((BasicEffect)EntityTypes.Murderer.Model.Meshes[2].Effects[0]).DiffuseColor = new Vector3(1f, 0f, 0f);
                ((BasicEffect)EntityTypes.Murderer.Model.Meshes[0].Effects[3]).DiffuseColor = new Vector3(1f, 0f, 0f);
                ((BasicEffect)EntityTypes.Murderer.Model.Meshes[4].Effects[1]).DiffuseColor = new Vector3(1f, 0f, 0f);
                ((BasicEffect)EntityTypes.Murderer.Model.Meshes[3].Effects[1]).DiffuseColor = new Vector3(1f, 0f, 0f);
                EntityTypes.Murderer.IsHuman = true;
                EntityTypes.Murderer.SetMethods(typeof(Human));
                EntityTypes.Murderer.DelTargetScan = (Action<Entity>)AIs.AttackOnSight;
                EntityTypes.Murderer.UpdateAI = (Action<Entity>)AIs.Murdurer;
                EntityTypes.Murderer.CreateBasicTargetingRules();
                EntityTypes.Murderer.TargetSelectingRulesForDetection[EntityRelationMode.Unknowen] = AITargetMode.FollowAndKill;
                EntityTypes.Murderer.EtEnemies.AddRange(new EntityType[]
                {
                    EntityTypes.Guard,
                    EntityTypes.Player1
                });
                EntityTypes.Murderer.EtFriendlies.Add(EntityTypes.Murderer);
                EntityTypes.Murderer.Transforms = this.SetupEffectDefaults(EntityTypes.Murderer.Model, Main.projectionMatrix, Main.viewMatrix).Last<Matrix>();
                EntityTypes.Murderer.Width = 30f;
                EntityTypes.Lst.Add(EntityTypes.Murderer);
                Tool tool = new Tool();
                tool.Model = base.Content.Load<Model>("Models\\RSword");
                tool.Transforms = this.SetupEffectDefaults(tool.Model, Main.projectionMatrix, Main.viewMatrix);
                Tool Sword = tool;
                Tool tool2 = Sword;
                tool2.Name = "Sword1";
                tool2.Rotation = Matrix.Identity;
                tool2.RelativePosition = new Vector3(100f, 0f, 0f);
                tool2.OriginalRelativePosition = new Vector3(100f, 0f, 0f);
                tool2.OriginalRotation = Matrix.CreateFromAxisAngle(tool2.Rotation.Left, 0.7f);
                tool2.DefualtRotation = Matrix.CreateFromAxisAngle(tool2.Rotation.Left, 0.7f);
                tool2.Type1 = Tool.Types.Sword;
                tool2.Length = 85f;
                tool2.MaxAttackingDistance = 85f;
                tool2.MinAttackingDistance = 10f;
                tool2.RewardsToVictim = new RewardSet(-5);
                Tool.Tools.Add(Sword);
                Tool.iSword1 = 0;
                Tool Bow = new Tool
                {
                    Model = base.Content.Load<Model>("Models\\BowD")
                };
                Bow.Transforms = this.SetupEffectDefaults(Bow.Model, Main.projectionMatrix, Main.viewMatrix);
                Tool tool3 = Bow;
                tool3.Name = "Bow1";
                tool3.Rotation = Matrix.Identity;
                tool3.RelativePosition = Vector3.Zero;
                tool3.OriginalRotation = Matrix.CreateFromAxisAngle(tool3.Rotation.Left, 0.7f);
                tool3.DefualtRotation = Matrix.CreateFromAxisAngle(tool3.Rotation.Left, 0.7f);
                tool3.Type1 = Tool.Types.Bow;
                tool3.Length = 10f;
                tool3.RewardsToVictim = new RewardSet(0);
                tool3.MaxAttackingDistance = 90f;
                tool3.MinAttackingDistance = 70f;
                Tool.Tools.Add(Bow);
                Tool.iBow1 = 1;
                Tool RSword = new Tool
                {
                    Model = base.Content.Load<Model>("Models\\RSword")
                };
                RSword.Transforms = this.SetupEffectDefaults(RSword.Model, Main.projectionMatrix, Main.viewMatrix);
                Tool tool4 = RSword;
                tool4.Name = "RSword";
                tool4.Rotation = Matrix.Identity;
                tool4.RelativePosition = Vector3.Zero;
                tool4.OriginalRotation = Matrix.CreateFromAxisAngle(tool4.Rotation.Left, 0.7f);
                tool4.DefualtRotation = Matrix.CreateFromAxisAngle(Vector3.Left, 0.7f);
                tool4.Type1 = Tool.Types.Sword;
                tool4.Length = 120f;
                tool4.RewardsToVictim = new RewardSet(-10);
                tool4.MaxAttackingDistance = 100f;
                tool4.MinAttackingDistance = 80f;
                Tool.Tools.Add(RSword);
                Tool.iRSword = 2;
                Tool Shovel = new Tool
                {
                    Model = base.Content.Load<Model>("Models\\Shovel")
                };
                Shovel.Transforms = this.SetupEffectDefaults(Shovel.Model, Main.projectionMatrix, Main.viewMatrix);
                Tool tool5 = Shovel;
                tool5.Name = "Shovel";
                tool5.Rotation = Matrix.Identity;
                tool5.RelativePosition = Vector3.Zero;
                tool5.OriginalRotation = Matrix.Identity;
                tool5.DefualtRotation = Matrix.Identity;
                tool5.Type1 = Tool.Types.Shovel;
                tool5.Length = 120f;
                tool5.RewardsToVictim = new RewardSet(-5);
                tool5.MaxAttackingDistance = 100f;
                tool5.MinAttackingDistance = 80f;
                Tool.Tools.Add(Shovel);
                Tool.iShovel = 3;
                Tool.InitializeAnimations();
                BlockType.Air = new BlockType
                {
                    ID = 0,
                    IsAir = true,
                    Name = "Air"
                };
                BlockType.BTList[0] = BlockType.Air;
                BlockType.PlaceHolder = new BlockType
                {
                    ID = 0,
                    Varient = 1,
                    IsAir = false,
                    Name = "PlaceHolder"
                };
                BlockType.BTList[0] = BlockType.PlaceHolder;
                BlockType.Dirt = new BlockType();
                Model DirtModel = base.Content.Load<Model>("Models\\Dirt");
                BlockType.Dirt.Mesh = DirtModel.Meshes[0];
                Matrix[] M = new Matrix[DirtModel.Bones.Count - 1 + 1];
                DirtModel.CopyBoneTransformsTo(M);
                BlockType.Dirt.Transform = M.Last<Matrix>();
                BlockType.Dirt.ID = 1;
                BlockType.BTList[1] = BlockType.Dirt;
                BlockType.Dirt.Name = "Dirt";
                BlockType.GrassBlock = new BlockType();
                Model GrassM = base.Content.Load<Model>("Models\\GrassBlock");
                M = new Matrix[GrassM.Bones.Count - 1 + 1];
                BlockType.GrassBlock.Name = "GrassBlock";
                BlockType.GrassBlock.Mesh = GrassM.Meshes[0];
                DirtModel.CopyAbsoluteBoneTransformsTo(M);
                BlockType.GrassBlock.Transform = M.Last<Matrix>();
                BlockType.GrassBlock.ID = 2;
                BlockType.GrassBlock.nEffects = true;
                BlockType.BTList[2] = BlockType.GrassBlock;
                BlockType.Sand = new BlockType();
                Model SandM = base.Content.Load<Model>("Models\\Sand");
                M = new Matrix[SandM.Bones.Count - 1 + 1];
                BlockType.Sand.Name = "Sand";
                BlockType.Sand.Mesh = SandM.Meshes[0];
                SandM.CopyAbsoluteBoneTransformsTo(M);
                BlockType.Sand.Transform = M.Last<Matrix>();
                BlockType.Sand.ID = 3;
                BlockType.BTList[3] = BlockType.Sand;
                BlockType.WoodPlank = new BlockType();
                Model WoodPlankModel = base.Content.Load<Model>("Models\\WoodPlank");
                BlockType.WoodPlank.Mesh = WoodPlankModel.Meshes[0];
                M = new Matrix[WoodPlankModel.Bones.Count - 1 + 1];
                WoodPlankModel.CopyBoneTransformsTo(M);
                BlockType.WoodPlank.Transform = M.Last<Matrix>();
                BlockType.WoodPlank.ID = 4;
                BlockType.BTList[4] = BlockType.WoodPlank;
                BlockType.WoodPlank.Name = "WoodPlank";
                BlockType.Brick = new BlockType();
                Model BrickModel = base.Content.Load<Model>("Models\\Brick");
                BlockType.Brick.Mesh = BrickModel.Meshes[0];
                M = new Matrix[BrickModel.Bones.Count - 1 + 1];
                BrickModel.CopyBoneTransformsTo(M);
                BlockType.Brick.Transform = M.Last<Matrix>();
                BlockType.Brick.ID = 5;
                BlockType.BTList[5] = BlockType.Brick;
                BlockType.Brick.Name = "Brick";
                BlockType.Stone = new BlockType();
                Model StoneModel = base.Content.Load<Model>("Models\\Stone");
                BlockType.Stone.Mesh = StoneModel.Meshes[0];
                M = new Matrix[StoneModel.Bones.Count - 1 + 1];
                StoneModel.CopyBoneTransformsTo(M);
                BlockType.Stone.Transform = M.Last<Matrix>();
                BlockType.Stone.ID = 6;
                BlockType.BTList[6] = BlockType.Stone;
                BlockType.Stone.Name = "Stone";
                BlockType.StoneWall = new BlockType();
                Model StoneWallModel = base.Content.Load<Model>("Models\\StoneWall");
                BlockType.StoneWall.Mesh = StoneWallModel.Meshes[0];
                M = new Matrix[StoneWallModel.Bones.Count - 1 + 1];
                StoneWallModel.CopyBoneTransformsTo(M);
                BlockType.StoneWall.Transform = M.Last<Matrix>();
                BlockType.StoneWall.ID = 7;
                BlockType.BTList[7] = BlockType.StoneWall;
                BlockType.StoneWall.Name = "StoneWall";
                BlockType.Tree1 = new BlockType();
                Model Tree1Model = base.Content.Load<Model>("Models\\Tree1");
                BlockType.Tree1.Mesh = Tree1Model.Meshes[0];
                M = new Matrix[Tree1Model.Bones.Count - 1 + 1];
                Tree1Model.CopyBoneTransformsTo(M);
                BlockType.Tree1.Transform = M.Last<Matrix>();
                BlockType.Tree1.ID = 8;
                BlockType.BTList[8] = BlockType.Tree1;
                BlockType.Tree1.Name = "Tree1";
                BlockType.Jak = new BlockType();
                Model JakModel = base.Content.Load<Model>("Models\\Jak");
                BlockType.Jak.Mesh = JakModel.Meshes[0];
                M = new Matrix[JakModel.Bones.Count - 1 + 1];
                JakModel.CopyBoneTransformsTo(M);
                BlockType.Jak.Transform = M.Last<Matrix>();
                BlockType.Jak.ID = 9;
                BlockType.BTList[9] = BlockType.Jak;
                BlockType.Jak.Name = "Jak";
                BlockType.Wood = new BlockType();
                Model WoodBlockModel = base.Content.Load<Model>("Models\\Wood");
                BlockType.Wood.Mesh = WoodBlockModel.Meshes[0];
                M = new Matrix[WoodBlockModel.Bones.Count - 1 + 1];
                WoodBlockModel.CopyBoneTransformsTo(M);
                BlockType.Wood.Transform = M.Last<Matrix>();
                BlockType.Wood.ID = 10;
                BlockType.BTList[10] = BlockType.Wood;
                BlockType.Wood.Name = "Wood";
                Main.Player1 = new Player(EntityTypes.Player1)
                {
                    Name = "Player1",
                    eType = EntityTypes.Player1
                };
                Player player = Main.Player1;
                player.ControlsList = Controls.NewControlList(Microsoft.Xna.Framework.Input.Keys.W, Microsoft.Xna.Framework.Input.Keys.S, Microsoft.Xna.Framework.Input.Keys.A, Microsoft.Xna.Framework.Input.Keys.D, Microsoft.Xna.Framework.Input.Keys.Space, Microsoft.Xna.Framework.Input.Keys.Z);
                player.ControlsList.Add(new Controls.Control(Controls.Control.MouseKeys.LeftClick, Actions.Attack));
                player.ControlsList.Add(new Controls.Control(Controls.Control.MouseKeys.RightClick, Actions.PlaceBlock));
                player.ControlsList.Add(new Controls.Control(Microsoft.Xna.Framework.Input.Keys.E, Actions.BreakBlock));
                player.ControlsList.Add(new Controls.Control(Controls.Control.MouseKeys.WheelUp, Actions.ChangeBlock));
                player.ControlsList.Add(new Controls.Control(Microsoft.Xna.Framework.Input.Keys.F, Actions.Interact));
                player.ControlsList.Add(new Controls.Control(Microsoft.Xna.Framework.Input.Keys.Q, Actions.C1));
                player.ControlsList.Add(new Controls.Control(Microsoft.Xna.Framework.Input.Keys.T, Actions.C2));
                player.ControlsList.Add(new Controls.Control(Microsoft.Xna.Framework.Input.Keys.R, Actions.C3));
                player.Health = 1000;
                player.MaxHealth = 1000;
                player.NeededBodyRotationGainingSpeed = 0.1f;
                player.CamPosType = RacingCameraAngle.Inside;
                player.Load(EntityTypes.Player1);
                player.GiveTool(Tool.Buy(Tool.iRSword), 1);
                player.SelectedBlockType = BlockType.Dirt;
                Board.SelectedToolAndBlock = "Tool:" + Main.Player1.CTool.Name + "; Block:" + Main.Player1.SelectedBlockType.Name;
                player.CollitionHierarchy = eCollitionHierarchy.CreateNewHumanHierarchy(Main.Player1);
                player.Accelaration = new Vector3(3f, 3f, 3.5f);
                player.Name = "Crazy Cat";
                BiomeList.Initialize();
                Struct.Initialize();
                bool newMap = Main.MapVariablePipeline.NewMap;
                if (newMap)
                {
                    Ground.Generate(Main.CurrentMapName, Main.MapVariablePipeline.NewMapSize, Main.MapVariablePipeline.NewMapBiome, Main.MapVariablePipeline.NewMapSpeedSave);
                }
                else
                {
                    Loader.LoadWorld(Main.CurrentMapName);
                }
                bool IsPlayerAlreadySaved = false;
                List<Entity> eLst = new List<Entity>();
                List<XEntity> XeLst = XEntity.Load(Loader.FileEntity);

                foreach (XEntity Xe in XeLst)
                {
                    bool flag = Xe.TypeID != EntityTypes.Player1.ID;
                    if (flag)
                    {
                        eLst.Add(Xe.GetEntity());
                    }
                    else
                    {
                        XEntity xentity = Xe;
                        Entity entity = Main.Player1;
                        xentity.SetEntitySettings(ref entity);
                        Main.Player1 = (Player)entity;
                        eLst.Add(Main.Player1);
                        IsPlayerAlreadySaved = true;
                    }
                }

                bool flag2 = !IsPlayerAlreadySaved;
                if (flag2)
                {
                    eLst.Add(Main.Player1);
                }
                Main.Player1.Position = Loader.MInfo.PlayerPosition;
                bool flag3 = XeLst.Count > 0;
                if (flag3)
                {
                    int num = XeLst.Count - 1;
                    for (int j = 0; j <= num; j++)
                    {
                        XEntity xentity2 = XeLst[j];
                        List<Entity> list;
                        int index;
                        Entity entity = (list = eLst)[index = j];
                        xentity2.GenerateRelationships(ref entity, ref eLst);
                        list[index] = entity;
                    }
                }
                Ground.CStack.eList.AddRange(eLst);
                int num2 = this.InitialHumanCount - 1;
                for (int k = 0; k <= num2; k++)
                {
                    Vector3 Pos = PhysicsFuncs.RNDVec3(300f, 300f);
                    Pos.Y = 500f;
                    Human H = Human.AddNewHuman(EntityTypes.Murderer, "Human" + k.ToString(), Pos, Tool.Buy(PhysicsFuncs.RandomOf<int>(new int[]
                    {
                        Tool.iSword1,
                        Tool.iRSword
                    })), 1);
                    H.AddToTheCurrentStack();
                }
                Stack.Volume = Stack.Size * Chunk.Size * 50f;
                Main.MaxRenderDistance = (Stack.Volume / 2f).Length();
                base.Window.AllowAltF4 = false;
                Main.FGame = (Form)Control.FromHandle(base.Window.Handle);
                Main.FHUI = new FrmHUI();
                Main.FHUI.Apply(Main.FGame);
                Main.MouseDefPosition = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                System.Timers.Timer Tmr10s = new System.Timers.Timer(10000.0);
                Tmr10s.Elapsed += delegate (object a0, ElapsedEventArgs a1)
                {
                    this.Tmr10sTick();
                };
                Tmr10s.Start();
                System.Timers.Timer Tmr1s = new System.Timers.Timer(1000.0);
                Tmr1s.Elapsed += delegate (object a0, ElapsedEventArgs a1)
                {
                    this.Tmr1sTick();
                };
                Tmr1s.Start();
                System.Timers.Timer Tmr200ms = new System.Timers.Timer(200.0);
                Tmr200ms.Elapsed += delegate (object a0, ElapsedEventArgs a1)
                {
                    this.Tmr200msTick();
                };
                Tmr200ms.Start();
                Main.STW.Start();
            }
        }

        // Token: 0x0600010A RID: 266 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
        protected override void UnloadContent()
        {
        }

        // Token: 0x0600010B RID: 267 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
        protected override void Update(GameTime gameTime)
        {
            Main.NowGameTime = checked((long)Math.Round(gameTime.TotalGameTime.TotalMilliseconds));
            KeyboardState KBState = Keyboard.GetState();
            bool flag = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape);
            if (flag)
            {
                Main.ExitApp();
            }
            else
            {
                Player M = Main.Player1;
                bool flag2 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P);
                if (flag2)
                {
                    bool flag3 = gameTime.TotalGameTime.TotalSeconds > this.MouseSwitchLast + 2.0;
                    if (flag3)
                    {
                        bool mouseLookEnadbled = Main.MouseLookEnadbled;
                        if (mouseLookEnadbled)
                        {
                            Main.MouseLookEnadbled = false;
                        }
                        else
                        {
                            Main.MouseLookEnadbled = true;
                        }
                        this.MouseSwitchLast = gameTime.TotalGameTime.TotalSeconds;
                    }
                }
                bool flag4 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.N);
                if (flag4)
                {
                    Main.TimeOfTheDay += 4.0;
                }
                else
                {
                    bool flag5 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.M);
                    if (flag5)
                    {
                        Main.TimeOfTheDay -= 4.0;
                    }
                    else
                    {
                        bool flag6 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.H);
                        checked
                        {
                            if (flag6)
                            {
                                bool flag7 = this.DevUtilsTime + 100L < Main.NowGameTime;
                                if (flag7)
                                {
                                    Vector3 RPos = PhysicsFuncs.RNDVec3(100f, -100f);
                                    RPos.Y = 0f;
                                    Human H = Human.AddNewHuman(EntityTypes.Human1, "Human" + Ground.CStack.eList.Count.ToString(), Main.Player1.Position + RPos, Tool.Buy(PhysicsFuncs.RandomOf<int>(new int[]
                                    {
                                        Tool.iSword1,
                                        Tool.iRSword
                                    })), 1);
                                    H.AddToTheCurrentStack();
                                    this.DevUtilsTime = Main.NowGameTime;
                                }
                            }
                            else
                            {
                                bool flag8 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.J);
                                if (flag8)
                                {
                                    bool flag9 = this.DevUtilsTime + 100L < Main.NowGameTime;
                                    if (flag9)
                                    {
                                        foreach (Entity e in Main.Player1.FacingEntities(2500f, 70f))
                                        {
                                            e.Hire(Main.Player1);
                                        }
                                        this.DevUtilsTime = Main.NowGameTime;
                                    }
                                }
                                else
                                {
                                    bool flag10 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Y);
                                    if (flag10)
                                    {
                                        Main.Player1.Health = 1000;
                                    }
                                    else
                                    {
                                        bool flag11 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z);
                                        if (flag11)
                                        {
                                            this.FCP = Main.cameraPosition;
                                        }
                                        else
                                        {
                                            bool flag12 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.B);
                                            if (flag12)
                                            {
                                                Entity FE = Main.Player1.FacingEntity(200f);
                                                bool flag13 = FE != null;
                                                if (flag13)
                                                {
                                                    FE.DefendHere();
                                                }
                                            }
                                            else
                                            {
                                                bool flag14 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F12);
                                                if (flag14)
                                                {
                                                    foreach (Entity E in Main.Player1.FacingEntities(200f, 70f))
                                                    {
                                                        E.DefendHere();
                                                    }
                                                }
                                                else
                                                {
                                                    bool flag15 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.I);
                                                    if (flag15)
                                                    {
                                                        bool flag16 = this.DevUtilsTime + 100L < Main.NowGameTime;
                                                        if (flag16)
                                                        {
                                                            Board.IsDebugInfoVisible = !Board.IsDebugInfoVisible;
                                                            this.DevUtilsTime = Main.NowGameTime;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool flag17 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F10);
                                                        if (flag17)
                                                        {
                                                            Loader.SaveChunks(new Chunk[]
                                                            {
                                                                Main.Player1.CurrentChunk
                                                            }, 1, false);
                                                        }
                                                        else
                                                        {
                                                            bool flag18 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F9);
                                                            if (flag18)
                                                            {
                                                                bool flag19 = gameTime.TotalGameTime.TotalSeconds > this.MouseSwitchLast;
                                                                if (flag19)
                                                                {
                                                                    bool isInTheSurface = Main.Player1.CurrentChunk.IsInTheSurface;
                                                                    if (isInTheSurface)
                                                                    {
                                                                        List<Chunk> SFCLst = Ground.SurfaceChunks.ToList<Chunk>();
                                                                        SFCLst.Remove(Main.Player1.CurrentChunk);
                                                                        Ground.SurfaceChunks = SFCLst.ToArray();
                                                                        Ground.FilledSFC--;
                                                                    }
                                                                    Loader.LoadAndReplaceChunk(Main.Player1.CurrentChunk.Index);
                                                                    this.MouseSwitchLast = gameTime.TotalGameTime.TotalSeconds;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                bool mouseLookEnadbled2 = Main.MouseLookEnadbled;
                if (mouseLookEnadbled2)
                {
                    int MouseGetStateX = Mouse.GetState().X;
                    Matrix RotChange = Matrix.CreateRotationY(MathHelper.ToRadians((float)(checked(Main.MouseDefPosition.X - MouseGetStateX)) * Main.MouseSensivity));
                    bool flag20 = MouseGetStateX > Main.MouseDefPosition.X;
                    if (flag20)
                    {
                        M.HeadRotation *= RotChange;
                        M.NeededBodyRotation *= RotChange;
                        M.NeededBodyRotationChanged = true;
                    }
                    else
                    {
                        bool flag21 = Mouse.GetState().X < Main.MouseDefPosition.X;
                        if (flag21)
                        {
                            M.HeadRotation *= RotChange;
                            M.NeededBodyRotation *= RotChange;
                            M.NeededBodyRotationChanged = true;
                        }
                    }
                    int MouseGetStateY = Mouse.GetState().Y;
                    float Amount = MathHelper.ToRadians((float)(checked(MouseGetStateY - Main.MouseDefPosition.Y)) * Main.MouseSensivity);
                    bool flag22 = MouseGetStateY > Main.MouseDefPosition.Y;
                    if (flag22)
                    {
                        bool flag23 = (double)(this.HRAngle + Amount) < 1.6;
                        if (flag23)
                        {
                            M.HeadRotation *= Matrix.CreateFromAxisAngle(M.HeadRotation.Left, Amount);
                            this.HRAngle += Amount;
                            M.InsiderHandXCurrRotation = Amount;
                            M.InsiderHandXRotation += Amount;
                        }
                    }
                    else
                    {
                        bool flag24 = MouseGetStateY < Main.MouseDefPosition.Y;
                        if (flag24)
                        {
                            bool flag25 = (double)(this.HRAngle + Amount) > -1.4;
                            if (flag25)
                            {
                                M.HeadRotation *= Matrix.CreateFromAxisAngle(M.HeadRotation.Left, Amount);
                                this.HRAngle += Amount;
                                M.InsiderHandXCurrRotation = Amount;
                                M.InsiderHandXRotation += Amount;
                            }
                        }
                    }
                    Mouse.SetPosition(100, 100);
                    Main.MouseDefPosition.X = 100;
                    Main.MouseDefPosition.Y = 100;
                }
                Main.Player1.UpdateMan(KBState, Mouse.GetState());

                foreach (Entity e2 in Ground.CStack.eList)
                {
                    e2.Update();
                }

                checked
                {
                    for (int i = 0; i < ePAnimation.LstAnimation.Count; i++)
                    {
                        ePAnimation.LstAnimation[i].Animate();
                    }

                    foreach (Entity e3 in Main.DeadEntities)
                    {
                        Ground.CStack.eList.Remove(e3);
                    }

                    bool flag26 = KBState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.L);
                    if (flag26)
                    {
                        bool flag27 = this.LastCamSwapedTime + 300L < Main.NowGameTime;
                        if (flag27)
                        {
                            bool flag28 = Main.Player1.CamPosType == RacingCameraAngle.Back;
                            if (flag28)
                            {
                                Main.Player1.CamPosType = RacingCameraAngle.Inside;
                                Main.Player1.BodyParts[6].Rotation *= Matrix.CreateFromAxisAngle(Main.Player1.ModelRotation.Left, -0.7853982f);
                            }
                            else
                            {
                                bool flag29 = Main.Player1.CamPosType == RacingCameraAngle.Inside;
                                if (flag29)
                                {
                                    Main.Player1.CamPosType = RacingCameraAngle.Back;
                                    Main.Player1.BodyParts[6].Rotation *= Matrix.CreateFromAxisAngle(Main.Player1.ModelRotation.Left, 0.7853982f);
                                }
                            }
                            this.LastCamSwapedTime = Main.NowGameTime;
                        }
                    }
                    bool flag30 = !Main.Player1.IsDead;
                    if (flag30)
                    {
                        bool flag31 = Main.Player1.CamPosType == RacingCameraAngle.Back;
                        if (flag31)
                        {
                            Vector3 P1CamPos = Main.Player1.Position + Main.Player1.FacingDirection * -400f + Vector3.Up * 450f;
                            Main.cameraPosition = P1CamPos;
                            Main.viewMatrix = Matrix.CreateLookAt(Main.cameraPosition, Main.Player1.Position + Main.Player1.FacingDirection * 300f, Vector3.Up);
                        }
                        else
                        {
                            bool flag32 = Main.Player1.CamPosType == RacingCameraAngle.Inside;
                            if (flag32)
                            {
                                Main.cameraPosition = Main.Player1.Position + Main.CameraRelativeYPos;
                                Main.viewMatrix = Matrix.CreateLookAt(Main.cameraPosition, Main.cameraPosition + Main.Player1.FacingDirection, Main.Player1.HeadRotation.Up);
                            }
                        }
                    }
                    base.Update(gameTime);
                    Main.CurrUpdateCount += 1L;
                }
            }
        }

        // Token: 0x0600010C RID: 268 RVA: 0x0000D538 File Offset: 0x0000B738
        protected override void Draw(GameTime gameTime)
        {
            checked
            {
                Main.FPS = (int)Math.Round(1000.0 / (double)Math.Max(Main.STW.ElapsedMilliseconds - Main.STWLast, 1L));
                Main.STWLast = Main.STW.ElapsedMilliseconds;
                base.GraphicsDevice.Clear(Main.BackColor);
                bool IsViewChanged = this.LastviewMatrix != Main.viewMatrix | Main.IsGroundChanged;
                Main.IsGroundChanged = false;
                this.LastviewMatrix = Main.viewMatrix;
                this.BF = new BoundingFrustum(Main.viewMatrix * Main.projectionMatrix);
                Main.Player1.DrawMan();

                foreach (Entity e in Ground.CStack.eList)
                {
                    bool flag = this.BF.Intersects(new BoundingSphere(e.Position, 150f));
                    if (flag)
                    {
                        e.Draw();
                    }
                }

                bool flag2 = IsViewChanged;
                if (flag2)
                {
                    Board.Sampler = Board.MovingImageSampler;
                    int i = 1;
                    do
                    {
                        this.CountForBT[i] = 0;
                        this.ArraysForBT[i] = null;
                        i++;
                    }
                    while (i <= 10);
                    int j = 0;
                    do
                    {
                        this.ArrayUsage[j] = false;
                        j++;
                    }
                    while (j <= 200);
                    this.ScanChunks();
                    this.AssignArraysForBlockTypes();
                    this.CopyTo_ArraysForBt();
                }
                else
                {
                    Board.Sampler = Board.StillImageSampler;
                }
                this.DrawBlocksFromArrays();
                Board.Draw();
                base.Draw(gameTime);
            }
        }

        // Token: 0x0600010D RID: 269 RVA: 0x0000D6DC File Offset: 0x0000B8DC
        private void ScanChunks()
        {
            this.nChunkArray = 0;
            float RenderDistanceSquared = Main.RenderDistance * Main.RenderDistance;
            checked
            {
                int num = Ground.FilledSFC - 1;
                for (int nCH = 0; nCH <= num; nCH++)
                {
                    Chunk ch = Ground.SurfaceChunks[nCH];
                    bool flag = ch != null && !ch.disposedValue;
                    if (flag)
                    {
                        bool flag2 = Vector3.DistanceSquared(ch.Position, Main.cameraPosition) < RenderDistanceSquared;
                        if (flag2)
                        {
                            this.BS.Center = ch.Position;
                            bool flag3 = this.BF.Intersects(this.BS);
                            if (flag3)
                            {
                                int i = 1;
                                do
                                {
                                    this.CountForBT[i] += ch.CountForBlockTypes[i];
                                    i++;
                                }
                                while (i <= 10);
                                this.ChunkArray[this.nChunkArray] = ch;
                                this.nChunkArray++;
                            }
                        }
                    }
                }
                bool flag4 = Ground.TmpFilledSFC > 0;
                if (flag4)
                {
                    int num2 = Ground.TmpFilledSFC - 1;
                    for (int nCH2 = 0; nCH2 <= num2; nCH2++)
                    {
                        Chunk ch = Ground.TmpSurfaceChunks[nCH2];
                        bool flag5 = Vector3.DistanceSquared(ch.Position, Main.cameraPosition) < unchecked(Main.RenderDistance * Main.RenderDistance);
                        if (flag5)
                        {
                            this.BS.Center = ch.Position;
                            bool flag6 = this.BF.Intersects(this.BS);
                            if (flag6)
                            {
                                int j = 1;
                                do
                                {
                                    this.CountForBT[j] += ch.CountForBlockTypes[j];
                                    j++;
                                }
                                while (j <= 10);
                                this.ChunkArray[this.nChunkArray] = ch;
                                this.nChunkArray++;
                            }
                        }
                    }
                }
            }
        }

        // Token: 0x0600010E RID: 270 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
        private void AssignArraysForBlockTypes()
        {
            int i = 1;
            checked
            {
                do
                {
                    int Count = this.CountForBT[i];
                    bool flag = Count == 0;
                    if (flag)
                    {
                        this.ArraysForBT[i] = null;
                        this.IVBsForBT[i] = null;
                    }
                    else
                    {
                        int j = Count / 256;
                        int k = j;
                        while (this.ArrayUsage[k])
                        {
                            k++;
                        }
                        this.ArraysForBT[i] = this.MAs[k];
                        this.ArrayUsage[k] = true;
                        this.IVBsForBT[i] = this.IVBs[j];
                    }
                    this.nForBT[i] = 0;
                    i++;
                }
                while (i <= 10);
            }
        }

        // Token: 0x0600010F RID: 271 RVA: 0x0000D944 File Offset: 0x0000BB44
        private void CopyTo_ArraysForBt()
        {
            checked
            {
                int num = this.nChunkArray - 1;
                for (int i = 0; i <= num; i++)
                {
                    Chunk Ch = this.ChunkArray[i];
                    int num2 = (int)(Ch.BIDFilledI - 1);
                    for (int CI = 0; CI <= num2; CI++)
                    {
                        byte BTID = Ch.BIDForBTranslations[CI];
                        this.ArraysForBT[(int)BTID][this.nForBT[(int)BTID]] = Ch.BlockTranslations[CI];
                        this.nForBT[(int)BTID]++;
                    }
                }
            }
        }

        // Token: 0x06000110 RID: 272 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
        private void DrawBlocksFromArrays()
        {
            int i = 1;

            int view, proj, sun, cam, sunDir;

            if (Main.platform == Main.Platform.DirectX)
            {
                view = 0; proj = 1; sun = 2; cam = 3; sunDir = 4;
            }
            else
            {
                view = 1; proj = 2; sun = 3; cam = 4; sunDir = 0;
            }


            do
            {
                BlockType BT = BlockType.BTList[i];
                int InstancesCount = this.CountForBT[i];
                bool flag = InstancesCount != 0;
                if (flag)
                {
                    Vector4[] BlockInstances = this.ArraysForBT[i];
                    DynamicVertexBuffer instanceVertexBuffer = this.IVBsForBT[i];
                    instanceVertexBuffer.SetData<Vector4>(BlockInstances, 0, this.CountForBT[i], SetDataOptions.Discard);
                    VertexBufferBinding[] MeshVertexBufferBinding = new VertexBufferBinding[]
                    {
                            default,
                            new VertexBufferBinding(instanceVertexBuffer, 0, 1)
                    };

                    foreach (ModelMeshPart meshPart in BT.Mesh.MeshParts)
                    {
                        MeshVertexBufferBinding[0] = new VertexBufferBinding(meshPart.VertexBuffer, meshPart.VertexOffset, 0);
                        base.GraphicsDevice.SetVertexBuffers(MeshVertexBufferBinding);
                        base.GraphicsDevice.Indices = meshPart.IndexBuffer;
                        Effect effect = meshPart.Effect;

                        effect.Parameters[view].SetValue(Main.viewMatrix);
                        effect.Parameters[proj].SetValue(Main.projectionMatrix);
                        effect.Parameters[sun].SetValue(Main.SunLightIntencity);
                        effect.Parameters[cam].SetValue(Main.cameraPosition);
                        effect.Parameters[sunDir].SetValue(Main.SunlightDirection);


                        foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            //base.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, meshPart.StartIndex, meshPart.PrimitiveCount, this.CountForBT[i]);
                            base.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, meshPart.NumVertices, meshPart.StartIndex, meshPart.PrimitiveCount, this.CountForBT[i]);
                        }

                    }

                }
                i++;
            }
            while (i <= 10);

        }





        // Token: 0x06000111 RID: 273 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
        public static void DrawModel(Model model, Matrix modelTransform, Matrix[] absoluteBoneTransforms1)
        {

            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (Effect effect2 in mesh.Effects)
                {
                    BasicEffect effect = (BasicEffect)effect2;
                    effect.Projection = Main.projectionMatrix;
                    effect.View = Main.viewMatrix;
                    effect.World = absoluteBoneTransforms1[mesh.ParentBone.Index] * modelTransform;
                }


                mesh.Draw();
            }

        }

        // Token: 0x06000112 RID: 274 RVA: 0x0000DCD0 File Offset: 0x0000BED0
        public void Tmr10sTick()
        {
            bool flag = Ground.CStack.nSavingChunks > 0;
            if (flag)
            {
                Loader.SaveChunks(Ground.CStack.SavingChunks, Ground.CStack.nSavingChunks, false);
                Ground.CStack.nSavingChunks = 0;
            }
            List<XEntity> XeLst = new List<XEntity>();
            foreach (Entity e in Ground.CStack.eList)
            {
                bool flag2 = !e.IsDead || e.eType.IsPlayer;
                if (flag2)
                {
                    XeLst.Add(new XEntity(e));
                }
            }
            XEntity.Save(Loader.FileEntity, XeLst);
            Loader.MInfo.PlayerPosition = Main.Player1.Position;
            Loader.MInfo.Save(Loader.MapInfoFile);
        }

        // Token: 0x06000113 RID: 275 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
        public void Tmr1sTick()
        {

            foreach (Entity E in Ground.CStack.eList)
            {
                E.ExpensiveUpdate();
                bool flag = Main.RunningSlow && E.IsDead && !E.eType.IsPlayer && !Main.DeadEntities.Contains(E);
                if (flag)
                {
                    Main.DeadEntities.Add(E);
                }
            }

        }

        // Token: 0x06000114 RID: 276 RVA: 0x0000DE60 File Offset: 0x0000C060
        public void Tmr200msTick()
        {
            bool flag = Main.FPS < Main.MinFPS;
            if (flag)
            {
                bool flag2 = Main.RenderDistance > Main.MinRenderDistance;
                if (flag2)
                {
                    Main.RenderDistance -= 70f;
                }
                Main.RunningSlow = true;
            }
            else
            {
                bool flag3 = Main.FPS > Main.MaxFPS;
                if (flag3)
                {
                    bool flag4 = Main.RenderDistance < Main.MaxRenderDistance;
                    if (flag4)
                    {
                        Main.RenderDistance += 50f;
                    }
                    Main.RunningSlow = false;
                }
            }
            Main.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(Main.FOV, Main.Viewport.AspectRatio, 1f, Main.RenderDistance);
            Main.TimeOfTheDay += 1.0;
            Main.SunLightIntencity = Main.GetSunlightIntencity(Main.TimeOfTheDay);
            Main.BackColor = Color.FromNonPremultiplied(Color.SkyBlue.ToVector4() * (float)Math.Min(1.2, (double)Main.SunLightIntencity));
            bool flag5 = Main.TimeOfTheDay > 1440.0;
            if (flag5)
            {
                Main.TimeOfTheDay = 0.0;
            }
            float SunOrMoonAngle = MathHelper.WrapAngle((float)((Main.TimeOfTheDay - 720.0) / 1440.0 * 6.2831854820251465));
            Main.SunlightDirection = Matrix.CreateFromAxisAngle(Vector3.Right, SunOrMoonAngle).Forward;
            IntVector3 RChIndex = Ground.ChunkIndexOfPosition(Main.Player1.Position);
            bool flag6 = !Ground.CStack.Scrolling;
            if (flag6)
            {
                Ground.CStack.Scroll(new IntVector3(RChIndex.X, 0, RChIndex.Z));
            }
            Main.FHUI.UpdateUI();
        }

        // Token: 0x06000115 RID: 277 RVA: 0x0000E010 File Offset: 0x0000C210
        public void ExitGame()
        {
            this.UnloadContent();
        }

        // Token: 0x0400011B RID: 283
        public int InitialHumanCount;

        // Token: 0x0400011C RID: 284
        public long LastCamSwapedTime;

        // Token: 0x0400011D RID: 285
        public float HRAngle;

        // Token: 0x0400011E RID: 286
        private double MouseSwitchLast;

        // Token: 0x0400011F RID: 287
        private long DevUtilsTime;

        // Token: 0x04000120 RID: 288
        public BoundingFrustum BF;

        // Token: 0x04000121 RID: 289
        public BoundingSphere BS;

        // Token: 0x04000122 RID: 290
        public Matrix LastviewMatrix;

        // Token: 0x04000123 RID: 291
        public Vector3 FCP;

        // Token: 0x04000124 RID: 292
        private const int nArrays = 200;

        // Token: 0x04000125 RID: 293
        private const int SizeOfRoom = 256;

        // Token: 0x04000126 RID: 294
        public DynamicVertexBuffer[] IVBs;

        // Token: 0x04000127 RID: 295
        public Vector4[][] MAs;

        // Token: 0x04000128 RID: 296
        public bool[] ArrayUsage;

        // Token: 0x04000129 RID: 297
        public Chunk[] ChunkArray;

        // Token: 0x0400012A RID: 298
        public int nChunkArray;

        // Token: 0x0400012B RID: 299
        public int[] CountForBT;

        // Token: 0x0400012C RID: 300
        public int[] nForBT;

        // Token: 0x0400012D RID: 301
        public Vector4[][] ArraysForBT;

        // Token: 0x0400012E RID: 302
        public DynamicVertexBuffer[] IVBsForBT;

        // Token: 0x0400012F RID: 303
        private static readonly VertexDeclaration instanceVertexDeclaration = new VertexDeclaration(new VertexElement[]
        {
            new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0)
        });
    }
}
