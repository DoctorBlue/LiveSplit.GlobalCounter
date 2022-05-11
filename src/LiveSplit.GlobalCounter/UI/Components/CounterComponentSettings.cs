using LiveSplit.Model.Input;
using LiveSplit.Options;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class CounterComponentSettings : UserControl
    {
        public CounterComponentSettings(bool allowGamepads)
        {
            InitializeComponent();

            Hook = new CompositeHook(allowGamepads);

            // Set default values.
            NumberPadEnabled = false;
            GlobalHotkeysEnabled = false;
            CounterFont = new Font("Segoe UI", 13, FontStyle.Regular, GraphicsUnit.Pixel);
            OverrideCounterFont = false;
            CounterTextColor = Color.FromArgb(255, 255, 255, 255);
            CounterValueColor = Color.FromArgb(255, 255, 255, 255);
            OverrideTextColor = false;
            BackgroundColor = Color.Transparent;
            BackgroundColor2 = Color.Transparent;
            BackgroundGradient = GradientType.Plain;
            CounterText = "Counter:";
            InitialValue = 0;

            // Hotkeys
            IncrementKey = new KeyOrButton(Keys.Add);
            DecrementKey = new KeyOrButton(Keys.Subtract);
            ResetKey = new KeyOrButton(Keys.NumPad0);

            // Set bindings.
            txtCounterText.DataBindings.Add("Text", this, "CounterText");
            numInitialValue.DataBindings.Add("Value", this, "InitialValue");
            chkGlobalHotKeys.DataBindings.Add("Checked", this, "GlobalHotkeysEnabled", false, DataSourceUpdateMode.OnPropertyChanged);
            chkNumberPadEnabled.DataBindings.Add("Checked", this, nameof(NumberPadEnabled), false, DataSourceUpdateMode.OnPropertyChanged);
            chkFont.DataBindings.Add("Checked", this, "OverrideCounterFont", false, DataSourceUpdateMode.OnPropertyChanged);
            lblFont.DataBindings.Add("Text", this, "CounterFontString", false, DataSourceUpdateMode.OnPropertyChanged);
            chkColor.DataBindings.Add("Checked", this, "OverrideTextColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor.DataBindings.Add("BackColor", this, "CounterTextColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor3.DataBindings.Add("BackColor", this, "CounterValueColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor1.DataBindings.Add("BackColor", this, "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "BackgroundColor2", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbGradientType.DataBindings.Add("SelectedItem", this, "GradientString", false, DataSourceUpdateMode.OnPropertyChanged);

            // Assign event handlers.
            cmbGradientType.SelectedIndexChanged += cmbGradientType_SelectedIndexChanged;
            chkFont.CheckedChanged += chkFont_CheckedChanged;
            chkColor.CheckedChanged += chkColor_CheckedChanged;
            chkGlobalHotKeys.CheckedChanged += chkGlobalHotKeys_CheckedChanged;
            chkNumberPadEnabled.CheckedChanged += chkNumberPadEnabled_CheckedChanged;

            Load += CounterSettings_Load;

            RegisterHotKeys();
        }

        public CompositeHook Hook { get; set; }

        public bool NumberPadEnabled { get; set; }
        public bool GlobalHotkeysEnabled { get; set; }

        public Color CounterTextColor { get; set; }
        public Color CounterValueColor { get; set; }
        public bool OverrideTextColor { get; set; }

        public string CounterFontString => $"{CounterFont.FontFamily.Name} {CounterFont.Style}";
        public Font CounterFont { get; set; }
        public bool OverrideCounterFont { get; set; }

        public Color BackgroundColor { get; set; }
        public Color BackgroundColor2 { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public string GradientString
        {
            get => BackgroundGradient.ToString();
            set => BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value);
        }

        public string CounterText { get; set; }
        public int InitialValue { get; set; }

        public KeyOrButton IncrementKey { get; set; }
        public KeyOrButton DecrementKey { get; set; }
        public KeyOrButton ResetKey { get; set; }

        public event EventHandler CounterReinitialiseRequired;

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            NumberPadEnabled = SettingsHelper.ParseBool(element[nameof(NumberPadEnabled)]);
            GlobalHotkeysEnabled = SettingsHelper.ParseBool(element["GlobalHotkeysEnabled"]);
            CounterTextColor = SettingsHelper.ParseColor(element["CounterTextColor"]);
            CounterValueColor = SettingsHelper.ParseColor(element["CounterValueColor"]);
            CounterFont = SettingsHelper.GetFontFromElement(element["CounterFont"]);
            OverrideCounterFont = SettingsHelper.ParseBool(element["OverrideCounterFont"]);
            OverrideTextColor = SettingsHelper.ParseBool(element["OverrideTextColor"]);
            BackgroundColor = SettingsHelper.ParseColor(element["BackgroundColor"]);
            BackgroundColor2 = SettingsHelper.ParseColor(element["BackgroundColor2"]);
            GradientString = SettingsHelper.ParseString(element["BackgroundGradient"]);
            CounterText = SettingsHelper.ParseString(element["CounterText"]);
            InitialValue = SettingsHelper.ParseInt(element["InitialValue"]);

            XmlElement incrementElement = element["IncrementKey"];
            IncrementKey = !string.IsNullOrEmpty(incrementElement?.InnerText) ? new KeyOrButton(incrementElement.InnerText) : null;
            XmlElement decrementElement = element["DecrementKey"];
            DecrementKey = !string.IsNullOrEmpty(decrementElement?.InnerText) ? new KeyOrButton(decrementElement.InnerText) : null;
            XmlElement resetElement = element["ResetKey"];
            ResetKey = !string.IsNullOrEmpty(resetElement?.InnerText) ? new KeyOrButton(resetElement.InnerText) : null;

            RegisterHotKeys();
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0") ^
            SettingsHelper.CreateSetting(document, parent, nameof(NumberPadEnabled), NumberPadEnabled) ^
            SettingsHelper.CreateSetting(document, parent, "GlobalHotkeysEnabled", GlobalHotkeysEnabled) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideCounterFont", OverrideCounterFont) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideTextColor", OverrideTextColor) ^
            SettingsHelper.CreateSetting(document, parent, "CounterFont", CounterFont) ^
            SettingsHelper.CreateSetting(document, parent, "CounterTextColor", CounterTextColor) ^
            SettingsHelper.CreateSetting(document, parent, "CounterValueColor", CounterValueColor) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
            SettingsHelper.CreateSetting(document, parent, "CounterText", CounterText) ^
            SettingsHelper.CreateSetting(document, parent, "InitialValue", InitialValue) ^
            SettingsHelper.CreateSetting(document, parent, "IncrementKey", IncrementKey) ^
            SettingsHelper.CreateSetting(document, parent, "DecrementKey", DecrementKey) ^
            SettingsHelper.CreateSetting(document, parent, "ResetKey", ResetKey);
        }

        // Behaviour essentially Lifted from LiveSplit Settings.
        private void SetHotkeyHandlers(TextBox txtBox, Action<KeyOrButton> keySetCallback)
        {
            string oldText = txtBox.Text;
            txtBox.Text = "Set Hotkey...";
            txtBox.Select(0, 0);

            // Handler for KeyDown
            KeyEventHandler handlerDown = (s, x) =>
            {
                KeyOrButton keyOrButton = x.KeyCode == Keys.Escape ? null : new KeyOrButton(x.KeyCode | x.Modifiers);

                // No action for special keys.
                if (x.KeyCode == Keys.ControlKey || x.KeyCode == Keys.ShiftKey || x.KeyCode == Keys.Menu)
                    return;

                keySetCallback(keyOrButton);

                // Remove Focus.
                txtBox.Select(0, 0);
                chkGlobalHotKeys.Select();

                txtBox.Text = FormatKey(keyOrButton);

                // Re-Register inputs.
                RegisterHotKeys();
            };

            // Handler for KeyUp (allows setting of special keys, shift, ctrl etc.).
            KeyEventHandler handlerUp = (s, x) =>
            {
                KeyOrButton keyOrButton = x.KeyCode == Keys.Escape ? null : new KeyOrButton(x.KeyCode | x.Modifiers);

                // No action for normal keys.
                if (x.KeyCode != Keys.ControlKey && x.KeyCode != Keys.ShiftKey && x.KeyCode != Keys.Menu)
                    return;

                keySetCallback(keyOrButton);
                txtBox.Select(0, 0);
                chkGlobalHotKeys.Select();
                txtBox.Text = FormatKey(keyOrButton);
                RegisterHotKeys();
            };

            EventHandler leaveHandler = (s, x) =>
            {
                txtBox.Text = oldText;
            };

            // Handler for gamepad/joystick inputs.
            EventHandlerT<GamepadButton> gamepadButtonPressed = (s, x) =>
            {
                KeyOrButton key = new KeyOrButton(x);
                keySetCallback(key);

                Action keyOrButton = () =>
                {
                    txtBox.Select(0, 0);
                    chkGlobalHotKeys.Select();
                    txtBox.Text = FormatKey(key);
                    RegisterHotKeys();
                };

                // May not be in the UI thread (likely).
                if (InvokeRequired)
                    Invoke(keyOrButton);
                else
                    keyOrButton();
            };

            txtBox.KeyDown += handlerDown;
            txtBox.KeyUp += handlerUp;
            txtBox.Leave += leaveHandler;

            Hook.AnyGamepadButtonPressed += gamepadButtonPressed;
        }

        /// <summary>
        /// Registers the hot keys (unregisters existing Hotkeys).
        /// </summary>
        private void RegisterHotKeys()
        {
            txtIncrement.Text = FormatKey(IncrementKey);
            txtDecrement.Text = FormatKey(DecrementKey);
            txtReset.Text = FormatKey(ResetKey);

            try
            {
                UnregisterAllHotkeys(Hook);

                Hook.RegisterHotKey(IncrementKey);
                Hook.RegisterHotKey(DecrementKey);
                Hook.RegisterHotKey(ResetKey);

                if (NumberPadEnabled)
                {
                    Hook.RegisterHotKey(Keys.NumPad1);
                    Hook.RegisterHotKey(Keys.NumPad2);
                    Hook.RegisterHotKey(Keys.NumPad3);
                    Hook.RegisterHotKey(Keys.NumPad4);
                    Hook.RegisterHotKey(Keys.NumPad5);
                    Hook.RegisterHotKey(Keys.NumPad6);
                    Hook.RegisterHotKey(Keys.NumPad7);
                    Hook.RegisterHotKey(Keys.NumPad8);
                    Hook.RegisterHotKey(Keys.NumPad9);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        /// <summary>
        /// Unregisters all hotkeys.
        /// </summary>
        public void UnregisterAllHotkeys(CompositeHook hook)
        {
            hook.UnregisterAllHotkeys();
            HotkeyHook.Instance.UnregisterAllHotkeys();
        }

        private string FormatKey(KeyOrButton key)
        {
            if (key == null)
                return "None";
            string str = key.ToString();
            if (key.IsButton)
            {
                int length = str.LastIndexOf(' ');
                if (length != -1)
                    str = str.Substring(0, length);
            }
            return str;
        }

        private void CounterSettings_Load(object sender, EventArgs e)
        {
            chkColor_CheckedChanged(null, null);
            chkFont_CheckedChanged(null, null);
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            var dialog = SettingsHelper.GetFontDialog(CounterFont, 7, 20);
            dialog.FontChanged += (s, ev) => CounterFont = ((CustomFontDialog.FontChangedEventArgs)ev).NewFont;
            dialog.ShowDialog(this);
            lblFont.Text = CounterFontString;
        }

        private void chkColor_CheckedChanged(object sender, EventArgs e)
        {
            label3.Enabled = btnColor.Enabled = label5.Enabled = btnColor3.Enabled = chkColor.Checked;
        }

        private void chkFont_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = lblFont.Enabled = btnFont.Enabled = chkFont.Checked;
        }

        private void chkGlobalHotKeys_CheckedChanged(object sender, EventArgs e)
        {
            GlobalHotkeysEnabled = chkGlobalHotKeys.Checked;
        }

        private void chkNumberPadEnabled_CheckedChanged(object sender, EventArgs e)
        {
            NumberPadEnabled = chkNumberPadEnabled.Checked;
            RegisterHotKeys();
        }

        private void cmbGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColor1.Visible = cmbGradientType.SelectedItem.ToString() != "Plain";
            btnColor2.DataBindings.Clear();
            btnColor2.DataBindings.Add("BackColor", this, btnColor1.Visible ? "BackgroundColor2" : "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            GradientString = cmbGradientType.SelectedItem.ToString();
        }

        private void txtIncrement_Enter(object sender, EventArgs e)
        {
            SetHotkeyHandlers((TextBox)sender, x => IncrementKey = x);
        }

        private void txtIncrement_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void txtDecrement_Enter(object sender, EventArgs e)
        {
            SetHotkeyHandlers((TextBox)sender,  x => DecrementKey = x);
        }

        private void txtDecrement_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void txtReset_Enter(object sender, EventArgs e)
        {
            SetHotkeyHandlers((TextBox)sender, x => ResetKey = x);
        }

        private void txtReset_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void numInitialValue_ValueChanged(object sender, EventArgs e)
        {
            InitialValue = (int)Math.Round(numInitialValue.Value, 0);
            CounterReinitialiseRequired(this, EventArgs.Empty);
        }
    }
}
