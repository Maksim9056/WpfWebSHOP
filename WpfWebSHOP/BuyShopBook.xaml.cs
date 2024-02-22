using Library.LibraryClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;
using static WpfWebSHOP.MainWindow;

namespace WpfWebSHOP
{
    /// <summary>
    /// Логика взаимодействия для BuyShopBook.xaml
    /// </summary>
    public partial class BuyShopBook : Window
    {
       public int id {  get; set; }
        /// <summary>
        /// Инициализация формы с параметрами
        /// </summary>
        /// <param name="book"></param>
        public BuyShopBook(Book book)
        {
            InitializeComponent();
            Id.Content = book.Id;
            id = book.Id;
            Auther.Content = book.Author;
             Name.Content = book.Name;
            Time.Content = book.Year_of_publication;

        }

        /// <summary>
        /// Нажатия на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FetchDataFromApiDelete();
        }

        /// <summary>
        /// Блок работы С api Удаление Delete
        /// </summary>
        /// <returns></returns>
        private async Task FetchDataFromApiDelete()
        {
            List<Book> books = new List<Book>();
            try
            {
                // Create an instance of HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // API endpoint
                    string url = $"https://localhost:7224/api/Books/{id}";

                    // Send a GET request to the API
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string jsonString = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON string to a list of Book objects
                        books = JsonConvert.DeserializeObject<List<Book>>(jsonString);
                        // Return the list of books
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve data from the API. Status code: " + response.StatusCode);
                    }
                   MainWindow buyShopBook = new MainWindow(); 
                    buyShopBook.Show(); // показываем окно
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            // Return an empty list if there is an error or no data retrieved
        }

        private void Calback(object sender, RoutedEventArgs e)
        {
            MainWindow buyShopBook = new MainWindow();
            buyShopBook.Show(); 
            this.Close();
        }
    }
}
