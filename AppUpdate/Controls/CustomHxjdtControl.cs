using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LayeredSkin.DirectUI;

namespace AppUpdate.Controls
{
    public class CustomHxjdtControl: DuiBaseControl
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 背景色
        /// </summary>
        private Color mainColor = Color.FromArgb(255, 92, 138);//背景色

        [Description("进度条背景色"), Category("自定义属性")]
        public Color MainColor
        {
            get { return mainColor; }
            set
            {
                mainColor = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 环形进度
        /// </summary>
        private int value = 30;
        [Description("进度"), Category("自定义属性")]
        public int Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.Invalidate();
            }
        }
        private int circularWidth = 16;//默认环形宽度
        [Description("环形宽度"), Category("自定义属性")]
        public int CircularWidth
        {
            get { return this.circularWidth; }
            set { this.circularWidth = value; }
        }
        private LineCap lcp = LineCap.Round;//起止点线帽样式
        [Description("起止点线帽样式"), Category("自定义属性")]
        public LineCap Lcp
        {
            get { return this.lcp; }
            set { this.lcp = value; }
        }
        private int lastWidth = 0;
        private int lastHeight = 0;
        private Point lastLocation = Point.Empty;


        public CustomHxjdtControl()
        {
            InitializeComponent();
            this.BackColor = Color.DarkGray;
        }
        private bool isSizeChangeAble = true; //是否允许OnSizeChanged执行
        protected override void OnSizeChanged(EventArgs e)
        {
            if (isSizeChangeAble)
            {
                isSizeChangeAble = false;
                if (this.Width < this.lastWidth || this.Height < this.lastHeight)
                {
                    this.Width = Math.Min(this.Width, this.Height);
                    this.Height = Math.Min(this.Width, this.Height);
                    this.lastWidth = this.Width;
                    this.lastHeight = this.Height;
                    base.OnSizeChanged(e);
                    isSizeChangeAble = true;
                    return;
                }
                if (this.Width > this.lastWidth || this.Height > this.lastHeight)
                {
                    this.Width = Math.Max(this.Width, this.Height);
                    this.Height = Math.Max(this.Width, this.Height);
                    this.lastWidth = this.Width;
                    this.lastHeight = this.Height;
                    base.OnSizeChanged(e);
                    isSizeChangeAble = true;
                    return;
                }
                this.lastWidth = this.Width;
                this.lastHeight = this.Height;
                base.OnSizeChanged(e);
                isSizeChangeAble = true;
                return;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            try
            {
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                if (this.value == 100)
                {
                    e.Graphics.FillEllipse(new SolidBrush(this.mainColor), new Rectangle(new Point(e.ClipRectangle.X + circularWidth / 2, e.ClipRectangle.Y + circularWidth / 2), new Size(e.ClipRectangle.Width - 1 - circularWidth, e.ClipRectangle.Height - 1 - circularWidth)));
                }
                using (Pen p = new Pen(Brushes.LightGray, circularWidth))
                {
                    //设置连续两段的联接样式  
                    p.LineJoin = LineJoin.Round;
                    e.Graphics.DrawEllipse(p, new Rectangle(new Point(e.ClipRectangle.X + circularWidth / 2, e.ClipRectangle.Y + circularWidth / 2), new Size(e.ClipRectangle.Width - 1 - circularWidth, e.ClipRectangle.Height - 1 - circularWidth)));
                }
                if (this.value < 100)
                {
                    using (Pen p = new Pen(new SolidBrush(this.mainColor), circularWidth))
                    {
                        //设置起止点线帽  
                        p.StartCap = lcp;
                        p.EndCap = lcp;
                        //设置连续两段的联接样式  
                        p.LineJoin = LineJoin.Round;
                        e.Graphics.DrawArc(p, new Rectangle(new Point(e.ClipRectangle.X + circularWidth / 2, e.ClipRectangle.Y + circularWidth / 2), new Size(e.ClipRectangle.Width - 1 - circularWidth, e.ClipRectangle.Height - 1 - circularWidth)), 45, (float)((float)this.value * 3.6));
                    }
                }
                if (this.value == 100)
                {
                    SizeF size = e.Graphics.MeasureString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold));
                    e.Graphics.DrawString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.White), new Point(this.Width / 2 - (int)size.Width / 2 - 1, this.Height / 2 - (int)size.Height / 2 + 2));
                    e.Graphics.DrawString("%", new Font("华文新魏", 9F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.White), new Point(this.Width / 2 + (int)size.Width / 2 - 4, this.Height / 2 - (int)size.Height + 18));
                }
                else
                {
                    SizeF size = e.Graphics.MeasureString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold));
                    e.Graphics.DrawString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.White), new Point(this.Width / 2 - (int)size.Width / 2 - 1, this.Height / 2 - (int)size.Height / 2 + 2));
                    e.Graphics.DrawString("%", new Font("华文新魏", 9F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.White), new Point(this.Width / 2 + (int)size.Width / 2 - 4, this.Height / 2 - (int)size.Height + 18));
                }
            }
            catch { }
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
    }
}
