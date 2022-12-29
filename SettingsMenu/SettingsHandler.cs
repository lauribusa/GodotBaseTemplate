using Godot;
using SettingsMenu;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Settings
{
	public enum Language
	{
		English,
		French
	}
	public enum KeyboardMode
	{
		WASD,
		ZQSD
	}
	public enum DisplayMode
	{
		Fullscreen,
		Windowed
	}
	public enum DisplayResolution
	{
		_2160p,
		_1440p,
		_1080p,
		_720p,
		_480p
	} 
    public partial class SettingsHandler : Window
    {
        [Export]
        public NodePath tabContainerPath;
        public TabContainer tabContainer;

		private string _savePath = "res://Config/settings.cfg";
		private ConfigFile _config = new ConfigFile();
		private Error loadResponse;

		#region Settings nodes
		[ExportCategory("Settings Nodes")]
		[ExportGroup("Game")]
		[Export]
		private OptionButton _languageOption;
		[Export]
		private OptionButton _keyboardModeOption;
		
		[ExportGroup("Video")]
		[Export]
		private OptionButton _displayModeOption;
		[Export]
		private OptionButton _screenResolutionOption;
		[Export]
		private CheckButton _vsyncOption;
		[Export]
		private CheckButton _displayFpsOption;
		[Export]
		private HSlider _maxFpsOption;
		[Export]
		private HSlider _brightnessOption;
		[ExportSubgroup("Value Labels")]
		[Export]
		private Label _maxFpsValueLabel;
		[Export]
		private Label _brightnessValueLabel;
		[ExportGroup("Audio")]
		[Export]
		private HSlider _mainVolumeOption;
		[Export]
		private HSlider _musicVolumeOption;
		[Export]
		private HSlider _sfxVolumeOption;
		[ExportSubgroup("Value Labels")]
		[Export]
		private Label _mainVolumeValueLabel;
		[Export]
		private Label _musicVolumeValueLabel;
		[Export]
		private Label _sfxVolumeValueLabel;

		private Label _fpsDisplayLabel;
		#endregion
		#region Lifecycle events
		public override void _Ready()
		{
			loadResponse = _config.Load(_savePath);
			if(loadResponse != Error.Ok)
			{

			}
			InitFromConfigFile();
			tabContainer = GetNode(tabContainerPath) as TabContainer;
			tabContainer.SetTabTitle(0, "settings.game");
			tabContainer.SetTabTitle(1, "settings.video");
			tabContainer.SetTabTitle(2, "settings.audio");
			GetAndSetNodes();
		}
		#endregion

		#region Signals
		public void _on_settings_window_close_requested()
		{
			_config.Save(_savePath);
			this.Hide();
		}
		public void _on_language_option_item_selected(int index)
		{
			Debug.WriteLine("language option: " + index);
			OnLanguageChanged((Language)index);
			SetConfigValue("Language", index);
		}
		public void _on_keyboard_mode_option_item_selected(int index)
		{
			Debug.WriteLine("keyboard mode: " + index);
			OnKeyboardModeChanged((KeyboardMode)index);
			SetConfigValue("Keyboard", index);
		}
		public void _on_display_mode_option_item_selected(int index)
		{
			Debug.WriteLine("display mode: " + index);
			OnDisplayModeChanged((DisplayMode)index);
			SetConfigValue("Display", index);
		}
		public void _on_screen_resolution_option_item_selected(int index)
		{
			Debug.WriteLine("screen resolution : " + (DisplayResolution)index);
			OnScreenResolutionChanged((DisplayResolution)index);
			this.PopupCenteredRatio(.7f);
			SetConfigValue("Resolution", index);
		}
		public void _on_vsync_option_toggled(bool buttonPressed)
		{
			Debug.WriteLine("vsync mode: " + buttonPressed);
			OnVsyncChanged(buttonPressed);
			SetConfigValue("Vsync", buttonPressed);
		}
		public void _on_display_fps_option_toggled(bool buttonPressed)
		{
			Debug.WriteLine("display fps mode: " + buttonPressed);
			OnDisplayFpsChanged(buttonPressed);
			SetConfigValue("DisplayFPS", buttonPressed);
		}
		public void _on_maxfps_value_changed(bool hasChanged)
		{
			if (hasChanged)
			{
				OnMaxFpsChanged((int)Math.Floor(_maxFpsOption.Value));
				SetConfigValue("MaxFPS", (int)Math.Floor(_maxFpsOption.Value));
			}
		}
		public void _on_brightness_value_changed(float value)
		{
			Debug.WriteLine("brightness: " + value);
			OnBrightnessChanged(value);
			SetConfigValue("Brightness", value);
		}
		public void _on_mainVolume_value_changed(float value)
		{
			OnMainVolumeChanged(value);
			SetConfigValue("MainVolume", value);
		}
		public void _on_musicVolume_value_changed(float value)
		{
			OnMusicVolumeChanged(value);
			SetConfigValue("MusicVolume", value);
		}
		public void _on_sfxVolume_value_changed(float value)
		{
			OnSfxVolumeChanged(value);
			SetConfigValue("SfxVolume", value);
		}
		#endregion
		#region Methods
		private void InitFromConfigFile()
		{
			if (loadResponse != Error.Ok) return;

			var languageValue = (int)GetConfigValue("Language");
			_languageOption.Select(languageValue);
			OnLanguageChanged((Language)languageValue);

			var keyboardValue = (int)GetConfigValue("Keyboard");
			_keyboardModeOption.Select(keyboardValue);

			var displayModeValue = (int)GetConfigValue("Display");
			_displayModeOption.Select(displayModeValue);

			var resolutionValue = (int)GetConfigValue("Resolution");
			_screenResolutionOption.Select(resolutionValue);

			var vsyncValue = (bool)GetConfigValue("Vsync");
			_vsyncOption.ToggleMode = vsyncValue;

			var displayFpsValue = (bool)GetConfigValue("DisplayFPS");
			_displayFpsOption.ToggleMode = displayFpsValue;

			var maxFpsValue = (int)GetConfigValue("MaxFPS");
			_maxFpsOption.Value= maxFpsValue;

			var brightnessValue = (float)GetConfigValue("Brightness");
			_brightnessOption.Value= brightnessValue;

			var mainVolumeValue = (float)GetConfigValue("MainVolume");
			_mainVolumeOption.Value= mainVolumeValue;

			var musicVolumeValue = (float)GetConfigValue("MainVolume");
			_musicVolumeOption.Value= musicVolumeValue;
			
			var sfxVolumeValue = (float)GetConfigValue("SfxVolume");
			_sfxVolumeOption.Value= sfxVolumeValue;
		}
		private void CreateDefaultSettings()
		{
			GD.Print("Create default");
		}
		private Variant GetConfigValue(string key)
		{
			return _config.GetValue("Settings", key);
		}
		private void SetConfigValue(string key, Variant value)
		{
			_config.SetValue("Settings", key, value);
		}
		private void GetAndSetNodes()
		{
			SetSliderMinMaxValueLabels(_maxFpsOption);
			var minFpsLabel = _maxFpsOption.GetChild(0) as Label;
			minFpsLabel.Text = "settings.uncapped";
			SetCustomMinMaxLabelsForSliders(_maxFpsOption, "settings.uncapped");

			_maxFpsValueLabel.Text = _maxFpsOption.Value.ToString();
			
			SetSliderMinMaxValueLabels(_brightnessOption);
			_brightnessValueLabel.Text = _brightnessOption.Value.ToString();
			
			_mainVolumeValueLabel.Text = GetAudioValuePercentage(_mainVolumeOption).ToString();
			SetCustomMinMaxLabelsForSliders(_mainVolumeOption, "0", "100");

			_musicVolumeValueLabel.Text = GetAudioValuePercentage(_musicVolumeOption).ToString();
			SetCustomMinMaxLabelsForSliders(_musicVolumeOption, "0", "100");

			_sfxVolumeValueLabel.Text = GetAudioValuePercentage(_sfxVolumeOption).ToString();
			SetCustomMinMaxLabelsForSliders(_sfxVolumeOption, "0", "100");

			_fpsDisplayLabel = GetNode("/root/FpsDisplay") as Label;
		}
		private double GetAudioValuePercentage(Godot.Range slider)
		{
			double value = (slider.Value - slider.MinValue) / (slider.MaxValue - slider.MinValue);
			double valueToPercent = value * 100;
			return Math.Floor(valueToPercent);
		}
		private void SetCustomMinMaxLabelsForSliders(HSlider slider, string? minText = null, string? maxText = null)
		{
			if(!string.IsNullOrEmpty(minText))
			{
				var min= slider.GetChild(0) as Label;
				min.Text = minText;
			}
			if(!string.IsNullOrEmpty(maxText)) {
				var max = slider.GetChild(1) as Label;
				max.Text = maxText;
			}
		}
		private void SetSliderMinMaxValueLabels(Godot.Range slider)
		{
			var minValueLabel = slider.GetChild(0) as Label;
			var maxValueLabel = slider.GetChild(1) as Label;
			if (minValueLabel == null || maxValueLabel == null) return;
			minValueLabel.Text = slider.MinValue.ToString();
			maxValueLabel.Text = slider.MaxValue.ToString();
		}
		private void OnKeyboardModeChanged(KeyboardMode value)
		{
			Debug.WriteLine(value);
		}
		private void OnDisplayModeChanged(DisplayMode value)
		{
			switch (value)
			{
				case DisplayMode.Fullscreen:
					DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
					break;
				case DisplayMode.Windowed:
					DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
					break;
				default:
					break;
			}
			DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
		}
		private void OnVsyncChanged(bool isVsyncActive)
		{
			SaveData data = SaveSystem.Load(1);
			GD.Print(data.saveSlotIndex);
			GD.Print(data.lastTimeSaved);
			if (isVsyncActive)
			{
				DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
				var refreshRate = (int)Math.Floor(DisplayServer.ScreenGetRefreshRate());
				if (refreshRate < 0) refreshRate = 0;
				_maxFpsOption.Value = refreshRate;
				OnMaxFpsChanged(refreshRate);
				_maxFpsOption.Editable = false;
				Debug.WriteLine(Engine.MaxFps);
				return;
			}
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);
			Debug.WriteLine(Engine.MaxFps);
			_maxFpsOption.Editable = true;
		}
		private void OnScreenResolutionChanged(DisplayResolution displayResolution)
		{
			var root = GetTree().Root;
			switch (displayResolution)
			{
				case DisplayResolution._2160p:
					root.Size = new Vector2i(3840, 2160);
					break;
				case DisplayResolution._1440p:
					root.Size = new Vector2i(2560, 1440);
					break;
				case DisplayResolution._1080p:
					root.Size = new Vector2i(1920, 1080);
					break;
				case DisplayResolution._720p:
					root.Size = new Vector2i(1280, 720);
					break;
				case DisplayResolution._480p:
					root.Size = new Vector2i(654, 480);
					break;
				default:
					break;
			}
			DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
			var screen_size = DisplayServer.ScreenGetSize();
			// (Vector2i)screen_size * 0.5 - root.Size * 0.5
			var _x = (int)Math.Floor((screen_size.x * 0.5) - (root.Size.x * 0.5));
			var _y = (int)Math.Floor((screen_size.y * 0.5) - (root.Size.y * 0.5));
			var newScreenPosition = new Vector2i(_x, _y);
			DisplayServer.WindowSetPosition(newScreenPosition);
			var settingsX = (int)Math.Floor(root.Size.x * .7f);
			var settingsY = (int)Math.Floor(root.Size.y * .7f);
			Size = new Vector2i(settingsX, settingsY);
		}
		private void OnMaxFpsChanged(int value)
		{
			Engine.MaxFps = value;
			_maxFpsValueLabel.Text = $"{value}";
		}
		private void OnBrightnessChanged(float value)
		{
			_brightnessValueLabel.Text = $"{value}";
		}
		private void OnMainVolumeChanged(float value)
		{
			var displayedValue = GetAudioValuePercentage(_mainVolumeOption);
			AudioServer.SetBusVolumeDb(0, value);
			_mainVolumeValueLabel.Text = $"{displayedValue}";
		}
		private void OnMusicVolumeChanged(float value)
		{
			var displayedValue = GetAudioValuePercentage(_musicVolumeOption);
			AudioServer.SetBusVolumeDb(1, value);
			_musicVolumeValueLabel.Text = $"{displayedValue}";
		}
		private void OnSfxVolumeChanged(float value)
		{
			var displayedValue = GetAudioValuePercentage(_sfxVolumeOption);
			AudioServer.SetBusVolumeDb(2, value);
			_sfxVolumeValueLabel.Text = $"{displayedValue}";
		}
		private void OnLanguageChanged(Language language)
		{
			switch (language)
			{
				case Language.English:
					TranslationServer.SetLocale("en");
					break;
				case Language.French:
					TranslationServer.SetLocale("fr");
					break;
				default:
					break;
			}
		}
		private void OnDisplayFpsChanged(bool showFps)
		{
			if (showFps)
			{
				_fpsDisplayLabel.Show();
				return;
			}
			_fpsDisplayLabel.Hide();
			
		}
		#endregion
	}

}
