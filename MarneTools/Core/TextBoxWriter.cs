namespace MarneTools.Core;

public class TextBoxWriter(TextBox textBox) : TextWriter
{
    public override Encoding Encoding => Encoding.UTF8;

    private readonly TextBox textBox = textBox;

    public override void WriteLine(string value)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            textBox?.AppendText($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}  {value}{Environment.NewLine}");
        });
    }
}
