using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bportfolio.Entities
{
    public class AboutMe
    {
        public int AboutMeId { get; set; }
        public string WelcomeMessage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}