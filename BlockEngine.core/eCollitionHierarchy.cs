using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000013 RID: 19
    public class eCollitionHierarchy
    {
        // Token: 0x06000057 RID: 87 RVA: 0x00006318 File Offset: 0x00004518
        public void AddNode(eCollitionNode Node)
        {
            Node.CollitionHierarchy = this;
            this.AllNodes.Add(Node);
            this.UpperMostNodes.Add(Node);
        }

        // Token: 0x06000058 RID: 88 RVA: 0x0000633C File Offset: 0x0000453C
        public void UpdateAllSpheres(Vector3 Pos)
        {

            foreach (eCollitionNode Node in this.AllNodes)
            {
                bool flag = Node.Type1 == eCollitionNode.Type.BoundingSphere;
                if (flag)
                {
                    Node.Update(Pos);
                }
            }

        }

        // Token: 0x06000059 RID: 89 RVA: 0x000063A4 File Offset: 0x000045A4
        public eCollitionHierarchy(Entity OOWner)
        {
            this.UpperMostNodes = new List<eCollitionNode>();
            this.AllNodes = new List<eCollitionNode>();
            this.Owner = OOWner;
        }

        // Token: 0x0600005A RID: 90 RVA: 0x000063CC File Offset: 0x000045CC
        public bool IsCollided(BoundingSphere BS)
        {
            bool IsC = false;

            foreach (eCollitionNode N in this.UpperMostNodes)
            {
                bool flag = N.IsCollided(BS);
                if (flag)
                {
                    IsC = true;
                }
            }

            return IsC;
        }

        // Token: 0x0600005B RID: 91 RVA: 0x00006438 File Offset: 0x00004638
        public bool IsCollided(Ray R, float RL)
        {
            bool IsC = false;

            foreach (eCollitionNode N in this.UpperMostNodes)
            {
                bool flag = N.IsCollided(R, RL);
                if (flag)
                {
                    IsC = true;
                }
            }

            return IsC;
        }

        // Token: 0x0600005C RID: 92 RVA: 0x000064A4 File Offset: 0x000046A4
        public eCollitionNode GetCollided(Ray R, float RL)
        {
            eCollitionNode IsC = null;

            foreach (eCollitionNode N in this.UpperMostNodes)
            {
                eCollitionNode CCN = N.GetCollided(R, RL);
                bool flag = !Information.IsNothing(CCN);
                if (flag)
                {
                    IsC = CCN;
                }
            }
            return IsC;
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00006520 File Offset: 0x00004720
        public static eCollitionHierarchy CreateNewHierarchyForArrow(Arrow A1)
        {
            A1.CollitionHierarchy = new eCollitionHierarchy(A1);
            eCollitionNode CN = new eCollitionNode(Vector3.Zero, 10f, new ePart(A1));
            A1.CollitionHierarchy.AddNode(CN);
            return A1.CollitionHierarchy;
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00006568 File Offset: 0x00004768
        public static eCollitionHierarchy CreateNewHumanHierarchy(Human H1)
        {
            H1.CollitionHierarchy = new eCollitionHierarchy(H1);
            eCollitionNode CN = new eCollitionNode(new Vector3(0f, 76f, 0f), 80f, new ePart(H1));
            H1.CollitionHierarchy.AddNode(CN);
            CN.AddLowerNode(new eCollitionNode(new Vector3(0f, 164f, 0f), 15f, H1.BodyParts[5])
            {
                RRewardMultiplier = new RewardSetMultiplier(4f, 1f, 1f)
            });
            eCollitionNode BodyNode = new eCollitionNode(new Vector3(0f, 125f, 0f), 25f, H1.BodyParts[3]);
            CN.AddLowerNode(BodyNode);
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(10f, 140f, 2f), 10f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(-10f, 140f, 2f), 10f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(9f, 130f, 1f), 10f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(-9f, 130f, 1f), 10f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(0f, 130f, 0f), 10f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(6f, 120f, -3f), 9f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(-6f, 120f, -3f), 9f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(5f, 110f, -3f), 9f, H1.BodyParts[3]));
            BodyNode.AddLowerNode(new eCollitionNode(new Vector3(-5f, 110f, -3f), 9f, H1.BodyParts[3]));
            eCollitionNode LegsNode = new eCollitionNode(new Vector3(0f, 50f, 0f), 35f, H1.BodyParts[2]);
            CN.AddLowerNode(LegsNode);
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(0f, 95f, -1f), 12f, H1.BodyParts[2]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(10f, 78f, -2f), 7f, H1.BodyParts[2]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(-10f, 78f, -2f), 7f, H1.BodyParts[7]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(10f, 63f, -2f), 7f, H1.BodyParts[2]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(-10f, 63f, -2f), 7f, H1.BodyParts[7]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(10f, 48f, 0f), 7f, H1.BodyParts[2]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(-10f, 48f, 0f), 7f, H1.BodyParts[7]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(10f, 33f, 2f), 7f, H1.BodyParts[4]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(-10f, 33f, 2f), 7f, H1.BodyParts[0]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(10f, 18f, 0f), 7f, H1.BodyParts[4]));
            LegsNode.AddLowerNode(new eCollitionNode(new Vector3(-10f, 18f, 0f), 7f, H1.BodyParts[0]));
            H1.CollitionHierarchy.UpdateAllSpheres(H1.Position);
            return H1.CollitionHierarchy;
        }

        // Token: 0x0400007C RID: 124
        public Entity Owner;

        // Token: 0x0400007D RID: 125
        public List<eCollitionNode> UpperMostNodes;

        // Token: 0x0400007E RID: 126
        public List<eCollitionNode> AllNodes;
    }
}
