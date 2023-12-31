﻿using UrlShortener.BLL.Constants;

namespace UrlShortener.BLL.Abstractions
{
    /// <summary>
    /// Сервис доступа к идентификатору и роли текущего пользователя
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Идентификатор текущего пользователя
        /// </summary>
        Guid CurrentUserId { get; }

        /// <summary>
        /// Тип роли текущего пользователя
        /// </summary>
        RoleType RoleType { get; }
    }
}
