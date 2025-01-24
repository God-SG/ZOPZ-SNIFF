using System;
using System.IO;

public class YourLogger
{
	private static readonly string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF", "errorLog.txt");

	public static void LogError(Exception ex)
	{
		try
		{
			string logDirectory = Path.GetDirectoryName(logFilePath);
			if (!Directory.Exists(logDirectory))
			{
				Directory.CreateDirectory(logDirectory);
			}
			using StreamWriter writer = new StreamWriter(logFilePath, append: true);
			writer.WriteLine($"{DateTime.Now} - {ex.GetType().Name}: {ex.Message}");
			writer.WriteLine(ex.StackTrace);
			writer.WriteLine();
		}
		catch (Exception ex2)
		{
			Console.WriteLine("Error logging exception: " + ex2.Message);
		}
	}

	public static void LogError(string errorMessage)
	{
		try
		{
			string logDirectory = Path.GetDirectoryName(logFilePath);
			if (!Directory.Exists(logDirectory))
			{
				Directory.CreateDirectory(logDirectory);
			}
			using StreamWriter writer = new StreamWriter(logFilePath, append: true);
			writer.WriteLine($"{DateTime.Now} - {errorMessage}");
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error logging exception: " + ex.Message);
		}
	}
}
