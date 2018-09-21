namespace OopFactory.X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OopFactory.X12.Shared.Properties;

    /// <summary>
    /// Represents a <see cref="LoopContainer"/> with hierarchical structure and data
    /// </summary>
    public abstract class HierarchicalLoopContainer : LoopContainer
    {
        private Dictionary<string, HierarchicalLoop> hLoops;

        public IEnumerable<HierarchicalLoop> HLoops => this.hLoops.Values;

        protected Dictionary<string, HierarchicalLoop> AllHLoops { get; set; }
        
        public abstract bool AllowsHierarchicalLoop(string levelCode);

        public abstract HierarchicalLoop AddHLoop(string id, string levelCode, bool? existingHierarchalLoops);

        public HierarchicalLoop FindHLoop(string id)
        {
            return this.AllHLoops.ContainsKey(id) ? this.AllHLoops[id] : null;
        }

        public bool HasHierachicalSpecs()
        {
            if (this is Transaction)
            {
                return true;
            }

            if (this is HierarchicalLoop)
            {
                return false;
            }

            if (this is Loop)
            {
                return ((Loop)this).Specification.HierarchicalLoopSpecifications.Count > 0;
            }

            return false;
        }

        /// <summary>
        /// Adds <see cref="HierarchicalLoop"/> to the current container with the provided segment string
        /// </summary>
        /// <param name="segmentString">String of loop to be added</param>
        /// <returns>Loop created from segment string</returns>
        /// <exception cref="TransactionValidationException">Thrown if the transaction has been previously added
        /// of if the transaction specification is null</exception>
        /// <exception cref="InvalidOperationException">Thrown if the current loop container doesn't have a valid
        /// parent</exception>
        public HierarchicalLoop AddHLoop(string segmentString)
        {
            Transaction transaction = this.Transaction;

            var hl = new HierarchicalLoop(this, this.DelimiterSet, segmentString);

            HierarchicalLoopContainer specContainer = this;
            while (!(specContainer is HierarchicalLoopContainer && specContainer.HasHierachicalSpecs()))
            {
                if (specContainer.Parent is HierarchicalLoopContainer)
                {
                    specContainer = (HierarchicalLoopContainer)specContainer.Parent;
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format(Resources.InvalidHLSpecError, segmentString));
                }
            }

            if (specContainer is Transaction)
            {
                hl.Specification = transaction.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                    hls => hls.LevelCode == null || hls.LevelCode.ToString() == hl.LevelCode);
            }

            if (specContainer is HierarchicalLoop hLoopWithSpec)
            {
                hl.Specification = hLoopWithSpec.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                    hls => hls.LevelCode == null || hls.LevelCode.ToString() == hl.LevelCode);
            }

            if (specContainer is Loop loopWithSpec)
            {
                hl.Specification = loopWithSpec.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                    hls => hls.LevelCode == null || hls.LevelCode.ToString() == hl.LevelCode);
            }

            if (hl.Specification == null)
            {
                throw new TransactionValidationException(
                    Resources.TransactionHLCodeError,
                    transaction.IdentifierCode,
                    transaction.ControlNumber,
                    "HL03",
                    hl.LevelCode);
            }

            this.hLoops.Add(hl.Id, hl);

            // loop id must be unique throughout the transaction
            try
            {
                specContainer.AddToHLoopDictionary(hl);
            }
            catch (ArgumentException)
            {
                throw new TransactionValidationException(
                    Resources.UnableToAddHLoop,
                    transaction.IdentifierCode,
                    transaction.ControlNumber,
                    "HL01",
                    hl.Id);
            }

            return hl;
        }

        /// <summary>
        /// Adds a provided <see cref="HierarchicalLoop"/> to the container
        /// </summary>
        /// <param name="hloop">Loop to be added</param>
        /// <exception cref="ArgumentException">Thrown if the loop ID is not unique</exception>
        internal void AddToHLoopDictionary(HierarchicalLoop hloop)
        {
            this.AllHLoops.Add(hloop.Id, hloop);
        }

        internal HierarchicalLoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
        {
            this.AllHLoops = new Dictionary<string, HierarchicalLoop>();
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            this.hLoops = new Dictionary<string, HierarchicalLoop>();
        }

        internal override int CountTotalSegments()
        {
            return base.CountTotalSegments() + this.HLoops.Sum(hl => hl.CountTotalSegments());
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            var sb = new StringBuilder(base.SerializeBodyToX12(addWhitespace));
            foreach (var hloop in this.HLoops)
            {
                sb.Append(hloop.ToX12String(addWhitespace));
            }

            return sb.ToString();
        }

        internal override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.SegmentId))
            {
                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                {
                    segment.WriteXml(writer);
                }

                foreach (var hloop in this.HLoops)
                {
                    hloop.WriteXml(writer);
                }

                foreach (var segment in this.TrailerSegments)
                {
                    segment.WriteXml(writer);
                }
            }
        }
    }
}
