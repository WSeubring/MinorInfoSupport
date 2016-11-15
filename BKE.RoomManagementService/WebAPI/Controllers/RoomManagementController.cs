using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Commands;
using Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RoomManagementController : Controller
    {
        private IRoom _room;

        public RoomManagementController(IRoom room)
        {
            _room = room;
        }

        [HttpGet]
        public IRoom CreateRoom(CreateRoomCommand crc)
        {
            if (crc == null)
            {
                throw new ArgumentNullException();
            }

            var room = _room.Create(crc);
            return room;
        }
    }
}
