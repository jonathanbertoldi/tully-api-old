namespace Tully.Api.Models
{
  public class Notificacao
  {
    public int Id { get; set; }
    public string Mensagem { get; set; }
    public bool Visto { get; set; }
    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; }
  }
}