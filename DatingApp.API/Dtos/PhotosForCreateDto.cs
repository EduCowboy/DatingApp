using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class PhotosForCreateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        public PhotosForCreateDto()
        {
            DateAdded =  DateTime.Now;
        }

    }
}