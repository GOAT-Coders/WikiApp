using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    



        private void BubbleSort(string?[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < arr.GetLength(0) - i - 1; j++)
                {
                    if (string.Compare(arr[j, 0], arr[j + 1, 0]) > 0)
                    {
                        if (arr[j + 1, 0] != null)
                        {
                            Swap(arr, j, j + 1);
                        }
                    }
                }
            }
            DisplayString(arr);
        }

        private void bubbleSortButton_Click(object sender, EventArgs e)
        {
            BubbleSort(stringArray);
        }

        private void Swap(string?[,] arr, int x, int y)
        {
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                string temp = arr[x, i];
                arr[x, i] = arr[y, i];
                arr[y, i] = temp;
            }
        }

        private void DisplayString(string?[,] arr)
        {
            listBox.Items.Clear();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (arr[i, 0] != null)
                {
                    listBox.Items.Add(new { Name = stringArray[i, 0], Structure = stringArray[i, 1], Definition = stringArray[i, 2], Category = stringArray[i, 3] });
                }
            }
            MessageBox.Show("The array was bubble sorted.", "Info");
        }



        private void binarySearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the name to search for from the searchTextBox
            string searchName = binarySearchBox.Text;

            // Sort the array using BubbleSort
            BubbleSort(stringArray);

            // Perform binary search on the name column of the sorted stringArray
            int row = BinarySearch(stringArray, searchName, 0);

            // Check if the search was successful
            if (row == -1)
            {
                // Display an error message if the name was not found
                MessageBox.Show($"The name '{searchName}' was not found in the array.", "Error");
            }
            else
            {
                // Display the information corresponding to the name if found
                nameBox.Text = stringArray[row, 0];
                structureBox.Text = stringArray[row, 1];
                definitionBox.Text = stringArray[row, 2];
                categoryBox.Text = stringArray[row, 3];
            }

            // Clear the searchTextBox
            binarySearchBox.Text = "";
        }




        private int BinarySearch(string?[,] array, string searchValue, int searchColumn)
        {
            // Initialize the min and max variables that will be used to keep track of the range of indexes in the array to search.
            int min = 0;
            int max = 0;

            // Loop through the first dimension of the array
            for (int i = 0; i < array.GetLength(0); i++)
            {
                // Check if the element in the first column of the current row is null
                if (array[i, 0] == null)
                {
                    // If it is null, store the index of the current row in 'max' variable and break the loop
                    max = i;
                    break;
                }
            }

            // If no element in the first column of the array is null, set 'max' to the length of the first dimension of the array
            if (max == 0) max = array.GetLength(0);


            // Use a while loop to continue searching while there are still indexes to search.
            while (min <= max)
            {
                // Calculate the mid index between min and max.
                int mid = (min + max) / 2;

                // Compare the searchValue with the value at the mid index in the name column.
                string midValue = array[mid, 0];

                // Compare the value at the mid index with the search value and store the result in the compareResult variable.
                int compareResult = string.Compare(midValue, searchValue);

                // If the searchValue is found at the mid index, return the index.
                if (compareResult == 0)
                {
                    MessageBox.Show($"The name '{searchValue}' was found in the array at index {mid}.", "Info");

                    // Found the searchValue
                    return mid;
                }
                // If the searchValue is greater than the value at the mid index, search the right half of the array.
                else if (compareResult < 0)
                {
                    // searchValue is greater than mid, search the right half of the array
                    min = mid + 1;
                }
                else
                {
                    // searchValue is less than mid, search the left half of the array
                    max = mid - 1;
                }
            }

            // searchValue was not found, return -1.
            return -1;

        }





        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // Set the default file name to "definitions.dat"
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "definitions.dat";
            saveFileDialog.Filter = "Data Files (*.dat)|*.dat|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() != true)
            {
                // User canceled the dialog
                return;
            }

            // Sort the array by name before saving
            BubbleSort(stringArray);

            // Open the file stream and binary writer
            using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                int rowCount = stringArray.GetLength(0);
                int columnCount = stringArray.GetLength(1);

                // Write the number of rows and columns
                binaryWriter.Write(rowCount);
                binaryWriter.Write(columnCount);

                // Write the data
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        string value = stringArray[i, j] ?? string.Empty;
                        if (!string.IsNullOrEmpty(value))
                        {
                            binaryWriter.Write(value);
                        }
                        else
                        {
                            binaryWriter.Write(string.Empty);
                        }
                    }

                }
            }
            MessageBox.Show("The entries were saved.", "Info");
        }





        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Binary files (*.dat)|*.dat";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                    {
                        BinaryReader binaryReader = new BinaryReader(fileStream);
                        int rowCount = binaryReader.ReadInt32();
                        int columnCount = binaryReader.ReadInt32();
                        string?[,] array = new string[rowCount, columnCount];

                        for (int i = 0; i < rowCount; i++)
                        {
                            for (int j = 0; j < columnCount; j++)
                            {
                                string value = binaryReader.ReadString();
                                array[i, j] = value;
                            }
                        }

                        binaryReader.Close();
                        stringArray = array;

                        // Clear and update the ListBox
                        listBox.Items.Clear();
                        for (int i = 0; i < rowCount; i++)
                        {
                            string item = "";
                            for (int j = 0; j < columnCount; j++)
                            {
                                string value = array[i, j] ?? "";
                                item += value + "\t";
                            }
                            // if (array[i, 0] != null && array[i, 1] != null && array[i, 2] != null && array[i, 3] != null)
                            if (stringArray[i, 0] != null && stringArray[i, 1] != null && stringArray[i, 2] != null && stringArray[i, 3] != null)
                            {
                                listBox.Items.Add(new { Name = stringArray[i, 0], Structure = stringArray[i, 1], Definition = stringArray[i, 2], Category = stringArray[i, 3] });
                            }
                        }
                    }
                    MessageBox.Show("The entries were loaded.", "Info");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
