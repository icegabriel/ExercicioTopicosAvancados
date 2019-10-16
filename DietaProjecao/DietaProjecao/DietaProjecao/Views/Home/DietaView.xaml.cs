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
    public partial class DietaView : ContentPage
    {
        private DataService DataService { get; }

        private DietaViewModel ViewModel { get; }

        public DietaView(DataService dataService)
        {
            InitializeComponent();

            DataService = dataService;

            ViewModel = new DietaViewModel(dataService);
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.RefreshDietaViewAsync();

            toolBarButton_Clicked(this, EventArgs.Empty);

            MessagingCenter.Subscribe<DietaViewModel>(this, "LoginBtnCliked", async (model) =>
                await Navigation.PushAsync(new AutenticacaoView(DataService)));

            MessagingCenter.Subscribe<Alimento>(this, "AlimentoUsusarioSelecionado", async (alimento) =>
            {
                await Navigation.PushAsync(new AlimentoDetalheView(DataService, alimento));

                alimentosListView.SelectedItem = null;
            });

            MessagingCenter.Subscribe<string>(this, "FalhaMudarLimiteCalorias", async (msg) =>
                await DisplayAlert("Error", msg, "Ok"));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<DietaViewModel>(this, "LoginBtnCliked");

            MessagingCenter.Unsubscribe<Alimento>(this, "AlimentoUsusarioSelecionado");

            MessagingCenter.Unsubscribe<string>(this, "FalhaMudarLimiteCalorias");
        }

        private void toolBarButton_Clicked(object sender, EventArgs e)
        {
            if (DataService.isLogado)
                toolBarButton.Text = "SAIR";
            else
                toolBarButton.Text = "LOGAR-SE";
        }
    }
}