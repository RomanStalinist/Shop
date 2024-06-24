using Shop.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Shop;

public partial class UsersPage : Page
{
    public UsersPage()
    {
        InitializeComponent();
        using (Db db = new())
        {
            UsersList.ItemsSource = db.GetUsers();
        }
    }

    private void Find_Click(object sender, RoutedEventArgs e)
    {
        string query = FindBox.Text;
        using (Db db = new())
        {
            UsersList.ItemsSource = db.GetUsersByLikeLogin(query);
        }
    }
}
