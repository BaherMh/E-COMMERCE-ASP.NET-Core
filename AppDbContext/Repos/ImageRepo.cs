using AppDbContext.IRepos;
using AppDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AppDbContext.Repos
{
    class ImageRepo : BaseRepo<Image>, IImageRepo
    {
        public ImageRepo(ECOMMERCEContext db) : base(db)
        {

        }
    }
}
