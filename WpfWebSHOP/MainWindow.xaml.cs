using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace WpfWebSHOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Book> Books = new List<Book>();

        public MainWindow()
        {
            InitializeComponent();

            FetchDataFromApi();
            //Task.Run(() => FetchDataFromApi());
            //var a = Books.Result;

        }


        private async Task FetchDataFromApi()
        {
            List<Book> books = new List<Book>();
            try
            {
                // Create an instance of HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // API endpoint
                    string url = "https://localhost:7224/api/Books";

                    // Send a GET request to the API
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string jsonString = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON string to a list of Book objects
                        books = JsonConvert.DeserializeObject<List<Book>>(jsonString);
                        Books = books;
                        // Return the list of books
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve data from the API. Status code: " + response.StatusCode);
                    }
                }
                Data.ItemsSource = Books;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            // Return an empty list if there is an error or no data retrieved
        }
        private void Data_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            // Get the selected cell
            //DataGridCellInfo cellInfo = Data.SelectedCells[0];
            object cellInfo1 = Data.SelectedCells[0].Item;

            Book book = (Book)cellInfo1;
            //this.  book
            BuyShopBook buyShopBook = new BuyShopBook(book); // создаем окно BuyShopBook и передаем ему объект book в качестве параметра
            buyShopBook.Show(); // показываем окно
            this.Close();
            // buyShopBook = new BuyShopBook(book);
            //buyShopBook.Owner.();
            // Get the value of the selected cell
            //string cellValue = ((TextBlock)cellInfo.Column.GetCellContent(cellInfo.Item)).Text;
            //var select = Books[cellValue];
            // Display the value in a message box
            //MessageBox.Show(cellValue);
        }



        public class Book
        {
            public int Id { get; set; }
            public string Author { get; set; }

            public string Name { get; set; }
            public string Year_of_publication { get; set; }
            public Book(int id, string author, string name, string year_of_publication)
            {
                Id = id;
                Author = author;
                Name = name;
                Year_of_publication = year_of_publication;
            }

            public Book()
            {
            }
        }
    }
}