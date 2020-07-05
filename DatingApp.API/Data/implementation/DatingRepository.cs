
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data.implementation
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DatingRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<UserForDetailedDto> GetUser(int id)
        {
            var user = await _context.Users.Include( p => p.Photos)
                        .FirstOrDefaultAsync(u => u.Id == id);
            
            var userDto = _mapper.Map<UserForDetailedDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserForListDto>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();

            var usersDto = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return usersDto;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}