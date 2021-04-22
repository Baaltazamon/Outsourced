using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Outsourced.Models;

namespace Outsourced.Implementations.Interfaces
{
    public interface ICommunity
    {
        /// <summary>
        /// Получение списка вебинаров
        /// </summary>
        /// <returns></returns>
        IEnumerable<CommunityModel> GetAll();

        /// <summary>
        /// Получение вебинара по id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        CommunityModel GetById(int id);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        void Commit();

        /// <summary>
        /// Добавить нового
        /// </summary>
        /// <param name="model"></param>
        void AddNew(CommunityModel model);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
