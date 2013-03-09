﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NTwitterText
{

	public class Spaces
	{
		private static readonly String[] UNICODE_SPACE_RANGES = {
    "\\u0009-\\u000d",      //  # White_Space # Cc   [5] <control-0009>..<control-000D>
    "\\u0020",              // White_Space # Zs       SPACE
    "\\u0085",              // White_Space # Cc       <control-0085>
    "\\u00a0",              // White_Space # Zs       NO-BREAK SPACE
    "\\u1680",              // White_Space # Zs       OGHAM SPACE MARK
    "\\u180E",              // White_Space # Zs       MONGOLIAN VOWEL SEPARATOR
    "\\u2000-\\u200a",      // # White_Space # Zs  [11] EN QUAD..HAIR SPACE
    "\\u2028",              // White_Space # Zl       LINE SEPARATOR
    "\\u2029",              // White_Space # Zp       PARAGRAPH SEPARATOR
    "\\u202F",              // White_Space # Zs       NARROW NO-BREAK SPACE
    "\\u205F",              // White_Space # Zs       MEDIUM MATHEMATICAL SPACE
    "\\u3000",              // White_Space # Zs       IDEOGRAPHIC SPACE
  };

		private static String _characterClass = null;

		static Spaces()
		{
			StringBuilder sb = new StringBuilder(UNICODE_SPACE_RANGES.Length + 1);
			for (int i = 0; i < UNICODE_SPACE_RANGES.Length; i++)
			{
				sb.Append(UNICODE_SPACE_RANGES[i]);
			}
			_characterClass = sb.ToString();
		}

		public static String GetCharacterClass()
		{
			return _characterClass;
		}
	}
}