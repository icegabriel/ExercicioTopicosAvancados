using DietaProjecao.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietaProjecao.ViewModels
{
    class AutenticacaoViewModel : BaseViewModel
    {

        // Tela de Login
        private string logUsuario;
        private string logSenha;

        public string LogUsuario
        {
            get => logUsuario;
            set
            {
                logUsuario = value;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }
        public string LogSenha
        {
            get => logSenha;
            set
            {
                logSenha = value;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        // Tela de registro

        private string regUsername;
        private string regEmail;
        private string regSenha;
        private string regSenhaConfr;
        private int regCalorias;
        private bool isCarregando;

        public string RegUsername
        {
            get => regUsername;
            set
            {
                regUsername = value;
                ((Command)RegistrarCommand).ChangeCanExecute();
            }
        }
        public string RegEmail
        {
            get => regEmail;
            set
            {
                regEmail = value;
                ((Command)RegistrarCommand).ChangeCanExecute();
            }
        }
        public string RegSenha
        {
            get => regSenha;
            set
            {
                regSenha = value;
                ((Command)RegistrarCommand).ChangeCanExecute();
            }
        }
        public string RegSenhaConfr
        {
            get => regSenhaConfr;
            set
            {
                regSenhaConfr = value;
                ((Command)RegistrarCommand).ChangeCanExecute();
            }
        }
        public int RegCalorias
        {
            get => regCalorias;
            set
            {
                regCalorias = value;
                ((Command)RegistrarCommand).ChangeCanExecute();
            }
        }

        public bool IsCarregando
        {
            get => isCarregando;
            set
            {
                isCarregando = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand RegistrarCommand { get; private set; }
        public ICommand RegistrarAtalhoCommand { get; private set; }

        private DataService DataService { get; }

        public AutenticacaoViewModel(DataService data)
        {
            this.DataService = data;

            this.LoginCommand = new Command(async c =>
            {
                IsCarregando = true;

                await DataService.LoginAsync(LogUsuario, LogSenha);

                IsCarregando = false;

            }, func => LoginCommandCanExecute());

            this.RegistrarCommand = new Command(async c =>
            {
                IsCarregando = true;

                var userRegistro = new Usuario(RegUsername, RegEmail, RegSenha, RegCalorias);
                await DataService.RegistrarAsync(userRegistro);

                IsCarregando = false;

            }, fun => RegistrarCommandCanExecute());

            this.RegistrarAtalhoCommand = new Command(o => MessagingCenter.Send(this, "ClickAtalhoRegistrar"));
        }

        private bool LoginCommandCanExecute() => 
            !string.IsNullOrEmpty(LogUsuario) && !string.IsNullOrEmpty(LogSenha);

        private bool RegistrarCommandCanExecute()
        {
            return !string.IsNullOrEmpty(RegUsername) &&
                   !string.IsNullOrEmpty(RegEmail) &&
                   !string.IsNullOrEmpty(RegSenha) &&
                   !string.IsNullOrEmpty(RegSenhaConfr) &&
                   RegCalorias != 0 &&
                   RegSenha == RegSenhaConfr;
        }
    }
}
