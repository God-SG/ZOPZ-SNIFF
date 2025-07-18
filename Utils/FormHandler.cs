using ZOPZ_SNIFF.Menus;

namespace ZOPZ_SNIFF.Utils
{
    public class FormHandler
    {
        private IWin32Window _window { get; set; }
        private Point _point { get; set; }
        private int _width { get; set; }
        public int _height { get; set; }

        public FormHandler(IWin32Window window, Point location, int width, int height)
        {
            _window = window;
            _point = location;
            _width = width;
            _height = height;
        }

        public void ShowMessage(string title, string? message)
        {
            using (MessageAlert menu = new MessageAlert(title, message))
            {
                menu.StartPosition = FormStartPosition.Manual;
                menu.Load += (s, e) =>
                {
                    Form parentForm = (Form)_window;
                    Rectangle parentBounds = parentForm.Bounds;
                    int centerX = parentBounds.Left + (parentBounds.Width - menu.Width) / 2;
                    int centerY = parentBounds.Top + (parentBounds.Height - menu.Height) / 2;
                    menu.Location = new Point(centerX, centerY);
                };
                menu.ShowDialog(_window);
            }
        }

        public string? ShowTextPromp(string placeholder)
        {
            using (Simpletextprompt simplePrompt = new Simpletextprompt())
            {
                simplePrompt.PlaceholderText = placeholder;
                simplePrompt.StartPosition = FormStartPosition.Manual;
                simplePrompt.Load += (s, e) =>
                {
                    Form parentForm = (Form)_window;
                    Rectangle parentBounds = parentForm.Bounds;
                    int centerX = parentBounds.Left + (parentBounds.Width - simplePrompt.Width) / 2;
                    int centerY = parentBounds.Top + (parentBounds.Height - simplePrompt.Height) / 2;
                    simplePrompt.Location = new Point(centerX, centerY);
                };

                return simplePrompt.ShowDialog(val => !string.IsNullOrEmpty(val));
            }
        }
    }
}
