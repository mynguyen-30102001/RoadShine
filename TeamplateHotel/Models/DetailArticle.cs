using System.Collections.Generic;
using ProjectLibrary.Database;

namespace TeamplateHotel.Models
{
    public class DetailArticle
    {
        public Article Article { get; set; }
        public List<Article> Articles { get; set; }
        public List<Article> Articlespost { get; set; }
        public List<ArticleGallery> ArticleGalleries { get; set; }
    }
}