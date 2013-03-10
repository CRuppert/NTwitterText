using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRE = System.Text.RegularExpressions;

namespace NTwitterText
{
	/**
 * A class to extract usernames, lists, hashtags and URLs from Tweet text.
 */
	public class Extractor
	{

		/**
		 * Create a new extractor.
		 */
		public Extractor()
		{
		}

		/**
		 * Extract @username references from Tweet text. A mention is an occurance of @username anywhere in a Tweet.
		 *
		 * @param text of the tweet from which to extract usernames
		 * @return List of usernames referenced (without the leading @ sign)
		 */
		public List<String> ExtractMentionedScreennames(String text)
		{
			if (text == null)
			{
				return null;
			}

			return ExtractList(Regex.VALID_MENTION_OR_LIST, text, Regex.VALID_MENTION_OR_LIST_GROUP_USERNAME);
		}

		/**
		 * Extract a @username reference from the beginning of Tweet text. A reply is an occurance of @username at the
		 * beginning of a Tweet, preceded by 0 or more spaces.
		 *
		 * @param text of the tweet from which to extract the replied to username
		 * @return username referenced, if any (without the leading @ sign). Returns null if this is not a reply.
		 */
		public String ExtractReplyScreenname(String text)
		{
			if (text == null)
			{
				return null;
			}

			TRE.Match matcher = Regex.VALID_REPLY.Match(text);
			if (matcher.Success)
			{
				return matcher.Groups[Regex.VALID_REPLY_GROUP_USERNAME].Value;
			}
			else
			{
				return null;
			}
		}

		/**
		 * Extract URL references from Tweet text.
		 *
		 * @param text of the tweet from which to extract URLs
		 * @return List of URLs referenced.
		 */
		public List<String> ExtractURLs(String text)
		{
			if (text == null)
			{
				return null;
			}
            Console.WriteLine(Regex.URL_VALID_SUBDOMAIN);
			return ExtractList(Regex.VALID_URL, text, Regex.VALID_URL_GROUP_URL);
		}

		/**
		 * Extract #hashtag references from Tweet text.
		 *
		 * @param text of the tweet from which to extract hashtags
		 * @return List of hashtags referenced (without the leading # sign)
		 */
		public List<String> ExtractHashtags(String text)
		{
			if (text == null)
			{
				return null;
			}
            //Console.WriteLine(Regex.URL_VALID_DOMAIN_NAME);
			return ExtractList(Regex.VALID_HASHTAG, text, Regex.VALID_HASHTAG_GROUP_TAG);
		}

		/**
		 * Helper method for extracting multiple matches from Tweet text.
		 *
		 * @param pattern to match and use for extraction
		 * @param text of the Tweet to extract from
		 * @param groupNumber the capturing group of the pattern that should be added to the list.
		 * @return list of extracted values, or an empty list if there were none.
		 */
		private List<String> ExtractList(TRE.Regex pattern, String text, int groupNumber)
		{
			List<String> extracted = new List<String>();
			TRE.Match matcher = pattern.Match(text);
			while (matcher.Success)
			{
				extracted.Add(matcher.Groups[groupNumber].Value);

				matcher = matcher.NextMatch();
			}
			return extracted;
		}
	}
}