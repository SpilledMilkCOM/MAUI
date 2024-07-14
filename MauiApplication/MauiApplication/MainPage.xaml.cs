
using Newtonsoft.Json;

namespace MauiApplication {

    public partial class MainPage : ContentPage {

        private const string DEFAULT_MESSAGE_KEY = "default_message";

        int _count = 0;

        public MainPage() {
            InitializeComponent();
        }

        private void ContentPage_Loaded(object sender, EventArgs e) {
            LogMessage("Main Page Loaded");

            var json = Preferences.Default.Get(DEFAULT_MESSAGE_KEY, string.Empty);

            if (!string.IsNullOrEmpty(json)) {
                IList<string> values = JsonConvert.DeserializeObject<List<string>>(json);

                if (values != null && values.Count > 0) {
                    uxDefaultMessages.Items.Clear();

                    foreach (var value in values) {
                        if (value != null) {
                            uxDefaultMessages.Items.Add(value);
                        }
                    }
                }
            }
        }

        //----==== COMMANDS ====------------------------------------------------------------------

        private void OnSaveClicked(object sender, EventArgs e) {
            LogMessage("Button Clicked");

            _count++;

            var plural = _count == 1 ? String.Empty : "s";

            uxSaveBtn.Text = $"Saved {_count} time{plural}";

            //SemanticScreenReader.Announce(uxCounterBtn.Text);

            SaveMessage(uxMessage.Text);
        }

        private void OnMenuClearClicked(object sender, EventArgs e) {
            LogMessage("Clicked Clear");

            Preferences.Default.Clear();
        }

        private void OnMenuReadClicked(object sender, EventArgs e) {
            LogMessage("Clicked Read");
        }

        private void OnMenuSendClicked(object sender, EventArgs e) {
            LogMessage($"Clicked Send: \"{uxMessage.Text}\"");
        }

        private void uxDefaultMessages_SelectedIndexChanged(object sender, EventArgs e) {
            LogMessage($"Default Post changed to: {uxDefaultMessages.SelectedIndex}");

            uxMessage.Text = uxDefaultMessages.Items[uxDefaultMessages.SelectedIndex].ToString();
        }

        //----==== METHODS ====------------------------------------------------------------------

        private void LogMessage(string message) {
            uxStatus.Text = message;
        }

        private void SaveMessage(string message) {

            // Only add the message if it's NOT in the list.

            if (!uxDefaultMessages.Items.Any(item => item == message)) {
                uxDefaultMessages.Items.Add(message);
            }

            // Note that "Preferences" is NOT SecureStorage.

            Preferences.Default.Set(DEFAULT_MESSAGE_KEY, JsonConvert.SerializeObject(uxDefaultMessages.Items));
        }
    }
}