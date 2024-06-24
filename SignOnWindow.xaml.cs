using Shop.Classes;
using System.Windows;

namespace Shop;

public partial class SignOnWindow : Window
{
    private string Password { get; set; }

    public SignOnWindow()
    {
        InitializeComponent();
    }

    public void Login(string login, string password)
    {
        using (Db db = new())
        {
            try
            {
                App.User = db.GetUserByLoginAndPassword(login, password);
                new MainWindow().Show();
                Close();
            }
            catch
            {
                MessageBox.Show("Пользователь не найден", "Ошибка авторизации");
            }
        }
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        Login(LoginBox.Text, PassBox.Text);
    }

    private void SignUp_Click(object sender, RoutedEventArgs e)
    {
        new SignUpWindow().Show();
        Close();
    }
}