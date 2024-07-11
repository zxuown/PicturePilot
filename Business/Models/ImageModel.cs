using Microsoft.AspNetCore.Mvc;
using PicturePilot.Data.Entities;

namespace PicturePilot.Business.Models
{
    public class ImageModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int FavoritesCount { get; set; }

        public ICollection<string> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
