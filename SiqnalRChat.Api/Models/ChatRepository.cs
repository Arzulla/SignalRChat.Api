using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiqnalRChat.Api.Models
{
    public interface IChatRepository
    {
        public Task<MessageModel> GetAsync(int id);

        public Task<IEnumerable<MessageModel>> GetAsync(string roomCode);

        public Task<ListResult<MessageModel>> GetAsync(string roomCode, int skip, int take);

        public Task<MessageModel> AddAsync(MessageModel message);

    }
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<MessageModel> AddAsync(MessageModel message)
        {
            var tmp = await _context.Messages.AddAsync(message);
            _context.SaveChanges();
            return tmp.Entity;
        }

        public async Task<MessageModel> GetAsync(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<MessageModel>> GetAsync(string roomCode)
        {
            return await _context.Messages.Where(m=>m.RoomCode==roomCode).ToListAsync();
        }

        public async Task<ListResult<MessageModel>> GetAsync(string roomCode, int skip, int take)
        {
            IEnumerable<MessageModel> list = await  _context.Messages.Where(m => m.RoomCode == roomCode).OrderByDescending(m => m.CreatedDate).Skip(skip).Take(take).ToListAsync();

            int totalCount =await _context.Messages.Where(m => m.RoomCode == roomCode).CountAsync();

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
