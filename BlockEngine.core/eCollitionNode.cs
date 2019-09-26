using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using BlockEngine;

namespace BlockEngine
{
    // Token: 0x02000014 RID: 20
    public class eCollitionNode
    {
        // Token: 0x0600005F RID: 95 RVA: 0x00006A58 File Offset: 0x00004C58
        public eCollitionNode(Vector3 RealativePos, float R, ePart eP)
        {
            this.LowerNodes = new List<eCollitionNode>();
            this.RRewardMultiplier = new RewardSetMultiplier(1f, 1f, 1f);
            this.Type1 = eCollitionNode.Type.BoundingSphere;
            this.BS = new BoundingSphere(RealativePos, R);
            this.RealativePos = RealativePos;
            this.BSR = R;
            this.eP = eP;
        }

        // Token: 0x06000060 RID: 96 RVA: 0x00006ABC File Offset: 0x00004CBC
        public eCollitionNode(Vector3 Min, Vector3 Max, ePart eP)
        {
            this.LowerNodes = new List<eCollitionNode>();
            this.RRewardMultiplier = new RewardSetMultiplier(1f, 1f, 1f);
            this.Type1 = eCollitionNode.Type.BoundingBox;
            this.BB = new BoundingBox(Min, Max);
            this.RealativePosMax = Max;
            this.RealativePosMin = Min;
            this.eP = eP;
        }

        /// <summary>
        /// Ray node
        /// </summary>
        // Token: 0x06000061 RID: 97 RVA: 0x00006B20 File Offset: 0x00004D20
        public eCollitionNode(Vector3 Start, Vector3 Direction, bool KeepThisTrueForRays, ePart eP)
        {
            this.LowerNodes = new List<eCollitionNode>();
            this.RRewardMultiplier = new RewardSetMultiplier(1f, 1f, 1f);
            this.Type1 = eCollitionNode.Type.Ray;
            this.RR = new Ray(Start, Direction);
            this.RealativePos = Start;
            this.eP = eP;
        }

        /// <summary>
        /// Bounding Fustrum node
        /// </summary>
        // Token: 0x06000062 RID: 98 RVA: 0x00006B80 File Offset: 0x00004D80
        public eCollitionNode(Matrix M, ePart eP)
        {
            this.LowerNodes = new List<eCollitionNode>();
            this.RRewardMultiplier = new RewardSetMultiplier(1f, 1f, 1f);
            this.Type1 = eCollitionNode.Type.BoundingFrustum;
            this.BF = new BoundingFrustum(M);
            this.eP = eP;
        }

        // Token: 0x06000063 RID: 99 RVA: 0x00006BD4 File Offset: 0x00004DD4
        public eCollitionNode(Vector3 P1, Vector3 P2, Vector3 P3, ePart eP)
        {
            this.LowerNodes = new List<eCollitionNode>();
            this.RRewardMultiplier = new RewardSetMultiplier(1f, 1f, 1f);
            this.Type1 = eCollitionNode.Type.Plane;
            this.PL = new Plane(P1, P2, P3);
            this.RP1 = P1;
            this.RP2 = P2;
            this.RP3 = P3;
            this.eP = eP;
        }

        // Token: 0x06000064 RID: 100 RVA: 0x00006C40 File Offset: 0x00004E40
        public void AddLowerNode(eCollitionNode LN)
        {
            LN.CollitionHierarchy = this.CollitionHierarchy;
            this.CollitionHierarchy.AllNodes.Add(LN);
            LN.UpperNode = this;
            this.LowerNodes.Add(LN);
        }

