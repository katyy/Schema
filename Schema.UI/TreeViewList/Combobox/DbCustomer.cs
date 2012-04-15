
namespace Schema.UI.TreeViewList.Combobox
{
    using System.ComponentModel;

    public class DbCustomer :INotifyPropertyChanged
    {
         public string CustomerName { get; set; }

            int _selectedOption1;
            public int SelectedOption1
            {
                get
                {
                    return _selectedOption1;
                }
                set
                {
                    if (_selectedOption1 != value)   //  <--- put breakpoint here to prove it is updating source
                    {
                        _selectedOption1 = value;
                    }
                }
            }

            int _selectedOption2;
            public int SelectedOption2
            {
                get
                {
                    return _selectedOption2;
                }
                set
                {
                    if (_selectedOption2 != value)
                    {
                        _selectedOption2 = value;   //  <--- put breakpoint here to prove it is updating source
                        OnPropertyChanged("SelectedOption2");
                    }
                }
            }

            void OnPropertyChanged(string prop)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            public event PropertyChangedEventHandler PropertyChanged;

        }
    }

