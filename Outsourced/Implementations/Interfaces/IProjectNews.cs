using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Outsourced.Models;

namespace Outsourced.Implementations.Interfaces
{
    public interface IProjectNews
    {
        /// <summary>
        /// Получение списка новостей проектов
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProjectNewsModel> GetAll();

        /// <summary>
        /// Получение новости по id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        ProjectNewsModel GetById(int id);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        void Commit();

        /// <summary>
        /// Добавить новую
        /// </summary>
        /// <param name="model"></param>
        void AddNew(ProjectNewsModel model);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
