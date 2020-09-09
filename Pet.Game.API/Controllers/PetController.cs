using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Pet.Game.API.Dtos;
using Pet.Game.Domain.Interfaces;
using System.Collections.Generic;

namespace Pet.Game.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<PetController> logger;
        private readonly IUserRepository userRepository;
        private readonly IPetRepository petRepository;
        private readonly IPetTypeRepository petTypeRepository;

        public PetController(ILogger<PetController> logger,
            IUserRepository userRepository,
            IPetRepository petRepository,
            IPetTypeRepository petTypeRepository,
            IMapper mapper)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.petRepository = petRepository;
            this.petTypeRepository = petTypeRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var pet = await this.petRepository.GetAsync(id);
                if (pet == null)
                    return NotFound();

                return Ok(mapper.Map<PetResponseDto>(pet));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to get User: {id}", id);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var pets = await this.petRepository.ListAsync();
                if (!pets.Any())
                    return NoContent();

                return Ok(mapper.Map<IEnumerable<PetResponseDto>>(pets));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to get the list of Users");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPet([FromBody] PetRequestDto data)
        {
            try
            {
                var user = await userRepository.GetAsync(data.UserId);
                if (user == null)
                    return NotFound($"UserId: {data.UserId}");

                var petType = await petTypeRepository.GetAsync(data.TypeId);
                if (petType == null)
                    return NotFound($"TypeId: {data.TypeId}");

                var petData = mapper.Map<Domain.Entities.Pet>(data);
                var newPet = new Domain.Entities.Pet(petData.Name, petData.Type, data.UserId);
                newPet = await petRepository.AddOrUpdateAsync(newPet);

                return Ok(mapper.Map<PetResponseDto>(newPet));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to add a new user");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Stroke/{petId}")]
        public async Task<IActionResult> StrokePet(Guid petId)
        {
            try
            {
                var pet = await this.petRepository.Stroke(petId);
                if (pet == null)
                    return NotFound();

                return Ok(mapper.Map<PetResponseDto>(pet));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to add a new user");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Feed/{petId}")]
        public async Task<IActionResult> FeedPet(Guid petId)
        {
            try
            {
                var pet = await this.petRepository.Feed(petId);
                if (pet == null)
                    return NotFound();

                return Ok(mapper.Map<PetResponseDto>(pet));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has Occurred when we try to add a new user");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
