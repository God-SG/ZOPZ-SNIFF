using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class CacheManager<TKey, TValue>
{
	private readonly ConcurrentDictionary<TKey, TValue> _cache = new ConcurrentDictionary<TKey, TValue>();

	private readonly string _filePath;

	public CacheManager(string fileName)
	{
		if (string.IsNullOrWhiteSpace(fileName))
		{
			throw new ArgumentNullException("fileName");
		}
		string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF");
		Directory.CreateDirectory(appDataFolder);
		_filePath = Path.Combine(appDataFolder, fileName);
		if (File.Exists(_filePath))
		{
			LoadCacheFromFile();
		}
	}

	private void LoadCacheFromFile()
	{
		string json = File.ReadAllText(_filePath);
		try
		{
			foreach (KeyValuePair<TKey, TValue> kvp in JsonConvert.DeserializeObject<ConcurrentDictionary<TKey, TValue>>(json))
			{
				_cache[kvp.Key] = kvp.Value;
			}
		}
		catch
		{
		}
	}

	public bool TryGetCachedResponse(TKey key, out TValue value)
	{
		return _cache.TryGetValue(key, out value);
	}

	public void CacheResponse(TKey key, TValue value)
	{
		_cache[key] = value;
	}

	public void SaveCacheToFile()
	{
		string json = JsonConvert.SerializeObject(_cache, Formatting.Indented);
		File.WriteAllText(_filePath, json);
	}
}
