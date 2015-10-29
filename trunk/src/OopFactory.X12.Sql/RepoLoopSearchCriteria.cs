namespace OopFactory.X12.Sql
{
	public class RepoLoopSearchCriteria
	{
		public object LoopId { get; set; }
		public object ParentLoopId { get; set; }
		public object InterchangeId { get; set; }
		public object TransactionSetId { get; set; }
		public string TransactionSetCode { get; set; }
		public string SpecLoopId { get; set; }
		public string LevelId { get; set; }
		public string LevelCode { get; set; }
		public string StartingSegmentId { get; set; }
		public string EntityIdentifierCode { get; set; }
	}
}