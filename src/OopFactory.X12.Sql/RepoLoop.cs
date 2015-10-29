namespace OopFactory.X12.Sql
{
	using Parsing;
	using Parsing.Model;

	public class RepoLoop
	{
		public RepoLoop(string segmentString, char segmentTerminator, char elementSeparator, char componentSeparator)
		{
			Segment = new DetachedSegment(
				new X12DelimiterSet(segmentTerminator, elementSeparator, componentSeparator),
				segmentString);
		}

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
		public int RevisionId { get; set; }
		public int PositionInInterchange { get; set; }
		public DetachedSegment Segment { get; private set; }
	}
}