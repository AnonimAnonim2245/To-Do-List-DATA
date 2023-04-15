using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using tema4_2.Models;
using tema4_2.Services;
using tema4_2.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace tema4_2.ViewModel
{
    [QueryProperty("Text", "Text")]

    public partial class EditViewModel : BaseViewModel
    {
        
        public List<ToDoModel> ToDolist { get; set; }

        public ICommand CloseCommand { get; set; }
        public ObservableCollection<string> Items { get; set; }
        //[ObservableProperty]
        private ToDoModel _todo;
        public ToDoModel Todo
        {
            get { return _todo; }
            set
            {
                if (_todo != value)
                {
                    _todo = value;
                    OnPropertyChanged(nameof(Todo));
                }
            }
        }
        

        public ToDoModel ToSaveOnDb { get; set; }

        public string Text { get; set; }

        //private readonly DbConnection _dbConnection;


        public EditViewModel(ToDoModel todo)
        {
            Title = todo.Name;
            Value = todo.Id;
        }

    }


}