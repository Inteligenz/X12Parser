using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum EntityTypeQualifier
    {
        Undefined = 0,
        Person = 1,
        NonPersonEntity = 2
    }
    public class TypedLoopNM1 : TypedLoop
    {
        private string _entityIdentifer;

        public TypedLoopNM1(string entityIdentifier)
            : base("NM1")
        {
            _entityIdentifer = entityIdentifier;
        }

        internal override string GetSegmentString(X12DelimiterSet delimiters)
        {
            return String.Format("{0}{1}{2}", _segmentId, delimiters.ElementSeparator, _entityIdentifer);
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            _loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string NM101_EntityIdCode
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public EntityTypeQualifier NM102_EntityTypeQualifier
        {
            get 
            {
                switch (_loop.GetElement(2))
                {
                    case "1": return EntityTypeQualifier.Person;
                    case "2": return EntityTypeQualifier.NonPersonEntity;
                    default: return EntityTypeQualifier.Undefined;
                }
                
            }
            set 
            {
                switch (value)
                {
                    case EntityTypeQualifier.Person:
                        _loop.SetElement(2, "1");
                        break;
                    case EntityTypeQualifier.NonPersonEntity:
                        _loop.SetElement(2, "2");
                        break;
                }
            }
        }

        public string NM103_NameLastOrOrganizationName
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public string NM104_NameFirst
        {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public string NM105_NameMiddle
        {
            get { return _loop.GetElement(5); }
            set { _loop.SetElement(5, value); }
        }

        public string NM106_NamePrefix
        {
            get { return _loop.GetElement(6); }
            set { _loop.SetElement(6, value); }            
        }

        public string NM107_NameSuffix
        {
            get { return _loop.GetElement(7); }
            set { _loop.SetElement(7, value); }
        }

        public string NM108_IdCodeQualifier
        {
            get { return _loop.GetElement(8); }
            set { _loop.SetElement(8, value); }
        }

        public string NM109_IdCode
        {
            get { return _loop.GetElement(9); }
            set { _loop.SetElement(9, value); }
        }

        public string NM110_EntityRelationshipCode
        {
            get { return _loop.GetElement(10); }
            set { _loop.SetElement(10, value); }
        }

        public string NM111_EntityIdentifierCode
        {
            get { return _loop.GetElement(11); }
            set { _loop.SetElement(11, value); }
        }

        public string NM112_NameLastOrOrganizationName
        {
            get { return _loop.GetElement(12); }
            set { _loop.SetElement(12, value); }
        }

        public Loop AddLoop(string segmentString)
        {
            return _loop.AddLoop(segmentString);
        }

        public T AddLoop<T>(T loop) where T : TypedLoop
        {
            return _loop.AddLoop(loop);
        }
        
        public Segment AddSegment(string segmentString)
        {
            return _loop.AddSegment(segmentString);
        }

        public T AddSegment<T>(T segment) where T : TypedSegment
        {
            return _loop.AddSegment(segment);
        }
    }
}
