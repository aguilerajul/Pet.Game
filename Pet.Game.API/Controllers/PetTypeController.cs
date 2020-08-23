using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Pet.Game.API.Dtos;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Interfaces;
using System.Collections.Generic;

namespace Pet.Game.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;
        private readonly IPetTypeRepository petTypeRepository;

        public PetTypeController(ILogger<UserController> logger, IPetTypeRepository petTypeRepository, IMapper mapper)
        {
            this.logger = logger;
            this.petTypeRepository = petTypeRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetTypeDto>> Get(Guid id)
        {
            try
            {
                var petType = await this.petTypeRepository.GetAsync(id);
                if (petType == null)
                    return NotFound();

                return Ok(mapper.Map<PetTypeDto>(petType));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to get PetType: {id}", id);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var petTypes = await this.petTypeRepository.ListAsync();
                if (!petTypes.Any())
                    return NoContent();

                return Ok(mapper.Map<IEnumerable<PetTypeDto>>(petTypes));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to get the list of PetTypes");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddPetType")]
        public async Task<IActionResult> Post([FromBody] PetTypeDto data)
        {
            try
            {
                var newPetType = await this.petTypeRepository.AddOrUpdateAsync(mapper.Map<PetType>(data));
                return Ok(mapper.Map<PetTypeDto>(newPetType));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to add a new PetType");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
