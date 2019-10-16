using DietaProjecao.Models;
using DietaProjecao.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietaProjecao.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlimentosView : ContentPage
    {
        private DataService DataService { get; }

        public AlimentosView(DataService dataService)
        {
            InitializeComponent();

            DataService = dataService;

            BindingContext = new AlimentosViewModel(dataService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Alimento>(this, "AlimentoSelecionado", async (alimento) =>
            {
                alimentosListView.SelectedItem = null;

                await Navigation.PushAsync(new AlimentoDetalheView(DataService, alimento));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Alimento>(this, "AlimentoSelecionado");
        }
    }
}