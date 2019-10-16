using DietaProjecao.Models;
using DietaProjecao.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietaProjecao.Views
{

    [DesignTimeVisible(false)]
    public partial class AutenticacaoView : TabbedPage
    {
        public AutenticacaoView(DataService data)
        {
            InitializeComponent();

            BindingContext = new AutenticacaoViewModel(data);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Usuario>(this, "MensagemLoginEfetuado", async (usr) =>
            {
                await DisplayAlert("Login", "Login Efetuado com Sucesso!", "Ok");
                await Navigation.PopAsync();
            });
            MessagingCenter.Subscribe<Usuario>(this, "MensagemLoginFalha", async (usr) => await DisplayAlert("Login", "Falha ao Efetuar Login!", "Ok"));

            MessagingCenter.Subscribe<string>(this, "MensagemRegistroEfetuado", async (msg) =>
            {
                await DisplayAlert("Login", msg, "Ok");
                CurrentPage = Children[0];
            });
            MessagingCenter.Subscribe<string>(this, "MensagemRegistroFalha", async (msg) => await DisplayAlert("Login", msg, "Ok"));


            MessagingCenter.Subscribe<AutenticacaoViewModel>(this, "ClickAtalhoRegistrar", (sender) => CurrentPage = Children[1]);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Usuario>(this, "MensagemLoginEfetuado");
            MessagingCenter.Unsubscribe<Usuario>(this, "MensagemLoginFalha");

            MessagingCenter.Unsubscribe<string>(this, "MensagemRegistroEfetuado");
            MessagingCenter.Unsubscribe<string>(this, "MensagemRegistroFalha");

            MessagingCenter.Unsubscribe<AutenticacaoViewModel>(this, "ClickAtalhoRegistrar");
        }
    }
}
