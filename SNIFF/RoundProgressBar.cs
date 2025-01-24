using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SNIFF;

public class RoundProgressBar : UserControl
{
	private int progress;

	private IContainer components;

	public int Progress
	{
		get
		{
			return progress;
		}
		set
		{
			progress = Math.Max(0, Math.Min(100, value));
			Invalidate();
		}
	}

	public RoundProgressBar()
	{
		InitializeComponent();
		DoubleBuffered = true;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		Graphics g = e.Graphics;
		Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
		int borderWidth = 10;
		using (SolidBrush backgroundBrush = new SolidBrush(Color.LightGray))
		{
			g.FillEllipse(backgroundBrush, rect);
		}
		using (SolidBrush progressBrush = new SolidBrush(Color.DarkGray))
		{
			g.FillPie(progressBrush, rect, -90f, 360 * progress / 100);
		}
		using (Pen borderPen = new Pen(Color.DarkGray, borderWidth))
		{
			g.DrawEllipse(borderPen, rect);
		}
		string text = $"{progress}%";
		SizeF textSize = g.MeasureString(text, Font);
		using SolidBrush textBrush = new SolidBrush(Color.Black);
		g.DrawString(text, Font, textBrush, ((float)base.Width - textSize.Width) / 2f, ((float)base.Height - textSize.Height) / 2f);
	}

	private void RoundProgressBar_Load(object sender, EventArgs e)
	{
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
		base.Name = "RoundProgressBar";
		base.Size = new System.Drawing.Size(675, 148);
		base.Load += new System.EventHandler(RoundProgressBar_Load);
		base.ResumeLayout(false);
	}
}
