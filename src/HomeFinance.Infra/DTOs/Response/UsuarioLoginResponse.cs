using System.Text.Json.Serialization;

namespace HomeFinance.Infra.DTOs.Response
{
    public class UsuarioLoginResponse
    {
        public bool Sucesso { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DataExpiracao { get; set; }
        public List<string> Erros { get; set; }

        public UsuarioLoginResponse() => 
            Erros = new List<string>();

        public UsuarioLoginResponse(bool sucesso = true) : this() => 
            Sucesso = sucesso;

        public UsuarioLoginResponse(bool sucesso, string token, DateTime dataExpiracao) : this()
        {
            Token = token;
            DataExpiracao = dataExpiracao;
        }
    }
}
