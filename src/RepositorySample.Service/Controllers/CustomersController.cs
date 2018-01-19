using Microsoft.AspNetCore.Mvc;
using RepositorySample.Framework;
using RepositorySample.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorySample.Service.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepositoryContext repositoryContext;
        private readonly IRepository<Customer> repository;
        private bool disposed;

        public CustomersController(IRepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            this.repository = repositoryContext.GetRepository<Customer>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await this.repository.FindBySpecificationAsync(_ => true);
            if (entities == null || entities.Count() == 0)
            {
                return NoContent();
            }

            return Ok(entities);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var entity = await this.repository.FindByKeyAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] dynamic model)
        {
            var name = (string)model.name;
            var id = Guid.NewGuid();
            var customer = new Customer { Id = id, Name = name };

            await this.repository.AddAsync(customer);
            await this.repositoryContext.CommitAsync();

            return Created(Url.Action("GetByIdAsync", new { id }), id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteByKeyAsync(Guid id)
        {
            await this.repository.RemoveByKeyAsync(id);

            await this.repositoryContext.CommitAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (! disposed)
            {
                if (disposing)
                {
                    this.repositoryContext.Dispose();
                }

                base.Dispose(disposing);
                disposed = true;
            }
        }
    }
}
