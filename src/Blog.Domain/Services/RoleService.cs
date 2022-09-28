using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class RoleService
    {
        private readonly IUnitOfWork _unit;

        public RoleService(IUnitOfWork unit)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<IReadOnlyList<Role>> GetAll()
        {
            return await _unit.RoleRepository.GetAll();
        }

        public async Task<IReadOnlyList<Role>> Add(string name)
        {
            if (await _unit.RoleRepository.IsUniqueName(name))
                throw new DuplicateRoleException();

            var role = new Role()
            {
                Name = name
            };

            await _unit.RoleRepository.Add(role);
            await _unit.SaveChangesAsync();

            return await _unit.RoleRepository.GetAll();
        }

        public async Task<IReadOnlyList<Role>> Update(int id, string name)
        {
            if (id == 1)
                throw new DefaultRoleException();

            if (await _unit.RoleRepository.IsUniqueName(name))
                throw new DuplicateCategoryException();

            var role = await _unit.RoleRepository.GetById(id);

            role.Name = name;

            await _unit.RoleRepository.Update(role);
            await _unit.SaveChangesAsync();

            return await _unit.RoleRepository.GetAll();
        }

        public async Task<IReadOnlyList<Role>> Remove(int id)
        {
            if (id == 1)
                throw new DefaultRoleException();

            var role = await _unit.RoleRepository.GetById(id);

            await _unit.RoleRepository.Remove(role);
            await _unit.SaveChangesAsync();

            return await _unit.RoleRepository.GetAll();
        }
    }
}
