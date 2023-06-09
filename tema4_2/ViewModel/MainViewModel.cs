﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using tema4_2.Models;
using tema4_2.Services;
using tema4_2.View;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
//using Android.OS;

namespace tema4_2.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        List<ToDoModel> toDolist;

        [ObservableProperty]
        ToDoModel todo;

        [ObservableProperty]
        ToDoModel toSaveOnDB;
        [ObservableProperty]
        ToDoModel toDeleteOnDB;

        private readonly DbConnection _dbConnection;
        private readonly ObservableCollection<ToDoModel> models;
        //public ICommand DeleteCommand { get; }


        public MainViewModel(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            toDolist = new List<ToDoModel>();
            toSaveOnDB = new ToDoModel();
            todo = new ToDoModel();
            //models = new ObservableCollection<ToDoModel>();
            //DeleteCommand = new AsyncRelayCommand(DeleteModelAsync);
            GetInitalDataCommand.Execute(null);
            
        }

        [RelayCommand]
        private async void GetInitalData()
        {
            ToDolist = await _dbConnection.GetItemsAsync();
        }
        [ObservableProperty]
        ObservableCollection<string> items;
        [ObservableProperty]
        string text;

        [RelayCommand]

        private async void GoToBasicNavigation()
        {
            Todo.Id = -5;
            var navigationParameter = new Dictionary<string, object>
                {
            { "Todo", Todo }
                };
            await Shell.Current.GoToAsync($"{nameof(AddItem)}", navigationParameter);
            //ToSaveOnDB.Name = null;
        }

        [RelayCommand]
        private async void GoToMoreInfo(ToDoModel todo)
        {
            Todo.Id = -3;
            var navigationParameter = new Dictionary<string, object>
                {
            { "Todo", todo }
                };

            await Shell.Current.GoToAsync($"{nameof(EditItem)}", navigationParameter);
           // ToSaveOnDB.Name = null;
        }
        [RelayCommand]
        public async Task DeleteOnDb(ToDoModel todo)
        {
            ToDolist.Remove(todo);
            await _dbConnection.DeleteItemAsync(todo);
        }
        [RelayCommand]
        private async void SaveOnDb()
        {

            await _dbConnection.SaveItemAsync(ToSaveOnDB);
                       

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("IdUser") && Todo.Id == -3)
            {
                Console.WriteLine(Todo.Ok);
                var id = (int)query["IdUser"];
                var todoItem = ToDolist.Where(x => x.Id == id).FirstOrDefault();
                ToDolist.Remove(todoItem);
                Console.WriteLine("1");
                //el=Preferences.Default.Get("huh", 2);
                Console.WriteLine("Before " + query);
                //query = null;
                query = new Dictionary<string, object>();
                Console.WriteLine("After " + query);

            }
            if (query.ContainsKey("NameUser") && Todo.Id == -5)
            {

                Console.WriteLine(Todo.Ok);

                var element = (ToDoModel)query["NameUser"];

                if (element == null)
                    return;
                ToSaveOnDB = element;

                Console.WriteLine("2");
                Console.WriteLine("Before " + query);

                //SaveOnDbCommand.Execute(null);
                ToDolist.Add(element);
                //query = null;
                query = new Dictionary<string, object>();
                Console.WriteLine("After " + query);
                ToSaveOnDB = null;
                //element.Name = null;
                //await _dbConnection.SaveItemAsync(element);


                //ToDolist.Add(element);
            }



        }

    }
    
}
