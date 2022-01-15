using System;
using Microsoft.AspNetCore.Http;

namespace Dtos
{
    public class PhotoForCreationDto
    {
        public string Url {get; set; }
        public IFormFile FormFile { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}