namespace MilesBackOffice.Web.Models.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MilesBackOffice.Web.Data.Entities;

    public class NewsViewModel
    {
        public IEnumerable<News> Newspaper { get; set; }


        public PublishNewsViewModel PublishViewModel { get; set; }

    }

    public class PublishNewsViewModel
    {
        [Required]
        public string Title { get; set; }


        [Required]
        public string Body { get; set; }

        //TODO refactor to a list of images 
        //then save to DB as bytes?
        public List<string> Images { get; set; }
    }
}
