using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine
{
    // Token: 0x0200002D RID: 45
    public class Player : Human
    {
        // Token: 0x060001C7 RID: 455 RVA: 0x00017A98 File Offset: 0x00015C98
        public void UpdateMan(KeyboardState controllerState, MouseState MouseState)
        {
            bool flag = !this.IsDead;
            if (flag)
            {
                this.FacingDirection = this.HeadRotation.Forward;


                foreach (Controls.Control C in this.ControlsList)
                {
                    Actions A = C.Action;
                    bool isKeyControl = C.IsKeyControl;
                    if (isKeyControl)
                    {
                        Keys K = C.Key;
                        bool flag2 = controllerState.IsKeyDown(K);
                        if (flag2)
                        {
                            Controls.Go(this, A);
                            this.Act(A);
                        }
                    }
                    else
                    {
                        switch (C.MouseControl)
                        {
                            case Controls.Control.MouseKeys.LeftClick:
                                {
                                    bool flag3 = MouseState.LeftButton == ButtonState.Pressed;
                                    if (flag3)
                                    {
                                        this.Act(A);
                                    }
                                    break;
                                }
                            case Controls.Control.MouseKeys.RightClick:
                                {
                                    bool flag4 = MouseState.RightButton == ButtonState.Pressed;
                                    if (flag4)
                                    {
                                        this.Act(A);
                                    }
                                    break;
                                }
                            case Controls.Control.MouseKeys.WheelUp:
                                {
                                    bool flag5 = MouseState.ScrollWheelValue > Main.MouseWheelValue;
                                    if (flag5)
                                    {
                                        this.Act(A);
                                        Main.MouseWheelValue = MouseState.ScrollWheelValue;
                                    }
                                    break;
                                }
                            case Controls.Control.MouseKeys.WheelDown:
                                {
                                    bool flag6 = MouseState.ScrollWheelValue < Main.MouseWheelValue;
                                    if (flag6)
                                    {
                                        this.Act(A);
                                        Main.MouseWheelValue = MouseState.ScrollWheelValue;
                                    }
                                    break;
                                }
                            case Controls.Control.MouseKeys.WheelPress:
                                {
                                    bool flag7 = MouseState.MiddleButton == ButtonState.Pressed;
                                    if (flag7)
                                    {
                                        this.Act(A);
                                    }
                                    break;
                                }
                        }
                    }
                }

            }
            bool movedFB = this.MovedFB;
            if (movedFB)
            {
                this.ManMovedOnce = true;
            }
            else
            {
                bool manMovedOnce = this.ManMovedOnce;
                if (manMovedOnce)
                {
                }
            }
        }

        // Token: 0x060001C8 RID: 456 RVA: 0x00017C6C File Offset: 0x00015E6C
        public void Act(Actions A)
        {
            switch (A)
            {
                case Actions.Attack:
                    this.ActAttack();
                    break;
                case Actions.C1:
                    this.ActC1();
                    break;
                case Actions.C2:
                    this.ActC2();
                    break;
                case Actions.C3:
                    this.ActC3();
                    break;
                case Actions.PlaceBlock:
                    this.ActPlaceBlock();
                    break;
                case Actions.BreakBlock:
                    this.ActBreakBlock();
                    break;
                case Actions.ChangeBlock:
                    this.ActChangeBlockType();
                    break;
                case Actions.Interact:
                    this.ActInteract();
                    break;
            }
        }

        // Token: 0x060001C9 RID: 457 RVA: 0x00017D08 File Offset: 0x00015F08
        public void ActInteract()
        {
            bool flag = this.Money >= 100;
            checked
            {
                if (flag)
                {
                    Entity FacingE = base.FacingEntity(1000f);
                    bool flag2 = !Information.IsNothing(FacingE);
                    if (flag2)
                    {
                        bool flag3 = FacingE.ChekRelation(this) != EntityRelationMode.Friends;
                        if (flag3)
                        {
                            this.Money -= 100;
                            FacingE.Hire(this);
                        }
                    }
                }
            }
        }

        // Token: 0x060001CA RID: 458 RVA: 0x00017D70 File Offset: 0x00015F70
        public void ActAttack()
        {
            bool flag = this.LastActionTime + 500.0 < (double)Main.NowGameTime;
            if (flag)
            {
                bool flag2 = !this.CTool.Owner.LockedeA;
                if (flag2)
                {


                    foreach (ePAnimation eA in this.CTool.Owner.CurrentAnimations)
                    {
                        eA.Reset();
                    }

                    this.CTool.Owner.Revert();
                    this.CTool.Use(PhysicsFuncs.RandomOf<Tool.Action>(new Tool.Action[]
                    {
                        Tool.Action.PrimaryAttack1,
                        Tool.Action.PrimaryAttack2,
                        Tool.Action.ShortAttack
                    }));
                    this.LastActionTime = (double)Main.NowGameTime;
                }
            }
        }

        // Token: 0x060001CB RID: 459 RVA: 0x00017E50 File Offset: 0x00016050
        public void ActC1()
        {


            foreach (Entity e in Ground.CStack.eList)
            {
                bool flag = !e.IsDead;
                if (flag)
                {
                    bool flag2 = !(e == this) && e.ChekRelation(this) != EntityRelationMode.Friends;
                    if (flag2)
                    {


                        foreach (Entity FE in this.Friendlies)
                        {
                            FE.LockTarget(e, AITargetMode.FollowAndKill, 600);
                        }


                    }
                }
            }


        }

        // Token: 0x060001CC RID: 460 RVA: 0x00017F2C File Offset: 0x0001612C
        public void ActC2()
        {
            int Fn = 0;
            bool flag = this.Friendlies.Count > 0;
            checked
            {
                if (flag)
                {

                    foreach (Entity e in Ground.CStack.eList)
                    {
                        bool flag2 = !e.IsDead;
                        if (flag2)
                        {
                            bool flag3 = !(e == this) && e.ChekRelation(this) != EntityRelationMode.Friends;
                            if (flag3)
                            {
                                Entity FE = this.Friendlies[Fn];
                                FE.LockTarget(e, AITargetMode.FollowAndKill, 600);
                                bool flag4 = Fn < this.Friendlies.Count - 1;
                                if (!flag4)
                                {
                                    break;
                                }
                                Fn++;
                            }
                        }
                    }

                }
            }
        }

        // Token: 0x060001CD RID: 461 RVA: 0x00018010 File Offset: 0x00016210
        public void ActC3()
        {
            Entity FacingE = base.FacingEntity(500f);
            bool flag = !Information.IsNothing(FacingE);
            if (flag)
            {


                foreach (Entity FE in this.Friendlies)
                {
                    FE.LockTarget(FacingE, AITargetMode.FollowAndKill, 600);
                }


            }
        }

        // Token: 0x060001CE RID: 462 RVA: 0x0001808C File Offset: 0x0001628C
        public void ActArrowThrow()
        {
            bool flag = checked(this.ArrowLastTime + 100L) < Main.NowGameTime;
            if (flag)
            {
                Arrow Ar = new Arrow(EntityTypes.Arrow1)
                {
                    Position = this.Position + this.Height
                };
                Ar.ModelRotation = PhysicsFuncs.CreateLookAtPosition(Ar.Position, Ar.Position + this.FacingDirection, this.HeadRotation.Up);
                Ar.Velocity = this.HeadRotation.Forward * 5f;
                Ar.CollitionHierarchy = eCollitionHierarchy.CreateNewHierarchyForArrow(Ar);
                Ar.AddToTheCurrentStack();
                this.ArrowLastTime = Main.NowGameTime;
            }
        }

        // Token: 0x060001CF RID: 463 RVA: 0x0001813C File Offset: 0x0001633C
        public void ActPlaceBlock()
        {
            bool flag = checked(this.PlaceBlockLastTime + 250L) < Main.NowGameTime;
            if (flag)
            {
                DBlock LookingBlock = Ground.GetBeforeBlockInTheDirection(this.Position + this.Height, this.HeadRotation.Forward, 1000, false);
                bool flag2 = !Information.IsNothing(LookingBlock);
                if (flag2)
                {
                    Ground.SetBlock(ref LookingBlock, this.SelectedBlockType);
                    this.PlaceBlockLastTime = Main.NowGameTime;
                }
            }
        }

        // Token: 0x060001D0 RID: 464 RVA: 0x000181B4 File Offset: 0x000163B4
        public void ActBreakBlock()
        {
            bool flag = checked(this.PlaceBlockLastTime + 250L) < Main.NowGameTime;
            if (flag)
            {
                DBlock LookingBlock = Ground.GetBlockInTheDirection(this.Position + this.Height, this.HeadRotation.Forward, 1000, false);
                bool flag2 = !Information.IsNothing(LookingBlock);
                if (flag2)
                {
                    bool flag3 = LookingBlock.Chunk.Index.Y > 0;
                    if (flag3)
                    {
                        Ground.BreakBlock(ref LookingBlock);
                        this.PlaceBlockLastTime = Main.NowGameTime;
                    }
                }
            }
        }

        // Token: 0x060001D1 RID: 465 RVA: 0x00018240 File Offset: 0x00016440
        public void ActChangeBlockType()
        {
            checked
            {
                bool flag = this.PlaceBlockLastTime + 250L < Main.NowGameTime;
                if (flag)
                {
                    int SBTID = (int)(this.SelectedBlockType.ID + 1);
                    bool flag2 = SBTID > 10;
                    if (flag2)
                    {
                        this.SelectedBlockType = BlockType.PlaceHolder;
                    }
                    else
                    {
                        this.SelectedBlockType = BlockType.BTList[SBTID];
                    }
                    this.PlaceBlockLastTime = Main.NowGameTime;
                    Board.SelectedToolAndBlock = "Tool:" + this.CTool.Name + "; Block:" + this.SelectedBlockType.Name;
                }
            }
        }

        // Token: 0x060001D2 RID: 466 RVA: 0x000182D4 File Offset: 0x000164D4
        public void DrawMan()
        {
            ePart BPRHand = this.BodyParts[6];
            BPRHand.OriginalRotation = this.ModelRotationY * Matrix.CreateFromAxisAngle(this.ModelRotationY.Left, this.InsiderHandXRotation - 0.7853982f);
            BPRHand.Rotation *= Matrix.CreateFromAxisAngle(this.ModelRotation.Left, this.InsiderHandXCurrRotation);
            ePart BPLHand = this.BodyParts[1];
            BPLHand.OriginalRotation = this.ModelRotationY;
            this.InsiderHandXCurrRotation = 0f;
            bool flag = this.CamPosType == RacingCameraAngle.Back;
            checked
            {
                if (flag)
                {
                    int i = 0;

                    foreach (ModelMesh mesh in this.eType.Model.Meshes)
                    {
                        ePart BP = this.BodyParts[i];

                        foreach (Effect effect6 in mesh.Effects)
                        {
                            BasicEffect effect = (BasicEffect)effect6;
                            effect.AmbientLightColor = new Vector3((float)((double)BP.Hurt / 25.0), 0f, 0f);
                            effect.Projection = Main.projectionMatrix;
                            effect.View = Main.viewMatrix;
                            effect.World = Matrix.Multiply(Matrix.Multiply(this.eType.Transforms, this.BodyParts[i].Rotation), Matrix.CreateTranslation(this.BodyParts[i].RelativePosition + this.Position));
                        }

                        mesh.Draw();
                        i++;
                    }

                    bool flag2 = !Information.IsNothing(this.CTool);
                    if (flag2)
                    {
                        int b = 0;


                        foreach (ModelMesh mesh2 in this.CTool.Model.Meshes)
                        {


                            foreach (Effect effect7 in mesh2.Effects)
                            {
                                BasicEffect effect2 = (BasicEffect)effect7;
                                effect2.Projection = Main.projectionMatrix;
                                effect2.View = Main.viewMatrix;
                                Matrix MeshTransform = this.CTool.Rotation * Matrix.CreateTranslation(this.CTool.RelativePosition + this.Position);
                                effect2.World = this.CTool.Transforms[mesh2.ParentBone.Index] * MeshTransform;
                            }
                            mesh2.Draw();
                            b++;
                        }
                    }
                }

                else
                {
                    bool flag3 = this.CamPosType == RacingCameraAngle.Inside;
                    if (flag3)
                    {
                        ModelMesh meshRHand = this.eType.Model.Meshes[6];


                        foreach (Effect effect8 in meshRHand.Effects)
                        {
                            BasicEffect effect3 = (BasicEffect)effect8;
                            effect3.AmbientLightColor = new Vector3((float)((double)BPRHand.Hurt / 25.0), 0f, 0f);
                            effect3.Projection = Main.projectionMatrix;
                            effect3.View = Main.viewMatrix;
                            effect3.World = this.eType.Transforms * BPRHand.Rotation * Matrix.CreateTranslation(BPRHand.RelativePosition + this.Position);
                        }


                        meshRHand.Draw();
                        ModelMesh meshLHand = this.eType.Model.Meshes[1];


                        foreach (Effect effect9 in meshLHand.Effects)
                        {
                            BasicEffect effect4 = (BasicEffect)effect9;
                            effect4.AmbientLightColor = new Vector3((float)((double)BPLHand.Hurt / 25.0), 0f, 0f);
                            effect4.Projection = Main.projectionMatrix;
                            effect4.View = Main.viewMatrix;
                            effect4.World = this.eType.Transforms * BPLHand.Rotation * Matrix.CreateTranslation(BPLHand.RelativePosition + this.Position);
                        }


                        meshLHand.Draw();
                        bool flag4 = !Information.IsNothing(this.CTool);
                        if (flag4)
                        {
                            int b2 = 0;


                            foreach (ModelMesh meshC in this.CTool.Model.Meshes)
                            {

                                foreach (Effect effect10 in meshC.Effects)
                                {
                                    BasicEffect effect5 = (BasicEffect)effect10;
                                    effect5.Projection = Main.projectionMatrix;
                                    effect5.View = Main.viewMatrix;
                                    Matrix MeshTransform2 = this.CTool.Rotation * Matrix.CreateTranslation(this.CTool.RelativePosition + this.Position);
                                    effect5.World = this.CTool.Transforms[meshC.ParentBone.Index] * MeshTransform2;
                                }


                                meshC.Draw();
                                b2++;
                            }


                        }
                    }
                }

                foreach (ePart BP2 in this.BodyParts)
                {
                    BP2.Hurten(-1f);
                }
            }
        }





        // Token: 0x060001D3 RID: 467 RVA: 0x00018A04 File Offset: 0x00016C04
        public void LoadPlayer()
        {
            this.BodyParts[6].Rotation *= Matrix.CreateFromAxisAngle(this.ModelRotation.Left, -0.7853982f);
            this.BodyParts[6].OriginalRotation *= Matrix.CreateFromAxisAngle(this.ModelRotation.Left, -0.7853982f);
        }

        // Token: 0x060001D4 RID: 468 RVA: 0x00018A84 File Offset: 0x00016C84
        public Player(EntityType eT) : base(eT)
        {
            this.CamPosType = RacingCameraAngle.Inside;
            this.ControlsList = new List<Controls.Control>();
            this.InsiderViewBodyPartes = new List<ePart>();
            this.InsiderHandXRotation = 0f;
            this.InsiderHandXCurrRotation = 0f;
            this.ManMovedOnce = false;
            this.Money = 500;
        }

        // Token: 0x060001D5 RID: 469 RVA: 0x00018AE0 File Offset: 0x00016CE0
        public Player()
        {
            this.CamPosType = RacingCameraAngle.Inside;
            this.ControlsList = new List<Controls.Control>();
            this.InsiderViewBodyPartes = new List<ePart>();
            this.InsiderHandXRotation = 0f;
            this.InsiderHandXCurrRotation = 0f;
            this.ManMovedOnce = false;
            this.Money = 500;
        }

        // Token: 0x040001DA RID: 474
        public RacingCameraAngle CamPosType;

        // Token: 0x040001DB RID: 475
        public List<Controls.Control> ControlsList;

        // Token: 0x040001DC RID: 476
        public List<ePart> InsiderViewBodyPartes;

        // Token: 0x040001DD RID: 477
        public float InsiderHandXRotation;

        // Token: 0x040001DE RID: 478
        public float InsiderHandXCurrRotation;

        // Token: 0x040001DF RID: 479
        public long ArrowLastTime;

        // Token: 0x040001E0 RID: 480
        public long PlaceBlockLastTime;

        // Token: 0x040001E1 RID: 481
        public BlockType SelectedBlockType;

        // Token: 0x040001E2 RID: 482
        public bool ManMovedOnce;

        // Token: 0x040001E3 RID: 483
        public int Money;
    }
}
