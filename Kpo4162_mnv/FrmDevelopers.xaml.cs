using System.Windows;
using SubdDevelopers.source;

namespace Kpo4162_mnv.Main
{
    /// <summary>
    /// Interaction logic for FrmDevelopers.xaml
    /// </summary>
    public partial class FrmDevelopers : Window
    {
        private Developer _developer;

        public Developer Developer => _developer;

        public FrmDevelopers()
        {
            InitializeComponent();
            _developer = null;
        }

        public void SetDeveloper(Developer developer)
        {
            _developer = developer;
            Name.Text = developer.Name;
            ProductCount.Text = developer.ProductCount.ToString();
            Proceeds.Text = developer.Proceeds.ToString();
            MarketPercentage.Text = developer.MarketPercentage.ToString();
        }
    }
}
