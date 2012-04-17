namespace Schema.UI.TreeViewList
{
    using System.Data;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using Aga.Controls.Tree;

    using Schema.Core.Helpers;
    using Schema.Core.Models;
    using Schema.Core.Reader;

    using Shema.Server.Models;

    /// <summary>
    /// Interaction logic for ServersTreeView.xaml
    /// </summary>
    public partial class ServersTreeView : UserControl
    {
        public ServersTreeView()
        {
            InitializeComponent();
            _tree.Model = new ServersTreeViewModel();
        }

        private TreeListItem GetParent(DependencyObject dependencyObject)
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            while (!(parent is TreeListItem || parent is TreeList))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

           return parent as TreeListItem;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            var parent = this.GetParent((DependencyObject)sender);
            if (parent == null)
            {
                return;
            }

            var dataBase = ((FrameworkElement)sender).DataContext as DataBaseModel;
            var serverModel = parent.Node.Parent.Tag as ServerModel;
              
            string passwordUsersOrIntegrated;
            if (!string.IsNullOrWhiteSpace(serverModel.Password) && !string.IsNullOrWhiteSpace(serverModel.UserName))
            {
                passwordUsersOrIntegrated = @";User Name=" + serverModel.UserName + @";Password=" + serverModel.Password + @";";
            }
            else
            {
                passwordUsersOrIntegrated = @";Integrated Security=true;";
            }

            if (dataBase == null)
            {
                return;
            }

            var connectionString = @"Data Source=" + serverModel.Name + @";Initial Catalog=" + dataBase.Name + passwordUsersOrIntegrated;


            var dataSet = new DataSet("dbDataSet");
            var mssqlReader = new MsSqlReader
                {
                    ConnectionString = connectionString,
                    DbName = dataBase.Name
                };
            var db = ModelFiller.GetModel(mssqlReader, dataSet);
            MainWindow.Model = db;
            foreach (var model in MainWindow.Model.Tables)
            {

                /*
                 * <ContentControl Width="130"
                    MinWidth="50"
                    Height="130"
                    MinHeight="50"
                    Canvas.Top="150"
                    Canvas.Left="470"
                    Template="{StaticResource DesignerItemTemplate}">
                <Ellipse Fill="Red"
               IsHitTestVisible="False" />
            </ContentControl>
                 */
                var contentControl = new ContentControl
                    {
                        MinWidth = 50,
                        Height = 130,
                        MinHeight = 50,
                        Template =
                            (ControlTemplate)
                            (MainWindow.serverWindow.Owner as MainWindow).Resources["DesignerItemTemplate"],
                        //"{StaticResource DesignerItemTemplate}"
                        Content = new Path
                        { 
                            Fill =new DrawingBrush(),
                            IsHitTestVisible=false
                        }
                    };


                (MainWindow.serverWindow.Owner as MainWindow).MainCanvas.Children.Add(contentControl);
               // (MainWindow.serverWindow.Owner as MainWindow).Stack.Children.Add(new TextBox { Text = model.Name });
            }

            
        }
       /* private void InitializeTable()
        {
           
            //Configure the window width

            this.Width = 700;

            /* The table object must be contained in a parent

                ontainer at the contrast of the grid one#1#

            var oDoc = new FlowDocument();

            // Create the Table object instance

            Table oTable = new Table();

            // Append the table to the document

            oDoc.Blocks.Add(oTable);
            // Create 5 columns and add them to the table's Columns collection.

            int numberOfColumns = 5;

            for (int x = 0; x < numberOfColumns; x++)
            {

                oTable.Columns.Add(new TableColumn());

                oTable.Columns[x].Width = new GridLength(130);

            }

            // Create and add an empty TableRowGroup Rows.

            oTable.RowGroups.Add(new TableRowGroup());

            // Add the table head row.

            oTable.RowGroups[0].Rows.Add(new TableRow());

            //Configure the table head row

            TableRow currentRow = oTable.RowGroups[0].Rows[0];

            currentRow.Background = Brushes.Navy;

            currentRow.Foreground = Brushes.White;



            // Add the header row with content,

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Country"))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Flag"))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Calling code"))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Internet TLD"))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Time zone"))));



            //Add Libya data row

            oTable.RowGroups[0].Rows.Add(new TableRow());

            currentRow = oTable.RowGroups[0].Rows[1];

            //Configure the row layout

            currentRow.Background = Brushes.White;

            currentRow.Foreground = Brushes.Navy;

            //Add the country name in the first cell

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Libya"))));

            Paragraph oParagraph0 = new Paragraph();

            //  oParagraph0.Background = new ImageBrush(bmp0);

            currentRow.Cells.Add(new TableCell(oParagraph0));

            //Add the calling code

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("218"))));

            //Add the internet TLD

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(".ly"))));

            //Add the Time Zone

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("GMT + 2 "))));

            //Add the given flow document to the window

            this.Content = oDoc;
            TextBox t=new TextBox{Text = "222"};
            MainWindow.MainGrid1.Children.Add(t);
            
        }*/

        }
}
