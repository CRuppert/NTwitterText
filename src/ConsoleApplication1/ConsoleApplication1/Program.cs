using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NTwitterText;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetWindowSize(160, 50);

			string testString = "@test1 #test1 this is an email@domain.com http://www.bing.com test www.etc.com to @test2 and @test3 #test2";
			string testString2 = "@test1 #test1 this is an email@domain.com http://www.bing.com/ test www.etc.com to @test2 and @test3 #test2";
			string testString3 = "@test1 #test1 this is an email@domain.com http://www.bing.com/somepage.aspx?key=value test www.etc.com to @test2 and @test3 #test2";
			string testString4 = "@test1 #test1 this is an email@domain.com http://www.bing.com/somepage.aspx?key=value test www.etc.com/somepage.php?key=value&key2=value to @test2 and @test3 #test2";

			Console.WriteLine("testString:\t" + testString);
			Console.WriteLine("testString2:\t" + testString2);
			Console.WriteLine("testString3:\t" + testString3);
			Console.WriteLine("testString4:\t" + testString4);
			
			Extractor testExtractor = new Extractor();
			
			Console.WriteLine("\nLooking for Mentioned Screennames ...");
			List<string> matches = testExtractor.ExtractMentionedScreennames(testString);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString:\t" + match);
			}

			matches = testExtractor.ExtractMentionedScreennames(testString2);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString2:\t" + match);
			}

			matches = testExtractor.ExtractMentionedScreennames(testString3);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString3:\t" + match);
			}

			matches = testExtractor.ExtractMentionedScreennames(testString4);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString4:\t" + match);
			}

			Console.WriteLine("\nLooking for URLs ...");
			matches = testExtractor.ExtractURLs(testString);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString:\t" + match);
			}

			matches = testExtractor.ExtractURLs(testString2);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString2:\t" + match);
			}

			matches = testExtractor.ExtractURLs(testString3);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString3:\t" + match);
			}

			matches = testExtractor.ExtractURLs(testString4);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString4:\t" + match);
			}

			Console.WriteLine("\nLooking for hashtags ...");
			matches = testExtractor.ExtractHashtags(testString);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString:\t" + match);
			}

			matches = testExtractor.ExtractHashtags(testString2);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString2:\t" + match);
			}

			matches = testExtractor.ExtractHashtags(testString3);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString3:\t" + match);
			}

			matches = testExtractor.ExtractHashtags(testString4);
			foreach (string match in matches)
			{
				Console.WriteLine("found in testString4:\t" + match);
			}

			Console.WriteLine("\n\n\n**********************\nTEST COMPLETE\n**********************");
			Console.ReadLine();
		}
	}
}
