using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000036 RID: 54
    [Serializable]
    public class XEntity
    {
        // Token: 0x06000214 RID: 532 RVA: 0x0001CE98 File Offset: 0x0001B098
        public XEntity()
        {
            this.Health = 200;
            this.MaxHealth = 500;
            this.Strength = 1f;
            this.Weight = 0.3f;
            this.XP = 0;
            this.Position = Vector3.Zero;
            this.Accelaration = new Vector3(3f, 3f, 3.4f);
            this.RotationAccelaration = new Vector3(0.02f, 0.03f, 0.02f);
            this.ModelVelocity = default;
            this.MaxVelocity = new Vector3(0.5f, 0.5f, 2f);
            this.Jumping = false;
            this.MovedFB = false;
            this.WalkingFw = false;
            this.FacingDirection = new Vector3(0f, 0f, 1f);
            this.FallingSpeed = 0f;
            this.RotationVelocity = Vector3.Zero;
            this.IsDead = false;
            this.OnGround = false;
            this.HighestTatget = false;
            this.NoTarget = true;
            this.InRandomMovement = false;
        }

        // Token: 0x06000215 RID: 533 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
        public XEntity(Entity e)
        {
            this.Health = 200;
            this.MaxHealth = 500;
            this.Strength = 1f;
            this.Weight = 0.3f;
            this.XP = 0;
            this.Position = Vector3.Zero;
            this.Accelaration = new Vector3(3f, 3f, 3.4f);
            this.RotationAccelaration = new Vector3(0.02f, 0.03f, 0.02f);
            this.ModelVelocity = default;
            this.MaxVelocity = new Vector3(0.5f, 0.5f, 2f);
            this.Jumping = false;
            this.MovedFB = false;
            this.WalkingFw = false;
            this.FacingDirection = new Vector3(0f, 0f, 1f);
            this.FallingSpeed = 0f;
            this.RotationVelocity = Vector3.Zero;
            this.IsDead = false;
            this.OnGround = false;
            this.HighestTatget = false;
            this.NoTarget = true;
            this.InRandomMovement = false;
            this.TypeID = e.eType.ID;
            this.Name = e.Name;
            this.ICode = e.ICode;
            this.Health = e.Health;
            this.MaxHealth = e.MaxHealth;
            this.Strength = e.Strength;
            this.Weight = e.Weight;
            this.XP = e.XP;
            this.Position = e.Position;
            this.Accelaration = e.Accelaration;
            this.RotationAccelaration = e.RotationAccelaration;
            this.ModelVelocity = e.Velocity;
            this.MaxVelocity = e.MaxVelocity;
            this.Jumping = e.Jumping;
            this.MovedFB = e.MovedFB;
            this.WalkingFw = e.WalkingFw;
            this.FacingDirection = e.FacingDirection;
            this.FallingSpeed = e.FallingSpeed;
            this.RotationVelocity = e.RotationVelocity;
            this.IsDead = e.IsDead;
            checked
            {
                this.Tools = new int[e.Tools.Count + 1];
                int num = e.Tools.Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    this.Tools[i] = e.Tools[i].Index;
                }
                bool flag = e.CTool != null;
                if (flag)
                {
                    this.CTool = e.CTool.Index;
                }
                else
                {
                    this.CTool = -1;
                }
                this.TargetLockedAIs = new int[e.TargetLockedAIs.Count - 1 + 1];
                int num2 = e.TargetLockedAIs.Count - 1;
                for (int j = 0; j <= num2; j++)
                {
                    this.TargetLockedAIs[j] = e.TargetLockedAIs[j].ICode;
                }
                this.Enemies = new int[e.Enemies.Count - 1 + 1];
                int num3 = e.Enemies.Count - 1;
                for (int k = 0; k <= num3; k++)
                {
                    this.Enemies[k] = e.Enemies[k].ICode;
                }
                this.Friendlies = new int[e.Friendlies.Count - 1 + 1];
                int num4 = e.Friendlies.Count - 1;
                for (int l = 0; l <= num4; l++)
                {
                    this.Friendlies[l] = e.Friendlies[l].ICode;
                }
                this.OnGround = e.OnGround;
                this.HighestTatget = e.HighestTatget;
                bool flag2 = e.HighestTatgetE != null;
                if (flag2)
                {
                    this.HighestTatgetE = e.HighestTatgetE.ICode;
                }
                this.HighestTargetMode = e.HighestTargetMode;
                bool flag3 = e.Target != null;
                if (flag3)
                {
                    this.Target = e.Target.ICode;
                }
                this.TargetMode = e.TargetMode;
                this.NoTarget = e.NoTarget;
                this.InRandomMovement = e.InRandomMovement;
                this.InRandomMovementTime = e.InRandomMovementTime;
                this.CRandomMovement = e.CRandomMovement;
                this.CRandomMovementTime = e.CRandomMovementTime;
            }
        }

        // Token: 0x06000216 RID: 534 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
        public static XEntity NewXE(Entity e, Vector3 Position)
        {
            return new XEntity(e)
            {
                Position = Position
            };
        }

        // Token: 0x06000217 RID: 535 RVA: 0x0001D404 File Offset: 0x0001B604
        public Entity GetEntity()
        {
            EntityType eType = EntityTypes.Lst[(int)this.TypeID];
            Entity e = (Entity)eType.T.GetConstructor(new Type[]
            {
                typeof(EntityType)
            }).Invoke(new object[]
            {
                eType
            });
            e.Load(eType);
            Entity entity = e;
            entity.InRandomMovement = this.InRandomMovement;
            entity.InRandomMovementTime = this.InRandomMovementTime;
            entity.CRandomMovement = this.CRandomMovement;
            entity.CRandomMovementTime = this.CRandomMovementTime;
            this.SetEntitySettings(ref e);
            return e;
        }

        // Token: 0x06000218 RID: 536 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
        public void SetEntitySettings(ref Entity e)
        {
            Entity entity = e;
            entity.Name = this.Name;
            entity.ICode = this.ICode;
            entity.Health = this.Health;
            entity.MaxHealth = this.MaxHealth;
            entity.Strength = this.Strength;
            entity.Weight = this.Weight;
            entity.XP = this.XP;
            entity.Position = this.Position;
            entity.Accelaration = this.Accelaration;
            entity.RotationAccelaration = this.RotationAccelaration;
            entity.Velocity = this.ModelVelocity;
            entity.MaxVelocity = this.MaxVelocity;
            entity.Jumping = this.Jumping;
            entity.MovedFB = this.MovedFB;
            entity.WalkingFw = this.WalkingFw;
            entity.FacingDirection = this.FacingDirection;
            entity.FallingSpeed = this.FallingSpeed;
            entity.RotationVelocity = this.RotationVelocity;
            entity.IsDead = this.IsDead;
            entity.OnGround = this.OnGround;
            entity.HighestTatget = this.HighestTatget;
            entity.HighestTargetMode = this.HighestTargetMode;
            entity.TargetMode = this.TargetMode;
            entity.NoTarget = this.NoTarget;
            bool flag = this.CTool > -1;
            if (flag)
            {
                entity.GiveTool(Tool.Buy(this.CTool), 1);
            }
        }

        // Token: 0x06000219 RID: 537 RVA: 0x0001D5FC File Offset: 0x0001B7FC
        public void GenerateRelationships(ref Entity e, ref List<Entity> EList)
        {
            e.TargetLockedAIs = new List<Entity>();
            e.Enemies = new List<Entity>();
            e.Friendlies = new List<Entity>();


            foreach (Entity exE in EList)
            {
                foreach (int eIcode in this.TargetLockedAIs)
                {
                    bool flag = exE.ICode == eIcode;
                    if (flag)
                    {
                        e.TargetLockedAIs.Add(exE);
                    }
                }
                foreach (int eIcode2 in this.Enemies)
                {
                    bool flag2 = exE.ICode == eIcode2;
                    if (flag2)
                    {
                        e.Enemies.Add(exE);
                    }
                }
                foreach (int eIcode3 in this.Friendlies)
                {
                    bool flag3 = exE.ICode == eIcode3;
                    if (flag3)
                    {
                        e.Friendlies.Add(exE);
                    }
                }
                bool flag4 = this.HighestTatgetE == exE.ICode;
                if (flag4)
                {
                    e.HighestTatgetE = exE;
                }
                bool flag5 = this.Target == exE.ICode;
                if (flag5)
                {
                    e.Target = exE;
                }
            }



        }

        // Token: 0x0600021A RID: 538 RVA: 0x0001D790 File Offset: 0x0001B990
        public static void Save(string Path, List<XEntity> ELst)
        {
            XmlSerializer XWriter = new XmlSerializer(typeof(List<XEntity>));
            StreamWriter StreamWriter = new StreamWriter(Path);
            try
            {
                XWriter.Serialize(StreamWriter, ELst);
                StreamWriter.Close();
            }
            catch (Exception)
            {
                StreamWriter.Close();
                XEntity.Save(Path + "Backup", ELst);
                File.Copy(Path + "Backup", Path, true);
            }
            File.Copy(Path, Path + "Backup", true);
        }

        // Token: 0x0600021B RID: 539 RVA: 0x0001D83C File Offset: 0x0001BA3C
        public static List<XEntity> Load(string Path)
        {
            XmlSerializer XWriter = new XmlSerializer(typeof(List<XEntity>));
            StreamReader StreamReader = new StreamReader(Path);
            List<XEntity> Out;
            try
            {
                Out = (List<XEntity>)XWriter.Deserialize(StreamReader);
                StreamReader.Close();
            }
            catch (Exception)
            {
                Out = new List<XEntity>();
                StreamReader.Close();
                bool flag = File.Exists(Path + "Backup");
                if (flag)
                {
                    File.Copy(Path + "Backup", Path, true);
                    Out = XEntity.Load(Path);
                }
            }
            return Out;
        }

        // Token: 0x04000239 RID: 569
        public short TypeID;

        // Token: 0x0400023A RID: 570
        public string Name;

        // Token: 0x0400023B RID: 571
        public int ICode;

        // Token: 0x0400023C RID: 572
        public int Health;

        // Token: 0x0400023D RID: 573
        public int MaxHealth;

        // Token: 0x0400023E RID: 574
        public float Strength;

        // Token: 0x0400023F RID: 575
        public float Weight;

        // Token: 0x04000240 RID: 576
        public int XP;

        // Token: 0x04000241 RID: 577
        public Vector3 Position;

        // Token: 0x04000242 RID: 578
        public Vector3 Accelaration;

        // Token: 0x04000243 RID: 579
        public Vector3 RotationAccelaration;

        // Token: 0x04000244 RID: 580
        public Vector3 ModelVelocity;

        // Token: 0x04000245 RID: 581
        public Vector3 MaxVelocity;

        // Token: 0x04000246 RID: 582
        public bool Jumping;

        // Token: 0x04000247 RID: 583
        public bool MovedFB;

        // Token: 0x04000248 RID: 584
        public bool WalkingFw;

        // Token: 0x04000249 RID: 585
        public Vector3 FacingDirection;

        // Token: 0x0400024A RID: 586
        public float FallingSpeed;

        // Token: 0x0400024B RID: 587
        public Vector3 RotationVelocity;

        // Token: 0x0400024C RID: 588
        public bool IsDead;

        // Token: 0x0400024D RID: 589
        public int[] TargetLockedAIs;

        // Token: 0x0400024E RID: 590
        public int[] Enemies;

        // Token: 0x0400024F RID: 591
        public int[] Friendlies;

        // Token: 0x04000250 RID: 592
        public bool OnGround;

        // Token: 0x04000251 RID: 593
        public bool HighestTatget;

        // Token: 0x04000252 RID: 594
        public int HighestTatgetE;

        // Token: 0x04000253 RID: 595
        public AITargetMode HighestTargetMode;

        // Token: 0x04000254 RID: 596
        public int Target;

        // Token: 0x04000255 RID: 597
        public AITargetMode TargetMode;

        // Token: 0x04000256 RID: 598
        public bool NoTarget;

        // Token: 0x04000257 RID: 599
        public bool InRandomMovement;

        // Token: 0x04000258 RID: 600
        public int InRandomMovementTime;

        // Token: 0x04000259 RID: 601
        public Actions CRandomMovement;

        // Token: 0x0400025A RID: 602
        public int CRandomMovementTime;

        // Token: 0x0400025B RID: 603
        public int[] Tools;

        // Token: 0x0400025C RID: 604
        public int CTool;
    }
}
