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
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VetManagementApp.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }


    /// <summary>
    /// Custom class implementing the TriggerAction which enables to set properties of associated objects on specified event trigger
    /// </summary>
    public class SetterAction : TriggerAction<DependencyObject>
    {
        public SetterAction()
        {
            Setters = new List<Setter>();
        }

        public List<Setter> Setters { get; set; }

        protected override void Invoke(object parameter)
        {
            foreach (var item in Setters)
            {
                AssociatedObject.SetValue(item.Property, item.Value);
            }
        }
    }
}
