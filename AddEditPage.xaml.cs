using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Karimov_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentServise = new Service();

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();
           
            if (SelectedService != null)
                _currentServise = SelectedService;

            DataContext = _currentServise;
        }

        private void SaveBitton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentServise.Title))
                errors.AppendLine("Укажите название услуги");
            if (_currentServise.Cost <= 0)
            {
                errors.AppendLine("Укажите правильно стоимость услуги");
            }
            if (_currentServise.Cost < 0 || _currentServise.Discount == null)
            {
                errors.AppendLine("Укажите правильно скидку");
            }
            if (_currentServise.DurationInSeconds == 0)
            {
                errors.AppendLine("Укажите длительность услуг");
            }
            if (_currentServise.DurationInSeconds > 240)
            {
                errors.AppendLine("Длительность не может быть больше 240 минут");
            }
            if (_currentServise.DurationInSeconds < 0)
            {
                errors.AppendLine("Длительность не может быть меньше нуля");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentServise.ID == 0)
                KarimovAutoservisEntities.GetContext().Service.Add(_currentServise);
            try
            {
                KarimovAutoservisEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            var allServices = KarimovAutoservisEntities.GetContext().Service.ToList();
            allServices = allServices.Where(p => p.Title == _currentServise.Title).ToList();

            if (allServices.Count == 0)
            {
                if (_currentServise.ID == 0)
                    KarimovAutoservisEntities.GetContext().Service.Add(_currentServise);
                try
                {
                    KarimovAutoservisEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Уже существует");
            }
        }
    }
}
