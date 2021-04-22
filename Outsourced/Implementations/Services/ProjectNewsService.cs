using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Outsourced.Implementations.Interfaces;
using Outsourced.Models;

namespace Outsourced.Implementations.Services
{
    public class ProjectNewsService: IProjectNews
    {
        private readonly List<ProjectNewsModel> _projectNews = new List<ProjectNewsModel>
        {
            new ProjectNewsModel
            {
                Id = 1,
                DateRelease = DateTime.Parse("20/06/2021"),
                Header = "Новый сайт московского метро",
                SubHeader = "постигай вершины",
                Description = "Одна из последних работ наших сотрудников — это разработка нового, " +
                              "современного сайта по гос. заказу администрации города.",
                BackGround = "img/bg-personal-project-2.jpg"
            },
            new ProjectNewsModel
            {
                Id = 2,
                Header = "Сайт для ремонтно-строительной компании",
                SubHeader = "постигай вершины",
                DateRelease = DateTime.Parse("20/10/2021"),
                Description = "Мой дом — моя крепость, так говорят в народе, а если в этом " +
                              "доме еще и чертовски красиво... Ммм...Жизнь, безусловно становится лучше.",
                BackGround = "img/bg-personal-project-1.jpg"
            },
            new ProjectNewsModel
            {
                Id = 3,
                Header = "Beutiful Life",
                SubHeader = "featured works",
                DateRelease = DateTime.Parse("20/10/2021"),
                Description = "Сайт для наших новых партнеров — фотостудии \"Beutiful Life\" взамен на "+
                "шикарные фотосессии. Ох, какие же там фоточки!!!",
                BackGround = "img/bg-personal-project-3.jpg"
            },
            new ProjectNewsModel
            {
                Id = 4,
                Header = "iron character",
                SubHeader = "featured works",
                DateRelease = DateTime.Parse("20/10/2021"),
                Description = "Спортивное движение в России набирает обороты. Наши сотрудники тоже в теме," +
                              "поэтому данный проект им особенно интересен.",
                BackGround = "img/bg-personal-project-4.jpg"
            }

        };

        public IEnumerable<ProjectNewsModel> GetAll()
        {
            return _projectNews;
        }

        public ProjectNewsModel GetById(int id)
        {
            return _projectNews.SingleOrDefault(c => c.Id == id);
        }

        public void Commit()
        {

        }

        public void AddNew(ProjectNewsModel model)
        {
            model.Id = _projectNews.Max(c => c.Id) + 1;
            _projectNews.Add(model);
        }

        public void Delete(int id)
        {
            var proj = _projectNews.SingleOrDefault(c => c.Id == id);
            if (proj is null)
                return;
            _projectNews.Remove(proj);
        }
    }
}