        // Token: 0x06000065 RID: 101 RVA: 0x00006C78 File Offset: 0x00004E78
        public bool IsCollided(BoundingSphere BBS)
        {
            bool IsC = false;
            bool flag = this.Type1 == eCollitionNode.Type.BoundingSphere;
            if (flag)
            {
                bool flag2 = this.BS.Intersects(BBS);
                if (flag2)
                {
                    bool flag3 = this.LowerNodes.Count > 0;
                    if (flag3)
                    {

                        foreach (eCollitionNode LN in this.LowerNodes)
                        {
                            bool flag4 = LN.IsCollided(BBS);
                            if (flag4)
                            {
                                IsC = true;
                            }
                        }
                    }

                    else
                    {
                        IsC = true;
                    }
                }
                else
                {
                    IsC = false;
                }
            }
            else
            {
                bool flag5 = this.Type1 == eCollitionNode.Type.BoundingBox;
                if (flag5)
                {
                    bool flag6 = this.BB.Intersects(BBS);
                    IsC = flag6;
                }
                else
                {
                    bool flag7 = this.Type1 == eCollitionNode.Type.Ray;
                    if (flag7)
                    {
                        bool flag8 = this.RR.Intersects(BBS) != null;
                        if (flag8)
                        {
                            bool flag9 = this.LowerNodes.Count > 0;
                            if (flag9)
                            {
                                foreach (eCollitionNode LN2 in this.LowerNodes)
                                {
                                    bool flag10 = LN2.IsCollided(BBS);
                                    if (flag10)
                                    {
                                        IsC = true;
                                    }
                                }
                            }
                            else
                            {
                                IsC = true;
                            }
                        }
                        else
                        {
                            IsC = false;
                        }
                    }
                    else
                    {
                        bool flag11 = this.Type1 == eCollitionNode.Type.BoundingFrustum;
                        if (flag11)
                        {
                            bool flag12 = this.BF.Intersects(BBS);
                            IsC = flag12;
                        }
                        else
                        {
                            bool flag13 = this.Type1 == eCollitionNode.Type.Plane;
                            if (flag13)
                            {
                                bool flag14 = this.PL.Intersects(BBS) == PlaneIntersectionType.Intersecting;
                                IsC = flag14;
                            }
                            else
                            {
                                IsC = false;
                            }
                        }
                    }
                }
            }
            return IsC;
        }

        // Token: 0x06000066 RID: 102 RVA: 0x00006E68 File Offset: 0x00005068
        public bool IsCollided(BoundingBox BBS)
        {
            bool flag = this.Type1 == eCollitionNode.Type.BoundingSphere;
            bool IsCollided;
            if (flag)
            {
                bool flag2 = this.BS.Intersects(BBS);
                IsCollided = flag2;
            }
            else
            {
                bool flag3 = this.Type1 == eCollitionNode.Type.BoundingBox;
                if (flag3)
                {
                    bool flag4 = this.BB.Intersects(BBS);
                    IsCollided = flag4;
                }
                else
                {
                    bool flag5 = this.Type1 == eCollitionNode.Type.Ray;
                    if (flag5)
                    {
                        bool flag6 = this.RR.Intersects(BBS) != null;
                        IsCollided = flag6;
                    }
                    else
                    {
                        bool flag7 = this.Type1 == eCollitionNode.Type.BoundingFrustum;
                        if (flag7)
                        {
                            bool flag8 = this.BF.Intersects(BBS);
                            IsCollided = flag8;
                        }
                        else
                        {
                            bool flag9 = this.Type1 == eCollitionNode.Type.Plane;
                            if (flag9)
                            {
                                bool flag10 = this.PL.Intersects(BBS) == PlaneIntersectionType.Intersecting;
                                IsCollided = flag10;
                            }
                            else
                            {
                                IsCollided = false;
                            }
                        }
                    }
                }
            }
            return IsCollided;
        }

