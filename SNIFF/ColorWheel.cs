using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SNIFF;

public class ColorWheel : UserControl
{
	private Color lastColor;

	private Color selectedColor = Color.Red;

	private bool isDragging;

	private Point indicatorPosition = new Point(-1, -1);

	private TrackBar trackBar;

	private Label colorValueLabel;

	private IContainer components;

	public Color SelectedColor
	{
		get
		{
			return selectedColor;
		}
		set
		{
			if (selectedColor != value)
			{
				selectedColor = value;
				OnColorChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	public Point IndicatorPosition
	{
		get
		{
			return indicatorPosition;
		}
		set
		{
			if (indicatorPosition != value)
			{
				indicatorPosition = value;
				Invalidate();
			}
		}
	}

	public event EventHandler ColorChanged;

	protected virtual void OnColorChanged(EventArgs e)
	{
		this.ColorChanged?.Invoke(this, e);
	}

	public ColorWheel()
	{
		InitializeComponent();
		base.Size = new Size(400, 300);
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		UpdateStyles();
		colorValueLabel = new Label
		{
			Size = new Size(60, 20),
			Location = new Point(base.Width - 80, 10),
			TextAlign = ContentAlignment.MiddleCenter,
			BackColor = Color.White,
			BorderStyle = BorderStyle.FixedSingle
		};
		base.Controls.Add(colorValueLabel);
		trackBar = new TrackBar
		{
			Orientation = Orientation.Vertical,
			Minimum = 0,
			Maximum = 360,
			TickFrequency = 10,
			Location = new Point(base.Width - 40, 30),
			Size = new Size(30, base.Height - 60),
			Value = 240,
			BackColor = Color.LightGray
		};
		trackBar.Scroll += TrackBar_Scroll;
		base.Controls.Add(trackBar);
		base.MouseDown += ColorWheel_MouseDown;
		base.MouseMove += ColorWheel_MouseMove;
		base.MouseUp += ColorWheel_MouseUp;
		DoubleBuffered = true;
		lastColor = Color.FromArgb(0, 0, 255);
		UpdateColorValueLabel();
		base.Resize += delegate
		{
			trackBar.Location = new Point(base.Width - 40, 30);
		};
	}

	private void TrackBar_Scroll(object sender, EventArgs e)
	{
		UpdateColorValueLabel();
		Invalidate();
	}

	private void UpdateColorValueLabel()
	{
		colorValueLabel.BackColor = ColorFromHSV((float)trackBar.Value / 360f, 1f, 1f);
		colorValueLabel.Text = trackBar.Value.ToString();
	}

	private Color GetColorFromGradient(Point point)
	{
		Rectangle rect = base.ClientRectangle;
		float fraction = (float)(point.Y - rect.Top) / (float)(rect.Height - 60);
		return ColorFromHSV((float)trackBar.Value / 360f, 1f, 1f - Clamp(fraction, 0f, 1f));
	}

	private Color ColorFromHSV(float h, float s, float v)
	{
		int r = 0;
		int g = 0;
		int b = 0;
		if (s == 0f)
		{
			r = (g = (b = (int)(v * 255f)));
		}
		else
		{
			float i = (float)Math.Floor(h * 6f);
			float f = h * 6f - i;
			int p = (int)(v * (1f - s) * 255f);
			int q = (int)(v * (1f - f * s) * 255f);
			int t2 = (int)(v * (1f - (1f - f) * s) * 255f);
			int vInt = (int)(v * 255f);
			switch ((int)i % 6)
			{
			case 0:
				r = vInt;
				g = t2;
				b = p;
				break;
			case 1:
				r = q;
				g = vInt;
				b = p;
				break;
			case 2:
				r = p;
				g = vInt;
				b = t2;
				break;
			case 3:
				r = p;
				g = q;
				b = vInt;
				break;
			case 4:
				r = t2;
				g = p;
				b = vInt;
				break;
			case 5:
				r = vInt;
				g = p;
				b = q;
				break;
			}
		}
		r = Clamp(r, 0, 255);
		g = Clamp(g, 0, 255);
		b = Clamp(b, 0, 255);
		return Color.FromArgb(r, g, b);
	}

	private void ColorWheel_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isDragging = true;
			UpdateColorAndIndicator(e.Location);
		}
	}

	private void ColorWheel_MouseMove(object sender, MouseEventArgs e)
	{
		if (isDragging)
		{
			UpdateColorAndIndicator(e.Location);
		}
	}

	private void ColorWheel_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isDragging = false;
			SaveSelectedColor();
		}
	}

	private void UpdateColorAndIndicator(Point point)
	{
		if (point.X >= 0 && point.X < base.Width - 40 && point.Y >= 0 && point.Y < base.Height)
		{
			SelectedColor = GetColorFromGradient(point);
			IndicatorPosition = point;
		}
	}

	private void SaveSelectedColor()
	{
		if (SelectedColor != lastColor)
		{
			lastColor = SelectedColor;
			OnColorChanged(EventArgs.Empty);
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		Graphics g = e.Graphics;
		Rectangle rect = base.ClientRectangle;
		for (int i = 0; i < rect.Height - 60; i++)
		{
			float fraction = (float)i / (float)(rect.Height - 60);
			using Brush brush = new SolidBrush(ColorFromHSV((float)trackBar.Value / 360f, 1f, 1f - fraction));
			g.FillRectangle(brush, new Rectangle(0, i + 30, rect.Width - 50, 1));
		}
		DrawColorIndicator(g, IndicatorPosition);
	}

	private void ColorWheel_Load(object sender, EventArgs e)
	{
	}

	private void DrawColorIndicator(Graphics g, Point position)
	{
		if (position.X < 0 || position.X >= base.Width - 40 || position.Y < 0 || position.Y >= base.Height)
		{
			return;
		}
		int indicatorSize = 12;
		using Brush brush = new SolidBrush(Color.Black);
		using Pen pen = new Pen(Color.White, 2f);
		g.FillEllipse(brush, position.X - indicatorSize / 2, position.Y - indicatorSize / 2, indicatorSize, indicatorSize);
		g.DrawEllipse(pen, position.X - indicatorSize / 2, position.Y - indicatorSize / 2, indicatorSize, indicatorSize);
	}

	private static float Clamp(float value, float min, float max)
	{
		return Math.Max(min, Math.Min(value, max));
	}

	private static int Clamp(int value, int min, int max)
	{
		return Math.Max(min, Math.Min(value, max));
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Name = "ColorWheel";
		base.Size = new System.Drawing.Size(772, 157);
		base.Load += new System.EventHandler(ColorWheel_Load);
		base.ResumeLayout(false);
	}
}
