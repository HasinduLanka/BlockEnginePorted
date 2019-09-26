using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine
{
    // Token: 0x02000009 RID: 9
    public class Arrow : Entity
    {
        // Token: 0x0600001C RID: 28 RVA: 0x00002E14 File Offset: 0x00001014
        public Arrow(EntityType eT) : base(eT)
        {
            this.ICode = (int)(checked(Entity.LICode + 1));
            Entity.LICode += 1;
            base.RotationX = 0f;
            base.RotationY = 0f;
            base.RotationZ = 0f;
            this.CurrentBlock = new DBlock(new Block());
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00002E79 File Offset: 0x00001079
        public void AddToTheCurrentStack()
        {
            Ground.CStack.eList.Add(this);
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00002E90 File Offset: 0x00001090
        public new void Update()
        {
            this.Position += this.Velocity;
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00002EBC File Offset: 0x000010BC
        public new void Draw()
        {
            int i = 0;
            checked
            {
                foreach (ModelMesh mesh in this.eType.Model.Meshes)
                {
                    Matrix W = this.eType.Transforms * this.ModelRotation * Matrix.CreateTranslation(this.Position);

                    foreach (Effect effect2 in mesh.Effects)
                    {
                        BasicEffect effect = (BasicEffect)effect2;
                        effect.Projection = Main.projectionMatrix;
                        effect.View = Main.viewMatrix;
                        effect.World = W;
                    }
                    mesh.Draw();
                    i++;
                }
            }
        }
    }
}
