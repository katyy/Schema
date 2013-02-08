namespace Schema.UI.Table
{
    using System.ComponentModel;

    using Core.Models.Table;

    public class TableVertex : INotifyPropertyChanged
    {
        private bool _active;

        private string _text;

        public TableVertex(string text)
        {
            Text = text;
        }

        public TableVertex(TableModel model)
        {
            Text = model.Name;
            Model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Active
        {
            get
            {
                return _active;
            }

            set
            {
                _active = value;
                NotifyChanged("Active");
            }
        }
        
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                NotifyChanged("Text");
            }
        }

        public TableModel Model { get; set; }

        public void Change()
        {
            Active = !Active;
        }
        
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
     }
}
