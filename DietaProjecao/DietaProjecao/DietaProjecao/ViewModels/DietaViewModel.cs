using DietaProjecao.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietaProjecao.ViewModels
{
    class DietaViewModel : BaseViewModel
    {
        public ICommand ToolbarButtonCommand { get; private set; }

        public ICommand SetCaloriasCommand { get; private set; }

        public ICommand AplicarLimiteCommand { get; private set; }

        public ICommand DeletarCommand { get; private set; }

        private DataService DataService { get; }

        public bool UsuarioIsLogado
        {
            get
            {
                OnPropertyChanged();
                return DataService.isLogado;
            }
        }
        public bool UsuarioCanLogin
        {
            get
            {
                OnPropertyChanged();
                return !DataService.isLogado;
            }
        }

        private List<Alimento> alimentosUsuario;
        public List<Alimento> AlimentosUsuario
        {
            get => alimentosUsuario;
            private set
            {
                alimentosUsuario = value;
                OnPropertyChanged();
            }
        }

        private Alimento selectedAlimento;
        public Alimento SelectedAlimento
        {
            get => selectedAlimento;
            set
            {
                selectedAlimento = value;

                if (selectedAlimento != null)
                    MessagingCenter.Send(selectedAlimento, "AlimentoUsusarioSelecionado");
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            private set
            {
                isRefreshing = value;
                OnPropertyChanged();
            } 
        }

        private Color caloriasTotaisColor;
        public Color CaloriasTotaisColor
        {
            get => caloriasTotaisColor;
            private set
            {
                caloriasTotaisColor = value;
                OnPropertyChanged();
            }
        }

        private string caloriasTotaisText;
        public string CaloriasTotaisText
        {
            get => caloriasTotaisText;
            private set
            {
                caloriasTotaisText = value;
                OnPropertyChanged();
            }
        }

        private bool isChagingLimite;
        public bool IsChagingLimite
        {
            get => isChagingLimite;

            set 
            {
                isChagingLimite = value;
                OnPropertyChanged();
            }
        }

        private int limeteCaloriasEntry;
        public int LimeteCaloriasEntry
        {
            get => limeteCaloriasEntry;
            set
            {
                limeteCaloriasEntry = value;
                OnPropertyChanged();
            }
        }


        public DietaViewModel(DataService dataService)
        {
            DataService = dataService;
            AlimentosUsuario = new List<Alimento>();

            ToolbarButtonCommand = new Command(sender =>
            {
                if (DataService.isLogado)
                    DataService.Sair();
                else
                    MessagingCenter.Send(this, "LoginBtnCliked");
            });

            SetCaloriasCommand = new Command(sender =>
            {
                if (IsChagingLimite)
                    IsChagingLimite = false;
                else
                    IsChagingLimite = true;
            });

            AplicarLimiteCommand = new Command(async sender =>
            {
                IsChagingLimite = false;

                await DataService.SetLimiteCalorias(LimeteCaloriasEntry);

                await RefreshDietaViewAsync();
            });

            DeletarCommand = new Command(async alimento =>
            {
                await DataService.DeletarAlimentoAsync((Alimento)alimento);
                await RefreshDietaViewAsync();
            });
        }

        internal async Task RefreshDietaViewAsync()
        {
            IsRefreshing = true;

            AlimentosUsuario = new List<Alimento>();

            AlimentosUsuario = await DataService.GetUsuarioAlimentos();

            await SetTotalCalorias();

            IsRefreshing = false;
        }

        internal async Task<int> GetTotalCaloriasAsync()
        {
            var alimentos = await DataService.GetUsuarioAlimentos();

            var calorias = 0.0;

            try
            {
                foreach (var alimento in alimentos)
                {
                    calorias += Convert.ToDouble(alimento.attributes.energy.kcal);
                }
            }
            catch
            {
                calorias += 0;
            }

            return Convert.ToInt32(calorias);
        }

        private async Task SetTotalCalorias()
        {
            var calorias = await GetTotalCaloriasAsync();

            if (calorias > DataService.GetLimiteCalorias())
                CaloriasTotaisColor = Color.Red;
            else
                CaloriasTotaisColor = Color.Default;

            CaloriasTotaisText = $"{calorias}/{DataService.GetLimiteCalorias()}";
        }

    }
}
