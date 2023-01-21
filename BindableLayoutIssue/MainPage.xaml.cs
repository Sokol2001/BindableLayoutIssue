using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BindableLayoutIssue;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ModelA> ItemCollections = new();
    private int count = 0;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = ItemCollections;
    }

    private void OnCounterClicked1(object sender, EventArgs e)
    {
        ItemCollections.Add(new ModelA()
        {
            Header = Guid.NewGuid().ToString(),
            ItemCollection = new ObservableCollection<ModelB>(new List<ModelB>()
            {
                new ModelB()
                {
                    Header = Guid.NewGuid().ToString()
                }
            })
        });
    }
}

public class ModelA : INotifyPropertyChanged
{
    private ObservableCollection<ModelB> _itemCollection;
    public ObservableCollection<ModelB> ItemCollection
    {
        get => _itemCollection;
        set
        {
            _itemCollection = value;
            OnPropertyChanged(nameof(ItemCollection));
        }
    }

    private string _header;
    public string Header
    {
        get => _header;
        set
        {
            _header = value;
            OnPropertyChanged(nameof(Header));
        }
    }

    public Command DoSomeCommand => new Command(p =>
    {
        ItemCollection.Add(new ModelB()
        {
            Header = Guid.NewGuid().ToString()
        });
    });

    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class ModelB: INotifyPropertyChanged
{
    private string _header;
    public string Header
    {
        get => _header;
        set
        {
            _header = value;
            OnPropertyChanged(nameof(Header));
        }
    }
    
    
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}