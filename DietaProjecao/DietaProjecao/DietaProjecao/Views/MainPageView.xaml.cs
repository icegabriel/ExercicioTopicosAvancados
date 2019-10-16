using DietaProjecao.Views.Home;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietaProjecao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : TabbedPage
    {
        public MainPageView()
        {
            InitializeComponent();

            var dataService = new Models.DataService();

            Children.Add(new AlimentosView(dataService));
            Children.Add(new DietaView(dataService));
        }
    }
}