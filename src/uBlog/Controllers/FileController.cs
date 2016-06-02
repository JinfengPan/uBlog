using Microsoft.AspNetCore.Mvc;
using uBlog.Repository;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;
using System.Threading.Tasks;
using uBlog.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace uBlog.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private UBlogContext blogContext;

        public FileController(UBlogContext blogContext)
        {
            this.blogContext = blogContext;
        }

        private async Task StoreImageAsync(IFormFile file, string blogpostId)
        {
            var bucket = new GridFSBucket(blogContext.Database);

            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", file.ContentType)
            };

            var imageId = await bucket.UploadFromStreamAsync(file.FileName, file.OpenReadStream(), options);


        }

        public IActionResult GetImage(string id)
        {
            try
            {
                var stream = blogContext.docsBucket.OpenDownloadStream(new ObjectId(id));

                var contentType = stream.FileInfo.Metadata["contentType"].AsString;

                return File(stream, contentType);
            }
            catch (GridFSFileNotFoundException)
            {
                return NotFound();
            }
           
        }

        private async Task DeleteImageAync(string id)
        {
            await blogContext.docsBucket.DeleteAsync(new ObjectId(id));



        }

    }
}
