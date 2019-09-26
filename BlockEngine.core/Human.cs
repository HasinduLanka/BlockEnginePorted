using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine
{
    // Token: 0x02000023 RID: 35
    public class Human : Entity
    {
        // Token: 0x0600013C RID: 316 RVA: 0x00011378 File Offset: 0x0000F578
        public override void Update()
        {
            bool flag = !this.IsDead;
            if (flag)
            {
                bool flag2 = !this.eType.IsPlayer && base.CheckOutOfStackRange();
                if (!flag2)
                {
                    this.Position += this.Velocity;
                    this.Velocity *= this.ModelVelocityReducingFactor;
                    bool flag3 = this.Velocity.LengthSquared() < 0.02f;
                    if (flag3)
                    {
                        this.Velocity = Vector3.Zero;
                    }
                    bool flag4 = base.CheckOutOfWorld();
                    if (flag4)
                    {
                        base.PutInsideOfTheWorld();
                    }
                    this.FacingDirection = this.HeadRotation.Forward;
                    this.FacingDirection.Normalize();
                    this.DualFacingDirections = Physics.Find2DDualDirectionsOfDirection(this.FacingDirection);
                    this.OnGround = false;
                    this.BlockEnv = Ground.GetBlockEnvironment(this.Position - Ground.BlockSizeHalfYonlyV3, true);
                    bool flag5 = Information.IsNothing(this.BlockEnv);
                    if (flag5)
                    {
                        base.ChunkOutOfStackRange();
                        this.BlockEnv = Ground.GetBlockEnvironment(this.Position - Ground.BlockSizeHalfYonlyV3, true);
                        bool flag6 = Information.IsNothing(this.BlockEnv);
                        if (flag6)
                        {
                            bool isPlayer = this.eType.IsPlayer;
                            if (!isPlayer)
                            {
                                return;
                            }
                            while (Information.IsNothing(this.BlockEnv))
                            {
                                base.ChunkOutOfStackRange();
                                this.BlockEnv = Ground.GetBlockEnvironment(this.Position - Ground.BlockSizeHalfYonlyV3, true);
                            }
                        }
                    }
                    this.CurrentBlock = this.BlockEnv.CurrentBlock;
                    this.CurrentChunk = this.CurrentBlock.Chunk;
                    this.DualFacingDirectionsFB = this.DualFacingDirections[0];
                    this.DualFacingDirectionsLR = this.DualFacingDirections[1];
                    bool flag7 = !this.BlockEnv.LegsBlock.IsAir;
                    if (flag7)
                    {
                        bool flag8 = this.TrappedCount > 10;
                        if (flag8)
                        {
                            this.Position.Y = this.Position.Y + 100f;
                            this.BodyParts[3].Hurten(20f);
                            this.TrappedCount = 0;
                            this.Update();
                            return;
                        }
                        checked
                        {
                            this.TrappedCount++;
                        }
                    }
                    else
                    {
                        this.TrappedCount = 0;
                    }
                    bool flag9 = !this.CurrentBlock.RealBlock.IsAir;
                    if (flag9)
                    {
                        this.Position.Y = (float)(checked((this.CurrentBlock.Index * 50).Y + 50));
                        bool flag10 = this.Velocity.Y < -10f;
                        if (flag10)
                        {
                            this.BodyParts[4].Hurten(this.Velocity.Y * -2f);
                            this.BodyParts[0].Hurten(this.Velocity.Y * -2f);
                            checked
                            {
                                this.Health -= (int)Math.Round((double)(unchecked(this.Velocity.Y * -2f)));
                            }
                        }
                        this.FallingSpeed = 0f;
                    }
                    else
                    {
                        this.FallingSpeed += this.Weight;
                        this.Velocity.Y = this.Velocity.Y - this.FallingSpeed;
                    }
                    bool flag11 = this.Velocity.Y < -10f;
                    if (flag11)
                    {
                        bool flag12 = Ground.GetIsAirDistanceInTheDirection(this.Position, Vector3.Down, -2, false) > 20;
                        if (flag12)
                        {
                            this.FallingSpeed = 0f;
                            this.Velocity.Y = 0f;
                            this.BlockEnv = Ground.GetBlockEnvironment(this.Position - Ground.BlockSizeYonlyV3, false);
                            this.CurrentBlock = this.BlockEnv.CurrentBlock;
                            this.DualFacingDirectionsFB = this.DualFacingDirections[0];
                            this.DualFacingDirectionsLR = this.DualFacingDirections[1];
                            this.DualFacingDirectionsFB = this.DualFacingDirections[0];
                            this.DualFacingDirectionsLR = this.DualFacingDirections[1];
                            this.CurrentChunk = this.CurrentBlock.Chunk;
                            this.Position.Y = (float)(checked((this.CurrentBlock.Index * 50).Y + 50));
                            this.OnGround = true;
                        }
                    }
                    bool isPlayer2 = this.eType.IsPlayer;
                    if (isPlayer2)
                    {
                        IntVector3 RealChunkIndex = Ground.ChunkIndexOfPosition(this.Position - Ground.BlockSizeYonlyV3);
                        bool flag13 = this.CurrentChunk.Index.X != RealChunkIndex.X || this.CurrentChunk.Index.Z != RealChunkIndex.Z;
                        if (flag13)
                        {
                            base.CheckOutOfStackRange();
                        }
                    }
                    bool flag14 = !this.CurrentBlock.RealBlock.IsAir;
                    if (flag14)
                    {
                        this.OnGround = true;
                    }
                    bool movedFB = this.MovedFB;
                    if (movedFB)
                    {
                        this.ResumeWalking();
                    }
                    else
                    {
                        this.PuaseWalking();
                    }
                    this.MovedFB = false;
                    bool flag15 = !this.NoAI;
                    if (flag15)
                    {
                        bool flag16 = this.DelUpdateAI != null;
                        if (flag16)
                        {
                            this.DelUpdateAI(this);
                        }
                    }
                    bool neededBodyRotationChanged = this.NeededBodyRotationChanged;
                    if (neededBodyRotationChanged)
                    {
                        bool flag17 = this.ModelRotation.Forward != this.NeededBodyRotation.Forward;
                        if (flag17)
                        {
                            bool flag18 = Vector3.Distance(this.NeededBodyRotation.Forward, this.ModelRotation.Forward) > this.GetingTargetBodyRotationLast;
                            if (flag18)
                            {
                                bool getingTargetBodyRotationLastDir = this.GetingTargetBodyRotationLastDir;
                                if (getingTargetBodyRotationLastDir)
                                {
                                    this.GetingTargetBodyRotationLastDir = false;
                                }
                                else
                                {
                                    this.GetingTargetBodyRotationLastDir = true;
                                }
                            }
                            this.GetingTargetBodyRotationLast = Vector3.Distance(this.ModelRotation.Forward, this.NeededBodyRotation.Forward);
                            bool flag19 = !this.GetingTargetBodyRotationLastDir;
                            if (flag19)
                            {
                                this.RotationVelocity.Y = (float)((double)this.RotationVelocity.Y + (double)Vector3.Distance(this.NeededBodyRotation.Forward, this.ModelRotation.Forward) * 0.2);
                            }
                            else
                            {
                                this.RotationVelocity.Y = (float)((double)this.RotationVelocity.Y - (double)Vector3.Distance(this.NeededBodyRotation.Forward, this.ModelRotation.Forward) * 0.2);
                            }
                            bool flag20 = (double)Vector3.Distance(this.NeededBodyRotation.Forward, this.ModelRotation.Forward) < 0.001;
                            if (flag20)
                            {
                                this.NeededBodyRotationChanged = false;
                            }
                        }
                    }
                    base.RotationY = MathHelper.WrapAngle(this.RotationVelocity.Y);
                    base.RotationX = MathHelper.WrapAngle(this.RotationVelocity.X);
                    base.RotationZ = MathHelper.WrapAngle(this.RotationVelocity.Z);
                    this.ModelRotation *= Matrix.Identity;
                    this.HeadRotation *= Matrix.Identity;
                    bool flag21 = base.RotationY != 0f;
                    if (flag21)
                    {
                        this.ModelRotation *= Matrix.CreateFromAxisAngle(Vector3.Up, base.RotationY);
                    }
                    bool flag22 = base.RotationX != 0f;
                    if (flag22)
                    {
                        this.ModelRotation *= Matrix.CreateFromAxisAngle(this.ModelRotation.Right, base.RotationX);
                    }
                    bool flag23 = base.RotationZ != 0f;
                    if (flag23)
                    {
                        this.ModelRotation *= Matrix.CreateFromAxisAngle(this.ModelRotation.Forward, base.RotationZ);
                    }
                    this.ModelRotationY *= Matrix.CreateRotationY(base.RotationY);
                    Matrix RotationYCHange = Matrix.CreateFromAxisAngle(this.ModelRotation.Up, base.RotationY);
                    this.BodyParts[5].Rotation = this.HeadRotation;
                    this.BodyParts[3].Rotation = this.ModelRotation;
                    this.BodyParts[1].RotateAsChild(this.ModelRotation.Up, base.RotationY);
                    this.BodyParts[6].RotateAsChild(this.ModelRotation.Up, base.RotationY);
                    this.BodyParts[2].RotateAsChild(RotationYCHange);
                    this.BodyParts[7].RotateAsChild(RotationYCHange);
                    this.BodyParts[5].OriginalRotation = this.HeadRotation * this.BodyParts[5].DefualtRotation;
                    this.BodyParts[3].OriginalRotation = this.ModelRotation * this.BodyParts[3].DefualtRotation;
                    this.BodyParts[7].OriginalRotation = this.ModelRotation * this.BodyParts[7].DefualtRotation;
                    this.BodyParts[2].OriginalRotation = this.ModelRotation * this.BodyParts[2].DefualtRotation;
                    this.BodyParts[0].OriginalRotation = this.ModelRotation * this.BodyParts[0].DefualtRotation;
                    this.BodyParts[4].OriginalRotation = this.ModelRotation * this.BodyParts[4].DefualtRotation;
                    bool flag24 = !Information.IsNothing(this.CTool);
                    if (flag24)
                    {
                        this.CTool.Rotate(RotationYCHange);
                        this.CTool.Update();
                        this.CTool.OriginalRotation = this.CTool.DefualtRotation * this.ModelRotation;
                    }
                    this.FacingDirection = this.HeadRotation.Forward;
                    this.FacingDirection.Normalize();
                    this.CollitionHierarchy.UpdateAllSpheres(this.Position);
                    float MeWidthSq = this.eType.Width * this.eType.Width * 2f;
                    bool IsAttacking = this.CTool != null && this.CTool.Attacking;


                    foreach (Entity e in Ground.CStack.eList)
                    {
                        bool flag25 = e != this;
                        if (flag25)
                        {
                            bool flag26 = IsAttacking;
                            if (flag26)
                            {
                                eCollitionNode CN = e.CollitionHierarchy.GetCollided(this.CTool.R, this.CTool.Length);
                                bool flag27 = !Information.IsNothing(CN);
                                if (flag27)
                                {
                                    Vector3 Dir = e.Position - this.Position;
                                    Dir.Y = 0f;
                                    Dir.Normalize();
                                    this.CTool.RewardsToVictim.Reward(e, CN.RRewardMultiplier);
                                    CN.eP.Hurten((float)(checked(-1 * this.CTool.RewardsToVictim.Health)));
                                    Controls.Go(e, Dir, 15f);
                                    e.ShotHit(this);
                                }
                            }
                            float DistanceSquaredToE = Vector3.DistanceSquared(e.Position, this.Position);
                            bool flag28 = DistanceSquaredToE < MeWidthSq;
                            if (flag28)
                            {
                                Vector3 Dir2 = (this.Position - e.Position) * Physics.YZero;
                                Controls.Go(this, Dir2, 0.1f);
                            }
                        }
                    }

                    base.RotationY = 0f;
                    base.RotationX = 0f;
                    base.RotationZ = 0f;
                    this.RotationVelocity.Y = this.RotationVelocity.Y * this.RotationVelocityReducingFactor.Y;
                    this.RotationVelocity.X = this.RotationVelocity.X * this.RotationVelocityReducingFactor.X;
                    this.RotationVelocity.Z = this.RotationVelocity.Z * this.RotationVelocityReducingFactor.Z;
                    bool flag29 = this.Health < 1;
                    if (flag29)
                    {
                        this.Kill();
                    }
                }
            }
            else
            {
                base.RotationY = MathHelper.WrapAngle(this.RotationVelocity.Y);
                base.RotationX = MathHelper.WrapAngle(this.RotationVelocity.X);
                base.RotationZ = MathHelper.WrapAngle(this.RotationVelocity.Z);
                bool flag30 = base.RotationX != 0f;
                if (flag30)
                {
                    this.ModelRotation *= Matrix.CreateFromAxisAngle(this.ModelRotation.Right, base.RotationX);
                    Matrix RotationXCHange = Matrix.CreateFromAxisAngle(this.ModelRotation.Right, base.RotationX);
                    this.BodyParts[5].Rotation = this.ModelRotation;
                    this.BodyParts[5].RelativePosition = Vector3.Transform(this.BodyParts[5].RelativePosition, RotationXCHange);
                    this.BodyParts[3].Rotation = this.ModelRotation;
                    this.BodyParts[3].RelativePosition = Vector3.Transform(this.BodyParts[3].RelativePosition, RotationXCHange);
                    this.BodyParts[1].Rotation = this.ModelRotation;
                    this.BodyParts[1].RelativePosition = Vector3.Transform(this.BodyParts[1].RelativePosition, RotationXCHange);
                    this.BodyParts[2].Rotation = this.ModelRotation;
                    this.BodyParts[2].RelativePosition = Vector3.Transform(this.BodyParts[2].RelativePosition, RotationXCHange);
                    this.BodyParts[6].Rotation = this.ModelRotation;
                    this.BodyParts[6].RelativePosition = Vector3.Transform(this.BodyParts[6].RelativePosition, RotationXCHange);
                    this.BodyParts[7].Rotation = this.ModelRotation;
                    this.BodyParts[7].RelativePosition = Vector3.Transform(this.BodyParts[7].RelativePosition, RotationXCHange);
                    this.BodyParts[0].Rotation = this.ModelRotation;
                    this.BodyParts[0].RelativePosition = Vector3.Transform(this.BodyParts[0].RelativePosition, RotationXCHange);
                    this.BodyParts[4].Rotation = this.ModelRotation;
                    this.BodyParts[4].RelativePosition = Vector3.Transform(this.BodyParts[4].RelativePosition, RotationXCHange);
                    bool flag31 = this.CTool != null;
                    if (flag31)
                    {
                        this.CTool.Rotation = this.ModelRotation;
                        this.CTool.RelativePosition = Vector3.Transform(this.CTool.RelativePosition, RotationXCHange);
                    }
                    base.RotationY = 0f;
                    base.RotationX = 0f;
                    base.RotationZ = 0f;
                    this.RotationVelocity.Y = this.RotationVelocity.Y * 0.95f;
                    this.RotationVelocity.X = this.RotationVelocity.X * 0.95f;
                    this.RotationVelocity.Z = this.RotationVelocity.Z * 0.95f;
                }
            }
        }

        // Token: 0x0600013D RID: 317 RVA: 0x00012388 File Offset: 0x00010588
        public override void Draw()
        {
            bool flag = !this.eType.IsPlayer;
            checked
            {
                if (flag)
                {
                    Vector3 AmbColor = new Vector3(0f);
                    int i = 0;
                    do
                    {
                        ModelMesh mesh = this.eType.Model.Meshes[i];
                        Matrix World = Matrix.Multiply(Matrix.Multiply(this.eType.Transforms, this.BodyParts[i].Rotation), Matrix.CreateTranslation(this.BodyParts[i].RelativePosition + this.Position));
                        AmbColor.X = (float)((double)this.BodyParts[i].Hurt / 25.0);
                        this.BodyParts[i].Hurten(-1f);


                        foreach (Effect effect3 in mesh.Effects)
                        {
                            BasicEffect effect = (BasicEffect)effect3;
                            effect.AmbientLightColor = AmbColor;
                            effect.Projection = Main.projectionMatrix;
                            effect.View = Main.viewMatrix;
                            effect.World = World;
                        }


                        mesh.Draw();
                        i++;
                    }
                    while (i <= 7);
                    bool flag2 = !Information.IsNothing(this.CTool);
                    if (flag2)
                    {


                        foreach (ModelMesh mesh2 in this.CTool.Model.Meshes)
                        {
                            Matrix World2 = this.CTool.Transforms[mesh2.ParentBone.Index] * this.CTool.Rotation * Matrix.CreateTranslation(this.CTool.RelativePosition + this.Position);


                            foreach (Effect effect4 in mesh2.Effects)
                            {
                                BasicEffect effect2 = (BasicEffect)effect4;
                                effect2.Projection = Main.projectionMatrix;
                                effect2.View = Main.viewMatrix;
                                effect2.World = World2;
                            }


                            mesh2.Draw();
                        }


                    }
                }
            }
        }

        // Token: 0x0600013E RID: 318 RVA: 0x00012610 File Offset: 0x00010810
        public override void Load(EntityType eT)
        {
            this.eType = eT;
            this.HeadRotation = Matrix.Identity;
            this.NeededBodyRotation = Matrix.Identity;
            this.NeededBodyRotationChanged = true;
            this.MovedFB = false;
            this.WalkingFw = false;
            this.GetingTargetBodyRotationLast = 0f;
            this.GetingTargetBodyRotationLastDir = false;
            this.DelUpdateAI = eT.UpdateAI;
            this.DelTargetScan = eT.DelTargetScan;
            this.BodyParts = new List<ePart>
            {
                new ePart("RightKnee", Matrix.Identity, new Vector3(11f, 52f, -5.7f) * 0.825f, this, 0),
                new ePart("LeftHand", Matrix.Identity, new Vector3(-22f, 139f, 4f) * 0.825f, this, 1),
                new ePart("LeftLeg", Matrix.Identity, new Vector3(-9f, 98f, -5.7f) * 0.825f, this, 2),
                new ePart("Body", Matrix.Identity, new Vector3(0f, 95f, 0f) * 0.825f, this, 3),
                new ePart("LeftKnee", Matrix.Identity, new Vector3(-11f, 52f, -5.7f) * 0.825f, this, 4),
                new ePart("Head", Matrix.Identity, new Vector3(0f, 151f, 0.4f) * 0.825f, this, 5),
                new ePart("RightHand", Matrix.Identity, new Vector3(22f, 139f, 4f) * 0.825f, this, 6),
                new ePart("RightLeg", Matrix.Identity, new Vector3(10f, 98f, -5.7f) * 0.825f, this, 7)
            };
            this.BodyParts[0].eAChildRotationGradient = 2f;
            this.BodyParts[4].eAChildRotationGradient = 2f;
            this.BodyParts[7].Children.Add(this.BodyParts[0]);
            this.BodyParts[2].Children.Add(this.BodyParts[4]);
            this.BodyParts[7].Length = 35f;
            this.BodyParts[2].Length = 35f;
            this.BodyParts[7].ChildDirection = "D"[0];
            this.BodyParts[2].ChildDirection = "D"[0];
            this.BodyParts[6].Length = 50f;
            this.BodyParts[1].Length = 50f;
            this.BodyParts[6].ChildDirection = "D"[0];
            this.BodyParts[1].ChildDirection = "D"[0];
            this.CollitionHierarchy = eCollitionHierarchy.CreateNewHumanHierarchy(this);
            bool isPlayer = this.eType.IsPlayer;
            if (isPlayer)
            {
                ((Player)this).LoadPlayer();
            }
            this.StartWalking();
        }

        // Token: 0x0600013F RID: 319 RVA: 0x000129A4 File Offset: 0x00010BA4
        public void StartWalking()
        {
            this.WalkingAnimation = new ePAnimationChain();
            this.FakeWA = new ePAnimation();
            this.WalkingAnimationL = new ePAnimation();
            this.WalkingAnimationR = new ePAnimation();
            this.WalkingAnimationHL = new ePAnimation();
            this.WalkingAnimationHR = new ePAnimation();
            ePart FakeBodyPart = new ePart(this);
            this.FakeWA = new ePAnimation(FakeBodyPart, default, new float[]
            {
                -0.4f,
                0.8f
            }, true, new float[]
            {
                20f,
                20f
            }, new Axis('L', false, FakeBodyPart))
            {
                IsFakeTimer = true
            };
            this.WalkingAnimationL = new ePAnimation(this.BodyParts[2], default, new float[]
            {
                -0.4f,
                0.8f
            }, true, new float[]
            {
                20f,
                20f
            }, new Axis('L', false, this.BodyParts[2]), 2f, true);
            this.WalkingAnimationR = new ePAnimation(this.BodyParts[7], default, new float[]
            {
                -0.8f,
                0.4f
            }, true, new float[]
            {
                20f,
                20f
            }, new Axis('R', false, this.BodyParts[7]), 2f, true);
            this.FakeWA.Parellel_eAs.Add(this.WalkingAnimationR);
            this.FakeWA.Parellel_eAs.Add(this.WalkingAnimationL);
            this.WalkingAnimationHL = new ePAnimation(this.BodyParts[1], default, new float[]
            {
                0.6f,
                -0.6f
            }, true, new float[]
            {
                20f,
                20f
            }, new Axis('L', false, this.BodyParts[1]), 0f, true);
            this.WalkingAnimationHR = new ePAnimation(this.BodyParts[6], default, new float[]
            {
                0.6f,
                -0.6f
            }, true, new float[]
            {
                20f,
                20f
            }, new Axis('R', false, this.BodyParts[6]), 0f, true);
            this.FakeWA.Parellel_eAs.Add(this.WalkingAnimationHL);
            this.FakeWA.Parellel_eAs.Add(this.WalkingAnimationHR);
            this.WalkingAnimation = new ePAnimationChain(new List<ePAnimation>
            {
                this.FakeWA
            }, true);
            this.WalkingAnimation.Start();
            this.WalkingAnimation.Pause();
            ePAnimation eALLeg = new ePAnimation(this.BodyParts[2], default, new float[]
            {
                this.WalkingAnimationL.CAngle * -1f
            }, false, new float[]
            {
                15f
            }, new Axis('L', false, this.BodyParts[2]));
            ePAnimation eARLeg2 = new ePAnimation(this.BodyParts[7], default, new float[]
            {
                this.WalkingAnimationR.CAngle * -1f
            }, false, new float[]
            {
                15f
            }, new Axis('R', false, this.BodyParts[7]));
            eARLeg2.Parellel_eAs.Add(eALLeg);
            this.StandStraitAnimations = new ePAnimationChain(new List<ePAnimation>
            {
                eARLeg2
            }, false)
            {
                Name = "Begining game1",
                Done = true
            };
        }

        // Token: 0x06000140 RID: 320 RVA: 0x00012D64 File Offset: 0x00010F64
        public void ResumeWalking()
        {
            bool flag = !this.WalkingFw;
            if (flag)
            {
                bool done = this.StandStraitAnimations.Done;
                if (done)
                {
                    this.WalkingAnimation.RResume();
                    this.WalkingFw = true;
                }
                else
                {
                    this.WalkingFw = false;
                }
            }
        }

        // Token: 0x06000141 RID: 321 RVA: 0x00012DB0 File Offset: 0x00010FB0
        public void PuaseWalking()
        {
            bool walkingFw = this.WalkingFw;
            if (walkingFw)
            {
                bool done = this.StandStraitAnimations.Done;
                if (done)
                {
                    this.WalkingAnimation.Pause();
                    this.WalkingFw = false;
                    this.StandStrait();
                }
            }
        }

        // Token: 0x06000142 RID: 322 RVA: 0x00012DF8 File Offset: 0x00010FF8
        public void StandStrait()
        {
            this.StandStraitAnimations = null;
            ePart FakeBP = new ePart(this);
            ePAnimation eAFBP = new ePAnimation(FakeBP, default, new float[]
            {
                this.WalkingAnimationL.CAngle * -1f
            }, false, new float[]
            {
                11f
            }, new Axis('L', false, FakeBP));
            ePAnimation eALLeg = new ePAnimation(this.BodyParts[2], default, new float[]
            {
                this.WalkingAnimationL.CAngle * -1f
            }, false, new float[]
            {
                11f
            }, new Axis('L', false, this.BodyParts[2]));
            ePAnimation eARLeg = new ePAnimation(this.BodyParts[7], default, new float[]
            {
                this.WalkingAnimationR.CAngle * -1f
            }, false, new float[]
            {
                11f
            }, new Axis('R', false, this.BodyParts[7]));
            eAFBP.Parellel_eAs.Add(eALLeg);
            eAFBP.Parellel_eAs.Add(eARLeg);
            this.StandStraitAnimations = new ePAnimationChain(new List<ePAnimation>
            {
                eAFBP
            }, false)
            {
                Done = false,
                Reset_eAC_After_eA = this.WalkingAnimation
            };
            bool flag = !this.BodyParts[1].LockedeA;
            if (flag)
            {
                ePAnimation eALHand = new ePAnimation(this.BodyParts[1], default, new float[]
                {
                    this.WalkingAnimationHL.CAngle * -1f
                }, false, new float[]
                {
                    11f
                }, new Axis('L', false, this.BodyParts[1]));
                eAFBP.Parellel_eAs.Add(eALHand);
                this.StandStraitAnimations.Revert_After_eA.Add(this.BodyParts[1]);
            }
            bool flag2 = !this.BodyParts[6].LockedeA;
            if (flag2)
            {
                ePAnimation eARHand = new ePAnimation(this.BodyParts[6], default, new float[]
                {
                    this.WalkingAnimationHR.CAngle * -1f
                }, false, new float[]
                {
                    11f
                }, new Axis('R', false, this.BodyParts[6]));
                eAFBP.Parellel_eAs.Add(eARHand);
                this.StandStraitAnimations.Revert_After_eA.Add(this.BodyParts[6]);
            }
            this.StandStraitAnimations.Revert_After_eA.AddRange(new ePart[]
            {
                this.BodyParts[2],
                this.BodyParts[7],
                this.BodyParts[4],
                this.BodyParts[0]
            });
            this.StandStraitAnimations.Start();
            this.WalkingFw = false;
        }

        // Token: 0x06000143 RID: 323 RVA: 0x00013100 File Offset: 0x00011300
        public void StandStraitInstantly()
        {
            this.StandStraitAnimations.Reset();
            this.StandStraitAnimations.Done = true;
            this.WalkingFw = false;
            this.WalkingAnimationR.Reset();
            this.BodyParts[7].Revert();
            this.WalkingAnimationL.Reset();
            this.BodyParts[2].Revert();
            this.BodyParts[0].Revert();
            this.BodyParts[4].Revert();
            this.WalkingAnimationHL.Reset();
            this.BodyParts[1].Revert();
            this.WalkingAnimationHR.Reset();
            this.BodyParts[6].Revert();
            this.ResumeWalking();
        }

        // Token: 0x06000144 RID: 324 RVA: 0x000131D0 File Offset: 0x000113D0
        public Human(EntityType eT) : base(eT)
        {
            this.GetingTargetBodyRotationLast = 0f;
            this.GetingTargetBodyRotationLastDir = false;
            this.DualFacingDirections = new Direction[2];
            this.FakeWA = new ePAnimation();
            checked
            {
                Entity.LICode += 1;
                this.ICode = (int)Entity.LICode;
                base.RotationX = 0f;
                base.RotationY = 0f;
                base.RotationZ = 0f;
                this.CurrentBlock = new DBlock(new Block());
            }
        }

        // Token: 0x06000145 RID: 325 RVA: 0x0001325C File Offset: 0x0001145C
        public Human()
        {
            this.GetingTargetBodyRotationLast = 0f;
            this.GetingTargetBodyRotationLastDir = false;
            this.DualFacingDirections = new Direction[2];
            this.FakeWA = new ePAnimation();
            this.ICode = (int)(checked(Entity.LICode + 1));
            Entity.LICode += 1;
            base.RotationX = 0f;
            base.RotationY = 0f;
            base.RotationZ = 0f;
            this.CurrentBlock = new DBlock(new Block());
        }

        // Token: 0x06000146 RID: 326 RVA: 0x000132EC File Offset: 0x000114EC
        public override void Kill()
        {
            this.Health = -1;
            this.IsDead = true;
            checked
            {
                for (int i = 0; i < ePAnimation.LstAnimation.Count; i++)
                {
                    bool flag = ePAnimation.LstAnimation[i].eP.Parent == this;
                    if (flag)
                    {
                        ePAnimation.LstAnimation[i].Reset();
                    }
                }
                this.WalkingAnimation.Reset();
                this.StandStraitAnimations.Reset();
                this.RotationVelocity.X = 0.08f;
                this.Position += new Vector3(0f, 10f, 0f);
                int num = this.Friendlies.Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    this.Friendlies[i].Friendlies.Remove(this);
                }
                int num2 = this.Enemies.Count - 1;
                for (int i = 0; i <= num2; i++)
                {
                    this.Enemies[i].Enemies.Remove(this);
                }
            }
        }

        /// <summary>
        /// Place: -1 = LHand, 1 = RHand, 0 = Inventory
        /// </summary>
        /// <param name="T"></param>
        /// <param name="Place"></param>
        // Token: 0x06000147 RID: 327 RVA: 0x00013410 File Offset: 0x00011610
        public override void GiveTool(Tool T, int Place)
        {
            T.Rotation = T.OriginalRotation;
            T.Parent = this;
            T.Charter = this;
            T.ChildRotationGradient = 0f;
            this.CTool = T;
            bool flag = Place == -1;
            if (flag)
            {
                T.Owner = this.BodyParts[1];
                T.RelativePositionFromParent = new Vector3(-0.15f, 0.13f, -0.25f);
                T.Side = -1;
            }
            bool flag2 = Place == 1;
            if (flag2)
            {
                T.Owner = this.BodyParts[6];
                T.RelativePositionFromParent = new Vector3(0.1f, 0.1f, -0.15f);
                T.Side = 1;
            }
            T.Owner.Children.Add(T);
        }

        // Token: 0x06000148 RID: 328 RVA: 0x000134E0 File Offset: 0x000116E0
        public static Human AddNewHuman(EntityType eT, string NName, Vector3 PPosition, Tool CCCTool, int CCCToolSide)
        {
            Human H = new Human(eT);
            H.Load(eT);
            H.Name = NName;
            H.eType.IsHuman = true;
            H.Position = PPosition;
            bool flag = CCCTool != null;
            if (flag)
            {
                H.GiveTool(CCCTool, CCCToolSide);
            }
            return H;
        }

        // Token: 0x06000149 RID: 329 RVA: 0x0001352E File Offset: 0x0001172E
        public void AddToTheCurrentStack()
        {
            Ground.CStack.eList.Add(this);
        }

        // Token: 0x04000162 RID: 354
        public float GetingTargetBodyRotationLast;

        // Token: 0x04000163 RID: 355
        public bool GetingTargetBodyRotationLastDir;

        // Token: 0x04000164 RID: 356
        public Direction[] DualFacingDirections;

        // Token: 0x04000165 RID: 357
        public const int iHead = 5;

        // Token: 0x04000166 RID: 358
        public const int iBody = 3;

        // Token: 0x04000167 RID: 359
        public const int iLHand = 1;

        // Token: 0x04000168 RID: 360
        public const int iRHand = 6;

        // Token: 0x04000169 RID: 361
        public const int iLLeg = 2;

        // Token: 0x0400016A RID: 362
        public const int iRLeg = 7;

        // Token: 0x0400016B RID: 363
        public const int iRKnee = 0;

        // Token: 0x0400016C RID: 364
        public const int iLKnee = 4;

        // Token: 0x0400016D RID: 365
        public ePAnimationChain WalkingAnimation;

        // Token: 0x0400016E RID: 366
        public ePAnimation FakeWA;

        // Token: 0x0400016F RID: 367
        public ePAnimation WalkingAnimationL;

        // Token: 0x04000170 RID: 368
        public ePAnimation WalkingAnimationR;

        // Token: 0x04000171 RID: 369
        public ePAnimation WalkingAnimationHL;

        // Token: 0x04000172 RID: 370
        public ePAnimation WalkingAnimationHR;

        // Token: 0x04000173 RID: 371
        public ePAnimationChain StandStraitAnimations;
    }
}
