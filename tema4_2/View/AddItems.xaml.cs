//using AndroidX.Lifecycle;
using tema4_2.ViewModel;
namespace tema4_2.View;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using tema4_2.Models;
using tema4_2.Services;
public partial class AddItems : Popup, IQueryAttributable
{
    private AddViewModel vm;
    //private readonly DbConnection _dbConnection;
    private ToDoModel _todo;
    private ToDoModel todos;
    public ToDoModel Todo
    {
        get
        {
            return todos;
        }
        set
        {
            todos = value;
        }
    }
    private ObservableCollection<string> items;

    public ObservableCollection<string> Items
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
        }
    }
    // public List<string> Items { get; set; }
    public AddItems(ToDoModel todo)
    {
        InitializeComponent();

        _todo = todo;
        //myTodo.Text = _todo.Name;
        Todo = new ToDoModel();
        Items = new ObservableCollection<string>();
        BindingContext = new AddViewModel(todo);

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Todo = query["Todo"] as ToDoModel;
        Todo.Ok = 0;
    }
    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        Console.WriteLine(sender);

        Close();
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {

        /*OccasionModel occasion = new OccasionModel
        {
            Date = OccasionDate.Date,
            Occasion = OccasionType.SelectedItem.ToString(),
            Notes = OccasionNotes.Text
        };
        
        Close(occasion);*/


        Close((BindingContext as AddViewModel).ToSaveOnDB);

    }

}