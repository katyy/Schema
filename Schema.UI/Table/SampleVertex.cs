// -----------------------------------------------------------------------
// <copyright file="SampleVertex.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Schema.UI.Table
{
    using System.ComponentModel;

    using Schema.Core.Models.Table;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SampleVertex : INotifyPropertyChanged
    {
        private TableModel _tableModel;
        private bool _active;

        private string _text;

        public bool Active
        {
            get
            {
                return this._active;
            }

            set
            {
                this._active = value;
                this.NotifyChanged("Active");
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                this.NotifyChanged("Text");
            }
        }
        public TableModel Table
        {
            get
            {
                return this._tableModel;
            }
            set
            {
                this._tableModel = value;
            }
        }


        public SampleVertex(string text)
        {
            this.Text = text;
        }

        public SampleVertex(string text,TableModel model)
        {
            this.Text = text;
            this.Table = model;
        }

        public void Change()
        {
            this.Active = !this.Active;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
