﻿using System.ComponentModel.DataAnnotations;

namespace UrlShortener.BLL.Entities
{
    /// <summary>
    /// Запись Url в базе данных
    /// </summary>
    public sealed class UrlRecord : EntityBase
    {
        private UrlRecord() { }
        
        public UrlRecord(string shortUrl, string longUrl, User? user)
        {
            ShortUrl = shortUrl;
            LongUrl = longUrl;
            
            Users = user is null ? new() : new() { user} ;
        }

        /// <summary>
        /// Короткая запись
        /// </summary>
        public string ShortUrl { get; set; }

        /// <summary>
        /// Длинная запись
        /// </summary>
        public string LongUrl { get; set; }

        /// <summary>
        /// Пользователи, запросившие создание данной записи 
        /// </summary>
        public List<User> Users { get; set; } = new();
    }
}
