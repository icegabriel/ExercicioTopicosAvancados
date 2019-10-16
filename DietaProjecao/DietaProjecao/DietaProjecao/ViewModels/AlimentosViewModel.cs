using DietaProjecao.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietaProjecao.ViewModels
{
    class AlimentosViewModel : BaseViewModel
    {
        private TacoService TacoService { get; }

        private List<Alimento> alimentos;
        public List<Alimento> Alimentos
        {
            get => alimentos;

            private set
            {
                alimentos = value;
                OnPropertyChanged();
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

        public ICommand RefreshCommand { get; private set; }


        private Alimento selectedAlimento;
        public Alimento SelectedAlimento
        {
            get => selectedAlimento;
            set
            {
                selectedAlimento = value;

                if (value != null)
                {
                    MessagingCenter.Send(selectedAlimento, "AlimentoSelecionado");
                    selectedAlimento = null;
                }
            }
        }

        public AlimentosViewModel(DataService dataService)
        {
            TacoService = new TacoService();

            Alimentos = new List<Alimento>();

            Task.Run(RefreshAlimentosList);
            RefreshCommand = new Command(c => RefreshAlimentosList());
        }

        public async void RefreshAlimentosList()
        {
            IsRefreshing = true;

            Alimentos = new List<Alimento>();

            Alimentos = await TacoService.GetListAlimentosAsync();

            IsRefreshing = false;
        }

    }
}
