
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed fixed1;

	private global::Gtk.Label label1;

	private global::Gtk.Entry entry1;

	private global::Gtk.Button button1;

	private global::Gtk.Label label2;

	private global::Gtk.Entry entry2;

	private global::Gtk.Button button2;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.fixed1 = new global::Gtk.Fixed();
		this.fixed1.Name = "fixed1";
		this.fixed1.HasWindow = false;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label1 = new global::Gtk.Label();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Just say hello");
		this.fixed1.Add(this.label1);
		global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label1]));
		w1.X = 15;
		w1.Y = 9;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.entry1 = new global::Gtk.Entry();
		this.entry1.CanFocus = true;
		this.entry1.Name = "entry1";
		this.entry1.IsEditable = true;
		this.entry1.InvisibleChar = '●';
		this.fixed1.Add(this.entry1);
		global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.entry1]));
		w2.X = 3;
		w2.Y = 28;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.button1 = new global::Gtk.Button();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString("Say");
		this.fixed1.Add(this.button1);
		global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button1]));
		w3.X = 190;
		w3.Y = 25;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label2 = new global::Gtk.Label();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Using my library");
		this.fixed1.Add(this.label2);
		global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label2]));
		w4.X = 6;
		w4.Y = 79;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.entry2 = new global::Gtk.Entry();
		this.entry2.CanFocus = true;
		this.entry2.Name = "entry2";
		this.entry2.IsEditable = true;
		this.entry2.InvisibleChar = '●';
		this.fixed1.Add(this.entry2);
		global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.entry2]));
		w5.X = 2;
		w5.Y = 109;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.button2 = new global::Gtk.Button();
		this.button2.CanFocus = true;
		this.button2.Name = "button2";
		this.button2.UseUnderline = true;
		this.button2.Label = global::Mono.Unix.Catalog.GetString("Say");
		this.fixed1.Add(this.button2);
		global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button2]));
		w6.X = 175;
		w6.Y = 108;
		this.Add(this.fixed1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 400;
		this.DefaultHeight = 300;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.button1.Pressed += new global::System.EventHandler(this.OnButton1Pressed);
		this.button2.Pressed += new global::System.EventHandler(this.OnButton2Pressed);
	}
}
