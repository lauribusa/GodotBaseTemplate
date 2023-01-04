using Godot;
using System;
using System.Text.Json;

namespace SettingsMenu
{
	public static class SaveSystem
	{
		#region Properties
		private static byte[] _key = new byte[32]
		{ 0xb6, 0xee, 0xf1, 0xef, 0xb7, 0xb6, 0x63, 0xd4, 0x06, 0xfb, 0x1e, 0xee, 0x19, 0x22, 0xce, 0x5b, 0xdf, 0x0f, 0x3b, 0xd3, 0x68, 0xe5, 0xeb, 0x11, 0xae, 0x89, 0x25, 0x7b, 0x8b, 0x4c, 0x61, 0x5e };

		#endregion
		private static string GetSaveFilePath(int index = 0)
		{
			return $"user://save{index}.dat";
		}
		#region Public Methods
		public static bool SaveExists(int slotIndex)
		{
			var saveSlotPath = GetSaveFilePath(slotIndex);
			return FileAccess.FileExists(saveSlotPath);
		}

		public static void Save(SaveData saveData)
	    {
			var filePath = GetSaveFilePath(saveData.saveSlotIndex);
			var jsonSaveData = JsonSerializer.Serialize(saveData);
			using var saveFile = FileAccess.OpenEncrypted(filePath, FileAccess.ModeFlags.Write, _key);
			if(saveFile == null)
			{
				var error = FileAccess.GetOpenError();
				GD.PrintErr(error);
			}
			saveFile.StoreString(jsonSaveData);
		}

		public static SaveData Load(int saveSlot)
		{
			if (!SaveExists(saveSlot)) return null;
			var filePath = GetSaveFilePath(saveSlot);
			using var saveFile = FileAccess.OpenEncrypted(filePath, FileAccess.ModeFlags.Read, _key);
			string serializedContent = saveFile.GetAsText(); 
			SaveData content = JsonSerializer.Deserialize<SaveData>(serializedContent);
			if (saveFile == null)
			{
				var error = FileAccess.GetOpenError();
				GD.PrintErr(error);
				return null;
			}
			return content;
		}
		#endregion
	}
	#region SaveData Model
	[Serializable]
	public class SaveData
	{
		public SaveData() { }
		public SaveData(DateTime _saveTime, int _saveSlotIndex) {
			lastTimeSaved = _saveTime;
			saveSlotIndex = _saveSlotIndex;
		}
		public DateTime lastTimeSaved { get; set; } = DateTime.Now;
		public int saveSlotIndex { get; set; } = 0;
	}
	#endregion
}
