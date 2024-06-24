using System.Windows;

namespace Shop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UserBlock.Text = App.User.Login;
        CurrentFrame.Content = new ShopPage();
    }

    private void Quit_Click(object sender, RoutedEventArgs e)
    {
        App.User = null;
        new SignOnWindow().Show();
        Close();
    }

    private void Shop_Click(object sender, RoutedEventArgs e)
    {
        CurrentFrame.Content = new ShopPage();
    }

    private void Users_Click(object sender, RoutedEventArgs e)
    {
        CurrentFrame.Content = new UsersPage();
    }

    private void AddThing_Click(object sender, RoutedEventArgs e)
    {
        new AddWindow().Show();
    }
}
