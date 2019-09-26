using System;

namespace BlockEngine
{
	// Token: 0x0200002E RID: 46
	public class GameType
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00018B3A File Offset: 0x00016D3A
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00018B44 File Offset: 0x00016D44
		public bool IsFree { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00018B4D File Offset: 0x00016D4D
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00018B57 File Offset: 0x00016D57
		public string Name { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00018B60 File Offset: 0x00016D60
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00018B6A File Offset: 0x00016D6A
		public string WinMode { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00018B73 File Offset: 0x00016D73
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00018B7D File Offset: 0x00016D7D
		public string LossMode { get; set; }

		// Token: 0x060001DE RID: 478 RVA: 0x00018B86 File Offset: 0x00016D86
		public GameType(GameType GT)
		{
			this.Name = GT.Name;
			this.LossMode = GT.LossMode;
			this.WinMode = GT.WinMode;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00018BB7 File Offset: 0x00016DB7
		public GameType()
		{
		}
	}
}
