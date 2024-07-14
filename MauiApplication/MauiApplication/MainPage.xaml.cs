namespace MauiApplication {

    public partial class MainPage : ContentPage {

        private const string DEFAULT_MESSAGE_KEY = "default_message";

        int _count = 0;

        public MainPage() {
            InitializeComponent();
        }

        private void ContentPage_Loaded(object sender, EventArgs e) {
            LogMessage("Main Page Loaded");

            uxMessage.Text = Preferences.Default.Get(DEFAULT_MESSAGE_KEY, string.Empty);
        }

        //----==== COMMANDS ====------------------------------------------------------------------

        private void OnCounterClicked(object sender, EventArgs e) {
            LogMessage("Button Clicked");

            _count++;

            var plural = _count == 1 ? String.Empty : "s";

            uxCounterBtn.Text = $"Saved {_count} time{plural}";

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

            // Note that "Preferences" is NOT SecureStorage.

            Preferences.Default.Set(DEFAULT_MESSAGE_KEY, message);
        }
    }
}