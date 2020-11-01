using System.Windows;

namespace MuzEncycl
{
    /// <summary>
    /// Логика взаимодействия для GenreWindow.xaml
    /// </summary>
    public partial class MainGenreWindow : Window
    {
        public MainGenre NewGenre { get; private set; }
        public MainGenreWindow(MainGenre genre)
        {
            InitializeComponent();
            NewGenre = genre;
            this.DataContext = NewGenre;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
