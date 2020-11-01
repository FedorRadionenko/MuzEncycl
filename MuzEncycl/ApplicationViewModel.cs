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
        ApplicationContext db;

        private MainGenre selectedMainGenre;
        private SubGenre selectedSubGenre;

        RelayCommand addMainCommand;
        RelayCommand editMainCommand;
        RelayCommand deleteMainCommand;

        RelayCommand addSubCommand;
        RelayCommand editSubCommand;
        RelayCommand deleteSubCommand;

        IEnumerable<MainGenre> mainGenres;
        IEnumerable<SubGenre> subGenres;

        public IEnumerable<MainGenre> MainGenres
        {
            get { return mainGenres; }
            set
            {
                mainGenres = value;
                OnPropertyChanged("MainGenres");
            }
        }
        public IEnumerable<SubGenre> SubGenres
        {
            get { return subGenres; }
            set
            {
                subGenres = value;
                OnPropertyChanged("SubGenres");
            }
        }

        public ApplicationViewModel()
        {
            db = new ApplicationContext();
            db.MainGenres.Load();
            MainGenres = db.MainGenres.Local.ToBindingList();
        }
        public MainGenre SelectedMainGenre
        {
            get { return selectedMainGenre; }
            set
            {
                selectedMainGenre = value;
                OnPropertyChanged("SelectedMainGenre");
            }
        }
        public SubGenre SelectedSubGenre
        {
            get { return selectedSubGenre; }
            set
            {
                selectedSubGenre = value;
                OnPropertyChanged("SelectedSubGenre");
            }
        }
        public RelayCommand AddMainCommand
        {
            get
            {
                return addMainCommand ??
                    (addMainCommand = new RelayCommand(obj =>
                    {
                        MainGenreWindow genreWindow = new MainGenreWindow(new MainGenre());
                        if (genreWindow.ShowDialog() == true)
                        {
                            MainGenre genre = genreWindow.NewGenre;
                            db.MainGenres.Add(genre);
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand EditMainCommand
        {
            get
            {
                return editMainCommand ??
                    (editMainCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        MainGenre selectedGenre = selectedItem as MainGenre;
                        MainGenre gnr = new MainGenre()
                        {
                            Name = selectedGenre.Name,
                            Time = selectedGenre.Time,
                            Place = selectedGenre.Place
                        };
                        MainGenreWindow genreWindow = new MainGenreWindow(gnr);

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
        public RelayCommand DeleteMainCommand
        {
            get
            {
                return deleteMainCommand ??
                    (deleteMainCommand = new RelayCommand((selectedGenre) =>
                    {
                        if (selectedGenre == null) return;
                        MainGenre genre = selectedGenre as MainGenre;
                        db.MainGenres.Remove(genre);
                        db.SaveChanges();
                    }));
            }
        }
        public RelayCommand AddSubCommand
        {
            get
            {
                return addSubCommand ??
                    (addSubCommand = new RelayCommand(obj =>
                    {
                        SubGenreWindow genreWindow = new SubGenreWindow(new SubGenre());
                        if (genreWindow.ShowDialog() == true)
                        {
                            SubGenre genre = genreWindow.NewGenre;
                            db.SubGenres.Add(genre);
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand EditSubCommand
        {
            get
            {
                return editSubCommand ??
                    (editSubCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        SubGenre selectedGenre = selectedItem as SubGenre;
                        SubGenre gnr = new SubGenre()
                        {
                            Name = selectedGenre.Name,
                            Time = selectedGenre.Time,
                            Place = selectedGenre.Place
                        };
                        SubGenreWindow genreWindow = new SubGenreWindow(gnr);

                        if (genreWindow.ShowDialog() == true)
                        {
                            selectedGenre.Name = genreWindow.NewGenre.Name;
                            selectedGenre.Genre = genreWindow.NewGenre.Genre;
                            selectedGenre.Time = genreWindow.NewGenre.Time;
                            selectedGenre.Origin = genreWindow.NewGenre.Origin;
                            selectedGenre.Place = genreWindow.NewGenre.Place;
                            db.Entry(selectedGenre).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand DeleteSubCommand
        {
            get
            {
                return deleteSubCommand ??
                    (deleteSubCommand = new RelayCommand((selectedGenre) =>
                    {
                        if (selectedGenre == null) return;
                        SubGenre genre = selectedGenre as SubGenre;
                        db.SubGenres.Remove(genre);
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
