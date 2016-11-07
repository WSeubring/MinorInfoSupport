using Commands;

namespace Interfaces
{
    public interface IRoom
    {
        IRoom Create(CreateRoomCommand crc);
    }
}