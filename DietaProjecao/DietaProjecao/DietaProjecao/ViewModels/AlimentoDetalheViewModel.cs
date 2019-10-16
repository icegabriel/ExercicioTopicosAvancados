using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DietaProjecao.Models;
using Xamarin.Forms;

namespace DietaProjecao.ViewModels
{
    class AlimentoDetalheViewModel : BaseViewModel
    {
        private DataService DataService { get; }
        public Alimento Alimento { get; }

        public string Calorias { get => Alimento.attributes.energy.kcal.Split('.')[0]; }

        private bool isAdicionandoAlimento;
        public bool IsAdicionandoAlimento
        {
            get => isAdicionandoAlimento;

            private set
            {
                isAdicionandoAlimento = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddAlimetoCommand { get; private set; }

        public AlimentoDetalheViewModel(DataService dataService, Alimento alimento)
        {
            DataService = dataService;
            Alimento = alimento;

            AddAlimetoCommand = new Command(async c =>
            {
                IsAdicionandoAlimento = true;

                await DataService.AddAlimentoAsync(Alimento.id);

                IsAdicionandoAlimento = false;
            }, b => DataService.isLogado);
        }
    }
}
