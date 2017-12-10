namespace Tully.Api.Models
{
  public class Notificacao
  {
    public Notificacao() { }

    public Notificacao(string remetente, Usuario usuario, TipoNotificacao tipo)
    {
      switch (tipo)
      {
        case (TipoNotificacao.Avaliacao):
          Mensagem = $"O usuario {remetente} avaliou a sua foto.";
          Usuario = usuario;
          Visto = false;
          break;
        case (TipoNotificacao.Seguindo):
          Mensagem = $"O usuario {remetente} agora é seu seguidor.";
          Usuario = usuario;
          Visto = false;
          break;
        default:
          break;
      }
    }

    public int Id { get; set; }
    public string Mensagem { get; set; }
    public bool Visto { get; set; }
    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; }
  }

  public enum TipoNotificacao
  {
    Avaliacao,
    Seguindo
  }
}