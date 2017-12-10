using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Tully.Api.Utils
{
  public class JsonContent : StringContent
  {
    public JsonContent(object obj) : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json") { }
  }
}
