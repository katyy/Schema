// -----------------------------------------------------------------------
// <copyright file="MoveThumb.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Schema.UI.MoveResize
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class MoveThumb : Thumb
    {
       #region Constructors

        public MoveThumb()
            : base()
        {
            this.StartLines = new List<LineGeometry>();
            this.EndLines = new List<LineGeometry>();
            DragDelta += this.MoveThumbDragDelta;
        }

        //  public MoveThumb(ControlTemplate template, string title, DragDeltaEventHandler dragDelta)
        //    : this()
        //{
        //    this.DragDelta += dragDelta;
        //}

        public MoveThumb(Point position)
        {
            this.SetPosition(position);
        }

        #endregion

        #region Properties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        
        public List<LineGeometry> EndLines { get; private set; }

        public List<LineGeometry> StartLines { get; private set; }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MoveThumb), new UIPropertyMetadata(string.Empty));
        
        #endregion

        private void MoveThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = e.Source as MoveThumb;

            if (thumb == null)
            {
                return;
            }

            var left = Canvas.GetLeft(thumb);
            var top = Canvas.GetTop(thumb);

            Canvas.SetLeft(thumb, left + e.HorizontalChange);
            Canvas.SetTop(thumb, top + e.VerticalChange);
            thumb.UpdateLinks();
        }



        public void UpdateLinks()
        {
            double left = Canvas.GetLeft(this);
            double top = Canvas.GetTop(this);

            for (int i = 0; i < this.StartLines.Count; i++)
                this.StartLines[i].StartPoint = new Point(left + this.ActualWidth / 2, top + this.ActualHeight / 2);

            for (int i = 0; i < this.EndLines.Count; i++)
                this.EndLines[i].EndPoint = new Point(left + this.ActualWidth / 2, top + this.ActualHeight / 2);
        }

        public void SetPosition(Point value)
        {
            Canvas.SetLeft(this, value.X);
            Canvas.SetTop(this, value.Y);
        }

        #region Linking logic
        // This method establishes a link between current thumb and specified thumb.
        // Returns a line geometry with updated positions to be processed outside.
        public LineGeometry LinkTo(MoveThumb target)
        {
            // Create new line geometry
            LineGeometry line = new LineGeometry();
            // Save as starting line for current thumb
            this.StartLines.Add(line);
            // Save as ending line for target thumb
            target.EndLines.Add(line);
            // Ensure both tumbs the latest layout
            this.UpdateLayout();
            target.UpdateLayout();
            // Update line position
            line.StartPoint = new Point(Canvas.GetLeft(this) + this.ActualWidth / 2, Canvas.GetTop(this) + this.ActualHeight / 2);
            line.EndPoint = new Point(Canvas.GetLeft(target) + target.ActualWidth / 2, Canvas.GetTop(target) + target.ActualHeight / 2);
            // return line for further processing
            return line;
        }

        // This method establishes a link between current thumb and target thumb using a predefined line geometry
        // Note: this is commonly to be used for drawing links with mouse when the line object is predefined outside this class
        //public bool LinkTo(MoveThumb target, LineGeometry line)
        //{
        //    // Save as starting line for current thumb
        //    this.StartLines.Add(line);
        //    // Save as ending line for target thumb
        //    target.EndLines.Add(line);
        //    // Ensure both tumbs the latest layout
        //    this.UpdateLayout();
        //    target.UpdateLayout();
        //    // Update line position
        //    line.StartPoint = new Point(Canvas.GetLeft(this) + this.ActualWidth / 2, Canvas.GetTop(this) + this.ActualHeight / 2);
        //    line.EndPoint = new Point(Canvas.GetLeft(target) + target.ActualWidth / 2, Canvas.GetTop(target) + target.ActualHeight / 2);
        //    return true;
        //}
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

           if (this.Title == string.Empty)
            {
                return;
            }

            var txt = this.Template.FindName("tplTextBlock", this) as TextBlock;
            if (txt != null)
            {
                txt.Text = this.Title;
            }
        }
    }
}
