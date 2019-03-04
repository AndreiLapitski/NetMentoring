using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButton1Pressed(object sender, EventArgs e)
    {
        var dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Hello, {0}!", this.entry1.Text);
        dialog.Run();
        dialog.Destroy();
        this.entry1.Text = "";
    }

    protected void OnButton2Pressed(object sender, EventArgs e)
    {
        string message = SpeakerClassLibrary.Speaker.SayHelloNow(this.entry2.Text);
        var dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "{0}", message);
        dialog.Run();
        dialog.Destroy();
        this.entry1.Text = "";
    }
}
