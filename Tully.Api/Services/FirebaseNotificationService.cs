using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tully.Api.Models;
using Tully.Api.Utils;

namespace Tully.Api.Services
{
  public static class FirebaseNotificationService
  {
    private static readonly string _baseAddress = "https://fcm.googleapis.com/";
    private static readonly string _postAddress = "fcm/send";
    private static readonly string _firebaseServerKey = "key=AAAAx3KX7YA:APA91bG9Eo-7fKn-KJUvEnyeipCTx-Om71IJqOaPZOfkbCDARIdM_lsyqYs7Quaj0cA36FZFCPS7gQUyf5975pW96kxicC7uOmnK1j7aDZWMqUaNUUPlK5tvrxlvv9h5Nf9aMtTW-jpu";

    private static readonly string _title = "Você tem uma nova atualização!";

    public static async Task<HttpResponseMessage> SendNotification(this Notificacao notificacao)
    {
      if (notificacao.Usuario.DeviceId == null) return null;

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(_baseAddress);
        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _firebaseServerKey);

        var content = new
        {
          data = new
          {
            title = _title,
            message = notificacao.Mensagem,
            sound = "default"
          },
          to = notificacao.Usuario.DeviceId
        };

        var post = await client.PostAsync(_postAddress, new JsonContent(content));

        return post;
      }
    }
  }
}
