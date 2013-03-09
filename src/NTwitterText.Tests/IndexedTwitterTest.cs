using System.Collections.Generic;

namespace NTwitterText.Tests
{
    public class IndexedTwitterTest
    {
        private IList<Expectation> _expectations = new List<Expectation>();
        public string Description { get; set; }
        public string TestString { get; set; }
        public IList<Expectation> Expectations
        {
            get { return _expectations; }
            set { _expectations = value; }
        }

        public class Expectation
        {
            public string HashTag { get; set; }
            public int[] Indices { get; set; }
        }
    }
}