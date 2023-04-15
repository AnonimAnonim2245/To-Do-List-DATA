﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using tema4_2.Models;
using tema4_2.Services;
using tema4_2.View;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using tema4_2.Messages;

namespace tema4_2.ViewModel
{
    [QueryProperty("Text", "Text")]
    public partial class AddViewModel : BaseViewModel
    {
        [ObservableProperty]
        List<ToDoModel> toDolist;

        [ObservableProperty]
        ToDoModel todo;

        [ObservableProperty]
        ToDoModel toSaveOnDB;

        private readonly Services.DbConnection _dbConnection;

        /* public AddViewModel(Services.DbConnection dbConnection)
         {
             _dbConnection = dbConnection;
             toDolist = new List<ToDoModel>();
             toSaveOnDB = new ToDoModel();
             GetInitalDataCommand.Execute(null);
         }*/
        public AddViewModel(ToDoModel todo)
        {
            ToSaveOnDB = todo;
            Value = todo.Id;
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
            await Shell.Current.GoToAsync(nameof(AddItem));
        }
        

        [RelayCommand]
        private async Task SaveOnDb()
        {
            await _dbConnection.SaveItemAsync(ToSaveOnDB);
            if (ToSaveOnDB.Name != null)
            {
                
                ToDolist = await _dbConnection.GetItemsAsync();
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async void MoveToANewTab1()
        {
            await Shell.Current.GoToAsync(nameof(AddItem));
        }

        [RelayCommand]
        async Task Back()
        {
           await Shell.Current.GoToAsync("..");

        }
    }
 }
