using DietaProjecao.Models;
using DietaProjecao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietaProjecao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlimentoDetalheView : ContentPage
    {
        public AlimentoDetalheView(DataService dataService, Alimento alimento)
        {
            InitializeComponent();

            BindingContext = new AlimentoDetalheViewModel(dataService, alimento);
        }
    }
}