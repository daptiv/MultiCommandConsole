using System;
using System.Collections.Generic;
using System.IO;
using Common.Logging;

namespace MultiCommandConsole.Util
{
    public class ConsoleFormatter : IConsoleFormatter
    {
		private static readonly ILog Log = LogManager.GetLogger<ConsoleFormatter>();

		public int OverriddenBufferWidth { get; set; }

		private int BufferWidth { get { return OverriddenBufferWidth == 0 ? GetBufferWidth() : OverriddenBufferWidth; } }

		private int GetBufferWidth()
		{
			try
			{
				return Environment.UserInteractive ? Console.BufferWidth : 0;
			}
			catch
			{
				/* assume we're in a test */
				OverriddenBufferWidth = 80; //Windows default buffer size
				Log.Debug("buffer width not found.  is this a test?  assigning default of " + OverriddenBufferWidth);
				return OverriddenBufferWidth;
			}
		}

		public IEnumerable<string> ChunkString(string text, int decreaseChunkBy = 0)
		{
		    return text.Chunk(BufferWidth - decreaseChunkBy);
		}

		public void ChunckStringTo(string text, TextWriter textWriter, string chunkPrefix = null)
		{
			chunkPrefix = chunkPrefix ?? string.Empty;

			foreach (var chunk in ChunkString(text, chunkPrefix.Length))
			{
				textWriter.WriteLine(chunkPrefix + chunk);
			}
		}
	}
}