﻿using DataAccess.DAO;
using DataAccess.Entities;
using Services.DTO;
using Services.ImageServices;
using System;

namespace Services
{
    public class UserService : IUserService
    {
        readonly AbstractDAO<User> _dao;
        private readonly IImageService _imageService;

        public UserService(AbstractDAO<User> dao)
        {
            _dao = dao;
            _imageService = new ImageService();
        }

        public User Get(int id)
        {
            return _dao.Read(id);
        }

        public void Edit(EditUserDTO userDto, System.Web.HttpServerUtilityBase serverObj)
        {
            User u = Get(userDto.Id);
            u.FirstName = userDto.FirstName;
            u.LastName = userDto.LastName;
            u.EMail = userDto.Email;

            long ticks = new DateTime(1970, 1, 1).Ticks;
            DateTime dt = new DateTime(ticks);

            u.Birthday = dt.AddMilliseconds(userDto.Birthday);

            if (userDto.ImageFile != null)
            {
                var image = _imageService.Create(userDto.ImageFile, serverObj);
                u.ImageId = image.Id;
            }

            _dao.Update(u);
        }
    }
}
