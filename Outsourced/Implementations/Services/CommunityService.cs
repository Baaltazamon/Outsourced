using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Outsourced.Implementations.Interfaces;
using Outsourced.Models;

namespace Outsourced.Implementations.Services
{
    public class CommunityService: ICommunity
    {
        private readonly List<CommunityModel> _community = new List<CommunityModel>
        {
            new CommunityModel
            {
                Id = 1,
                DateWebinar = DateTime.Parse("21/06/2021"),
                Header = "Landing за час",
                TypeCommunity = "WEB community",
                Description = "Поговорим о концепциях лендинга. Пощупаем современные инструментарии разработки."
            },
            new CommunityModel
            {
                Id = 2,
                DateWebinar = DateTime.Parse("28/06/2021"),
                Header = "Как стать Senior разработчиком",
                TypeCommunity = "Developer community",
                Description = "Обсудим основные обязанности Senior'а." +
                              "Наши сотрудники расскажут свои истории успеха."
            },
            new CommunityModel
            {
                Id = 3,
                DateWebinar = DateTime.Parse("05/07/2021"),
                Header = "Разработка брендбука",
                TypeCommunity = "Designer community",
                Description = "Основы дизайна. Дизайн принтов на одежду. Исскуство слов."
            },
            new CommunityModel
            {
                Id = 4,
                DateWebinar = DateTime.Parse("12/07/2021"),
                Header = "Что нужно уметь junior'у",
                TypeCommunity = "Designer community",
                Description = "Основы Photoshop. Adobe Illustrator."
            },
            new CommunityModel
            {
                Id = 5,
                DateWebinar = DateTime.Parse("19/07/2021"),
                Header = "Что нужно уметь junior'у",
                TypeCommunity = "Designer community",
                Description = "Основы Photoshop. Adobe Illustrator."
            },
            new CommunityModel
            {
                Id = 6,
                DateWebinar = DateTime.Parse("08/08/2021"),
                Header = "уточняется",
                TypeCommunity = "WEB community",
                Description = "уточняется"
            }

        };

        public IEnumerable<CommunityModel> GetAll()
        {
            return _community;
        }

        public CommunityModel GetById(int id)
        {
            return _community.SingleOrDefault(c => c.Id == id);
        }

        public void Commit()
        {

        }

        public void AddNew(CommunityModel model)
        {
            model.Id = _community.Max(c => c.Id) + 1;
            _community.Add(model);
        }

        public void Delete(int id)
        {
            var proj = _community.SingleOrDefault(c => c.Id == id);
            if (proj is null)
                return;
            _community.Remove(proj);
        }
    }
}
