using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietaProjecao.Models
{
    public class DataService
    {
        private const string STRING_DE_CONEXAO = "mongodb://127.0.0.1:27017/";

        private static IMongoDatabase DataBase { get => _client.GetDatabase("dietaprojecao"); }

        private static MongoClient _client;

        private Usuario UsuarioLogado { get; set; }

        public bool isLogado { get; private set; }

        private static IMongoCollection<Usuario> Usuarios { get => DataBase.GetCollection<Usuario>("usuarios"); }

        public DataService()
        {
            _client = new MongoClient(STRING_DE_CONEXAO);
        }

        internal async Task LoginAsync(string usr, string senha)
        {
            usr = usr.ToLower();

            try
            {
                var filter = Builders<Usuario>.Filter;
                var condit = filter.Eq(x => x.Username, usr)
                    & filter.Eq(x => x.Senha, senha);

                var login = await Usuarios.FindAsync(condit);
                var loginList = login.ToList();

                var user = loginList.FirstOrDefault();

                if (user != null)
                {
                    UsuarioLogado = user;
                    isLogado = true;

                    MessagingCenter.Send(user, "MensagemLoginEfetuado");
                }
                else
                {
                    var userFalha = new Usuario { Username = usr };
                    isLogado = false;

                    MessagingCenter.Send(userFalha, "MensagemLoginFalha");
                }
            }
            catch
            {
                MessagingCenter.Send(new Usuario(), "MensagemLoginFalha");
            }
        }

        internal async Task RegistrarAsync(Usuario userRegistro)
        {
            try 
            {
                var filter = Builders<Usuario>.Filter;
                var condit = filter.Eq(x => x.Username, userRegistro.Username.ToLower())
                    | filter.Eq(x => x.Email, userRegistro.Email.ToLower());

                var login = await Usuarios.FindAsync(condit);
                var loginList = login.ToList();

                if (loginList.Count == 0)
                {
                    await Usuarios.InsertOneAsync(userRegistro);
                    MessagingCenter.Send("Usuario Registrado Com Successo!", "MensagemRegistroEfetuado");
                }
                else
                    MessagingCenter.Send("Usuario ou email ja registrados.", "MensagemRegistroFalha");
            }
            catch
            {
                MessagingCenter.Send("Falha Ao Registrar Usuario Tente Novamente mais Tarde!", "MensagemRegistroFalha");
            }
        }

        internal void Sair()
        {
            UsuarioLogado = null;
            isLogado = false;
        }

        internal async Task AddAlimentoAsync(int id)
        {
            var filter = Builders<Usuario>.Filter.Eq(x => x.Id, UsuarioLogado.Id);
            var update = Builders<Usuario>.Update.Push(x => x.Alimentos, id);

            await Usuarios.UpdateOneAsync(filter, update);

            await RefreshUsuarioLogado();
        }

        internal async Task RefreshUsuarioLogado()
        {
            if (isLogado == true)
            {
                var filter = Builders<Usuario>.Filter.Eq(x => x.Id, UsuarioLogado.Id);

                var login = await Usuarios.FindAsync(filter);
                var loginList = login.ToList();

                UsuarioLogado = loginList.FirstOrDefault();
            }
            else
                return;

        }

        internal bool VerificaAlimentoNaLista(int id)
        {

            if (UsuarioLogado.Alimentos.Count != 0)
            {
                var exists = UsuarioLogado.Alimentos.Exists(ali => ali == id);

                return exists;
            }
            else
                return false;

        }

        internal async Task DeletarAlimentoAsync(Alimento alimento)
        {
            var filter = Builders<Usuario>.Filter.Eq(x => x.Id, UsuarioLogado.Id);

            UsuarioLogado.Alimentos.Remove(alimento.id);

            await Usuarios.ReplaceOneAsync(filter, UsuarioLogado);

            await RefreshUsuarioLogado();
        }

        internal async Task<List<Alimento>> GetUsuarioAlimentos()
        {
            var tacoService = new TacoService();
            var listAlimentos = new List<Alimento>();

            if (isLogado)
            {
                foreach (var alimentoId in UsuarioLogado.Alimentos)
                {
                    listAlimentos.Add(await tacoService.GetAlimentoAsync(alimentoId));
                }
            }

            return listAlimentos;
        }

        internal int GetLimiteCalorias()
        {
            if (isLogado)
                return UsuarioLogado.Calorias;
            else
                return 0;
        }

        internal async Task SetLimiteCalorias(int limiteCalorias)
        {
            try
            {
                var filter = Builders<Usuario>.Filter.Eq(x => x.Id, UsuarioLogado.Id);
                var update = Builders<Usuario>.Update.Set(x => x.Calorias, limiteCalorias);

                await Usuarios.UpdateOneAsync(filter, update);

                await RefreshUsuarioLogado();
            }
            catch
            {
                MessagingCenter.Send("Falha ao tentar mudar o limite da calorias, tente novamente mais tarde.", "FalhaMudarLimiteCalorias");
            }
        }
    }
}