        // Token: 0x06000067 RID: 103 RVA: 0x00006F60 File Offset: 0x00005160
        public bool IsCollided(Ray BBS, float RL)
        {
            bool IsC = false;
            bool flag = this.Type1 == eCollitionNode.Type.BoundingSphere;
            if (flag)
            {
                bool flag2 = this.BS.Intersects(BBS) != null;
                if (flag2)
                {
                    bool flag3 = Vector3.Distance(this.BS.Center, BBS.Position) < RL;
                    if (flag3)
                    {
                        bool flag4 = this.LowerNodes.Count > 0;
                        if (flag4)
                        {
                            foreach (eCollitionNode LN in this.LowerNodes)
                            {
                                bool flag5 = LN.IsCollided(BBS, RL);
                                if (flag5)
                                {
                                    IsC = true;
                                }
                            }
                        }
                        else
                        {
                            IsC = true;
                        }
                    }
                }
                else
                {
                    IsC = false;
                }
            }
            else
            {
                bool flag6 = this.Type1 == eCollitionNode.Type.BoundingBox;
                if (flag6)
                {
                    bool flag7 = this.BB.Intersects(BBS) != null;
                    IsC = flag7;
                }
                else
                {
                    bool flag8 = this.Type1 == eCollitionNode.Type.Ray;
                    if (flag8)
                    {
                        IsC = false;
                    }
                    else
                    {
                        bool flag9 = this.Type1 == eCollitionNode.Type.BoundingFrustum;
                        if (flag9)
                        {
                            bool flag10 = this.BF.Intersects(BBS) != null;
                            IsC = flag10;
                        }
                        else
                        {
                            bool flag11 = this.Type1 == eCollitionNode.Type.Plane;
                            IsC = (flag11 && false);
                        }
                    }
                }
            }
            return IsC;
        }

        // Token: 0x06000068 RID: 104 RVA: 0x000070DC File Offset: 0x000052DC
        public eCollitionNode GetCollided(Ray BBS, float RL)
        {
            eCollitionNode IsC = null;
            bool flag = this.Type1 == eCollitionNode.Type.BoundingSphere;
            if (flag)
            {
                bool flag2 = this.BS.Intersects(BBS) != null;
                if (flag2)
                {
                    bool flag3 = Vector3.Distance(this.BS.Center, BBS.Position) < RL;
                    if (flag3)
                    {
                        bool flag4 = this.LowerNodes.Count > 0;
                        if (flag4)
                        {
                            foreach (eCollitionNode LN in this.LowerNodes)
                            {
                                eCollitionNode CCN = LN.GetCollided(BBS, RL);
                                bool flag5 = !Information.IsNothing(CCN);
                                if (flag5)
                                {
                                    IsC = CCN;
                                }
                            }

                        }
                        else
                        {
                            IsC = this;
                        }
                    }
                }
                else
                {
                    IsC = null;
                }
            }
            else
            {
                bool flag6 = this.Type1 == eCollitionNode.Type.BoundingBox;
                if (flag6)
                {
                    bool flag7 = this.BB.Intersects(BBS) != null;
                    if (flag7)
                    {
                        IsC = this;
                    }
                    else
                    {
                        IsC = null;
                    }
                }
                else
                {
                    bool flag8 = this.Type1 == eCollitionNode.Type.Ray;
                    if (flag8)
                    {
                        IsC = null;
                    }
                    else
                    {
                        bool flag9 = this.Type1 == eCollitionNode.Type.BoundingFrustum;
                        if (flag9)
                        {
                            bool flag10 = this.BF.Intersects(BBS) != null;
                            if (flag10)
                            {
                                IsC = this;
                            }
                            else
                            {
                                IsC = null;
                            }
                        }
                        else
                        {
                            bool flag11 = this.Type1 == eCollitionNode.Type.Plane;
                            if (flag11)
                            {
                                IsC = this;
                            }
                            else
                            {
                                IsC = null;
                            }
                        }
                    }
                }
            }
            return IsC;
        }

        // Token: 0x06000069 RID: 105 RVA: 0x00007268 File Offset: 0x00005468
        public bool IsCollided(Plane BBS)
        {
            bool flag = this.Type1 == eCollitionNode.Type.BoundingSphere;
            bool IsC;
            if (flag)
            {
                bool flag2 = this.BS.Intersects(BBS) == PlaneIntersectionType.Intersecting;
                IsC = flag2;
            }
            else
            {
                bool flag3 = this.Type1 == eCollitionNode.Type.BoundingBox;
                if (flag3)
                {
                    bool flag4 = this.BB.Intersects(BBS) == PlaneIntersectionType.Intersecting;
                    IsC = flag4;
                }
                else
                {
                    bool flag5 = this.Type1 == eCollitionNode.Type.Ray;
                    if (flag5)
                    {
                        bool flag6 = this.RR.Intersects(BBS) != null;
                        IsC = flag6;
                    }
                    else
                    {
                        bool flag7 = this.Type1 == eCollitionNode.Type.BoundingFrustum;
                        if (flag7)
                        {
                            bool flag8 = this.BF.Intersects(BBS) == PlaneIntersectionType.Intersecting;
                            IsC = flag8;
                        }
                        else
                        {
                            bool flag9 = this.Type1 == eCollitionNode.Type.Plane;
                            IsC = (flag9 && false);
                        }
                    }
                }
            }
            return IsC;
        }

