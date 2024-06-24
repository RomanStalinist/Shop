using Shop.Classes;
using System.Windows;

namespace Shop;

public partial class SignUpWindow : Window
{
    public SignUpWindow()
    {
        InitializeComponent();
    }

    private void SignUp(string login, string password)
    {
        using (Db db = new())
        {
            User user = new()
            {
                Login = login,
                Password = password
            };

            db.AddUser(user);
            App.User = user;
        }
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        new SignOnWindow().Show();
        Close();
    }

    private void SignUp_Click(object sender, RoutedEventArgs e)
    {
        if (PassBox.Text != RepPassBox.Text)
            MessageBox.Show("Пароли не совпадают!", "Ошибка");
        else
            try
            {
                SignUp(LoginBox.Text, PassBox.Text);
                MessageBox.Show("Успех!");
                new MainWindow().Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
    }
}
