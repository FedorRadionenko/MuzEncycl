using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace MuzEncycl
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private MainGenre selectedGenre;

        ApplicationContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        IEnumerable<MainGenre> mainGenres;

        public IEnumerable<MainGenre> MainGenres
        {
            get { return mainGenres; }
            set
            {
                mainGenres = value;
                OnPropertyChanged("MainGenres");
            }
        }
        public ApplicationViewModel()
        {
            db = new ApplicationContext();
            db.Genres.Load();
            MainGenres = db.Genres.Local.ToBindingList();
        }
        public MainGenre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                selectedGenre = value;
                OnPropertyChanged("SelectedGenre");
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        GenreWindow genreWindow = new GenreWindow(new MainGenre());
                        if (genreWindow.ShowDialog() == true)
                        {
                            MainGenre genre = genreWindow.NewGenre;
                            db.Genres.Add(genre);
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        MainGenre selectedGenre = selectedItem as MainGenre;
                        MainGenre gnr = new MainGenre()
                        {
                            Name = selectedGenre.Name,
                            Time = selectedGenre.Time,
                            Place = selectedGenre.Place
                        };
                        GenreWindow genreWindow = new GenreWindow(gnr);

                        if (genreWindow.ShowDialog() == true)
                        {
                            selectedGenre.Name = genreWindow.NewGenre.Name;
                            selectedGenre.Time = genreWindow.NewGenre.Time;
                            selectedGenre.Place = genreWindow.NewGenre.Place;
                            db.Entry(selectedGenre).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand((selectedGenre) =>
                    {
                        if (selectedGenre == null) return;
                        MainGenre genre = selectedGenre as MainGenre;
                        db.Genres.Remove(genre);
                        db.SaveChanges();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
