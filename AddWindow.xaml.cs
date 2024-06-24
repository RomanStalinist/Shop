using Shop.Classes;
using System.Windows;

namespace Shop;

public partial class AddWindow : Window
{
    public AddWindow()
    {
        InitializeComponent();
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text;
        string desc = DescBox.Text;
        double price = PriceBox.Value;

        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(desc))
            MessageBox.Show("Не все поля заполнены", "Ошибка");
        else
            try
            {
                using (Db db = new())
                {
                    db.AddThing(new Thing
                    {
                        Name = name,
                        Description = desc,
                        Price = price
                    });
                }
                MessageBox.Show("Вещь добавлена успешно");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
    }
}
