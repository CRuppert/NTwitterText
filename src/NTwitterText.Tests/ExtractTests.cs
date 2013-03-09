using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using YamlDotNet.RepresentationModel;

namespace NTwitterText.Tests
{
    [TestFixture]
    public class ExtractTests
    {
        [Test, TestCaseSource("HashTagCases")]
        public void PassesTwitterTextExtractHashTagTexts(SimpleTwitterCase testCase)
        {
            Extractor testExtractor = new Extractor();
            var m = testExtractor.ExtractHashtags(testCase.TestString);
            Assert.AreEqual(testCase.ExpectedTags.Count(), m.Count, testCase.Description);
            Assert.Pass(testCase.Description);
        }

        [Test, TestCaseSource("MentionCases")]
        public void PassesTwitterMentionExtraction(SimpleTwitterCase testCase)
        {
            Extractor testExtractor = new Extractor();
            var m = testExtractor.ExtractMentionedScreennames(testCase.TestString);
            Assert.AreEqual(testCase.ExpectedTags.Count(), m.Count);
            Assert.Pass(testCase.Description);
        }

        public IEnumerable MentionCases
        {
            get
            {
                return GetSingleNestedTagCaseData("mentions")
                    .Select(d =>
                        new TestCaseData(d)
                            .SetName(d.Description)

                    );
            }
        } 

        public IEnumerable  HashTagCases
        {
            get
            {
                return GetSingleNestedTagCaseData("hashtags")
                    .Select(d =>
                        new TestCaseData(d)
                            .SetName(d.Description)
                            
                    );
            }
        }

        public SimpleTwitterCase[] GetSingleNestedTagCaseData(string testCollectionName)
        {
            
            var cases = new List<SimpleTwitterCase>();
            using (TextReader reader = File.OpenText(@"./extract.yml"))
            {
                var ymlDoc = new YamlStream();
                ymlDoc.Load(reader);

                var root = (YamlMappingNode)ymlDoc.Documents[0].RootNode;

                var tests = (YamlMappingNode)root.Children[new YamlScalarNode("tests")];
                foreach (YamlMappingNode test in (YamlSequenceNode)tests.Children[new YamlScalarNode(testCollectionName)])
                {
                    
                    var expectedTagNode = (YamlSequenceNode)test.Children[new YamlScalarNode("expected")];
                    var tags = expectedTagNode.Children.Select(t => t.ToString()).ToArray();
                    var c = new SimpleTwitterCase()
                        {
                            Description = test.Children[new YamlScalarNode("description")].ToString(),
                            TestString = test.Children[new YamlScalarNode("text")].ToString(),
                            ExpectedTags = tags
                        };
                    cases.Add(c);
                }
            }
            return cases.ToArray();
        }

        public class SimpleTwitterCase
        {
            public string Description { get; set; }
            public string TestString { get; set; }
            public string[] ExpectedTags { get; set; }
        }
    }
}
