using Shop.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Shop;

public partial class ShopPage : Page
{
    public ShopPage()
    {
        InitializeComponent();
        using (Db db = new())
        {
            ThingsList.ItemsSource = db.GetThings();
        }
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button b || b.Tag is not long id)
            return;

        using (Db db = new())
        {
            db.RemoveThingById(id);
            ThingsList.ItemsSource = db.GetThings();
        }
    }

    private void Find_Click(object sender, RoutedEventArgs e)
    {
        string query = FindBox.Text;
        using (Db db = new())
        {
            ThingsList.ItemsSource = db.GetThingsByLikeQuery(query);
        }
    }
}
