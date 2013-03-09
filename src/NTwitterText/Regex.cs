﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRE = System.Text.RegularExpressions;

namespace NTwitterText
{

public class Regex {
  private static readonly String[] RESERVED_ACTION_WORDS = {"twitter","lists",
  "retweet","retweets","following","followings","follower","followers",
  "with_friend","with_friends","statuses","status","activity","favourites",
  "favourite","favorite","favorites"};

  public static readonly String HASHTAG_CHARACTERS = "[a-z0-9_\\u00c0-\\u00d6\\u00d8-\\u00f6\\u00f8-\\u00ff]";

  /* URL related hash regex collection */
  private static readonly String URL_VALID_PRECEEDING_CHARS = "(?:[^/\"':!=]|^|\\:)";
  //private static readonly String URL_VALID_DOMAIN = "(?:[\\.-]|[^\\p{Punct}])+\\.[a-z]{2,}(?::[0-9]+)?";
	private static readonly String URL_VALID_DOMAIN = "(?:[\\.-]|[^!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~])+\\.[a-z]{2,}(?::[0-9]+)?";
	
  private static readonly String URL_VALID_URL_PATH_CHARS = "[a-z0-9!\\*'\\(\\);:&=\\+\\$/%#\\[\\]\\-_\\.,~]";
  // Valid end-of-path chracters (so /foo. does not gobble the period).
  //   1. Allow ) for Wikipedia URLs.
  //   2. Allow =&# for empty URL parameters and other URL-join artifacts
  private static readonly String URL_VALID_URL_PATH_ENDING_CHARS = "[a-z0-9\\)=#/]";
  private static readonly String URL_VALID_URL_QUERY_CHARS = "[a-z0-9!\\*'\\(\\);:&=\\+\\$/%#\\[\\]\\-_\\.,~]";
  private static readonly String URL_VALID_URL_QUERY_ENDING_CHARS = "[a-z0-9_&=#]";
  private static readonly String VALID_URL_PATTERN_STRING = "(" +     //  $1 total match
    "(" + URL_VALID_PRECEEDING_CHARS + ")" +                       //  $2 Preceeding chracter
    "(" +                                                          //  $3 URL
      "(https?://|www\\.)" +                                       //  $4 Protocol or beginning
      "(" + URL_VALID_DOMAIN + ")" +                               //  $5 Domain(s) and optional port number
      "(/" + URL_VALID_URL_PATH_CHARS + "*" +                      //  $6 URL Path
             URL_VALID_URL_PATH_ENDING_CHARS + "?)?" +
      "(\\?" + URL_VALID_URL_QUERY_CHARS + "*" +                   //  $7 Query String
              URL_VALID_URL_QUERY_ENDING_CHARS + ")?" +
    ")" +
  ")";

  /* Begin public constants */

	public static readonly TRE.Regex AUTO_LINK_HASHTAGS = new TRE.Regex("(^|[^0-9A-Z&/]+)(#|\uFF03)([0-9A-Z_]*[A-Z_]+" + HASHTAG_CHARACTERS + "*)", TRE.RegexOptions.IgnoreCase);
  public static readonly int AUTO_LINK_HASHTAGS_GROUP_BEFORE = 1;
  public static readonly int AUTO_LINK_HASHTAGS_GROUP_HASH = 2;
  public static readonly int AUTO_LINK_HASHTAGS_GROUP_TAG = 3;

  public static readonly TRE.Regex AUTO_LINK_USERNAMES_OR_LISTS = new TRE.Regex("([^a-z0-9_]|^)([@\uFF20]+)([a-z0-9_]{1,20})(/[a-z][a-z0-9\\x80-\\xFF-]{0,79})?", TRE.RegexOptions.IgnoreCase);
  public static readonly int AUTO_LINK_USERNAME_OR_LISTS_GROUP_BEFORE = 1;
  public static readonly int AUTO_LINK_USERNAME_OR_LISTS_GROUP_AT = 2;
  public static readonly int AUTO_LINK_USERNAME_OR_LISTS_GROUP_USERNAME = 3;
  public static readonly int AUTO_LINK_USERNAME_OR_LISTS_GROUP_LIST = 4;

  public static readonly TRE.Regex VALID_URL = null;//new TRE.Regex(VALID_URL_PATTERN_STRING, TRE.RegexOptions.IgnoreCase);
  public static readonly int VALID_URL_GROUP_BEFORE = 2;
  public static readonly int VALID_URL_GROUP_URL = 3;

  public static readonly TRE.Regex EXTRACT_MENTIONS = new TRE.Regex("(^|[^a-z0-9_])[@\uFF20]([a-z0-9_]{1,20})(?!@)", TRE.RegexOptions.IgnoreCase);
  public static readonly int EXTRACT_MENTIONS_GROUP_BEFORE = 1;
  public static readonly int EXTRACT_MENTIONS_GROUP_USERNAME = 2;

  public static readonly TRE.Regex EXTRACT_REPLY = new TRE.Regex("^(?:[" + Spaces.GetCharacterClass() + "])*[@\uFF20]([a-z0-9_]{1,20}).*", TRE.RegexOptions.IgnoreCase);
  public static readonly int EXTRACT_REPLY_GROUP_USERNAME = 1;

	static Regex()
	{
		try
		{
			VALID_URL = new TRE.Regex(VALID_URL_PATTERN_STRING, TRE.RegexOptions.IgnoreCase);
		}
		catch (Exception ex)
		{

		}
	}
}
}