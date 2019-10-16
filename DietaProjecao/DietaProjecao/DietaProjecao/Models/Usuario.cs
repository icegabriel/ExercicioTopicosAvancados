using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DietaProjecao.Models
{
    class Usuario
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Calorias { get; set; }
        public List<int> Alimentos { get; set; }

        public Usuario()
        {

        }

        public Usuario(string username, string email, string senha, int calorias)
        {
            Username = username.ToLower();
            Email = email.ToLower();
            Senha = senha;
            Calorias = calorias;
            Alimentos = new List<int>();
        }
    }
}
