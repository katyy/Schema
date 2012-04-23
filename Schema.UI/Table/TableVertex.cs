// -----------------------------------------------------------------------
// <copyright file="SampleVertex.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Schema.UI.Table
{
    using System.ComponentModel;

    using Schema.Core.Models.Table;
    public class TableVertex : INotifyPropertyChanged
    {
        private bool active;

        private string text;

        public TableVertex(string text)
        {
            this.Text = text;
        }

        public TableVertex(TableModel model)
        {
            this.Text = model.Name;
            this.Model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Active
        {
            get
            {
                return this.active;
            }

            set
            {
                this.active = value;
                this.NotifyChanged("Active");
            }
        }
        
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.NotifyChanged("Text");
            }
        }

        public TableModel Model { get; set; }

        public void Change()
        {
            this.Active = !this.Active;
        }
        
        protected void NotifyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
     }
}
