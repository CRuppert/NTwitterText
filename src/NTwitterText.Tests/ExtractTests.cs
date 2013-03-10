using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using YamlDotNet.RepresentationModel;

namespace NTwitterText.Tests
{
    [TestFixture]
    public class ExtractTests
    {
        [Test, TestCaseSource("HashTagCases")]
        public void ExtractsHashtagsCorrectly(SimpleTwitterCase testCase)
        {
            Extractor testExtractor = new Extractor();
            var m = testExtractor.ExtractHashtags(testCase.TestString);
            Assert.AreEqual(testCase.ExpectedTags.Count(), m.Count, testCase.Description);
            foreach (var tag in m)
            {
                Assert.Contains(tag, testCase.ExpectedTags, tag + " Not Found");
            }
        }

        [Test, TestCaseSource("MentionCases")]
        public void ExtractsMentionsCorrectly(SimpleTwitterCase testCase)
        {
            Extractor testExtractor = new Extractor();
            var m = testExtractor.ExtractMentionedScreennames(testCase.TestString);
            Assert.AreEqual(testCase.ExpectedTags.Count(), m.Count);
        }

        [Test, TestCaseSource("Urls")]
        public void ExtractsUrlsCorrectly(SimpleTwitterCase testCase)
        {
            Extractor testExtractor = new Extractor();
            var m = testExtractor.ExtractURLs(testCase.TestString);
            Assert.AreEqual(testCase.ExpectedTags.Count(), m.Count);
        }

        [Test, TestCaseSource("Replies")]
        public void ExtractsRepliesCorrectly(SimpleTwitterCase testCase)
        {
            Extractor testExtractor = new Extractor();
            var m = testExtractor.ExtractReplyScreenname(testCase.TestString);
            
            if (m == null)
            {
                Assert.IsEmpty(testCase.ExpectedTags.First());
            }
            else
            {
                Assert.AreEqual(testCase.ExpectedTags.First(), m);
            }
        }

        [Test, TestCaseSource("HashWithIndices")]
        public void PassesTwitterIndexesForHashTags(IndexedTwitterTest testCase)
        {
            

            Extractor testExtractor = new Extractor();
            var m = testExtractor.ExtractHashtags(testCase.TestString);
            foreach (var expect in testCase.Expectations)
            {
                Assert.Fail("NotImplemented");
            }
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
        public IEnumerable Urls
        {
            get
            {
                return GetSingleNestedTagCaseData("urls")
                    .Select(d =>
                        new TestCaseData(d)
                            .SetName(d.Description)

                    );
            }
        }
        public IEnumerable Replies
        {
            get
            {
                return GetSingleNestedSingleTagCaseData("replies")
                    .Select(d =>
                        new TestCaseData(d)
                            .SetName(d.Description)

                    );
            }
        }
        public IEnumerable HashWithIndices
        {
            get
            {
                return GetIndexedTagCaseData("hashtags_with_indices", "hashtag")
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

        public SimpleTwitterCase[] GetSingleNestedSingleTagCaseData(string testCollectionName)
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

                    var expectedTagNode = (YamlScalarNode)test.Children[new YamlScalarNode("expected")];
                    
                    var c = new SimpleTwitterCase()
                    {
                        Description = test.Children[new YamlScalarNode("description")].ToString(),
                        TestString = test.Children[new YamlScalarNode("text")].ToString(),
                        ExpectedTags = new string[]{expectedTagNode.ToString()}
                    };
                    cases.Add(c);
                }
            }
            return cases.ToArray();
        }

        public IndexedTwitterTest[] GetIndexedTagCaseData(string testCollectionName, string keyName)
        {

            var cases = new List<IndexedTwitterTest>();
            using (TextReader reader = File.OpenText(@"./extract.yml"))
            {
                var ymlDoc = new YamlStream();
                ymlDoc.Load(reader);

                var root = (YamlMappingNode)ymlDoc.Documents[0].RootNode;

                var tests = (YamlMappingNode)root.Children[new YamlScalarNode("tests")];
                foreach (YamlMappingNode test in (YamlSequenceNode)tests.Children[new YamlScalarNode(testCollectionName)])
                {
                    var c = new IndexedTwitterTest()
                    {
                        Description = test.Children[new YamlScalarNode("description")].ToString(),
                        TestString = test.Children[new YamlScalarNode("text")].ToString(),
                    };
                    var expectations = (YamlSequenceNode)test.Children[new YamlScalarNode("expected")];
                    
                    foreach (YamlMappingNode expectation in expectations.Children)
                    {
                        var tag = expectation.Children[new YamlScalarNode(keyName)].ToString();
                        var indicesNode = (YamlSequenceNode)expectation.Children[new YamlScalarNode("indices")];
                        var indices = indicesNode.Children.Select(t => int.Parse(t.ToString())).ToArray();

                        var e = new IndexedTwitterTest.Expectation()
                            {
                                HashTag = tag,
                                Indices = indices
                            };
                        c.Expectations.Add(e);
                    }
                    
                    cases.Add(c);
                }
            }
            return cases.ToArray();
        }
        
        
    }
}
