using Library.LibraryClass;
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
            //Вызываем метод получения данных пол запросу
            FetchDataFromApi();
       

        }

        /// <summary>
        /// Получение данных из api
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Обрабатываем нажатие на строку и на ячеку в данный момент на строку так как это получение данных  за константу 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }



   
    }
}