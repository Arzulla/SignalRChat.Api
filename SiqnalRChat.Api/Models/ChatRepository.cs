using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiqnalRChat.Api.Models
{
    public interface IChatRepository
    {
        public MessageModel Get(int id);

        public IEnumerable<MessageModel> Get(string roomCode);

        public ListResult<MessageModel> Get(string roomCode, int skip, int take);

        public MessageModel Add(MessageModel message);

    }
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public MessageModel Add(MessageModel message)
        {
            var tmp = _context.Messages.Add(message);
            _context.SaveChanges();
            return tmp.Entity;
        }

        public MessageModel Get(int id)
        {
            return _context.Messages.Find(id);
        }

        public IEnumerable<MessageModel> Get(string roomCode)
        {
            return _context.Messages;
        }

        public ListResult<MessageModel> Get(string roomCode, int skip, int take)
        {
            IEnumerable<MessageModel> list = _context.Messages.Where(m => m.RoomCode == roomCode).OrderByDescending(m => m.CreatedDate).Skip(skip).Take(take);

            int totalCount = _context.Messages.Where(m => m.RoomCode == roomCode).Count();

            ListResult<MessageModel> result = new ListResult<MessageModel>
            {
                List = list,
                TotalCount = totalCount
            };
            return result;
        }
    }

    public class ListResult<T> where T : class
    {
        public IEnumerable<T> List { get; set; }

        public int TotalCount { get; set; }
    }
}