        /// <summary>
        /// Bounding Spheare node
        /// </summary>
        // Token: 0x0600006A RID: 106 RVA: 0x0000735A File Offset: 0x0000555A
        public void Update(Vector3 Pos)
        {
            this.BS = new BoundingSphere(Pos + this.RealativePos, this.BSR);
        }

        /// <summary>
        /// Bounding Box node
        /// </summary>
        // Token: 0x0600006B RID: 107 RVA: 0x0000737A File Offset: 0x0000557A
        public void Update(Vector3 Min, Vector3 Max)
        {
            this.BB = new BoundingBox(Min + this.RealativePosMin, Max + this.RealativePosMax);
        }

        /// <summary> 
        /// Ray node 
        /// </summary>
        // Token: 0x0600006C RID: 108 RVA: 0x000073A0 File Offset: 0x000055A0
        public void Update(Vector3 Start, bool KeepThisTrueForRays)
        {
            this.RR = new Ray(Start + this.RealativePos, this.RR.Direction);
        }

        /// <summary>
        /// Bounding Fustrum node
        /// </summary>
        // Token: 0x0600006D RID: 109 RVA: 0x000073C5 File Offset: 0x000055C5
        public void Update(Matrix M)
        {
            this.BF = new BoundingFrustum(M);
        }

        /// <summary>
        /// Plane node
        /// </summary>
        // Token: 0x0600006E RID: 110 RVA: 0x000073D4 File Offset: 0x000055D4
        public void Update(Vector3 P1, Vector3 P2, Vector3 P3)
        {
            this.PL = new Plane(P1, P2, P3);
        }

        // Token: 0x0400007F RID: 127
        public eCollitionNode.Type Type1;

        // Token: 0x04000080 RID: 128
        public BoundingSphere BS;

        // Token: 0x04000081 RID: 129
        public float BSR;

        // Token: 0x04000082 RID: 130
        public BoundingBox BB;

        // Token: 0x04000083 RID: 131
        public BoundingFrustum BF;

        // Token: 0x04000084 RID: 132
        public Ray RR;

        // Token: 0x04000085 RID: 133
        public float RRLength;

        // Token: 0x04000086 RID: 134
        public Plane PL;

        // Token: 0x04000087 RID: 135
        public eCollitionHierarchy CollitionHierarchy;

        // Token: 0x04000088 RID: 136
        public eCollitionNode UpperNode;

        // Token: 0x04000089 RID: 137
        public List<eCollitionNode> LowerNodes;

        // Token: 0x0400008A RID: 138
        public ePart eP;

        // Token: 0x0400008B RID: 139
        public RewardSetMultiplier RRewardMultiplier;

        /// <summary>
        /// Bounding Spheare node
        /// </summary>
        // Token: 0x0400008C RID: 140
        public Vector3 RealativePos;

        /// <summary>
        /// Bounding Box node
        /// </summary>
        // Token: 0x0400008D RID: 141
        public Vector3 RealativePosMin;

        // Token: 0x0400008E RID: 142
        public Vector3 RealativePosMax;

        /// <summary>
        /// Plane node
        /// </summary>
        // Token: 0x0400008F RID: 143
        public Vector3 RP1;

        // Token: 0x04000090 RID: 144
        public Vector3 RP2;

        // Token: 0x04000091 RID: 145
        public Vector3 RP3;

        // Token: 0x0200003E RID: 62
        public enum Type
        {
            // Token: 0x04000280 RID: 640
            BoundingSphere,
            // Token: 0x04000281 RID: 641
            BoundingBox,
            // Token: 0x04000282 RID: 642
            Ray,
            // Token: 0x04000283 RID: 643
            BoundingFrustum,
            // Token: 0x04000284 RID: 644
            Plane
        }
    }
}
