using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;

namespace Tully.Api.Services
{
  public static class FirebaseStorageService
  {
    public async static Task<string> UploadFile(string firebaseUrl, string firebaseFolder, string fileName, Stream fileStream)
    {
      return await new FirebaseStorage(firebaseUrl)
        .Child(firebaseFolder)
        .Child(fileName)
        .PutAsync(fileStream);
    }
  }
}
