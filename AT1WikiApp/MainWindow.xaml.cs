using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AT1WikiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Static variables for the dimensions of the 2D string array
        private static int numRows = 12;
        private static int numCols = 4;
        private int ptr = 0;
        // Global 2D string array
        private string?[,] stringArray = new string[numRows, numCols];

        public MainWindow()
        {
            InitializeComponent();
            statusBar.Items.Add(new TextBlock() { Text = "Status Bar" });
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if any of the text boxes are empty
            if (string.IsNullOrEmpty(nameBox.Text) || string.IsNullOrEmpty(structureBox.Text) || string.IsNullOrEmpty(definitionBox.Text) || string.IsNullOrEmpty(categoryBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Info");
                return;
            }

            // Create a string array for the row data
            string[] rowArray = new string[] { nameBox.Text, structureBox.Text, definitionBox.Text, categoryBox.Text };

            // Add the row data to the 2D array
            int rowIndex = listBox.Items.Count - 1;
            if (ptr < 12)
            {
                stringArray[ptr, 0] = rowArray[0];
                stringArray[ptr, 1] = rowArray[1];
                stringArray[ptr, 2] = rowArray[2];
                stringArray[ptr, 3] = rowArray[3];
                ptr++;

                // Add the row data to the ListBox
                listBox.Items.Add(new { Name = rowArray[0], Structure = rowArray[1], Definition = rowArray[2], Category = rowArray[3] });
            }

            // Clear the text boxes
            nameBox.Clear();
            structureBox.Clear();
            definitionBox.Clear();
            categoryBox.Clear();


            MessageBox.Show("The entry was added.", "Info");


            /*            statusBarItem.Content = "Entry added!";*/
            /*            statusBar.Items.Clear();*/
            statusBar.Items.Add("Entry added!");
            /*statusBar.Items.Add(new StatusBarItem { Content = "Entry added!" });*/

        }


        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the ListBox
            dynamic selectedItem = listBox.SelectedItem;

            // Create a new string array from the selected item's properties
            string[] rowArray = new string[]
            {
                selectedItem.Name,
                selectedItem.Structure,
                selectedItem.Definition,
                selectedItem.Category
            };

            // Update the row data
            rowArray[0] = nameBox.Text;
            rowArray[1] = structureBox.Text;
            rowArray[2] = definitionBox.Text;
            rowArray[3] = categoryBox.Text;

            // Re-assign the ListBox item with the updated row data
            listBox.Items[listBox.SelectedIndex] = new
            {
                Name = rowArray[0],
                Structure = rowArray[1],
                Definition = rowArray[2],
                Category = rowArray[3]
            };

            // Clear the text boxes
            nameBox.Clear();
            structureBox.Clear();
            definitionBox.Clear();
            categoryBox.Clear();

            MessageBox.Show("The entry was edited.", "Info");
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                // Get the selected row from the ListBox and cast it to the appropriate anonymous type
                var selectedRow = (dynamic)listBox.SelectedItem;

                // Populate the text boxes with the values from the selected row's properties
                nameBox.Text = selectedRow.Name;
                structureBox.Text = selectedRow.Structure;
                definitionBox.Text = selectedRow.Definition;
                categoryBox.Text = selectedRow.Category;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                // Get the selected index from the ListBox
                int selectedIndex = listBox.SelectedIndex;

                // Display a message box warning
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this entry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Remove the selected item from the ListBox
                    listBox.Items.RemoveAt(selectedIndex);

                    // Set the corresponding entry in the string array to null
                    stringArray[selectedIndex, 0] = null;
                    stringArray[selectedIndex, 1] = null;
                    stringArray[selectedIndex, 2] = null;
                    stringArray[selectedIndex, 3] = null;

                    // Display a message box confirmation
                    MessageBox.Show("Entry has been deleted.", "Info");
                }
            }
            else
            {
                // Display a message box error if no item is selected
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the text boxes
            nameBox.Clear();
            structureBox.Clear();
            definitionBox.Clear();
            categoryBox.Clear();

            MessageBox.Show("The text boxes were cleared.", "Info");
        }
    }
}
